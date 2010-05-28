using System;
using System.Collections.Generic;
using System.Text;
using SharpPcap;
using PacketDotNet;
using Kopf.PacketPal.Util;
using Kopf.PacketPal.TCPIPLayers;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kopf.PacketPal.PacketEditors
{
    public class UDPEditor : PacketEditor
    {
        // layer of operation
        private TCPIPLayer myLayer;

        /*
         * Constructor needs to take no arguments.
         */
        public UDPEditor()
        {
            myLayer = new Kopf.PacketPal.TCPIPLayers.TransportLayer();
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
            return "UDP Packet Editor";
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
            return "UDP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            return (packet is UdpPacket);
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
            if (!(packet is UdpPacket))
            {
                throw new EditorInvalidPacket("Not a valid UDP Packet!");
            }

            object[] fields = explode(packet);

            UDPEditorForm form = new UDPEditorForm(this,
                (int)fields[0],
                (int)fields[1],
                (int)fields[2],
                (string)fields[3],
                (string)fields[4]
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                fields[0] = form.getSourcePort();
                fields[1] = form.getDestinationPort();
                fields[2] = form.getLength();
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

            // fill up minimum amount of bytes
            // 64 bytes minimum for ethernet, including 46 bytes for payload
            byte[] temp = new byte[128];
            for (int x = 0; x < 60; x++)
            {
                temp[x] = 0xFF;
            }
            // set IPv4 ethernet flag
            temp[12] = 0x08;
            temp[13] = 0x00;
            // IP data starts at index 14
            // set UDP protocol type
            temp[23] = 0x11;

            UdpPacket packet = new UdpPacket(1, 2);

            // just use the other function now that we have a valid packet
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is UdpPacket))
            {
                throw new EditorInvalidPacket("Not a valid UDP Packet!");
            }

            UdpPacket udpPacket = (UdpPacket)packet;

            /*
             * Split into fields:
             *  - source port
             *  - destination port
             *  - length
             *  - checksum
             *  - data
             */

            object[] ret = new object[5];
            ret[0] = System.Convert.ToInt32(udpPacket.SourcePort);
            ret[1] = System.Convert.ToInt32(udpPacket.DestinationPort);
            ret[2] = System.Convert.ToInt32(udpPacket.Length);
            ret[3] = HexEncoder.ToString(ByteUtil.getBytes(udpPacket.Bytes, UdpFields.ChecksumPosition, UdpFields.ChecksumLength));
            ret[4] = HexEncoder.ToString(udpPacket.Bytes);

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
                /*
                 * source port
                 * dest port
                 * length
                 * checksum
                 * data
                 */
                if (!(fields[0] is int) || !(fields[1] is int) ||
                !(fields[2] is int) || !(fields[3] is string) ||
                !(fields[4] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 2 byte arrays, 4 integers, and 1 string.");
                }

                if (!verifySourcePort((int)fields[0]))
                {
                    throw new EditorInvalidField("Invalid UDP Source Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifyDestinationPort((int)fields[1]))
                {
                    throw new EditorInvalidField("Invalid UDP Destination Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifyLength((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid UDP Datagram length. Expecting an integer from 0 to 65535.");
                }
                if (!verifyChecksum((string)fields[3]))
                {
                    throw new EditorInvalidField("Invalid UDP Checksum. Expecting an integer from 0 to 65535.");
                }
                if (!verifyData((string)fields[4]))
                {
                    throw new EditorInvalidField("Invalid UDP Data. Expecting a hexadecimal string.");
                }

                int discarded = 0;
                byte[] mySrc = HexEncoder.GetBytes((int)fields[0], 4, out discarded);
                byte[] myDest = HexEncoder.GetBytes((int)fields[1], 4, out discarded);
                byte[] myLen = HexEncoder.GetBytes((int)fields[2], 4, out discarded);
                byte[] myCheck = HexEncoder.GetBytes((string)fields[3], out discarded);
                byte[] myData = HexEncoder.GetBytes((string)fields[4], out discarded);

                byte[] packetBytes = ByteUtil.combineBytes(mySrc, myDest, myLen, myCheck, myData);

                if (packet == null)
                {
                    packet = new UdpPacket(packetBytes, 0);
                }
                else
                {
                    Packet parent = packet.ParentPacket;
                    packet = new UdpPacket(packetBytes, 0);
                    packet.ParentPacket = parent;
                }

                return packet;
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct a UDP Packet.");
            }
        }

        #endregion required_methods


        /**
         * field verification
         */

        /*
         * source
         */
        public bool verifySourcePort(int type)
        {
            return (type >= 0 && type < 65536);
        }

        /*
        * destination
        */
        public bool verifyDestinationPort(int code)
        {
            return (code >= 0 && code < 65536);
        }

        /*
        * length
        */
        public bool verifyLength(int code)
        {
            return (code >= 0 && code < 65536);
        }

        /*
        * checksum
        */
        public bool verifyChecksum(string checksum)
        {
            return (checksum.Length == 4 && HexEncoder.InHexFormat(checksum));
        }

        /*
        * Verify data
        */
        public bool verifyData(string data)
        {
            return (data.Length <= 2960 && HexEncoder.InHexFormat(data));
        }

    }
}
