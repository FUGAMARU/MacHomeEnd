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

        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("===== OnStartup =====");
            base.OnStartup(e);

            hook.Subscribe();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            hook.UnSubscribe();

            Console.WriteLine("===== OnExit =====");
            base.OnExit(e);
        }
    }
}
