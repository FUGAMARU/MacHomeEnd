using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MacHomeEnd
{
    class MyKeyboardHook : KeyboardHook
    {
        private List<int> pressingKeys = new List<int>();

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
                            return new IntPtr(1);
                        case 39:
                            Console.WriteLine("End");
                            releaseCtrl();
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

        private int getKeyCode(IntPtr lParam)
        {
            var kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            var vkCode = (int)kb.vkCode;
            return vkCode;
        }

        private void releaseCtrl()
        {
            pressingKeys.Remove(162);
            pressingKeys.Remove(163);
        }
    }
}
