﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIRCUIT.Utilities
{
    public interface IWindowService
    {
        void Maximize();
        void Minimize();
    }

    class WindowControlService : IWindowService
    {
        private readonly Window _windowService;

        public WindowControlService(Window window)
        {
            _windowService = window;
        }

        public void Maximize()
        {
            if (this._windowService.WindowState == WindowState.Normal)
                _windowService.WindowState = WindowState.Maximized;
            else
                _windowService.WindowState = WindowState.Normal;

        }

        public void Minimize()
        {
            _windowService.WindowState = WindowState.Minimized;
        }
    }
}