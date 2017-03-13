using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwen.Control;
using System.Drawing;
using ShiftOS.WinForms.Tools;

namespace ShiftOS.WinForms
{
    class ShiftMenu : Gwen.Control.Base
    {
        IList<Base> menuItems = new List<Base>();
        int currentY = 0;

        public ShiftMenu(Base parent = null) : base(parent)
        {
        }

        public void AddMenuItem(Base item)
        {
            item.SetPosition(0, currentY);
            currentY += item.Bounds.Height;

            AddChild(item);
            menuItems.Add(item);
            
            SetSize(100, currentY);
        }
    }
}
