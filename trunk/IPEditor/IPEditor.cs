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
    /*
     * Our class for editing IPPacket objects from Pcap.
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
            return "IP Packet Editor";
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
            return "IP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            if (packet is IPPacket)
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
            if (!(packet is IPPacket))
            {
                throw new EditorInvalidPacket("Not a valid IP Packet!");
            }

            IPEditorForm form = new IPEditorForm(this,
                ((IPPacket)packet).Version,
                ((IPPacket)packet).IpHeaderLength,
                ((IPPacket)packet).TypeOfService,
                ((IPPacket)packet).IPTotalLength,
                ((IPPacket)packet).Id,
                ((IPPacket)packet).FragmentFlags,
                ((IPPacket)packet).FragmentOffset,
                ((IPPacket)packet).TimeToLive,
                ((IPPacket)packet).IPProtocol,
                ((IPPacket)packet).IPChecksum,
                ((IPPacket)packet).SourceAddress,
                ((IPPacket)packet).DestinationAddress,
                HexEncoder.ToString(((IPPacket)packet).IPData)
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                // modify the easy fields
                ((IPPacket)packet).Version = form.getVersion();
                //((IPPacket)packet).HeaderLength = form.getHeaderLength();
                ((IPPacket)packet).TypeOfService = form.getTypeOfService();
                ((IPPacket)packet).IPTotalLength = form.getTotalLength();
                ((IPPacket)packet).Id = form.getID();
                ((IPPacket)packet).FragmentFlags = form.getFlags();
                ((IPPacket)packet).FragmentOffset = form.getFragmentOffset();
                ((IPPacket)packet).TimeToLive = form.getTimeToLive();
                ((IPPacket)packet).IPProtocol = form.getProtocol();
                ((IPPacket)packet).IPChecksum = form.getChecksum();
                ((IPPacket)packet).SourceAddress = form.getSourceAddress();
                ((IPPacket)packet).DestinationAddress = form.getDestinationAddress();

                // recalculate checksum
                if (form.reCompute)
                {
                    ((IPPacket)packet).ComputeIPChecksum(true);
                }

                // if the payload was altered, we need to recompile the packet from scratch
                if (form.reCompile)
                {
                    // hex string to byte
                    int discarded;
                    byte[] ipData = HexEncoder.GetBytes(form.getData(), out discarded);

                    // set new total length in the header
                    ((IPPacket)packet).IPTotalLength = ((IPPacket)packet).IpHeaderLength + ipData.Length; // in bytes already

                    // byte array for full packet
                    byte[] full = new byte[((IPPacket)packet).EthernetHeader.Length + ((IPPacket)packet).IPHeader.Length + ipData.Length];
                    // copy bytes to full packet
                    ((IPPacket)packet).EthernetHeader.CopyTo(full, 0);
                    ((IPPacket)packet).IPHeader.CopyTo(full, ((IPPacket)packet).EthernetHeader.Length);
                    ipData.CopyTo(full, ((IPPacket)packet).EthernetHeader.Length + ((IPPacket)packet).IPHeader.Length);

                    object[] fields = new object[1];
                    fields[0] = full;

                    // compile the hex string into a new packet
                    packet = compile(fields);
                }
                // destroy the form
                form.Dispose();
                // return our packet
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

            Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

            // just use the other function now that we have a valid packet
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is IPPacket))
            {
                throw new EditorInvalidPacket("Not a valid IP Packet!");
            }

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
             *  - protocol, int
             *  - header checksum, int
             *  - source ip, string
             *  - destination ip, string
             *  - data, string
             */

            object[] ret = new object[14];
            ret[0] = ((IPPacket)packet).EthernetHeader;
            ret[1] = ((IPPacket)packet).Version;
            ret[2] = ((IPPacket)packet).HeaderLength;
            ret[3] = ((IPPacket)packet).TypeOfService;
            ret[4] = ((IPPacket)packet).IPTotalLength;
            ret[5] = ((IPPacket)packet).Id;
            ret[6] = ((IPPacket)packet).FragmentFlags;
            ret[7] = ((IPPacket)packet).FragmentOffset;
            ret[8] = ((IPPacket)packet).TimeToLive;
            ret[9] = ((IPPacket)packet).IPProtocol;
            ret[10] = ((IPPacket)packet).IPChecksum;
            ret[11] = ((IPPacket)packet).SourceAddress;
            ret[12] = ((IPPacket)packet).DestinationAddress;
            ret[13] = HexEncoder.ToString(((IPPacket)packet).IPData);

            return ret;
        }

        /*
         * Create a new EthernetPacket based on provided fields.
         */
        public override Packet compile(object[] fields)
        {
            // make sure the array is properly constructed
            if (fields.Length == 14)
            {
                if (!(fields[0] is byte[]) ||
                !(fields[1] is int) || !(fields[2] is int) ||
                !(fields[3] is int) || !(fields[4] is int) ||
                !(fields[5] is int) || !(fields[6] is int) ||
                !(fields[7] is int) || !(fields[8] is int) ||
                !(fields[9] is int) || !(fields[10] is int) ||
                !(fields[11] is string) || !(fields[12] is string) ||
                !(fields[13] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 1 byte array, 10 integers, and 3 strings.");
                }

                if (!verifyVersion((int)fields[1]))
                {
                    throw new EditorInvalidField("Invalid Version. Expecting an integer from 0 to 15.");
                }
                if (!verifyProtocol((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid Protocol. Expecting an integer from 0 to 15.");
                }
                if (!verifyTOS((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid Type of Service. Expecting an integer from 0 to 255.");
                }
                if (!verifyTotalLength((int)fields[4]))
                {
                    throw new EditorInvalidField("Invalid Total Length. Expecting an integer from 0 to 65535.");
                }
                if (!verifyID((int)fields[5]))
                {
                    throw new EditorInvalidField("Invalid ID number. Expecting an integer from 0 to 65535.");
                }
                if (!verifyFlags((int)fields[6]))
                {
                    throw new EditorInvalidField("Invalid Flags. Expecting an integer from 0 to 7.");
                }
                if (!verifyFragmentOffset((int)fields[7]))
                {
                    throw new EditorInvalidField("Invalid Fragment Offset. Expecting an integer from 0 to 65535.");
                }
                if (!verifyTimeToLive((int)fields[8]))
                {
                    throw new EditorInvalidField("Invalid Time To Live. Expecting an integer from 0 to 255.");
                }
                if (!verifyProtocol((int)fields[9]))
                {
                    throw new EditorInvalidField("Invalid Protocol. Expecting an integer from 0 to 255.");
                }
                if (!verifyChecksum((int)fields[10]))
                {
                    throw new EditorInvalidField("Invalid Checksum. Expecting an integer from 0 to 65535.");
                }
                if (!verifySourceAddress((string)fields[11]))
                {
                    throw new EditorInvalidField("Invalid Source IP Address. Expecting a string of format ");
                }
                if (!verifyDestinationAddress((string)fields[12]))
                {
                    throw new EditorInvalidField("Invalid Destination IP Address. Expecting a string of format ");
                }
                if (!verifyData((string)fields[13]))
                {
                    throw new EditorInvalidField("Invalid Data string. Expecting a hexadecimal string with a length from 0 to 2960 (1480 bytes).");
                }

                // we need to combine the flags and fragment offset to get the right number of bytes
                // since the flags are smaller than a byte, we have to do this at the binary level 
                // and work our way back up
                string fragstring = padHex(System.Convert.ToString(((int)fields[5]), 2), 3) +
                    padHex(System.Convert.ToString(((int)fields[6]), 2), 13);
                int frags = System.Convert.ToInt32(fragstring, 2);




                // use the packet factory to compile
                // this will allow something possibly higher than an EthernetPacket to be constructed
                int discarded;
                return PacketFactory.dataToPacket(
                    LinkLayers_Fields.IEEE802,
                    HexEncoder.GetBytes(
                    // need to convert everything to hex strings for this to work
                        ((int)fields[0]).ToString("x") + // version
                        ((int)fields[1]).ToString("x") + // version
                        ((int)fields[2]).ToString("x") + // version
                        ((int)fields[3]).ToString("x") + // version
                        ((int)fields[4]).ToString("x") + // version
                        frags.ToString("x") + // flags + fragment offset
                        ((int)fields[7]).ToString("x") + // version
                        ((int)fields[8]).ToString("x") + // version
                        ((int)fields[9]).ToString("x") // version

                        /**
                         * NOT FINISHED
                         */
                        ,
                        out discarded
                    )
                );
            }
            else if (fields.Length == 1)
            {
                // ethernet header
                // ip header
                // new ip data string
                if (!(fields[0] is byte[]))
                {
                    throw new EditorInvalidField("Invalid field specified. Expecting 1 byte array.");
                }
                return PacketFactory.dataToPacket(
                    LinkLayers_Fields.IEEE802,
                    (byte[])fields[0]
                );

            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct an IP Packet.");
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
         * Field verification functions
         */

        /*
         * Verify version, 4 bit max, 2^4-1 = 15
         */
        public bool verifyVersion(int version)
        {
            if (version >= 0 && version < 16)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify header length, 4 bit max
         */
        public bool verifyHeaderLength(int len)
        {
            if (len >= 0 && len < 64)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify type of service, 8 bits
         */
        public bool verifyTOS(int tos)
        {
            if (tos >= 0 && tos < 256)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify total length, 16 bits
         */
        public bool verifyTotalLength(int len)
        {
            if (len >= 0 && len < 65536)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify ID, 16 bits
         */
        public bool verifyID(int id)
        {
            if (id >= 0 && id < 65536)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify Flags, 3 bits
         */
        public bool verifyFlags(int flags)
        {
            if (flags >= 0 && flags < 8)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify fragment offset, 13 bits
         */
        public bool verifyFragmentOffset(int offset)
        {
            if (offset >= 0 && offset < 8192)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify TTL, 8 bits
         */
        public bool verifyTimeToLive(int ttl)
        {
            if (ttl >= 0 && ttl < 256)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify protocol, 8 bits
         */
        public bool verifyProtocol(int protocol)
        {
            if (protocol >= 0 && protocol < 256)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify header checksum, 16 bits
         * 
         * don't want to recalculate here, just make sure it's within range
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
         * Verify source address, 32 bits
         */
        public bool verifySourceAddress(string src)
        {
            Regex test1 = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
            Match m = test1.Match(src);
            if(m.Success && m.Length == src.Length)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify destination address, 32 bits
         */
        public bool verifyDestinationAddress(string dest)
        {
            Regex test1 = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
            Match m = test1.Match(dest);
            if (m.Success && m.Length == dest.Length)
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
