using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Gwen;
using static ShiftOS.Engine.SkinEngine;
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
        int endposf = 0;

        public ShiftMenu(Base parent = null) : base(parent)
        {
        }

        public void AddMenuItem(Base item, int endpos)
        {
            item.SetPosition(0, currentY);
            currentY += item.Bounds.Height;

            maxX = System.Math.Max(maxX, item.Width);

            AddChild(item);
            menuItems.Add(item);
            //all you had to do was follow the damn train, CJ!
            SetSize(maxX, currentY);
            endposf = endpos;
        }

        protected override void Render(Gwen.Skin.Base skin)
        {
            var r = skin.Renderer;
            r.Begin();
            r.DrawColor = this.BackgroundColor;
            r.DrawFilledRect(new System.Drawing.Rectangle(this.LocalPosToCanvas(new Point(0,endposf * -1)),this.Size)); //what is life
            r.End();
        }
    }
}
