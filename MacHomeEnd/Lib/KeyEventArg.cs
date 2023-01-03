// Code By 『鳩でもわかるC#』 (https://lets-csharp.com/keyboard-hook/)

using System;

namespace MacHomeEnd.Lib
{
    public class KeyEventArg : EventArgs
    {
        public int KeyCode { get; }

        public KeyEventArg(int keyCode)
        {
            KeyCode = keyCode;
        }
    }
}
