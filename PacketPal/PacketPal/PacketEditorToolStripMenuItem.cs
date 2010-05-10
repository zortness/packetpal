using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Kopf.PacketPal.PacketEditors;

namespace Kopf.PacketPal
{
    /**
     * We need to extend the ToolStripMenuItem class to hold a reference to our
     * specific PacketEditor and the index of the selected packet.
     */

    public class PacketEditorToolStripMenuItem : ToolStripMenuItem
    {
        PacketEditor myEditor;
        int myIndex;

        public PacketEditorToolStripMenuItem(PacketEditor editor, int index, string message) : base(message)
        {
            myEditor = editor;
            myIndex = index;
        }

        public PacketEditor getEditor()
        {
            return myEditor;
        }

        public int getIndex()
        {
            return myIndex;
        }
    }
}
