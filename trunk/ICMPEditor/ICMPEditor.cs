using System;
using System.Collections.Generic;
using System.Text;
using Kopf.PacketPal.Util;
using Kopf.PacketPal.TCPIPLayers;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SharpPcap;
using PacketDotNet;

namespace Kopf.PacketPal.PacketEditors
{
    public class ICMPEditor : PacketEditor
    {
        // layer of operation
        private TCPIPLayer myLayer;

        /*
         * Constructor needs to take no arguments.
         */
        public ICMPEditor()
        {
            myLayer = new Kopf.PacketPal.TCPIPLayers.NetworkLayer();
        }


        /*
        * Abstract methods, required to be overridden.
        */
        #region required_methods

        /*
         * Get the name of this editor.
         */
        public override string getName()
        {
            return "ICMPv4 Packet Editor";
        }

        /*
         * Get the version of this editor.
         */
        public override string getVersion()
        {
            return "1.1";
        }

        /*
         * Get the author of this editor.
         */
        public override string getAuthor()
        {
            return "Kurtis Kopf";
        }

        /*
         * Get the web address of this editor.
         */
        public override string getWebAddress()
        {
            return "Kurtis.Kopf@gmail.com";
        }

        /*
         * Get the "Edit As:" string.
         */
        public override string getEditAs()
        {
            return "ICMPv4 Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            return (packet is ICMPv4Packet);
        }

        /*
         * Get the layer of the TCP/IP Model that this Editor functions at.
         */
        public override TCPIPLayer getLayer()
        {
            return myLayer;
        }

        /*
         * GUI Editing of an existing packet.
         */
        public override Packet guiEdit(Packet packet)
        {
            if (!(packet is ICMPv4Packet))
            {
                throw new EditorInvalidPacket("Not a valid ICMPv4 Packet!");
            }

            object[] fields = explode(packet);

            ICMPEditorForm form = new ICMPEditorForm(this,
                (string)fields[1],
                (string)fields[2],
                (string)fields[3],
                (string)fields[4]
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                fields[1] = form.getType();
                fields[2] = form.getCode();
                fields[3] = form.getChecksum();
                fields[4] = form.getData();
                packet = compile(fields, packet);

                if (form.reCompute)
                {
                    packet.UpdateCalculatedValues();
                }

                // destroy the form
                form.Dispose();

                // return our packet
                return packet;
            }

            return null;
        }

        /*
         * GUI Editing of a new packet.
         */
        public override Packet guiEdit()
        {
            // we have to build this one by hand
            byte[] temp = new byte[74];
            // start with all 0xFF
            for (int x = 0; x < 74; x++)
            {
                temp[x] = 0xFF;
            }

            // set IPv4 ethernet flag
            temp[12] = 0x08;
            temp[13] = 0x00;
            // type and len
            temp[14] = 0x45;
            temp[15] = 0x00;
            // total len
            temp[16] = 0x00;
            temp[17] = 0x3c;
            // id
            temp[18] = 0xde;
            temp[19] = 0x96;
            // flags & frag offset
            temp[20] = 0x00;
            temp[21] = 0x00;
            // ttl = 128
            temp[22] = 0x80;
            // set ICMP protocol type
            temp[23] = 0x01;
            // header checksum 24-25
            // source ip 26-29
            // dest ip 30-33
            // icmp type ping request
            temp[34] = 0x08;
            // icmp code
            temp[35] = 0x00;
            // checksum
            temp[36] = 0x3a;
            temp[37] = 0x5c;
            // id
            temp[38] = 0x02;
            temp[39] = 0x00;
            // sequence #
            temp[40] = 0x11;
            temp[41] = 0x00;

            Packet packet = new ICMPv4Packet(temp, 0);
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is ICMPv4Packet))
            {
                throw new EditorInvalidPacket("Not a valid ICMP Packet!");
            }
            ICMPv4Packet icmpPacket = (ICMPv4Packet)packet;

            /*
             * Split into fields:
             *  - header
             *  - type
             *  - code
             *  - checksum
             *  - data
             */

            object[] ret = new object[5];
            ret[0] = HexEncoder.ToString(icmpPacket.Header);
            ret[1] = HexEncoder.ToString(ByteUtil.getBytes(icmpPacket.Bytes, ICMPv4Fields.TypeCodePosition, 1));
            ret[2] = HexEncoder.ToString(ByteUtil.getBytes(icmpPacket.Bytes, ICMPv4Fields.TypeCodePosition + 1, 1));
            ret[3] = HexEncoder.ToString(ByteUtil.getBytes(icmpPacket.Bytes, ICMPv4Fields.ChecksumPosition, ICMPv4Fields.ChecksumLength));
            ret[4] = HexEncoder.ToString(icmpPacket.Data);

            return ret;
        }

        public override Packet compile(object[] fields)
        {
            return compile(fields, null);
        }

        /*
         * Create a new ICMPPacket based on provided fields.
         */
        public override Packet compile(object[] fields, Packet packet)
        {
            // make sure the array is properly constructed
            if (fields.Length == 5)
            {
                if (!(fields[0] is string) || !(fields[1] is string) ||
                !(fields[2] is string) || !(fields[3] is string) ||
                !(fields[4] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 5 strings.");
                }

                if (!verifyMessageType((string)fields[1]))
                {
                    throw new EditorInvalidField("Invalid ICMP Message Type. Expecting a hexadecimal string.");
                }
                if (!verifyMessageCode((string)fields[2]))
                {
                    throw new EditorInvalidField("Invalid ICMP Message Code. Expecting a hexadecimal string.");
                }
                if (!verifyChecksum((string)fields[3]))
                {
                    throw new EditorInvalidField("Invalid ICMP Checksum. Expecting a hexadecimal string.");
                }
                if (!verifyData((string)fields[4]))
                {
                    throw new EditorInvalidField("Invalid ICMP Data. Expecting a hexadecimal string.");
                }

                int discarded;

                byte[] myType = HexEncoder.GetBytes((string)fields[1], out discarded);
                byte[] myCode = HexEncoder.GetBytes((string)fields[2], out discarded);
                byte[] myCheck = HexEncoder.GetBytes((string)fields[3], out discarded);
                byte[] myData = HexEncoder.GetBytes((string)fields[4], out discarded);

                byte[] packetBytes = ByteUtil.combineBytes(myType, myCode, myCheck, myData);

                if (packet == null)
                {
                    packet = new ICMPv4Packet(packetBytes, 0);
                }
                else
                {
                    Packet parent = packet.ParentPacket;
                    packet = new ICMPv4Packet(packetBytes, 0);
                    packet.ParentPacket = parent;
                }

                return packet;
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct an ICMP Packet.");
            }
        }

        #endregion required_methods


        /**
         * field verification
         */

        /*
         * type
         */
        public bool verifyMessageType(string type)
        {
            return (type.Length == 1 && HexEncoder.InHexFormat(type));
        }

        /*
        * code
        */
        public bool verifyMessageCode(string code)
        {
            return (code.Length == 1 && HexEncoder.InHexFormat(code));
        }

        /*
        * checksum
        */
        public bool verifyChecksum(string checksum)
        {
            return (checksum.Length == 1 && HexEncoder.InHexFormat(checksum));
        }

        /*
        * Verify data
        */
        public bool verifyData(string data)
        {
            return (HexEncoder.InHexFormat(data));
        }
        
    }
}
