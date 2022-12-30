using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput.Native;
using WindowsInput;

namespace MacHomeEnd
{
    class MyKeyboardHook : KeyboardHook
    {
        private InputSimulator inputSimulator = new InputSimulator();
        private List<short> pressingKeys = new List<short>();

        public void Subscribe() => Hook();
        public void UnSubscribe() => UnHook();

        public override IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0) return new IntPtr(0);

            // キーダウン
            if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                var keyCode = getKeyCode(lParam);
                //Console.WriteLine($"{keyCode}, onKeyDown");
                pressingKeys.Add(keyCode);

                if ((keyCode == 37 || keyCode == 39) && (pressingKeys.Contains(162) || pressingKeys.Contains(163)))
                {
                    switch (keyCode)
                    {
                        case 37:
                            Console.WriteLine("Home");
                            releaseCtrl();
                            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
                            return new IntPtr(1);
                        case 39:
                            Console.WriteLine("End");
                            releaseCtrl();
                            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
                            return new IntPtr(1);
                    }
                }
            }

            // キーアップ
            if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                //Console.WriteLine($"{getKeyCode(lParam)}, onKeyUp");
                pressingKeys.Remove(getKeyCode(lParam));
            }

            return new IntPtr(0);
        }

        private short getKeyCode(IntPtr lParam)
        {
            var kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            var vkCode = (short)kb.vkCode;
            return vkCode;
        }

        private void releaseCtrl()
        {
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            pressingKeys.Remove(162);
            pressingKeys.Remove(163);
        }
    }
}
