using System.Windows.Forms;

namespace MacHomeEnd
{
    class SettingControl
    {
        public static ContextMenuStrip SetContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem menuItem = new ToolStripMenuItem();

            var exit = new ToolStripMenuItem("Exit", null, (s1, e1) =>
            {
                System.Windows.Application.Current.Shutdown();
            });

            menu.Items.AddRange(new ToolStripMenuItem[]{
                exit
            });

            return menu;
        }
    }
}
