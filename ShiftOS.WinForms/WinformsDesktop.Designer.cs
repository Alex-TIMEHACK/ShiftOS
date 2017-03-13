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

            renderer.Begin();
            //Draw desktop background color.
            toplevel.BackgroundColor = LoadedSkin.DesktopColor;
            //Draw the desktop background image.
            var img = GetImage("desktopbackground");
            if(img != null)
            {
                var bgtex = new Texture(renderer);
                bgtex.LoadRaw(img.Width, img.Height, ImageToBinary(img));
                toplevel.BackgroundImage = bgtex;
            }
            renderer.End();

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
            this.btnnotifications = new Gwen.Control.Button(toplevel);
            input.Initialize(this.toplevel);
            // 
            // desktopCanvas
            // 
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
            lbtime.AutoSizeToContents = true;
            this.lbtime.Text = "label1";
            lbtime.Show();
            // 
            // menuStrip1
            // 
            appButton = new Gwen.Control.ImagePanel(toplevel);
            appButton.Show();
            appButton.Clicked += (o, a) =>
            {
                apps.MenuVisible = !apps.MenuVisible;
            };
            // 
            // apps
            // 
            this.apps = new SimpleAppLauncher(toplevel);
            //this.apps.Name = "apps";
            //apps.MenuVisible = false;
            //apps.Show();
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
        private Gwen.Control.ImagePanel desktoppanel;
        private Gwen.Control.Label lbtime;
        private SimpleAppLauncher apps;
        private Gwen.Control.ImagePanel appButton;
        private Gwen.Control.Button btnnotifications;
        
    }


    public static class RenderHintConstants
    {
        public const string AL_STRIP = "al_strip";
        public const string AL_BUTTON = "al_button";
        public const string AL_ITEM = "al_item";
        public const string TERMINALBOX = "trm";
    }




}

