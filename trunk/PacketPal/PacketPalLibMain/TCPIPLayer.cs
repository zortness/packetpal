using System;
using System.Collections.Generic;
using System.Text;

namespace Kopf.PacketPal.TCPIPLayers
{
    /**
     * Base class for all of the Network Layer representations from the TCP/IP
     * model. Also provides some static functions for comparisson between layers.
     */
    public abstract class TCPIPLayer
    {
        /*
         * Public Functions to Override
         */
        #region public_abstract_functions

        // ==
        public abstract bool equals(TCPIPLayer a);

        // >
        public abstract bool higherThan(TCPIPLayer a);

        // <
        public abstract bool lowerThan(TCPIPLayer a);

        // ?
        public abstract int compare(TCPIPLayer a);

        // integer representation
        public abstract int toInt();

        #endregion public_abstract_functions
    }
}
