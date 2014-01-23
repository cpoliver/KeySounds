using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeySounds.KeyboardManagement
{
    public class KeyCapturer
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;

        private readonly IntPtr _hookId = IntPtr.Zero;
        private readonly LowLevelKeyboardProc _hookProc; 
        private readonly List<int> _keysDown = new List<int>();

        public bool IgnoreHeldKeyDownEvents { get; set; }
        public Action<int> KeyDownCallback { get; set; }
        public Action<int> KeyUpCallback { get; set; }

        public KeyCapturer(Action<int> keyDownCallback,
                           Action<int> keyUpCallback,
                           bool ignoreHeldKeyDownEvents = true)
        {
            _hookProc = HookCallback; // keep reference to prevent GC
            _hookId = SetHook(_hookProc);
            KeyDownCallback = keyDownCallback;
            KeyUpCallback = keyUpCallback;
            IgnoreHeldKeyDownEvents = ignoreHeldKeyDownEvents;
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var currentProcess = Process.GetCurrentProcess())
            using (var currentModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(currentModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var keyCode = Marshal.ReadInt32(lParam);

                if (wParam == (IntPtr) WM_KEYUP)
                {
                    _keysDown.RemoveAll(k => k == keyCode);
                    KeyUpCallback.Invoke(keyCode);
                }

                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    if (!IgnoreHeldKeyDownEvents || !_keysDown.Contains(keyCode))
                        KeyDownCallback.Invoke(keyCode);

                    _keysDown.Add(keyCode);
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }

        ~KeyCapturer()
        {
            Dispose();
        }

        #region DLL Imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion
    }
}
