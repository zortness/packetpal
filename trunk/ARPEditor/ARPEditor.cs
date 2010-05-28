using System;
using System.Collections.Generic;
using System.Text;
using Kopf.PacketPal.Util;
using Kopf.PacketPal.TCPIPLayers;
using System.Windows.Forms;
using PacketDotNet;

namespace Kopf.PacketPal.PacketEditors
{
    /*
     * Our class for editing IPPacket objects from Pcap.
     */
    public class ARPEditor : PacketEditor
    {
        // layer of operation
        private TCPIPLayer myLayer;

        /*
         * Constructor needs to take no arguments.
         */
        public ARPEditor()
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
            return "ARP Packet Editor";
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
            return "ARP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            return (packet is ARPPacket);
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
            if (!(packet is ARPPacket))
            {
                throw new EditorInvalidPacket("Not a valid ARP Packet!");
            }

            object[] fields = explode(packet);

            ARPEditorForm form = new ARPEditorForm(this,
                (string)fields[0],
                (string)fields[1],
                (int)fields[2],
                (int)fields[3],
                (string)fields[4],
                (string)fields[5],
                (string)fields[6],
                (string)fields[7],
                (string)fields[8]
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                fields[0] = form.getHardwareType();
                fields[1] = form.getProtocolType();
                fields[2] = form.getHardwareLength();
                fields[3] = form.getProtocolLength();
                fields[4] = form.getOperation();
                fields[5] = form.getSenderHardwareAddress();
                fields[6] = form.getSenderProtocolAddress();
                fields[7] = form.getTargetHardwareAddress();
                fields[8] = form.getTargetProtocolAddress();

                packet = compile(fields, packet);

                // destroy the form
                form.Dispose();

                // return our packet
                return packet;
            }

            // someone hit cancel or something, discard the packet
            return null;
        }

