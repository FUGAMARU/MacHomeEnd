using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace MacHomeEnd
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private MyKeyboardHook hook = new MyKeyboardHook();
        private NotifyIcon notifyIcon = new NotifyIcon();
        //private SettingControl settings = new SettingControl();

        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("===== OnStartup =====");
            base.OnStartup(e);

            notifyIcon.Icon = MacHomeEnd.Properties.Resources.icon_64x64;
            notifyIcon.Text = "MacHomeEnd";
            notifyIcon.ContextMenuStrip = SettingControl.SetContextMenu();
            notifyIcon.Visible = true;

            hook.Subscribe();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            hook.UnSubscribe();

            notifyIcon.Dispose();

            Console.WriteLine("===== OnExit =====");
            base.OnExit(e);
        }
    }
}
