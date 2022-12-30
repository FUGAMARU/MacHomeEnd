using System;
using System.Runtime.InteropServices;

namespace MacHomeEnd
{
    class MyKeyboardHook : KeyboardHook
    {
        public void Subscribe() => Hook();
        public void UnSubscribe() => UnHook();

        public override IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Console.WriteLine($"{GetKeyCode(lParam)}, onKeyDown");
            }
            else if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP))
            {
                Console.WriteLine($"{GetKeyCode(lParam)}, onKeyUp");
            }

            return new IntPtr(0);
        }

        private int GetKeyCode(IntPtr lParam)
        {
            var kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            var vkCode = (int)kb.vkCode;
            return vkCode;
        }
    }
}
