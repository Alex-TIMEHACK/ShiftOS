/*
 * MIT License
 * 
 * Copyright (c) 2017 Michael VanOverbeek and ShiftOS devs
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Drawing;
using Gwen;
using Gwen.Control;
using Gwen.Renderer;
using static Gwen.Control.Base;
using static ShiftOS.Engine.SkinEngine;

namespace ShiftOS.WinForms
{
    partial class WinformsDesktop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            renderer = new Gwen.Renderer.OpenTK();
            input = new Gwen.Input.OpenTK(this);

            var skn = new ShiftOSSkin(this.renderer);
            this.desktoppanel = new Gwen.Control.Canvas(skn);
            this.btnnotifications = new Gwen.Control.Button(desktoppanel);
            this.lbtime = new Gwen.Control.Label(desktoppanel);
            this.panelbuttonholder = new Gwen.Control.Canvas(skn);
            this.sysmenuholder = new Gwen.Control.Canvas(skn);
            this.menuStrip1 = new Gwen.Control.MenuStrip(sysmenuholder);
            this.apps = new Gwen.Control.MenuItem(menuStrip1);
            this.pnlscreensaver = new Gwen.Control.Canvas(skn);
            this.pnlssicon = new Gwen.Control.Canvas(skn);
            // 
            // desktopCanvas
            // 
            this.desktoppanel.Dock = Pos.Top;
            this.desktoppanel.Name = "desktopCanvas";
            // 
            // btnnotifications
            // 
            this.btnnotifications.Name = "btnnotifications";
            this.btnnotifications.Text = "Notifications (0)";
            this.btnnotifications.Clicked += new GwenEventHandler<ClickedEventArgs>(this.btnnotifications_Click);
            // 
            // lbtime
            // 
            this.lbtime.Name = "lbtime";
            this.lbtime.Text = "label1";
            // 
            // Canvasbuttonholder
            // 
            this.panelbuttonholder.Name = "Canvasbuttonholder";
            // 
            // sysmenuholder
            // 
            this.sysmenuholder.Name = "sysmenuholder";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = Pos.Top;
            
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new Padding(0,0,0,0);
            // 
            // apps
            // 
            this.apps.Name = "apps";
            this.apps.Padding = new Padding(0,0,0,0);
            this.apps.Width = 58;
            this.apps.Height = 20;
            this.apps.Text = "ShiftOS";
            // 
            // WinformsDesktop
            // 
            this.Title = "ShiftOS 1.2 Alpha 1.0";
            this.Load += new System.EventHandler<System.EventArgs>(this.Desktop_Load);
            

        }

        #endregion

        private Gwen.Input.OpenTK input;
        private Gwen.Renderer.Base renderer;
        private Gwen.Control.Canvas toplevel;
        private Gwen.Control.Canvas desktoppanel;
        private Gwen.Control.Label lbtime;
        private Gwen.Control.Canvas sysmenuholder;
        private Gwen.Control.MenuStrip menuStrip1;
        private Gwen.Control.MenuItem apps;
        private Gwen.Control.Canvas panelbuttonholder;
        private Gwen.Control.Button btnnotifications;
        private Gwen.Control.Canvas pnlscreensaver;
        private Gwen.Control.Canvas pnlssicon;
    }

    public class ShiftOSSkin : Gwen.Skin.Base
    {
        public ShiftOSSkin(Gwen.Renderer.Base renderer) : base(renderer)
        {
        }

        public override void DrawArrowDown(Rectangle rect)
        {
            Renderer.DrawColor = LoadedSkin.Menu_TextColor;
            Renderer.Begin();
            int pad = 0;
            for (int y = rect.Y; y <= rect.Y + rect.Height; y++)
            {
                for (int x = rect.X + pad; x <= (rect.X + rect.Width) - pad; x++)
                {
                    Renderer.DrawPixel(x, y);
                }
                pad++;
            }
            Renderer.End();
        }

        public override void DrawArrowUp(Rectangle rect)
        {
            Renderer.DrawColor = LoadedSkin.Menu_TextColor;
            Renderer.Begin();
            int pad = 0;
            for (int y = rect.Y + rect.Height; y >= rect.Y; y--)
            {
                for (int x = rect.X + pad; x <= (rect.X + rect.Width) - pad; x++)
                {
                    Renderer.DrawPixel(x, y);
                }
                pad++;
            }
            Renderer.End();
        }

        public override void DrawArrowLeft(Rectangle rect)
        {
            Renderer.DrawColor = LoadedSkin.Menu_TextColor;
            Renderer.Begin();
            int pad = 0;
            for (int x = rect.X + rect.Width; x >= rect.X; x--)
            {
                for (int y = rect.Y + pad; y <= (rect.Y + rect.Height) - pad; y++)
                {
                    Renderer.DrawPixel(x, y);
                }
                pad++;
            }
            Renderer.End();
        }

        public override void DrawArrowRight(Rectangle rect)
        {
            Renderer.DrawColor = LoadedSkin.Menu_TextColor;
            Renderer.Begin();
            int pad = 0;
            for (int x = rect.X; x <= rect.X + rect.Width; x++)
            {
                for (int y = rect.Y + pad; y <= (rect.Y + rect.Height) - pad; y++)
                {
                    Renderer.DrawPixel(x, y);
                }
                pad++;
            }
            Renderer.End();
        }

        public override void DrawButton(Gwen.Control.Base control, bool depressed, bool hovered, bool disabled)
        {
            Renderer.Begin();
            Renderer.DrawColor = LoadedSkin.ControlTextColor;
            Renderer.DrawFilledRect(new Rectangle(control.X, control.Y, control.Width, control.Height));

            Renderer.DrawColor = LoadedSkin.ControlColor;
            if (hovered)
                Renderer.DrawColor = LoadedSkin.Menu_ButtonSelectedHighlight;
            if (depressed)
                Renderer.DrawColor = LoadedSkin.Menu_ButtonPressedHighlight;
            if (disabled)
                Renderer.DrawColor = LoadedSkin.ControlColor;

            Renderer.DrawFilledRect(new Rectangle(control.X + 2, control.Y + 2, control.Width - 2, control.Height - 2));

            //draw background texture
            if (control.BackgroundImage != null)
                Renderer.DrawTexturedRect(control.BackgroundImage, new Rectangle(control.X, control.Y, control.Width, control.Height));
            
            Renderer.DrawColor = LoadedSkin.ControlTextColor;
            var font = new Gwen.Font(Renderer, LoadedSkin.MainFont.Name, (int)LoadedSkin.MainFont.Size);
            var textSize = Renderer.MeasureText(font, (control as Button).Text);
            var centerPoint = new Point(
                    (control.Width - textSize.X) / 2,
                    (control.Height - textSize.Y) / 2
                );
            Renderer.RenderText(font, centerPoint, (control as Button).Text);
             
        }
    }
}

