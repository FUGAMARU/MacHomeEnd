using System;
using System.Windows.Forms;
using System.Configuration;

namespace MacHomeEnd
{
    class SettingControl
    {
        public static bool isEnableCmdKey = true;
        public static bool isEnableOptionKey = true;

        private static readonly ToolStripMenuItem isCmdChecked = new ToolStripMenuItem("command", null, (s, e) => toggleCmdSetting());
        private static readonly ToolStripMenuItem isOptionChecked = new ToolStripMenuItem("option", null, (s, e) => toggleOptionSetting());

        public static ContextMenuStrip SetContextMenu()
        {
            loadConfig();

            ContextMenuStrip menu = new ContextMenuStrip();

            var exit = new ToolStripMenuItem("Exit", null, (s, e) => System.Windows.Application.Current.Shutdown());

            menu.Items.AddRange(new ToolStripItem[] { isCmdChecked, isOptionChecked, new ToolStripSeparator(), exit });

            return menu;
        }

        private static void loadConfig()
        {
            isEnableCmdKey = Convert.ToBoolean(ConfigurationManager.AppSettings["command"]);
            isEnableOptionKey = Convert.ToBoolean(ConfigurationManager.AppSettings["option"]);

            isCmdChecked.Checked = isEnableCmdKey;
            isOptionChecked.Checked = isEnableOptionKey;
        }

        private static void saveConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["command"].Value = isEnableCmdKey.ToString();
            config.AppSettings.Settings["option"].Value = isEnableOptionKey.ToString();
            config.Save();
        }

        private static void toggleCmdSetting()
        {
            isEnableCmdKey = !isEnableCmdKey;
            isCmdChecked.Checked = isEnableCmdKey;
            saveConfig();
        }

        private static void toggleOptionSetting()
        {
            isEnableOptionKey = !isEnableOptionKey;
            isOptionChecked.Checked = isEnableOptionKey;
            saveConfig();
        }
    }
}
