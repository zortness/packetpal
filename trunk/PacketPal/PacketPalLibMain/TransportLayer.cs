using System;
using System.Collections.Generic;
using System.Text;

namespace Kopf.PacketPal.TCPIPLayers
{
    /**
     * Representation of the Transport Layer of the TCP/IP model.
     */
    public class TransportLayer : TCPIPLayer
    {
        /*
         * Private Fields
         */
        #region fields

        private int layer = 4;

        private string name = "Transport Layer";

        #endregion fields


        /*
         * Override Functions
         */
        #region overrides

        // string representation
        public override string ToString()
        {
            return name;
        }

        // ==
        public override bool equals(TCPIPLayer a)
        {
            if (layer == a.toInt())
                return true;
            return false;
        }

        // >
        public override bool higherThan(TCPIPLayer a)
        {
            if (layer > a.toInt())
                return true;
            return false;
        }

        // <
        public override bool lowerThan(TCPIPLayer a)
        {
            if (layer < a.toInt())
                return true;
            return false;
        }

        // ?
        public override int compare(TCPIPLayer a)
        {
            if (layer == a.toInt())
                return 0;
            else if (layer > a.toInt())
                return 1;
            else
                return -1;
        }

        // integer representation
        public override int toInt()
        {
            return layer;
        }

        #endregion overrides

    }
}
