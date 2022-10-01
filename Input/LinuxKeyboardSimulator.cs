using System;
using LinuxInput.Native;
using UnityEngine;

namespace LinuxInput
{
    public interface ILinuxKeyboardSimulator
    {
        ILinuxKeyboardSimulator KeyDown(VirtualLKeyCode keyCode);
        ILinuxKeyboardSimulator KeyPress(VirtualLKeyCode keyCode);
        ILinuxKeyboardSimulator KeyUp(VirtualLKeyCode keyCode);
        ILinuxKeyboardSimulator TextEntry(string text);
        
        ILinuxKeyboardSimulator Sleep(int millsecondsTimeout);
    }

    public class LinuxKeyboardSimulator : ILinuxKeyboardSimulator
    {
        private BashShell bashShell;
        private readonly ILinuxInputSimulator _inputSimulator;
        
        public LinuxKeyboardSimulator(ILinuxInputSimulator inputSimulator)
        {
            bashShell = new BashShell();
            _inputSimulator = inputSimulator;
        }
        public ILinuxKeyboardSimulator KeyDown(VirtualLKeyCode keyCode)
        {
            bashShell.RunBashCommand("xdotool keydown " + $"0x{keyCode:x}");
            return this;
        }
        public ILinuxKeyboardSimulator KeyPress(VirtualLKeyCode keyCode)
        {
            bashShell.RunBashCommand("xdotool key " + $"0x{keyCode:x}");
            return this;
        }
        public ILinuxKeyboardSimulator KeyUp(VirtualLKeyCode keyCode)
        {
            bashShell.RunBashCommand("xdotool keyup " + $"0x{keyCode:x}");
            return this;
        }
        public ILinuxKeyboardSimulator TextEntry(string text)
        {
            bashShell.RunBashCommand("xdotool type " + text);
            return this;
        }

        public ILinuxKeyboardSimulator Sleep(int millsecondsTimeout)
        {
            bashShell.RunBashCommand("xdotool sleep " + millsecondsTimeout);
            return this;
        }
    }
}