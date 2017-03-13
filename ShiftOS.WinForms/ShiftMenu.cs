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
        int maxX = 0;

        public ShiftMenu(Base parent = null) : base(parent)
        {
        }

        public void AddMenuItem(Base item)
        {
            item.SetPosition(0, currentY);
            currentY += item.Bounds.Height;

            maxX = System.Math.Max(maxX, item.Width);

            AddChild(item);
            menuItems.Add(item);
            
            SetSize(maxX, currentY);
        }
    }
}
