using System;
using System.Windows;

namespace FastMove
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        KeyboardHook hook = new KeyboardHook();

        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("===== OnStartup =====");
            base.OnStartup(e);

            hook.KeyDownEvent += hook_KeyDownEvent;
            hook.KeyUpEvent += hook_KeyUpEvent;
            hook.Hook();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            hook.UnHook();

            Console.WriteLine("===== OnExit =====");
            base.OnExit(e);
        }

        private void hook_KeyDownEvent(object sender, KeyEventArg e)
        {
            Console.WriteLine($"{e.KeyCode} down");
        }

        private void hook_KeyUpEvent(object sender, KeyEventArg e)
        {
            Console.WriteLine($"{e.KeyCode} up");
        }
    }
}
