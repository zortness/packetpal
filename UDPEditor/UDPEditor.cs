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
            return "UDP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            if (packet is UDPPacket)
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
            if (!(packet is UDPPacket))
            {
                throw new EditorInvalidPacket("Not a valid UDP Packet!");
            }

            UDPEditorForm form = new UDPEditorForm(this,
                ((UDPPacket)packet).SourcePort,
                ((UDPPacket)packet).DestinationPort,
                ((UDPPacket)packet).UDPLength,
                ((UDPPacket)packet).UDPChecksum,
                HexEncoder.ToString(((UDPPacket)packet).UDPData)
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                ((UDPPacket)packet).SourcePort = form.getSourcePort();
                ((UDPPacket)packet).DestinationPort = form.getDestinationPort();
                ((UDPPacket)packet).UDPLength = form.getLength();
                ((UDPPacket)packet).UDPChecksum = form.getChecksum();

                if (form.reCompile)
                {
                    object[] temp = new object[4];
                    temp[0] = ((UDPPacket)packet).EthernetHeader;
                    temp[1] = ((UDPPacket)packet).IPHeader;
                    temp[2] = ((UDPPacket)packet).UDPHeader;
                    temp[3] = form.getData();
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

            Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

            // just use the other function now that we have a valid packet
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is UDPPacket))
            {
                throw new EditorInvalidPacket("Not a valid UDP Packet!");
            }

            /*
             * Split into fields:
             *  - ethernet header
             *  - ip header
             *  - source port
             *  - destination port
             *  - length
             *  - checksum
             *  - data
             */

            object[] ret = new object[7];
            ret[0] = ((UDPPacket)packet).EthernetHeader;
            ret[1] = ((UDPPacket)packet).IPHeader;
            ret[2] = ((UDPPacket)packet).SourcePort;
            ret[3] = ((UDPPacket)packet).DestinationPort;
            ret[4] = ((UDPPacket)packet).UDPLength;
            ret[5] = ((UDPPacket)packet).UDPChecksum;
            ret[6] = ((UDPPacket)packet).UDPData;

            return ret;
        }

        /*
         * Create a new ICMPPacket based on provided fields.
         */
        public override Packet compile(object[] fields)
        {
            // make sure the array is properly constructed
            if (fields.Length == 7)
            {
                /*
                 * ethernet header
                 * ip header
                 * source port
                 * dest port
                 * length
                 * checksum
                 * data
                 */
                if (!(fields[0] is byte[]) || !(fields[1] is byte[]) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is int) || !(fields[5] is int) ||
                !(fields[6] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 2 byte arrays, 4 integers, and 1 string.");
                }

                if (!verifySourcePort((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid UDP Source Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifyDestinationPort((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid UDP Destination Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifyLength((int)fields[4]))
                {
                    throw new EditorInvalidField("Invalid UDP Datagram length. Expecting an integer from 0 to 65535.");
                }
                if (!verifyChecksum((int)fields[5]))
                {
                    throw new EditorInvalidField("Invalid UDP Checksum. Expecting an integer from 0 to 65535.");
                }
                if (!verifyData((string)fields[6]))
                {
                    throw new EditorInvalidField("Invalid UDP Data. Expecting a hexadecimal string.");
                }

                int discarded;

                // convert source port to byte[]
                byte[] mySrc = HexEncoder.GetBytes(padHex(((int)fields[2]).ToString("x"), 4), out discarded);
                // convert destination port to byte[]
                byte[] myDest = HexEncoder.GetBytes(padHex(((int)fields[3]).ToString("x"), 4), out discarded);
                // convert length to byte[]
                byte[] myLen = HexEncoder.GetBytes(padHex(((int)fields[4]).ToString("x"), 4), out discarded);
                // convert checksum to byte[]
                byte[] myCheck = HexEncoder.GetBytes(padHex(((int)fields[4]).ToString("x"), 4), out discarded);
                // convert data to byte[]
                byte[] myData = HexEncoder.GetBytes((string)fields[5], out discarded);


                // container for everything
                byte[] temp = new byte[
                    ((byte[])fields[0]).Length + ((byte[])fields[1]).Length +
                    16 + myData.Length];

                // copy our bytes over to the temp array for parsing
                ((byte[])fields[0]).CopyTo(temp, 0);
                ((byte[])fields[1]).CopyTo(temp, ((byte[])fields[0]).Length);
                mySrc.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length);
                myDest.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 4);
                myLen.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 8);
                myCheck.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 12);
                myData.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 16);


                // use the packet factory to compile
                // this will allow something possibly higher than an EthernetPacket to be constructed
                return PacketFactory.dataToPacket(
                    LinkLayers_Fields.IEEE802,
                    temp
                );
            }
            else if (fields.Length == 4)
            {
                /*
                 * ethernet header
                 * ip header
                 * udp header
                 * udp data
                 */

                if (!verifyData((string)fields[3]))
                {
                    throw new EditorInvalidField("Invalid UDP Data. Expecting a hexadecimal string.");
                }

                int discarded;
                // convert data to byte[]
                byte[] myData = HexEncoder.GetBytes((string)fields[3], out discarded);

                // container for everything
                byte[] temp = new byte[
                    ((byte[])fields[0]).Length + ((byte[])fields[1]).Length +
                    ((byte[])fields[2]).Length + myData.Length];

                // copy our bytes over to the temp array for parsing
                ((byte[])fields[0]).CopyTo(temp, 0);
                ((byte[])fields[1]).CopyTo(temp, ((byte[])fields[0]).Length);
                ((byte[])fields[2]).CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length);
                myData.CopyTo(temp, ((byte[])fields[0]).Length + ((byte[])fields[1]).Length + ((byte[])fields[2]).Length);

                // use the packet factory to compile
                // this will allow something possibly higher than an EthernetPacket to be constructed
                return PacketFactory.dataToPacket(
                    LinkLayers_Fields.IEEE802,
                    temp
                );
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct a UDP Packet.");
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
         * source
         */
        public bool verifySourcePort(int type)
        {
            if (type >= 0 && type < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * destination
        */
        public bool verifyDestinationPort(int code)
        {
            if (code >= 0 && code < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * length
        */
        public bool verifyLength(int code)
        {
            if (code >= 0 && code < 65536)
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
