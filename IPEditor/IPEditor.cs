using System;
using System.Collections.Generic;
using System.Text;
using SharpPcap;
using PacketDotNet;
using PacketDotNet.Utils;
using Kopf.PacketPal.Util;
using Kopf.PacketPal.TCPIPLayers;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    /*
     * Our class for editing IPv4Packet objects from Pcap.
     */
    public class IPEditor : PacketEditor
    {
        // layer of operation
        private TCPIPLayer myLayer;

        /*
         * Constructor needs to take no arguments.
         */
        public IPEditor()
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
            return "IPv4 Packet Editor";
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
            return "IPv4 Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            return (packet is IPv4Packet);
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
            if (!(packet is IPv4Packet))
            {
                throw new EditorInvalidPacket("Not a valid IP Packet!");
            }

            object[] fields = explode(packet);

            IPEditorForm form = new IPEditorForm(this,
                (int)fields[0],
                (int)fields[1],
                (int)fields[2],
                (int)fields[3],
                (int)fields[4],
                (int)fields[5],
                (int)fields[6],
                (int)fields[7],
                (string)fields[8],
                (string)fields[9],
                (string)fields[10],
                (string)fields[11],
                (string)fields[12]
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                // modify the easy fields
                fields[0] = form.getVersion();
                fields[1] = form.getHeaderLength();
                fields[2] = form.getTypeOfService();
                fields[3] = form.getTotalLength();
                fields[4] = form.getID();
                fields[5] = form.getFlags();
                fields[6] = form.getFragmentOffset();
                fields[7] = form.getTimeToLive();
                fields[8] = form.getProtocol();
                fields[9] = form.getChecksum();
                fields[10] = form.getSourceAddress();
                fields[11] = form.getDestinationAddress();
                fields[12] = form.getData();

                packet = compile(fields, packet);

                // recalculate checksum
                if (form.reCompute)
                {
                    ((IPv4Packet)packet).CalculateIPChecksum();
                }

                form.Dispose();
                return packet;
            }
            else
            {
                return null;
            }
            
        }

        /*
         * GUI Editing of a new packet.
         */
        public override Packet guiEdit()
        {

            // fill up minimum amount of bytes
            // 64 bytes minimum for ethernet, including 46 bytes for payload
            byte[] temp = new byte[64];
            for (int x = 0; x < 64; x++)
            {
                temp[x] = 0xFF;
            }
            
            // set IPv4 ethernet flag
            temp[12] = 0x08;
            temp[13] = 0x00;
            Packet packet = new IPv4Packet(System.Net.IPAddress.Parse("1.1.1.1"), System.Net.IPAddress.Parse("2.2.2.2"));
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is IPv4Packet))
            {
                throw new EditorInvalidPacket("Not a valid IP Packet!");
            }

            IPv4Packet ipPacket = (IPv4Packet)packet;

            /*
             * Split into fields:
             *  - ethernet header byte array
             *  - version, int
             *  - header length, int
             *  - type of service, int
             *  - total length, int
             *  - id, int
             *  - flags, int
             *  - fragment offset, int
             *  - ttl, int
             *  - protocol, string
             *  - header checksum, string
             *  - source ip, string
             *  - destination ip, string
             *  - data, string
             */

            byte[] protocol = ByteUtil.getBytes(packet.Bytes, IPv4Fields.ProtocolPosition, IPv4Fields.ProtocolLength);

            byte[] checksum = ByteUtil.getBytes(packet.Bytes, IPv4Fields.ChecksumPosition, IPv4Fields.ChecksumLength);

            byte[] payload = packet.PayloadData;
            if (payload == null && packet.PayloadPacket != null)
            {
                payload = packet.PayloadPacket.Bytes;
            }

            object[] ret = new object[13];
            ret[0] = 4;
            ret[1] = ipPacket.HeaderLength;
            ret[2] = ipPacket.TypeOfService;
            ret[3] = ipPacket.TotalLength;
            ret[4] = System.Convert.ToInt32(ipPacket.Id);
            ret[5] = ipPacket.FragmentFlags;
            ret[6] = ipPacket.FragmentOffset;
            ret[7] = ipPacket.TimeToLive;
            ret[8] = HexEncoder.ToString(protocol);
            ret[9] = HexEncoder.ToString(checksum);
            ret[10] = ipPacket.SourceAddress.ToString();
            ret[11] = ipPacket.DestinationAddress.ToString();
            ret[12] = HexEncoder.ToString(payload);

            return ret;
        }

        public override Packet compile(object[] fields)
        {
            return compile(fields, null);
        }

        /*
         * Create a new EthernetPacket based on provided fields.
         */
        public override Packet compile(object[] fields, Packet packet)
        {
            // make sure the array is properly constructed
            if (fields.Length == 13)
            {
                if (!(fields[0] is int) || !(fields[1] is int) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is int) || !(fields[5] is int) ||
                !(fields[6] is int) || !(fields[7] is int) ||
                !(fields[8] is string) || !(fields[9] is string) ||
                !(fields[10] is string) || !(fields[11] is string) ||
                !(fields[12] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 8 integers, and 5 strings.");
                }

                if (!verifyVersion((int)fields[0]))
                {
                    throw new EditorInvalidField("Invalid Version. Expecting an integer from 0 to 15.");
                }
                if (!verifyHeaderLength((int)fields[1]))
                {
                    throw new EditorInvalidField("Invalid Protocol. Expecting an integer from 0 to 15.");
                }
                if (!verifyTOS((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid Type of Service. Expecting an integer from 0 to 255.");
                }
                if (!verifyTotalLength((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid Total Length. Expecting an integer from 0 to 65535.");
                }
                if (!verifyID((int)fields[4]))
                {
                    throw new EditorInvalidField("Invalid ID number. Expecting an integer from 0 to 65535.");
                }
                if (!verifyFlags((int)fields[5]))
                {
                    throw new EditorInvalidField("Invalid Flags. Expecting an integer from 0 to 7.");
                }
                if (!verifyFragmentOffset((int)fields[6]))
                {
                    throw new EditorInvalidField("Invalid Fragment Offset. Expecting an integer from 0 to 65535.");
                }
                if (!verifyTimeToLive((int)fields[7]))
                {
                    throw new EditorInvalidField("Invalid Time To Live. Expecting an integer from 0 to 255.");
                }
                if (!verifyProtocol((string)fields[8]))
                {
                    throw new EditorInvalidField("Invalid Protocol. Expecting a hexadecimal string.");
                }
                if (!verifyChecksum((string)fields[9]))
                {
                    throw new EditorInvalidField("Invalid Checksum. Expecting a hexadeciaml string.");
                }
                if (!verifySourceAddress((string)fields[10]))
                {
                    throw new EditorInvalidField("Invalid Source IP Address. Expecting an IP address as a string.");
                }
                if (!verifyDestinationAddress((string)fields[11]))
                {
                    throw new EditorInvalidField("Invalid Destination IP Address. Expecting an IP address as a string.");
                }
                if (!verifyData((string)fields[12]))
                {
                    throw new EditorInvalidField("Invalid Data string. Expecting a hexadecimal string with a length from 0 to 2960 (1480 bytes).");
                }

                int discarded = 0;

                // combine version and header length (4 bits each)
                // [vers][hlen] -> 0000 0000b -> int32 -> bytes (1)
                // vers * 2^4 + hlen
                int verHlenInt = ((int)fields[0] * 16) + (int)fields[1];
                byte[] verHlen = HexEncoder.GetBytes(verHlenInt, 2, out discarded);

                byte[] tos = HexEncoder.GetBytes((int)fields[2], 2, out discarded);
                byte[] totalLength = HexEncoder.GetBytes((int)fields[3], 4, out discarded);
                byte[] id = HexEncoder.GetBytes((int)fields[4], 4, out discarded);

                // combine flags and frag offset (3 bits & 13 bits)
                // [flags][fragoffset] -> 000 0000000000000b -> int32 -> bytes (2)
                // flags * 2^13 + fragoffset
                int fragInt = ((int)fields[5] * 8192) + (int)fields[6];
                byte[] frag = HexEncoder.GetBytes(fragInt, 4, out discarded);

                byte[] ttl = HexEncoder.GetBytes((int)fields[7], 2, out discarded);
                byte[] protocol = HexEncoder.GetBytes((string)fields[8], out discarded);
                byte[] checksum = HexEncoder.GetBytes((string)fields[9], out discarded);
                byte[] sourceIp = System.Net.IPAddress.Parse((string)fields[10]).GetAddressBytes();
                byte[] destIp = System.Net.IPAddress.Parse((string)fields[11]).GetAddressBytes();
                byte[] data = HexEncoder.GetBytes((string)fields[12], out discarded);

                byte[] packetBytes = ByteUtil.combineBytes(verHlen, tos, totalLength, id, frag, ttl, protocol, checksum, sourceIp, destIp, data);

                if (packet == null)
                {
                    packet = new IPv4Packet(packetBytes, 0);
                }
                else
                {
                    Packet parent = packet.ParentPacket;
                    packet = new IPv4Packet(packetBytes, 0);
                    packet.ParentPacket = parent;
                }

                return packet;
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct an IP Packet.");
            }
        }

        #endregion required_methods

        /**
         * Field verification functions
         */

        /*
         * Verify version, 4 bit max, 2^4-1 = 15
         */
        public bool verifyVersion(int version)
        {
            return (version >= 0 && version < 16);
        }

        /*
         * Verify header length, 4 bit max
         */
        public bool verifyHeaderLength(int len)
        {
            return (len >= 0 && len < 64);
        }

        /*
         * Verify type of service, 8 bits
         */
        public bool verifyTOS(int tos)
        {
            return (tos >= 0 && tos < 256);
        }

        /*
         * Verify total length, 16 bits
         */
        public bool verifyTotalLength(int len)
        {
            return (len >= 0 && len < 65536);
        }

        /*
         * Verify ID, 16 bits
         */
        public bool verifyID(int id)
        {
            return (id >= 0 && id < 65536);
        }

        /*
         * Verify Flags, 3 bits
         */
        public bool verifyFlags(int flags)
        {
            return (flags >= 0 && flags < 8);
        }

        /*
         * Verify fragment offset, 13 bits
         */
        public bool verifyFragmentOffset(int offset)
        {
            return (offset >= 0 && offset < 8192);
        }

        /*
         * Verify TTL, 8 bits
         */
        public bool verifyTimeToLive(int ttl)
        {
            return (ttl >= 0 && ttl < 256);
        }

        /*
         * Verify protocol, 8 bits
         */
        public bool verifyProtocol(string protocol)
        {
            return (protocol.Length == 1 && HexEncoder.InHexFormat(protocol));
        }

        /*
         * Verify header checksum, 16 bits
         * 
         * don't want to recalculate here, just make sure it's within range
         */
        public bool verifyChecksum(string checksum)
        {
            return (checksum.Length == 4 && HexEncoder.InHexFormat(checksum));
        }

        /*
         * Verify source address, 32 bits
         */
        public bool verifySourceAddress(string src)
        {
            try
            {
                System.Net.IPAddress.Parse(src);
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        /*
         * Verify destination address, 32 bits
         */
        public bool verifyDestinationAddress(string dest)
        {
            try
            {
                System.Net.IPAddress.Parse(dest);
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        /*
         * Verify data
         */
        public bool verifyData(string data)
        {
            return (data.Length >= 0 && data.Length <= 2960 && HexEncoder.InHexFormat(data));
        }
    }
}
