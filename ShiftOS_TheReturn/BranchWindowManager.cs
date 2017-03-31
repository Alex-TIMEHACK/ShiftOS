using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftOS.Engine;

namespace ShiftOS.Engine
{
    /// <summary>
    /// Used for branching operations between multiple window managers. Useful when porting a front-end to the compositor system while keeping non-compositing apps compatible.
    /// </summary>
    public class BranchWindowManager : WindowManager
    {
        private readonly Dictionary<Type, WindowManager> WindowManagerList;
        private readonly Dictionary<Type, WindowManager> BorderManagerList;


        /// <summary>
        /// Creates a new instance of the <see cref="BranchWindowManager"/> class. 
        /// </summary>
        /// <param name="windowmanagers">A list of window managers used to handle <see cref="IShiftOSWindow"/> operations - keys of which are the derivatives of <see cref="IShiftOSWindow"/> supported, and their values are the window managers supporting said type.</param>
        /// <param name="bordermanagers">A list of window managers used to handle <see cref="IWindowBorder"/> operations - keys of which are the derivatives of <see cref="IWindowBorder"/> supported, and their values are the window managers supporting said type.</param>
        public BranchWindowManager(Dictionary<Type, WindowManager> windowmanagers, Dictionary<Type, WindowManager> bordermanagers)
        {
            WindowManagerList = windowmanagers;
            BorderManagerList = bordermanagers;
        }

        public override void Close(IShiftOSWindow win)
        {
            SelectManager(win).Close(win);
        }

        public override void InvokeAction(Action act)
        {
            act?.Invoke();
        }

        public override void Maximize(IWindowBorder border)
        {
            SelectManager(border).Maximize(border);
        }

        public override void Minimize(IWindowBorder border)
        {
            SelectManager(border).Minimize(border);
        }

        public override void SetTitle(IShiftOSWindow win, string title)
        {
            SelectManager(win).SetTitle(win, title);
        }

        public override void SetupDialog(IShiftOSWindow win, bool decorated)
        {
            SelectManager(win).SetupDialog(win, decorated);
        }

        public override void SetupWindow(IShiftOSWindow win, bool decorated)
        {
            SelectManager(win).SetupWindow(win, decorated);

        }

        internal WindowManager SelectManager(IShiftOSWindow win)
        {
            var type = win.GetType();
            if (WindowManagerList.ContainsKey(type))
                return WindowManagerList[type];
            if (WindowManagerList.ContainsKey(type.BaseType))
                return WindowManagerList[type.BaseType];
            throw new NotSupportedException("This branch manager could not find a supported window manager for IShiftOSWindow derivative \"" + type.Name + "\".");
        }

        internal WindowManager SelectManager(IWindowBorder border)
        {
            var type = border.GetType();
            if (BorderManagerList.ContainsKey(type))
                return BorderManagerList[type];
            if (BorderManagerList.ContainsKey(type.BaseType))
                return BorderManagerList[type.BaseType];

            throw new NotSupportedException("This branch manager could not find a supported window manager for IWindowBorder derivative \"" + type.Name + "\".");
        }

    }
}
