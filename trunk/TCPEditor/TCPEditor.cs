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
    public class TCPEditor : PacketEditor
    {
        // layer of operation
        private TCPIPLayer myLayer;

        /*
         * Constructor needs to take no arguments.
         */
        public TCPEditor()
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
            return "TCP Packet Editor";
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
            return "TCP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            if (packet is TCPPacket)
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
            if (!(packet is TCPPacket))
            {
                throw new EditorInvalidPacket("Not a valid TCP Packet!");
            }

            TCPEditorForm form = new TCPEditorForm(this,
                ((TCPPacket)packet).SourcePort,
                ((TCPPacket)packet).DestinationPort,
                ((TCPPacket)packet).SequenceNumber,
                ((TCPPacket)packet).AcknowledgementNumber,
                ((TCPPacket)packet).TCPHeaderLength,
                ((TCPPacket)packet).CWR,
                ((TCPPacket)packet).ECN,
                ((TCPPacket)packet).Urg,
                ((TCPPacket)packet).Ack,
                ((TCPPacket)packet).Psh,
                ((TCPPacket)packet).Rst,
                ((TCPPacket)packet).Syn,
                ((TCPPacket)packet).Fin,
                ((TCPPacket)packet).WindowSize,
                ((TCPPacket)packet).TCPChecksum,
                ((TCPPacket)packet).getUrgentPointer(),
                HexEncoder.ToString(((TCPPacket)packet).TCPData)
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                ((TCPPacket)packet).SourcePort = form.getSourcePort();
                ((TCPPacket)packet).DestinationPort = form.getDestinationPort();
                ((TCPPacket)packet).SequenceNumber = form.getSequenceNumber();
                ((TCPPacket)packet).AcknowledgementNumber = form.getAcknowledgementNumber();
                ((TCPPacket)packet).TCPHeaderLength = form.getHeaderLength();
                ((TCPPacket)packet).CWR = form.getCwrFlag();
                ((TCPPacket)packet).ECN = form.getEcnFlag();
                ((TCPPacket)packet).Urg = form.getUrgFlag();
                ((TCPPacket)packet).Ack = form.getAckFlag();
                ((TCPPacket)packet).Psh = form.getPshFlag();
                ((TCPPacket)packet).Rst = form.getRstFlag();
                ((TCPPacket)packet).Syn = form.getSynFlag();
                ((TCPPacket)packet).Fin = form.getFinFlag();
                ((TCPPacket)packet).WindowSize = form.getWindowSize();
                ((TCPPacket)packet).TCPChecksum = form.getChecksum();
                ((TCPPacket)packet).setUrgentPointer(form.getUrgentPointer());

                if (form.reCompile)
                {
                    int discarded;
                    byte[] myData = HexEncoder.GetBytes(form.getData(), out discarded);
                    ((TCPPacket)packet).SetData(myData);
                }

                if (form.reCompute)
                {
                    ((TCPPacket)packet).ComputeTCPChecksum(true);
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
            // set TCP protocol type
            temp[23] = 0x06;

            Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

            // just use the other function now that we have a valid packet
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is TCPPacket))
            {
                throw new EditorInvalidPacket("Not a valid TCP Packet!");
            }

            object[] ret = new object[19];
            ret[0] = ((TCPPacket)packet).EthernetHeader;
            ret[1] = ((TCPPacket)packet).IPHeader;
            ret[2] = ((TCPPacket)packet).SourcePort;
            ret[3] = ((TCPPacket)packet).DestinationPort;
            ret[4] = ((TCPPacket)packet).SequenceNumber;
            ret[5] = ((TCPPacket)packet).AcknowledgementNumber;
            ret[6] = ((TCPPacket)packet).TCPHeaderLength;
            ret[7] = ((TCPPacket)packet).CWR;
            ret[8] = ((TCPPacket)packet).ECN;
            ret[9] = ((TCPPacket)packet).Urg;
            ret[10] = ((TCPPacket)packet).Ack;
            ret[11] = ((TCPPacket)packet).Psh;
            ret[12] = ((TCPPacket)packet).Rst;
            ret[13] = ((TCPPacket)packet).Syn;
            ret[14] = ((TCPPacket)packet).Fin;
            ret[15] = ((TCPPacket)packet).WindowSize;
            ret[16] = ((TCPPacket)packet).TCPChecksum;
            ret[17] = ((TCPPacket)packet).getUrgentPointer();
            ret[18] = ((TCPPacket)packet).TCPData;

            return ret;
        }

        /*
         * Create a new ICMPPacket based on provided fields.
         */
        public override Packet compile(object[] fields)
        {
            // make sure the array is properly constructed
            if (fields.Length == 19)
            {
                /*
                 * ethernet header
                 * ip header
                 * source port
                 * dest port
                 * sequence number
                 * acknoledgement number
                 * header length
                 * cwr
                 * ecn
                 * urg
                 * ack
                 * psh
                 * rst
                 * syn
                 * fin
                 * window size
                 * checksum
                 * urgent pointer
                 * data
                 */
                if (!(fields[0] is byte[]) || !(fields[1] is byte[]) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is long) || !(fields[5] is long) ||
                !(fields[6] is int) || !(fields[7] is bool) ||
                !(fields[8] is bool) || !(fields[9] is bool) ||
                !(fields[10] is bool) || !(fields[11] is bool) ||
                !(fields[12] is bool) || !(fields[13] is bool) ||
                !(fields[14] is bool) || !(fields[15] is int) ||
                !(fields[16] is int) || !(fields[17] is int) ||
                !(fields[18] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 2 byte arrays, 2 integers, 2 longs, 8 bools, 3 more integers, and 1 string.");
                }

                if (!verifySourcePort((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid TCP Source Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifyDestinationPort((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid TCP Destination Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifySequenceNumber((long)fields[4]))
                {
                    throw new EditorInvalidField("Invalid TCP sequence number. Expecting a double from 0 to 4294967296.");
                }
                if (!verifyAcknowledgementNumber((long)fields[5]))
                {
                    throw new EditorInvalidField("Invalid TCP ACK number. Expecting an integer from 0 to 4294967296.");
                }
                if (!verifyLength((int)fields[6]))
                {
                    throw new EditorInvalidField("Invalid TCP header length. Expecting an integer from 0 to 65535.");
                }
                if (!verifyWindowSize((int)fields[15]))
                {
                    throw new EditorInvalidField("Invalid TCP window size. Expecting an integer from 0 to 65535.");
                }
                if (!verifyChecksum((int)fields[16]))
                {
                    throw new EditorInvalidField("Invalid TCP checksum. Expecting an integer from 0 to 65535.");
                }
                if (!verifyUrgentPointer((int)fields[17]))
                {
                    throw new EditorInvalidField("Invalid TCP urgent pointer. Expecting an integer from 0 to 65535.");
                }
                if (!verifyData((string)fields[18]))
                {
                    throw new EditorInvalidField("Invalid TCP Data. Expecting a hexadecimal string.");
                }

                // the easiest way to do this is to create a bogus packet then change the information
                // fill up minimum amount of bytes
                // 64 bytes minimum for ethernet, including 46 bytes for payload
                byte[] temp;
                if (((byte[])fields[0]).Length + ((byte[])fields[1]).Length > 64)
                {
                    temp = new byte[((byte[])fields[0]).Length + ((byte[])fields[1]).Length + 16];
                }
                else
                {
                    temp = new byte[64];
                }

                ((byte[])fields[0]).CopyTo(temp, 0);
                ((byte[])fields[1]).CopyTo(temp, ((byte[])fields[0]).Length);

                Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

                if (packet is TCPPacket)
                {
                    int discarded;
                    // set the fields
                    ((TCPPacket)packet).SourcePort = (int)fields[2];
                    ((TCPPacket)packet).DestinationPort = (int)fields[3];
                    ((TCPPacket)packet).SequenceNumber = (long)fields[4];
                    ((TCPPacket)packet).AcknowledgementNumber = (long)fields[5];
                    ((TCPPacket)packet).TCPHeaderLength = (int)fields[6];
                    ((TCPPacket)packet).CWR = (bool)fields[7];
                    ((TCPPacket)packet).ECN = (bool)fields[8];
                    ((TCPPacket)packet).Urg = (bool)fields[9];
                    ((TCPPacket)packet).Ack = (bool)fields[10];
                    ((TCPPacket)packet).Psh = (bool)fields[11];
                    ((TCPPacket)packet).Rst = (bool)fields[12];
                    ((TCPPacket)packet).Syn = (bool)fields[13];
                    ((TCPPacket)packet).Fin = (bool)fields[14];
                    ((TCPPacket)packet).TCPChecksum = (int)fields[15];
                    ((TCPPacket)packet).WindowSize = (int)fields[16];
                    ((TCPPacket)packet).setUrgentPointer((int)fields[17]);
                    ((TCPPacket)packet).SetData(HexEncoder.GetBytes((string)fields[18], out discarded));

                    return packet;
                }
                else
                {
                    throw new EditorInvalidPacket("Unable to create a TCP packet with the provided information.");
                }
            }
            else if (fields.Length == 4)
            {
                /*
                 * ethernet header
                 * ip header
                 * tcp header
                 * tcp data
                 */

                if (!verifyData((string)fields[3]))
                {
                    throw new EditorInvalidField("Invalid TCP Data. Expecting a hexadecimal string.");
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
                throw new EditorInvalidField("Invalid field count to construct a TCP Packet.");
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
        public bool verifySourcePort(int port)
        {
            if (port >= 0 && port < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * destination
        */
        public bool verifyDestinationPort(int port)
        {
            if (port >= 0 && port < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * sequence number
        */
        public bool verifySequenceNumber(long seq)
        {
            if (seq >= 0 && seq < 4294967296)
            {
                return true;
            }
            return false;
        }

        /*
        * ack number
        */
        public bool verifyAcknowledgementNumber(long ack)
        {
            if (ack >= 0 && ack < 4294967296)
            {
                return true;
            }
            return false;
        }

        /*
        * length
        */
        public bool verifyLength(int len)
        {
            if (len >= 0 && len < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * window size
        */
        public bool verifyWindowSize(int size)
        {
            if (size >= 0 && size < 65536)
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
        * urgent pointer
        */
        public bool verifyUrgentPointer(int ptr)
        {
            if (ptr >= 0 && ptr < 65536)
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
