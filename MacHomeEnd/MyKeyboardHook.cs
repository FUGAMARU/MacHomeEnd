using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using MacHomeEnd.lib;

namespace MacHomeEnd
{
    class MyKeyboardHook : KeyboardHook
    {
        private InputSimulator inputSimulator = new InputSimulator();

        private bool isAltMode = false;
        private int currentParam = 0;
        private short pressedPhysicalCtrlKey = 0;
        private short pressedPhysicalAltKey = 0;

        const short LEFT_CTRL = (short)Keys.LControlKey;
        const short RIGHT_CTRL = (short)Keys.RControlKey;
        const short LEFT_ARROW = (short)Keys.Left;
        const short RIGHT_ARROW = (short)Keys.Right;
        const short LEFT_ALT = (short)Keys.LMenu;
        const short RIGHT_ALT = (short)Keys.RMenu;

        public override IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0 || isAltMode) return new IntPtr(0);

            if (currentParam == 0) currentParam = (int)lParam;

            var keyCode = (short)((KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT))).vkCode;

            // キーダウン
            if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                // 物理キーが押された場合キーコードを更新
                if ((keyCode == LEFT_CTRL || keyCode == RIGHT_CTRL) && (currentParam == (int)lParam)) pressedPhysicalCtrlKey = keyCode;
                if ((keyCode == LEFT_ALT || keyCode == RIGHT_ALT) && (currentParam == (int)lParam)) pressedPhysicalAltKey = keyCode;                            

                if (keyCode == LEFT_ARROW || keyCode == RIGHT_ARROW)
                {
                    // Commandキーと矢印キーのキーコンビネーション
                    var isLeftCtrlDown = inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.LCONTROL);
                    var isRightCtrlDown = inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.RCONTROL);

                    if ((SettingControl.IsEnableCmdKey) && (isLeftCtrlDown || isRightCtrlDown))
                    {
                        cmd(keyCode);
                        return new IntPtr(1);
                    }

                    // Optionキーと矢印キーのキーコンビネーション
                    var isLeftAltDown = inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.LMENU);
                    var isRightAltDown = inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.RMENU);

                    if ((SettingControl.IsEnableOptionKey) && (isLeftAltDown || isRightAltDown))
                    {
                        option(keyCode);
                        return new IntPtr(1);
                    }
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
            if (pressedPhysicalCtrlKey == LEFT_CTRL) inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            if (pressedPhysicalCtrlKey == RIGHT_CTRL) inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RCONTROL);
        }

        private void pressAlt()
        {
            if (pressedPhysicalAltKey == LEFT_ALT) inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LMENU);
            if (pressedPhysicalAltKey == RIGHT_ALT) inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RMENU);
        }
        private void cmd(short keyCode)
        {
            releaseCtrl();
            inputSimulator.Keyboard.KeyPress(keyCode == LEFT_ARROW ? VirtualKeyCode.HOME : VirtualKeyCode.END);
            pressCtrl();
        }

        private void option(short keyCode)
        {
            isAltMode = true;

            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyPress(keyCode == LEFT_ARROW ? VirtualKeyCode.LEFT : VirtualKeyCode.RIGHT);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

            pressAlt();

            isAltMode = false;
        }
    }
}
