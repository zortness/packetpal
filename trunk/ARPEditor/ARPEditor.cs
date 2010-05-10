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
            return "ARP Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            if (packet is ARPPacket)
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
            if (!(packet is ARPPacket))
            {
                throw new EditorInvalidPacket("Not a valid ARP Packet!");
            }

            ARPEditorForm form = new ARPEditorForm(this,
                ((ARPPacket)packet).ARPHwType,
                ((ARPPacket)packet).ARPProtocolType,
                ((ARPPacket)packet).ARPHwLength,
                ((ARPPacket)packet).ARPProtocolLength,
                ((ARPPacket)packet).ARPOperation,
                ((ARPPacket)packet).ARPSenderHwAddress,
                ((ARPPacket)packet).ARPSenderProtoAddress,
                ((ARPPacket)packet).ARPTargetHwAddress,
                ((ARPPacket)packet).ARPTargetProtoAddress
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                ((ARPPacket)packet).ARPHwType = form.getHardwareType();
                ((ARPPacket)packet).ARPProtocolType = form.getProtocolType();
                ((ARPPacket)packet).ARPHwLength = form.getHardwareLength();
                ((ARPPacket)packet).ARPProtocolLength = form.getProtocolLength();
                ((ARPPacket)packet).ARPOperation = form.getOperation();
                ((ARPPacket)packet).setARPSenderHwAddress(Tamir.IPLib.Util.IPUtil.MacToLong(form.getSenderHardwareAddress()));
                ((ARPPacket)packet).ARPSenderProtoAddress = form.getSenderProtocolAddress();
                ((ARPPacket)packet).setARPTargetHwAddress(Tamir.IPLib.Util.IPUtil.MacToLong(form.getTargetHardwareAddress()));
                ((ARPPacket)packet).ARPTargetProtoAddress = form.getTargetProtocolAddress();

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

            Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

            // just use the other function now that we have a valid packet
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

            object[] ret = new object[9];
            ret[0] = ((ARPPacket)packet).ARPHwType;
            ret[1] = ((ARPPacket)packet).ARPProtocolType;
            ret[2] = ((ARPPacket)packet).ARPHwLength;
            ret[3] = ((ARPPacket)packet).ARPProtocolLength;
            ret[4] = ((ARPPacket)packet).ARPOperation;
            ret[5] = ((ARPPacket)packet).ARPSenderHwAddress;
            ret[6] = ((ARPPacket)packet).ARPSenderProtoAddress;
            ret[7] = ((ARPPacket)packet).ARPTargetHwAddress;
            ret[8] = ((ARPPacket)packet).ARPTargetProtoAddress;

            return ret;
        }

        /*
         * Create a new EthernetPacket based on provided fields.
         */
        public override Packet compile(object[] fields)
        {
            // make sure the array is properly constructed
            if (fields.Length == 9)
            {
                if (!(fields[0] is int) || !(fields[1] is int) ||
                !(fields[2] is int) || !(fields[3] is int) ||
                !(fields[4] is int) || !(fields[5] is string) ||
                !(fields[6] is string) || !(fields[7] is string) ||
                !(fields[8] is string))
                {
                    throw new EditorInvalidField("One or more invalid fields specified. Expecting 5 integers, and 4 strings.");
                }

                if (!verifyHardwareType((int)fields[0]))
                {
                    throw new EditorInvalidField("Invalid Hardware Type. Expecting an integer from 0 to 65535.");
                }
                if (!verifyProtocolType((int)fields[1]))
                {
                    throw new EditorInvalidField("Invalid Protocol Type. Expecting an integer from 0 to 65535.");
                }
                if (!verifyHardwareLength((int)fields[2]))
                {
                    throw new EditorInvalidField("Invalid Hardware Length. Expecting an integer from 0 to 255.");
                }
                if (!verifyProtocolLength((int)fields[3]))
                {
                    throw new EditorInvalidField("Invalid Protocol Length. Expecting an integer from 0 to 255.");
                }
                if (!verifyOperation((int)fields[4]))
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

                // easiest way to do this is create a new arp packet and then use the get/set methods
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

                // use the packet factory to compile
                // this will allow something possibly higher than an EthernetPacket to be constructed
                Packet myPacket = PacketFactory.dataToPacket(
                    LinkLayers_Fields.IEEE802,
                    temp
                );

                if (myPacket is ARPPacket)
                {
                    ((ARPPacket)myPacket).ARPHwType = (int)fields[0];
                    ((ARPPacket)myPacket).ARPProtocolType = (int)fields[1];
                    ((ARPPacket)myPacket).ARPHwLength = (int)fields[2];
                    ((ARPPacket)myPacket).ARPProtocolLength = (int)fields[3];
                    ((ARPPacket)myPacket).ARPOperation = (int)fields[4];
                    ((ARPPacket)myPacket).setARPSenderHwAddress(Tamir.IPLib.Util.IPUtil.MacToLong((string)fields[5]));
                    ((ARPPacket)myPacket).ARPSenderProtoAddress = (string)fields[6];
                    ((ARPPacket)myPacket).setARPTargetHwAddress(Tamir.IPLib.Util.IPUtil.MacToLong((string)fields[7]));
                    ((ARPPacket)myPacket).ARPTargetProtoAddress = (string)fields[8];

                    return myPacket;
                }
                else
                {
                    throw new EditorInvalidField("Unable to construct an ARP Packet with the data provided.");
                }
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
        public bool verifyHardwareType(int htype)
        {
            if (htype >= 0 && htype < 65536)
            {
                return true;
            }
            return false;
        }

        /*
        * verify ptype, 16 bit
        */
        public bool verifyProtocolType(int ptype)
        {
            if (ptype >= 0 && ptype < 65536)
            {
                return true;
            }
            return false;
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
        public bool verifyOperation(int oper)
        {
            if (oper >= 0 && oper < 65536)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify sha, FF:FF:FF:FF:FF:FF
         */
        public bool verifySenderHardwareAddress(string sha)
        {
            // we know this is on top of ethernet, so this should be a mac address
            Regex test1 = new Regex("([0-9a-fA-F]{2}\\:){5}([0-9a-fA-F]{2})");
            Match m = test1.Match(sha);
            if (m.Success && m.Length == sha.Length)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify spa, 255.255.255.255
         */
        public bool verifySenderProtocolAddress(string spa)
        {
            // this is MOST LIKELY an IP address, but we can only assume
            if (spa.Length > 0)
            {
                return true;
            }
            return false;
            /*
            Regex test1 = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
            Match m = test1.Match(spa);
            if (m.Success && m.Length == spa.Length)
            {
                return true;
            }
            return false;
            */
        }

        /*
         * Verify tha, FF:FF:FF:FF:FF:FF
         */
        public bool verifyTargetHardwareAddress(string tha)
        {
            // we know this is on top of ethernet, so this should be a mac address
            Regex test1 = new Regex("([0-9a-fA-F]{2}\\:){5}([0-9a-fA-F]{2})");
            Match m = test1.Match(tha);
            if (m.Success && m.Length == tha.Length)
            {
                return true;
            }
            return false;
        }

        /*
         * Verify tpa, 255.255.255.255
         */
        public bool verifyTargetProtocolAddress(string tpa)
        {
            // this is MOST LIKELY an IP address, but we can only assume
            if (tpa.Length > 0)
            {
                return true;
            }
            return false;
            /*
            Regex test1 = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
            Match m = test1.Match(tpa);
            if (m.Success && m.Length == tpa.Length)
            {
                return true;
            }
            return false;
            */
        }

    }
}

