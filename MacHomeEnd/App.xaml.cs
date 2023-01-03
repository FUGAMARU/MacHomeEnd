using System;
using System.Windows;

namespace MacHomeEnd
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        MyKeyboardHook hook = new MyKeyboardHook();
        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();

        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("===== OnStartup =====");
            base.OnStartup(e);
                       
            notifyIcon.Icon = MacHomeEnd.Properties.Resources.icon_64x64;                               
            notifyIcon.Text = "MacHomeEnd";
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
