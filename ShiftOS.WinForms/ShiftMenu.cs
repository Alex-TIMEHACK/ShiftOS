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
using Gwen.Skin;

namespace ShiftOS.WinForms
{
    class ShiftMenu : Gwen.Control.Base
    {
        IList<Gwen.Control.Base> menuItems = new List<Gwen.Control.Base>();
        int currentY = 0;
        int maxX = 0;
        int endposf = 0;

        public ShiftMenu(Gwen.Control.Base parent = null) : base(parent)
        {
        }

        public void AddMenuItem(Gwen.Control.Base item, int endpos)
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

        protected override void PostLayout(Gwen.Skin.Base skin)
        {
            foreach(var ch in this.Children)
            {
                ch.Width = this.Width;
            }
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
