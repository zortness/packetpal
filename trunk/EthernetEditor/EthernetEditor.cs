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
            return "Ethernet Packet";
        }

        /*
         * Can this editor handle the specified packet?
         */
        public override bool canHandle(Packet packet)
        {
            if (packet is EthernetPacket)
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
            if (!(packet is EthernetPacket))
            {
                throw new EditorInvalidPacket("Not a valid Ethernet Packet!");
            }

            //MessageBox.Show(HexEncoder.ToString(((EthernetPacket)packet).EthernetData));

            // Convert original data to the correct formats for the editing form,
            // in this case: hexadecimal strings, then send to the form.
            EthernetEditorForm form = new EthernetEditorForm(this,
                ((EthernetPacket)packet).DestinationHwAddress,
                ((EthernetPacket)packet).SourceHwAddress,
                padHex(System.Convert.ToString(
                    ((EthernetPacket)packet).EthernetProtocol, 16)
                    ,4),
                HexEncoder.ToString(((EthernetPacket)packet).EthernetData)
                //Tamir.IPLib.Util.Convert.BytesToHex(((EthernetPacket)packet).EthernetData)
            );

            // show the form, wait for it to close
            form.ShowDialog();
            // if SAVE was clicked
            if (form.DialogResult == DialogResult.OK)
            {
                // if the payload was altered, we need to recompile the packet from scratch
                if (form.reCompile)
                {
                    object[] fields = new object[4];
                    fields[0] = form.getDest();
                    fields[1] = form.getSource();
                    fields[2] = form.getProtocol();
                    fields[3] = form.getPayload();

                    // compile the hex strings into a new packet
                    packet = compile(fields);
                }
                else
                {
                    // the payload wasn't changed
                    // modify the easy fields
                    ((EthernetPacket)packet).DestinationHwAddress = form.getDest();
                    ((EthernetPacket)packet).SourceHwAddress = form.getSource();
                    ((EthernetPacket)packet).EthernetProtocol = int.Parse(form.getProtocol(),
                        System.Globalization.NumberStyles.HexNumber);
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
            // 64 bytes minimum, 4 for CRC
            byte[] temp = new byte[60];
            for (int x = 0; x < 60; x++)
            {
                temp[x] = 0xFF;
            }
            //Random myRand = new Random();
            //myRand.NextBytes(temp);
            // make new smallest possible ethernet packet
            //EthernetPacket packet = new EthernetPacket(temp.Length, temp);

            // compute CRC
            //CRCTool crccalc = new CRCTool();
            //ulong crc = crccalc.crcbitbybitfast(temp);
            /** convert ulong to bytes[] ?? **/

            Packet packet = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, temp);

            // just use the other function now that we have a valid packet
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

            object[] ret = new object[4];

            ret[0] = ((EthernetPacket)packet).DestinationHwAddress;
            ret[1] = ((EthernetPacket)packet).SourceHwAddress;
            ret[2] = padHex(System.Convert.ToString(
                ((EthernetPacket)packet).EthernetProtocol, 16),4);
            ret[3] = HexEncoder.ToString(((EthernetPacket)packet).EthernetData);
            //Tamir.IPLib.Util.Convert.BytesToHex(((EthernetPacket)packet).EthernetData);

            return ret;
        }

        /*
         * Create a new EthernetPacket based on provided fields.
         */
        public override Packet compile(object[] fields)
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
                throw new EditorInvalidField("Invalid MAC address. Expecting a hexadecimal string of format FF:FF:FF:FF:FF:FF.");
            }
            if (!verifyProtocol((string)fields[2]))
            {
                throw new EditorInvalidField("Invalid protocol type. Expecting a hexadecimal string of format FFFF.");
            }
            if (!verifyPayload((string)fields[3]))
            {
                throw new EditorInvalidField("Invalid payload. Expecting a hexadecimal string of length 92 to 3000.");
            }
            // use the packet factory to compile
            // this will allow something possibly higher than an EthernetPacket to be constructed
            int discarded;
            return PacketFactory.dataToPacket(
                LinkLayers_Fields.IEEE802,
                HexEncoder.GetBytes(
                    ((string)fields[0]).Replace(":","") +
                    ((string)fields[1]).Replace(":","") +
                    (string)fields[2] +
                    (string)fields[3],
                    out discarded
                )
            );
            /*
             * Tamir.IPLib.Util.Convert.GetBytes(
                    ((string)fields[0]).Replace(":","") +
                    ((string)fields[1]).Replace(":","") +
                    (string)fields[2] +
                    (string)fields[3]
                )
             *
             */
            
        }

        #endregion required_methods

        /*
         * Verify a MAC address string.
         */
        public bool verifyMac(string mac)
        {
            Regex test = new Regex("([0-9a-fA-F]{2}\\:){5}([0-9a-fA-F]{2})");
            return test.IsMatch(mac);
        }

        /*
         * Verify PROTOCOL field string.
         */
        public bool verifyProtocol(string protocol)
        {
            Regex test = new Regex("[0-9a-fA-F]{4}");
            return test.IsMatch(protocol);
        }

        /*
         * Verify PAYLOAD field string.
         */
        public bool verifyPayload(string payload)
        {
            Regex test1 = new Regex("[0-9a-fA-F]{92,3000}");
            if (test1.IsMatch(payload))
            {
                // make sure there's no characters that shouldn't be in the string
                Regex test2 = new Regex(".*[^0-9a-fA-F].*");
                return (!test2.IsMatch(payload));
            }
            return false;
        }

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


        /*
         * Generate a random payload.
         */
        public string randomPayload()
        {
            // 1500 bytes
            Random myRand = new Random();
            byte[] myBytes = new byte[1500];
            myRand.NextBytes(myBytes);
            return HexEncoder.ToString(myBytes);
            //return Tamir.IPLib.Util.Convert.BytesToHex(myBytes);
        }
    }
}
