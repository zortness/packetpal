using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Kopf.PacketPal.PacketEditors;

namespace Kopf.PacketPal
{
    /**
     * This is a wrapper class so we can call a specific PacketEditor with
     * the use of a ToolStripMenuItem through a ContextMenu.
     */
    class PacketEditorToolStripMenuItem: ToolStripMenuItem
    {
        private PacketEditor myEditor;
        private int myIndex;
        

        public PacketEditorToolStripMenuItem(PacketEditor editor, int index, string text): base (text)
        {
            myIndex = index;
            myEditor = editor;
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
