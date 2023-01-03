using System.Windows;
using System.Windows.Forms;
using MacHomeEnd.Class;
using Application = System.Windows.Application;

namespace MacHomeEnd
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private MyKeyboardHook keyboardHook = new MyKeyboardHook();
        private NotifyIcon notifyIcon = new NotifyIcon();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            notifyIcon.Icon = MacHomeEnd.Properties.Resources.icon_64x64;
            notifyIcon.Text = "MacHomeEnd";
            notifyIcon.ContextMenuStrip = SettingControl.SetContextMenu();
            notifyIcon.Visible = true;

            keyboardHook.Hook();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            keyboardHook.UnHook();

            notifyIcon.Dispose();
            
            base.OnExit(e);
        }
    }
}
