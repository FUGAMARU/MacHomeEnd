using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;

namespace MacHomeEnd
{
    class MyKeyboardHook : KeyboardHook
    {
        private InputSimulator inputSimulator = new InputSimulator();

        private int currentParam = 0;
        private short pressedPhysicalKey = 0;

        const short LEFT_CTRL = (short)Keys.LControlKey;
        const short RIGHT_CTRL = (short)Keys.RControlKey;
        const short LEFT_ARROW = (short)Keys.Left;
        const short RIGHT_ARROW = (short)Keys.Right;

        public void Subscribe() => Hook();
        public void UnSubscribe() => UnHook();

        public override IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0) return new IntPtr(0);

            if (currentParam == 0) currentParam = (int)lParam;

            var keyCode = (short)((KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT))).vkCode;

            // キーダウン
            if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                // 物理的にCtrlキーが押された場合キーコードを更新
                if ((keyCode == LEFT_CTRL || keyCode == RIGHT_CTRL) && (currentParam == (int)lParam)) pressedPhysicalKey = keyCode;

                var isLeftCtrlDown = inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.LCONTROL);
                var isRightCtrlDown = inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.RCONTROL);

                if ((keyCode == LEFT_ARROW || keyCode == RIGHT_ARROW) && (isLeftCtrlDown || isRightCtrlDown))
                {
                    releaseCtrl();
                    inputSimulator.Keyboard.KeyPress(keyCode == LEFT_ARROW ? VirtualKeyCode.HOME : VirtualKeyCode.END);
                    pressCtrl();
                    return new IntPtr(1);
                }
            }

            return new IntPtr(0);
        }

        private void releaseCtrl()
        {
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RCONTROL);
        }

        private void pressCtrl()
        {
            if (pressedPhysicalKey == LEFT_CTRL) inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            if (pressedPhysicalKey == RIGHT_CTRL) inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RCONTROL);
        }
    }
}
