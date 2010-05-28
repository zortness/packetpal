using System;
using System.Collections.Generic;
using System.Text;
using Kopf.PacketPal.Util;
using Kopf.PacketPal.TCPIPLayers;
using System.Windows.Forms;
using SharpPcap;
using PacketDotNet;

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
            return "TCP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            return (packet is TcpPacket);
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
            if (!(packet is TcpPacket))
            {
                throw new EditorInvalidPacket("Not a valid TCP Packet!");
            }

            object[] fields = explode(packet);

            TCPEditorForm form = new TCPEditorForm(this,
                (int)fields[0],
                (int)fields[1],
                (int)fields[2],
                (int)fields[3],
                (int)fields[4],
                (int)fields[5],
                (bool)fields[6],
                (bool)fields[7],
                (bool)fields[8],
                (bool)fields[9],
                (bool)fields[10],
                (bool)fields[11],
                (int)fields[12],
                (string)fields[13],
                (int)fields[14],
                (string)fields[15]
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                fields[0] = form.getSourcePort();
                fields[1] = form.getDestinationPort();
                fields[2] = form.getSequenceNumber();
                fields[3] = form.getAcknowledgementNumber();
                fields[4] = form.getHeaderLength();
                fields[5] = form.getReserved();
                fields[6] = form.getUrgFlag();
                fields[7] = form.getAckFlag();
                fields[8] = form.getPshFlag();
                fields[9] = form.getRstFlag();
                fields[10] = form.getSynFlag();
                fields[11] = form.getFinFlag();
                fields[12] = form.getWindowSize();
                fields[13] = form.getChecksum();
                fields[14] = form.getUrgentPointer();
                fields[15] = form.getData();

                packet = compile(fields, packet);

                if (form.reCompute)
                {
                    ((TcpPacket)packet).UpdateCalculatedValues();
                }
                form.Dispose();
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

            TcpPacket packet = new TcpPacket(1, 2);

            // just use the other function now that we have a valid packet
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is TcpPacket))
            {
                throw new EditorInvalidPacket("Not a valid TCP Packet!");
            }

            TcpPacket tcpPacket = (TcpPacket)packet;

            int start = TcpFields.AckNumberPosition + TcpFields.AckNumberLength;
            // convert to a hex string
            string tempHexStr = HexEncoder.ToString(ByteUtil.getBytes(packet.Bytes, start, 2));
            // convert to an integer
            int tempInt = System.Convert.ToInt32(tempHexStr, 16);
            // [hlen][reserverd][codebits] -> 0000 000000 000000
            // hlen mask: 1111000000000000b = 61440
            int hlen = tempInt | 61440;
            if (hlen > 0)
            {
                hlen /= 2 ^ 12;
            }
            // reserved mask = 0000111111000000 = 4032
            int reserved = tempInt | 4032;
            if (reserved >= 0)
            {
                reserved /= 2 ^ 6;
            }

            byte[] checksum = ByteUtil.getBytes(packet.Bytes, TcpFields.ChecksumPosition, TcpFields.ChecksumLength);

            byte[] payload = packet.PayloadData;
            if (payload == null && packet.PayloadPacket != null)
            {
                payload = packet.PayloadPacket.Bytes;
            }

            object[] ret = new object[16];
            ret[0] = System.Convert.ToInt32(tcpPacket.SourcePort);
            ret[1] = System.Convert.ToInt32(tcpPacket.DestinationPort);
            ret[2] = System.Convert.ToInt32(tcpPacket.SequenceNumber);
            ret[3] = System.Convert.ToInt32(tcpPacket.AcknowledgmentNumber);
            ret[4] = hlen;
            ret[5] = reserved;
            ret[6] = tcpPacket.Urg;
            ret[7] = tcpPacket.Ack;
            ret[8] = tcpPacket.Psh;
            ret[9] = tcpPacket.Rst;
            ret[10] = tcpPacket.Syn;
            ret[11] = tcpPacket.Fin;
            ret[12] = tcpPacket.WindowSize;
            ret[13] = HexEncoder.ToString(checksum);
            ret[14] = tcpPacket.UrgentPointer;
            ret[15] = HexEncoder.ToString(payload);

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
            if (fields.Length == 16)
            {
                /*
                 * source port, int
                 * dest port, int
                 * sequence number, int
                 * acknoledgement number, int
                 * header length, int
                 * reserved, int
                 * urg, bool
                 * ack, bool
                 * psh, bool
                 * rst, bool
                 * syn, bool
                 * fin, bool
                 * window size, int
                 * checksum, string (hex)
                 * urgent pointer, int
                 * data, string (hex)
                 */
                if (!(fields[0] is int) || !(fields[1] is int) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is int) || !(fields[5] is int) ||
                !(fields[6] is bool) || !(fields[7] is bool) ||
                !(fields[8] is bool) || !(fields[9] is bool) ||
                !(fields[10] is bool) || !(fields[11] is bool) ||
                !(fields[12] is int) || !(fields[13] is string) ||
                !(fields[14] is int) || !(fields[15] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 6 integers, 6 booleans, 1 int, 1 string, 1 int, 1 string.");
                }

                if (!verifySourcePort((int)fields[0]))
                {
                    throw new EditorInvalidField("Invalid TCP Source Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifyDestinationPort((int)fields[1]))
                {
                    throw new EditorInvalidField("Invalid TCP Destination Port. Expecting an integer from 0 to 65535.");
                }
                if (!verifySequenceNumber((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid TCP sequence number. Expecting a double from 0 to 4294967296.");
                }
                if (!verifyAcknowledgementNumber((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid TCP ACK number. Expecting an integer from 0 to 4294967296.");
                }
                if (!verifyLength((int)fields[4]))
                {
                    throw new EditorInvalidField("Invalid TCP header length. Expecting an integer from 0 to 65535.");
                }
                if (!verifyReserved((int)fields[5]))
                {
                    throw new EditorInvalidField("Invalid TCP reserved fields. Expecting hexadecimal string.");
                }
                if (!verifyWindowSize((int)fields[12]))
                {
                    throw new EditorInvalidField("Invalid TCP window size. Expecting an integer from 0 to 65535.");
                }
                if (!verifyChecksum((string)fields[13]))
                {
                    throw new EditorInvalidField("Invalid TCP checksum. Expecting an integer from 0 to 65535.");
                }
                if (!verifyUrgentPointer((int)fields[14]))
                {
                    throw new EditorInvalidField("Invalid TCP urgent pointer. Expecting an integer from 0 to 65535.");
                }
                if (!verifyData((string)fields[15]))
                {
                    throw new EditorInvalidField("Invalid TCP Data. Expecting a hexadecimal string.");
                }
                int discarded = 0;

                byte[] src = HexEncoder.GetBytes((int)fields[0], 4, out discarded);
                byte[] dest = HexEncoder.GetBytes((int)fields[1], 4, out discarded);
                byte[] seqNum = HexEncoder.GetBytes((int)fields[2], 8, out discarded);
                byte[] ackNum = HexEncoder.GetBytes((int)fields[3], 8, out discarded);

                // [hlen][reserved][codes] = 0000 000000 000000b
                // urg ack psh rst syn fin (6 - 11)
                int hlen = (int)fields[4] * (2 ^ 12);
                int reserved = (int)fields[5] * (2 ^ 6);
                int combined = hlen + reserved;
                if ((bool)fields[6])
                {
                    combined += 64;
                }
                if ((bool)fields[7])
                {
                    combined += 32;
                }
                if ((bool)fields[8])
                {
                    combined += 16;
                }
                if ((bool)fields[9])
                {
                    combined += 8;
                }
                if ((bool)fields[10])
                {
                    combined += 2;
                }
                if ((bool)fields[11])
                {
                    combined += 1;
                }
                byte[] combinedFields = HexEncoder.GetBytes(combined, 4, out discarded);

                byte[] window = HexEncoder.GetBytes((int)fields[12], 4, out discarded);
                byte[] checksum = HexEncoder.GetBytes((string)fields[13], out discarded);
                byte[] urgentPtr = HexEncoder.GetBytes((int)fields[14], 4, out discarded);
                byte[] data = HexEncoder.GetBytes((string)fields[15], out discarded);

                byte[] packetBytes = ByteUtil.combineBytes(src, dest, seqNum, ackNum, combinedFields, window, checksum, urgentPtr, data);

                if (packet == null)
                {
                    packet = new TcpPacket(packetBytes, 0);
                }
                else
                {
                    Packet parent = packet.ParentPacket;
                    packet = new TcpPacket(packetBytes, 0);
                    packet.ParentPacket = parent;
                }
                return packet;
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct a TCP Packet.");
            }
        }

        #endregion required_methods

        /**
         * field verification
         */

        /*
         * source
         */
        public bool verifySourcePort(int port)
        {
            return (port >= 0 && port < 65536);
        }

        /*
        * destination
        */
        public bool verifyDestinationPort(int port)
        {
            return (port >= 0 && port < 65536);
        }

        /*
        * sequence number
        */
        public bool verifySequenceNumber(long seq)
        {
            return (seq >= 0 && seq < 4294967296);
        }

        /*
        * ack number
        */
        public bool verifyAcknowledgementNumber(long ack)
        {
            return (ack >= 0 && ack < 4294967296);
        }

        /*
        * length
        */
        public bool verifyLength(int len)
        {
            return (len >= 0 && len < 16);
        }

        public bool verifyReserved(int reserved)
        {
            return reserved <= 64;
        }

        /*
        * window size
        */
        public bool verifyWindowSize(int size)
        {
            return (size >= 0 && size < 65536);
        }

        /*
        * checksum
        */
        public bool verifyChecksum(string checksum)
        {
            return (checksum.Length == 4 && HexEncoder.InHexFormat(checksum));
        }

        /*
        * urgent pointer
        */
        public bool verifyUrgentPointer(int ptr)
        {
            return (ptr >= 0 && ptr < 65536);
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
