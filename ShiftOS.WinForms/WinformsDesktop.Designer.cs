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

using System;
using System.Drawing;
using Gwen;
using Gwen.Control;
using Gwen.Renderer;
using OpenTK;
using static Gwen.Control.Base;
using static ShiftOS.Engine.SkinEngine;
using OpenTK.Graphics.OpenGL;
using ShiftOS.WinForms.Tools;
using ShiftOS.Engine;

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
        /// Respond to resize events here.
        /// </summary>
        /// <param name="e">Contains information on the new GameWindow size.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 1);

            toplevel?.SetSize(Width, Height);
        }

        /// <summary>
        /// Add your game rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            toplevel.RenderCanvas();
            //Render the mouse.
            int x = Mouse.X;
            int y = Mouse.Y;
            int w = Properties.Resources.rylan_cursor_default.Width;
            int h = Properties.Resources.rylan_cursor_default.Height;
            renderer.Begin();
            
            var tex = new Texture(renderer);
            tex.LoadRaw(w, h, SkinEngine.ImageToBinary(Properties.Resources.rylan_cursor_default));
            renderer.DrawColor = Color.White;
            renderer.DrawTexturedRect(tex, new Rectangle(x, y, w, h));
            renderer.End();
            SwapBuffers();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            renderer = new Gwen.Renderer.OpenTK();
            input = new Gwen.Input.OpenTK(this);

            var skn = new ShiftOSSkin(this.renderer);
            this.toplevel = new Gwen.Control.Canvas(skn); this.desktoppanel = new Gwen.Control.ImagePanel(toplevel);
            this.lbtime = new Gwen.Control.Label(toplevel);
            this.menuStrip1 = new Gwen.Control.MenuStrip(toplevel);
            this.desktopbg = new Gwen.Control.ImagePanel(toplevel);
            this.btnnotifications = new Gwen.Control.Button(toplevel);
            input.Initialize(this.toplevel);
            // 
            // desktopCanvas
            // 
            this.desktoppanel.Dock = Pos.Top;
            this.desktoppanel.Name = "desktopCanvas";

            this.desktopbg.Dock = Pos.Fill;
            this.desktopbg.BringToFront();

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
            lbtime.AutoSizeToContents = true;
            this.lbtime.Text = "label1";
            lbtime.Show();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Show();
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new Padding(0,0,0,0);
            this.menuStrip1.RenderHint = RenderHintConstants.AL_STRIP;
            // 
            // apps
            // 
            this.apps = menuStrip1.AddItem("ShiftOS");
            this.apps.Name = "apps";
            this.apps.Padding = new Padding(0,0,0,0);
            this.apps.Width = 58;
            this.apps.Height = 20;
            this.apps.Text = "ShiftOS";
            apps.Show();
            // 
            // WinformsDesktop
            // 
            this.Title = "ShiftOS 1.2 Alpha 1.0";
            this.Load += new System.EventHandler<System.EventArgs>(this.Desktop_Load);
            

        }

        #endregion

        private Gwen.Input.OpenTK input;
        private Gwen.Renderer.Base renderer;
        private Gwen.Control.ImagePanel desktopbg;
        private Gwen.Control.Canvas toplevel;
        private Gwen.Control.ImagePanel desktoppanel;
        private Gwen.Control.Label lbtime;
        private Gwen.Control.MenuStrip menuStrip1;
        private Gwen.Control.MenuItem apps;
        private Gwen.Control.Button btnnotifications;
        
    }


    public static class RenderHintConstants
    {
        public const string AL_STRIP = "al_strip";
        public const string AL_BUTTON = "al_button";

    }

    public class ShiftOSSkin : Gwen.Skin.Base
    {
        public ShiftOSSkin(Gwen.Renderer.Base renderer) : base(renderer)
        {
            Colors.Button.Disabled = Color.Gray;
            Colors.Button.Normal = LoadedSkin.ControlTextColor;
            Colors.ModalBackground = LoadedSkin.ControlColor;
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

        public override void SetDefaultFont(string faceName, int size = 10)
        {
            this.DefaultFont = new Gwen.Font(this.Renderer, LoadedSkin.MainFont.Name, (int)LoadedSkin.MainFont.SizeInPoints);
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

        public override void DrawMenuStrip(Gwen.Control.Base control)
        {
            Renderer.Begin();
            if (control.RenderHint == RenderHintConstants.AL_STRIP)
            {
                Renderer.DrawColor = Color.Transparent;
                Renderer.DrawFilledRect(control.RenderBounds);
            }
            else
            {
                DrawGradient(control.RenderBounds, 0, LoadedSkin.Menu_MenuStripGradientBegin, LoadedSkin.Menu_MenuStripGradientEnd);
            }
            Renderer.End();
        }

        public void DrawGradient(Rectangle rect, int dir, Color a, Color b)
        {
            byte[] gradient = new byte[8];
            gradient[0] = a.A;
            gradient[1] = a.B;
            gradient[3] = a.G;
            gradient[4] = a.R;
            gradient[5] = b.A;
            gradient[6] = b.B;
            gradient[7] = b.G;
            gradient[8] = b.R;

            var tex = new Texture(Renderer);
            if (dir == 0)
            {
                tex.LoadRaw(1, 2, gradient);
            }
            else
            {
                tex.LoadRaw(2, 1, gradient);
            }
            Renderer.DrawTexturedRect(tex, rect);
        }

        public override void DrawButton(Gwen.Control.Base control, bool depressed, bool hovered, bool disabled)
        {
            Renderer.Begin();
            Renderer.DrawColor = LoadedSkin.ControlTextColor;
            Renderer.DrawFilledRect(control.RenderBounds);

            Renderer.DrawColor = LoadedSkin.ControlColor;
            if (hovered)
                Renderer.DrawColor = LoadedSkin.Menu_ButtonSelectedHighlight;
            if (depressed)
                Renderer.DrawColor = LoadedSkin.Menu_ButtonPressedHighlight;
            if (disabled)
                Renderer.DrawColor = LoadedSkin.ControlColor;

            Renderer.DrawFilledRect(new Rectangle(control.RenderBounds.X + 2, control.RenderBounds.Y + 2, control.RenderBounds.Width - 4, control.RenderBounds.Height - 4));

            //draw background texture
            if (control.BackgroundImage != null)
                Renderer.DrawTexturedRect(control.BackgroundImage, control.RenderBounds);
            
            Renderer.DrawColor = LoadedSkin.ControlTextColor;
            var font = ControlManager.CreateGwenFont(Renderer, LoadedSkin.MainFont);
            var textSize = Renderer.MeasureText(font, (control as Button).Text);
            var centerPoint = new Point(
                    (control.RenderBounds.Width - textSize.X) / 2,
                    (control.RenderBounds.Height - textSize.Y) / 2
                );
            Renderer.RenderText(font, centerPoint, (control as Button).Text);
             
        }

        
    }

    
}

