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
     * Our class for editing EthernetPacket objects from Pcap.
     */
    public class EthernetEditor : PacketEditor
    {
        // layer of operation
        private TCPIPLayer myLayer;

        /*
         * Constructor needs to take no arguments.
         */
        public EthernetEditor()
        {
            myLayer = new Kopf.PacketPal.TCPIPLayers.LinkLayer();
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
            return "Ethernet Packet Editor";
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
            return "Ethernet Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            return (packet is EthernetPacket) ;
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
            if (!(packet is EthernetPacket))
            {
                throw new EditorInvalidPacket("Not a valid Ethernet Packet!");
            }
            object[] fields = explode(packet);

            EthernetEditorForm form = new EthernetEditorForm(this,
                (string)fields[0],
                (string)fields[1],
                (string)fields[2],
                (string)fields[3]
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                fields[0] = form.getDest();
                fields[1] = form.getSource();
                fields[2] = form.getProtocol();
                fields[3] = form.getPayload();
                packet = compile(fields, packet);

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
            Packet packet = new EthernetPacket(
                new System.Net.NetworkInformation.PhysicalAddress(new byte[6]),
                new System.Net.NetworkInformation.PhysicalAddress(new byte[6]),
                EthernetPacketType.None);
            return guiEdit(packet);
        }

        /*
         * break the packet into objects to be edited
         */
        public override object[] explode(Packet packet)
        {
            if (!(packet is EthernetPacket))
            {
                throw new EditorInvalidPacket("Not a valid Ethernet Packet!");
            }

            /*
             * Split into 5 hex strings:
             *  - destination
             *  - source
             *  - protocol
             *  - payload
             */
            byte[] type = ByteUtil.getBytes(((EthernetPacket)packet).Bytes, EthernetFields.TypePosition, EthernetFields.TypeLength);

            byte[] payload = packet.PayloadData;
            if (payload == null && packet.PayloadPacket != null)
            {
                payload = packet.PayloadPacket.Bytes;
            }

            object[] ret = new object[4];
            ret[0] = HexEncoder.ToString(((EthernetPacket)packet).DestinationHwAddress.GetAddressBytes());
            ret[1] = HexEncoder.ToString(((EthernetPacket)packet).SourceHwAddress.GetAddressBytes());
            ret[2] = HexEncoder.ToString(type);
            ret[3] = HexEncoder.ToString(payload);
            return ret;
        }

        /*
         * Create a new EthernetPacket based on provided fields.
         */
        public override Packet compile(object[] fields, Packet packet)
        {
            /*
             * Expecting 4 strings:
             *  - destination
             *  - source
             *  - protocol
             *  - payload
             */
            // make sure the array is properly constructed
            if (fields.Length != 4)
            {
                throw new EditorInvalidField("Invalid field count to construct an Ethernet Packet.");
            }
            if (!(fields[0] is string) || !(fields[1] is string) ||
                !(fields[2] is string) || !(fields[3] is string))
            {
                throw new EditorInvalidField("One or more invalid fields specified. Expecting four hexadecimal strings.");
            }
            if (!verifyMac((string)fields[0]) || !verifyMac((string)fields[1]))
            {
                throw new EditorInvalidField("Invalid MAC address. Expecting a hexadecimal string of format FFFFFFFFFFFF.");
            }
            if (!verifyPayload((string)fields[3]))
            {
                throw new EditorInvalidField("Invalid payload. Expecting a hexadecimal string of length 92 to 3000.");
            }

            int discarded = 0;
            byte[] destAddr = HexEncoder.GetBytes((string)fields[0], out discarded);
            byte[] srcAddr = HexEncoder.GetBytes((string)fields[1], out discarded);
            byte[] type = HexEncoder.GetBytes((string)fields[2], out discarded);
            byte[] payload = HexEncoder.GetBytes((string)fields[3], out discarded);

            byte[] packetBytes = ByteUtil.combineBytes(destAddr, srcAddr, type, payload);

            if (packet == null)
            {
                packet = new EthernetPacket(packetBytes, 0);
            }
            else
            {
                Packet parent = packet.ParentPacket;
                packet = new EthernetPacket(packetBytes, 0);
                packet.ParentPacket = parent;
            }

            return packet;
        }

        public override Packet compile(object[] fields)
        {
            return compile(fields, null);
        }

        #endregion required_methods

        /*
         * Verify a MAC address string.
         */
        public bool verifyMac(string mac)
        {
            return (mac.Length == 12 && HexEncoder.InHexFormat(mac));
        }

        /*
         * Verify PROTOCOL field string.
         */
        public bool verifyProtocol(string protocol)
        {
            return (protocol.Length == 4 && HexEncoder.InHexFormat(protocol));
        }

        /*
         * Verify PAYLOAD field string.
         */
        public bool verifyPayload(string payload)
        {
            return (payload.Length >= 92 && payload.Length <= 3000 && HexEncoder.InHexFormat(payload));
        }

        /*
         * Generate a random payload.
         */
        public string randomPayload()
        {
            // 1500 bytes
            Random myRand = new Random();
            //int length = myRand.Next(46, 1500);
            byte[] myBytes = new byte[1500];
            myRand.NextBytes(myBytes);
            return HexEncoder.ToString(myBytes);
        }
    }
}
