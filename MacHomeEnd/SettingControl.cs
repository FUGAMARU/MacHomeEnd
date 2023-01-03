using System;
using System.Windows.Forms;

namespace MacHomeEnd
{
    class SettingControl
    {
        public static ContextMenuStrip SetContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            var isEnableCmdKey = new ToolStripMenuItem("command", null, (s, e) => Console.WriteLine("commandを有効化する"));
            isEnableCmdKey.Checked = true;

            var isEnableOptionKey = new ToolStripMenuItem("option", null, (s, e) => Console.WriteLine("optionを有効化する"));
            isEnableOptionKey.Checked = true;

            var exit = new ToolStripMenuItem("Exit", null, (s, e) => System.Windows.Application.Current.Shutdown());

            menu.Items.AddRange(new ToolStripItem[] { isEnableCmdKey, isEnableOptionKey, new ToolStripSeparator(), exit });

            return menu;
        }
    }
}
