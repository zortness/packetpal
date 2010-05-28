using System;
using System.Collections.Generic;
using System.Text;
using PacketDotNet;
using Kopf.PacketPal.TCPIPLayers;

namespace Kopf.PacketPal.PacketEditors
{
    /**
     * Custom exceptions to throw within this namespace.
     */
    #region exceptions

    /*
     * General exception to throw when an invalid Packet object
     * is passed to a configurator.
     */
    public class EditorInvalidPacket : Exception
    {
        public EditorInvalidPacket(string msg) : base(msg) { }
    }

    /*
     * Exception when the build() function gets an invalid field.
     */
    public class EditorInvalidField : Exception
    {
        public EditorInvalidField(string msg) : base(msg) { }
    }

    #endregion exceptions



    /**
     * The Configurator class. This is the abstract class of the 
     * packet configuration implimentations. The child classes are
     * supposed to allow configuration of a new or existing Packet
     * object child from the SharpPcap library. 
     */
    public abstract class PacketEditor
    {
        /*
         * Return a name for this specific editor as a string
         * for use by the main program.
         */
        abstract public string getName();

        /*
         * Return the version of this editor as a string.
         */
        abstract public string getVersion();

        /*
         * Return the author of this editor as a string.
         */
        abstract public string getAuthor();

        /*
         * Return the web address of this editor as a string.
         */
        abstract public string getWebAddress();

        /*
         * Return a string for this specific editor to describe
         * how a packet will be edited. IE: Edit Packet As "<string>".
         */
        abstract public string getEditAs();

        /*
         * This should return true or false depending on weather
         * or not this configurator can handle the current type
         * of packet.
         */
        abstract public bool canHandle(Packet packet);

        /*
         * This returns the TCP/IP Layer representation of which
         * layer the editor functions on.
         */
        abstract public TCPIPLayer getLayer();

        /*
         * This should allow configuration of an existing Pcap
         * packet by bringing up an edit window of some sort.
         * Returns null when cancel is pressed.
         */
        abstract public Packet guiEdit(Packet packet);

        /*
         * This should allow configuration of a new packet by
         * bringing up an edit window similar to the one for
         * an existing packet.
         */
        abstract public Packet guiEdit();

        /*
         * Split the packet into object[] fields for use.
         */
        abstract public object[] explode(Packet packet);

        /*
         * Build a new Pcap Packet based on the parameters.
         */
        abstract public Packet compile(object[] fields);

        /*
         * Rebuild a Pcap Packet based on the parameters.
         */
        abstract public Packet compile(object[] fields, Packet packet);
    }
}
