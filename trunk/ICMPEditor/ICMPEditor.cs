using System;
using System.Collections.Generic;
using System.Text;
using Tamir.IPLib.Packets;
using Tamir.IPLib.Util;
using Tamir.IPLib.Packets.Util;
using Kopf.PacketPal.Util;
using Kopf.PacketPal.TCPIPLayers;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            return "ICMP Packet Editor";
        }

        /*
         * Get the version of this editor.
         */
        public override string getVersion()
        {
            return "1.0";
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
            return "ICMP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            if (packet is ICMPPacket)
            {
                return true;
            }
            return false;
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
            if (!(packet is ICMPPacket))
            {
                throw new EditorInvalidPacket("Not a valid ICMP Packet!");
            }

            int myType = ((ICMPPacket)packet).MessageType;
            int myCode = ((ICMPPacket)packet).MessageCode;
            int myChecksum = ((ICMPPacket)packet).ICMPChecksum;

            ICMPEditorForm form = new ICMPEditorForm(this,
                ((ICMPPacket)packet).MessageType,
                ((ICMPPacket)packet).MessageCode,
                ((ICMPPacket)packet).ICMPChecksum,
                HexEncoder.ToString(((ICMPPacket)packet).ICMPData)
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                ((ICMPPacket)packet).MessageType = form.getType();
                ((ICMPPacket)packet).MessageCode = form.getCode();
                ((ICMPPacket)packet).ICMPChecksum = form.getChecksum();
                if (form.reCompile)
                {
                    object[] temp = new object[6];
                    temp[0] = ((ICMPPacket)packet).EthernetHeader;
                    temp[1] = ((ICMPPacket)packet).IPHeader;
                    temp[2] = ((ICMPPacket)packet).MessageType;
                    temp[3] = ((ICMPPacket)packet).MessageCode;
                    temp[4] = ((ICMPPacket)packet).ICMPChecksum;
                    temp[5] = ((ICMPPacket)packet).ICMPData;
                    packet = compile(temp);
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

            Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

            // just use the other function now that we have a valid packet
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is ICMPPacket))
            {
                throw new EditorInvalidPacket("Not a valid ICMP Packet!");
            }

            /*
             * Split into fields:
             *  - ethernet header
             *  - ip header
             *  - type
             *  - code
             *  - checksum
             *  - data
             */

            object[] ret = new object[6];
            ret[0] = ((ICMPPacket)packet).EthernetHeader;
            ret[1] = ((ICMPPacket)packet).IPHeader;
            ret[2] = ((ICMPPacket)packet).MessageType;
            ret[3] = ((ICMPPacket)packet).MessageCode;
            ret[4] = ((ICMPPacket)packet).ICMPChecksum;
            ret[5] = ((ICMPPacket)packet).ICMPData;

            return ret;
        }

        /*
         * Create a new ICMPPacket based on provided fields.
         */
        public override Packet compile(object[] fields)
        {
            // make sure the array is properly constructed
            if (fields.Length == 6)
            {
                if (!(fields[0] is byte[]) || !(fields[1] is byte[]) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is int) || !(fields[5] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 2 byte arrays, 3 integers, and 1 string.");
                }

                if (!verifyMessageType((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid ICMP Message Type. Expecting an integer from 0 to 255.");
                }
                if (!verifyMessageCode((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid ICMP Message Code. Expecting an integer from 0 to 255.");
                }
                if (!verifyChecksum((int)fields[4]))
                {
                    throw new EditorInvalidField("Invalid ICMP Checksum. Expecting an integer from 0 to 65535.");
                }
                if (!verifyData((string)fields[5]))
                {
                    throw new EditorInvalidField("Invalid ICMP Data. Expecting a hexadecimal string.");
                }

                int discarded;

                // convert type to byte[]
                byte[] myType = HexEncoder.GetBytes(padHex(((int)fields[2]).ToString("x"),2), out discarded);
                // convert code to byte[]
                byte[] myCode = HexEncoder.GetBytes(padHex(((int)fields[3]).ToString("x"), 2), out discarded);
                // convert checksum to byte[]
                byte[] myCheck = HexEncoder.GetBytes(padHex(((int)fields[4]).ToString("x"), 4), out discarded);
                // convert data to byte[]
                byte[] myData = HexEncoder.GetBytes((string)fields[5], out discarded);

                
                // container for everything
                byte[] temp = new byte[
                    ((byte[])fields[0]).Length + ((byte[])fields[1]).Length +
                    8 + myData.Length];

                // copy our bytes over to the temp array for parsing
                ((byte[])fields[0]).CopyTo(temp, 0);
                ((byte[])fields[1]).CopyTo(temp, ((byte[])fields[0]).Length);
                myType.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length);
                myCode.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 2);
                myCheck.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 4);
                myData.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 8);

                
                // use the packet factory to compile
                // this will allow something possibly higher than an EthernetPacket to be constructed
                return PacketFactory.dataToPacket(
                    LinkLayers_Fields.IEEE802,
                    temp
                );
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct an ICMP Packet.");
            }

        }

        #endregion required_methods



        /*
         * Pad a hex string with leading zeroes.
         */
        public string padHex(string inString, int minLength)
        {
            while (inString.Length < minLength)
            {
                inString = "0" + inString;
            }

            return inString;
        }

        /**
         * field verification
         */

        /*
         * type
         */
        public bool verifyMessageType(int type)
        {
            if (type >= 0 && type < 256)
            {
                return true;
            }
            return false;
        }

        /*
        * code
        */
        public bool verifyMessageCode(int code)
        {
            if (code >= 0 && code < 256)
            {
                return true;
            }
            return false;
        }

        /*
        * checksum
        */
        public bool verifyChecksum(int checksum)
        {
            if (checksum >= 0 && checksum < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * Verify data
        */
        public bool verifyData(string data)
        {
            Regex test1 = new Regex("[0-9a-fA-F]{0,2960}");
            if (test1.IsMatch(data))
            {
                // make sure there's no characters that shouldn't be in the string
                Regex test2 = new Regex(".*[^0-9a-fA-F].*");
                return (!test2.IsMatch(data));
            }
            return false;
        }
        
    }
}
