using System;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;

namespace MacHomeEnd.Class
{
    class SettingControl
    {
        public static bool IsEnableCmdKey = true;
        public static bool IsEnableOptionKey = true;

        private static readonly ToolStripMenuItem isCmdChecked = new ToolStripMenuItem("command", null, (s, e) => toggleCmdSetting());
        private static readonly ToolStripMenuItem isOptionChecked = new ToolStripMenuItem("option", null, (s, e) => toggleOptionSetting());

        private static readonly Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public static ContextMenuStrip SetContextMenu()
        {
            loadConfig();

            ContextMenuStrip menu = new ContextMenuStrip();

            var exit = new ToolStripMenuItem("Exit", null, (s, e) => System.Windows.Application.Current.Shutdown());

            menu.Items.AddRange(new ToolStripItem[] { isCmdChecked, isOptionChecked, new ToolStripSeparator(), exit });

            return menu;
        }

        private static void checkValidConfig()
        {
            var keys = config.AppSettings.Settings.AllKeys;

            if (!keys.Contains("command")) config.AppSettings.Settings.Add("command", "True");
            if (!keys.Contains("option")) config.AppSettings.Settings.Add("option", "True");

            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static void loadConfig()
        {
            checkValidConfig();

            IsEnableCmdKey = Convert.ToBoolean(ConfigurationManager.AppSettings["command"]);
            IsEnableOptionKey = Convert.ToBoolean(ConfigurationManager.AppSettings["option"]);

            isCmdChecked.Checked = IsEnableCmdKey;
            isOptionChecked.Checked = IsEnableOptionKey;
        }

        private static void saveConfig()
        {
            config.AppSettings.Settings["command"].Value = IsEnableCmdKey.ToString();
            config.AppSettings.Settings["option"].Value = IsEnableOptionKey.ToString();
            config.Save();
        }

        private static void toggleCmdSetting()
        {
            IsEnableCmdKey = !IsEnableCmdKey;
            isCmdChecked.Checked = IsEnableCmdKey;
            saveConfig();
        }

        private static void toggleOptionSetting()
        {
            IsEnableOptionKey = !IsEnableOptionKey;
            isOptionChecked.Checked = IsEnableOptionKey;
            saveConfig();
        }
    }
}