        /*
         * GUI Editing of a new packet.
         */
        public override Packet guiEdit()
        {
            // fill up minimum amount of bytes
            // 64 bytes minimum for ethernet, including 46 bytes for payload
            byte[] temp = new byte[42];
            // destination address
            temp[0] = 0x01;
            temp[1] = 0x01;
            temp[2] = 0x01;
            temp[3] = 0x01;
            temp[4] = 0x01;
            temp[5] = 0x01;
            // source address
            temp[6] = 0x01;
            temp[7] = 0x01;
            temp[8] = 0x01;
            temp[9] = 0x01;
            temp[10] = 0x01;
            temp[11] = 0x01;
            // set ARP ethernet flag
            temp[12] = 0x08;
            temp[13] = 0x06;
            // set hardware type to ethernet
            temp[14] = 0x00;
            temp[15] = 0x01;
            // protocol type is IP
            temp[16] = 0x08;
            temp[17] = 0x00;
            // hardware len is 6
            temp[18] = 0x06;
            // protocol len is 4 
            temp[19] = 0x04;
            // opcode is request (1)
            temp[20] = 0x00;
            temp[21] = 0x01;
            // sender's mac address (again)
            temp[22] = 0x01;
            temp[23] = 0x01;
            temp[24] = 0x01;
            temp[25] = 0x01;
            temp[26] = 0x01;
            temp[27] = 0x01;
            // sender's ip address
            temp[28] = 0x01;
            temp[29] = 0x01;
            temp[30] = 0x01;
            temp[31] = 0x01;
            // target's mac address
            temp[32] = 0x00;
            temp[33] = 0x00;
            temp[34] = 0x00;
            temp[35] = 0x00;
            temp[36] = 0x00;
            temp[37] = 0x00;
            // target's ip address
            temp[38] = 0x02;
            temp[39] = 0x02;
            temp[40] = 0x02;
            temp[41] = 0x02;

            byte[] targetMac = new byte[6] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02};
            byte[] targetIp = new byte[4] { 0x02, 0x02, 0x02, 0x02 };
            byte[] senderMac = new byte[6] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01};
            byte[] senderIp = new byte[4] { 0x01, 0x01, 0x01, 0x01 };

            //Packet packet = (Packet)(new ARPPacket(temp, 0));

            Packet packet = (Packet)new ARPPacket(ARPOperation.Request, new System.Net.NetworkInformation.PhysicalAddress(targetMac), new System.Net.IPAddress(targetIp),
                new System.Net.NetworkInformation.PhysicalAddress(senderMac), new System.Net.IPAddress(senderIp));

            //Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is ARPPacket))
            {
                throw new EditorInvalidPacket("Not a valid ARP Packet!");
            }

            /*
             * Split into fields:
             *  - ethernet header
             *  - htype
             *  - ptype
             *  - hlen
             *  - plen
             *  - oper
             *  - sha
             *  - spa
             *  - tha
             *  - tpa
             */

            ARPPacket arpPacket = (ARPPacket)packet;
            object[] ret = new object[9];
            ret[0] = HexEncoder.ToString(ByteUtil.getBytes(arpPacket.Bytes, ARPFields.HardwareAddressTypePosition, ARPFields.AddressTypeLength));
            ret[1] = HexEncoder.ToString(ByteUtil.getBytes(arpPacket.Bytes, ARPFields.ProtocolAddressTypePosition, ARPFields.AddressTypeLength));
            ret[2] = arpPacket.HardwareAddressLength;
            ret[3] = arpPacket.ProtocolAddressLength;
            ret[4] = HexEncoder.ToString(ByteUtil.getBytes(arpPacket.Bytes, ARPFields.OperationPosition, ARPFields.OperationLength));
            ret[5] = HexEncoder.ToString(arpPacket.SenderHardwareAddress.GetAddressBytes());
            ret[6] = arpPacket.SenderProtocolAddress.ToString();
            ret[7] = HexEncoder.ToString(arpPacket.TargetHardwareAddress.GetAddressBytes());
            ret[8] = arpPacket.TargetProtocolAddress.ToString();

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
            if (fields.Length == 9)
            {
                if (!(fields[0] is string) || !(fields[1] is string) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is string) || !(fields[5] is string) ||
                !(fields[6] is string) || !(fields[7] is string) ||
                !(fields[8] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 2 integers, and 7 strings.");
                }
                if (!verifyHardwareType((string)fields[0]))
                {
                    throw new EditorInvalidField("Invalid Hardware Type. Expecting an instance of LinkLayers.");
                }
                if (!verifyProtocolType((string)fields[1]))
                {
                    throw new EditorInvalidField("Invalid Protocol Type. Expecting an instance of EthernetPacketType.");
                }
                if (!verifyHardwareLength((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid Hardware Length. Expecting an integer from 0 to 255.");
                }
                if (!verifyProtocolLength((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid Protocol Length. Expecting an integer from 0 to 255.");
                }
                if (!verifyOperation((string)fields[4]))
                {
                    throw new EditorInvalidField("Invalid ARP Operation. Expecting an integer from 0 to 65535.");
                }
                if (!verifySenderHardwareAddress((string)fields[5]))
                {
                    throw new EditorInvalidField("Invalid Sender Hardware Address. Expecting a MAC Address string of the form FF:FF:FF:FF:FF:FF.");
                }
                if (!verifySenderProtocolAddress((string)fields[6]))
                {
                    throw new EditorInvalidField("Invalid Sender Protocol Address. Expecting a nonempty string.");
                }
                if (!verifyTargetHardwareAddress((string)fields[7]))
                {
                    throw new EditorInvalidField("Invalid Target Hardware Address. Expecting a MAC Address string of the form FF:FF:FF:FF:FF:FF.");
                }
                if (!verifyTargetProtocolAddress((string)fields[8]))
                {
                    throw new EditorInvalidField("Invalid Target Protocol Address. Expecting a nonempty string.");
                }
                int discarded = 0;
                byte[] hwType = HexEncoder.GetBytes((string)fields[0], out discarded);
                byte[] protoType = HexEncoder.GetBytes((string)fields[1], out discarded);
                byte[] hwLen = HexEncoder.GetBytes((int)fields[2], 2, out discarded);
                byte[] protoLen = HexEncoder.GetBytes((int)fields[3], 2, out discarded);
                byte[] operation = HexEncoder.GetBytes((string)fields[4], out discarded);
                byte[] senderHwAddr = HexEncoder.GetBytes((string)fields[5], out discarded);
                byte[] senderProtoAddr = System.Net.IPAddress.Parse((string)fields[6]).GetAddressBytes();
                byte[] targetHwAddr = HexEncoder.GetBytes((string)fields[7], out discarded);
                byte[] targetProtoAddr = System.Net.IPAddress.Parse((string)fields[7]).GetAddressBytes();

                byte[] packetBytes = ByteUtil.combineBytes(hwType, protoType, hwLen, protoLen,
                    operation, senderHwAddr, senderProtoAddr, targetHwAddr, targetProtoAddr);

                if (packet == null)
                {
                    packet = new ARPPacket(packetBytes, 0);
                }
                else
                {
                    Packet parent = packet.ParentPacket;
                    packet = new ARPPacket(packetBytes, 0);
                    packet.ParentPacket = parent;
                }

                return (Packet)packet;

                //// easiest way to do this is create a new arp packet and then use the get/set methods
                //byte[] temp = new byte[42];
                //// destination address
                //temp[0] = 0x01;
                //temp[1] = 0x01;
                //temp[2] = 0x01;
                //temp[3] = 0x01;
                //temp[4] = 0x01;
                //temp[5] = 0x01;
                //// source address
                //temp[6] = 0x01;
                //temp[7] = 0x01;
                //temp[8] = 0x01;
                //temp[9] = 0x01;
                //temp[10] = 0x01;
                //temp[11] = 0x01;
                //// set ARP ethernet flag
                //temp[12] = 0x08;
                //temp[13] = 0x06;
                //// set hardware type to ethernet
                //temp[14] = 0x00;
                //temp[15] = 0x01;
                //// protocol type is IP
                //temp[16] = 0x08;
                //temp[17] = 0x00;
                //// hardware len is 6
                //temp[18] = 0x06;
                //// protocol len is 4 
                //temp[19] = 0x04;
                //// opcode is request (1)
                //temp[20] = 0x00;
                //temp[21] = 0x01;
                //// sender's mac address (again)
                //temp[22] = 0x01;
                //temp[23] = 0x01;
                //temp[24] = 0x01;
                //temp[25] = 0x01;
                //temp[26] = 0x01;
                //temp[27] = 0x01;
                //// sender's ip address
                //temp[28] = 0x01;
                //temp[29] = 0x01;
                //temp[30] = 0x01;
                //temp[31] = 0x01;
                //// target's mac address
                //temp[32] = 0x00;
                //temp[33] = 0x00;
                //temp[34] = 0x00;
                //temp[35] = 0x00;
                //temp[36] = 0x00;
                //temp[37] = 0x00;
                //// target's ip address
                //temp[38] = 0x02;
                //temp[39] = 0x02;
                //temp[40] = 0x02;
                //temp[41] = 0x02;

                //// use the packet factory to compile
                //// this will allow something possibly higher than an EthernetPacket to be constructed
                //Packet myPacket = PacketFactory.dataToPacket(
                //    LinkLayers_Fields.IEEE802,
                //    temp
                //);

                //if (myPacket is ARPPacket)
                //{
                //    ((ARPPacket)myPacket).ARPHwType = (int)fields[0];
                //    ((ARPPacket)myPacket).ARPProtocolType = (int)fields[1];
                //    ((ARPPacket)myPacket).ARPHwLength = (int)fields[2];
                //    ((ARPPacket)myPacket).ARPProtocolLength = (int)fields[3];
                //    ((ARPPacket)myPacket).ARPOperation = (int)fields[4];
                //    ((ARPPacket)myPacket).setARPSenderHwAddress(Tamir.IPLib.Util.IPUtil.MacToLong((string)fields[5]));
                //    ((ARPPacket)myPacket).ARPSenderProtoAddress = (string)fields[6];
                //    ((ARPPacket)myPacket).setARPTargetHwAddress(Tamir.IPLib.Util.IPUtil.MacToLong((string)fields[7]));
                //    ((ARPPacket)myPacket).ARPTargetProtoAddress = (string)fields[8];

                //    return myPacket;
                //}
                //else
                //{
                //    throw new EditorInvalidField("Unable to construct an ARP Packet with the data provided.");
                //}
            }
            else
            {
                throw new EditorInvalidField("Invalid field count to construct an ARP Packet.");
            }


        }

        #endregion required_methods


        /**
         * Convert an IP string to a long
         */

        /**
         * Field verification
         */
        /*
         * verify htype, 16 bit
         */
        public bool verifyHardwareType(string htype)
        {
            return (htype.Length == 4 && HexEncoder.InHexFormat(htype));
        }

        /*
        * verify ptype, 16 bit
        */
        public bool verifyProtocolType(string ptype)
        {
            return (ptype.Length == 4 && HexEncoder.InHexFormat(ptype));
        }

        /*
        * verify hlen, 8 bit
        */
        public bool verifyHardwareLength(int hlen)
        {
            if (hlen >= 0 && hlen < 256)
            {
                return true;
            }
            return false;
        }

        /*
        * verify plen, 8 bit
        */
        public bool verifyProtocolLength(int plen)
        {
            if (plen >= 0 && plen < 256)
            {
                return true;
            }
            return false;
        }

        /*
        * verify oper, 16 bit
        */
        public bool verifyOperation(string oper)
        {
            return (oper.Length == 4 && HexEncoder.InHexFormat(oper));
        }

        /*
         * Verify sha, FF:FF:FF:FF:FF:FF
         */
        public bool verifySenderHardwareAddress(string sha)
        {
            return (sha.Length == 12 && HexEncoder.InHexFormat(sha));
        }

        /*
         * Verify spa, 255.255.255.255
         */
        public bool verifySenderProtocolAddress(string spa)
        {
            try
            {
                System.Net.IPAddress.Parse(spa);
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        /*
         * Verify tha, FF:FF:FF:FF:FF:FF
         */
        public bool verifyTargetHardwareAddress(string tha)
        {
            return (tha.Length == 12 && HexEncoder.InHexFormat(tha));
        }

        /*
         * Verify tpa, 255.255.255.255
         */
        public bool verifyTargetProtocolAddress(string tpa)
        {
            try
            {
                System.Net.IPAddress.Parse(tpa);
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }
    }
}

