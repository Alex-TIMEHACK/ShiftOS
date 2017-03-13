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

using ShiftOS.Engine;
using ShiftOS.WinForms.Tools;
using Gwen.Control;
using Gwen.Control.Layout;
using Gwen;
using static ShiftOS.WinForms.Tools.ControlManager;

namespace ShiftOS.WinForms
{
    partial class WindowBorder
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnltitle = new ImagePanel(this);
            this.pnlicon = new ImagePanel(this);
            this.pnlminimize = new ImagePanel(this);
            this.pnlmaximize = new ImagePanel(this);
            this.pnlclose = new ImagePanel(this);
            this.pnltitleleft = new ImagePanel(this);
            this.pnltitleright = new ImagePanel(this);
            this.lbtitletext = new Label(this);
            this.pnlbottom = new ImagePanel(this);
            this.pnlbottomr = new ImagePanel(this);
            this.pnlbottoml = new ImagePanel(this);
            this.pnlleft = new ImagePanel(this);
            this.pnlright = new ImagePanel(this);
            this.pnlcontents = new Positioner(this);
            // 
            // pnltitle
            // 
            this.pnltitle.BackgroundColor = System.Drawing.Color.Black;
            this.pnltitle.Dock = Pos.Top;
            this.pnltitle.Location = new System.Drawing.Point(0, 0);
            this.pnltitle.Name = "pnltitle";
            this.pnltitle.Size = new System.Drawing.Size(730, 30);
            // 
            // pnlicon
            // 
            this.pnlicon.Location = new System.Drawing.Point(9, -76);
            this.pnlicon.Name = "pnlicon";
            this.pnlicon.Size = new System.Drawing.Size(200, 100);
            // 
            // pnlminimize
            // 
            this.pnlminimize.BackgroundColor = System.Drawing.Color.Green;
            this.pnlminimize.Location = new System.Drawing.Point(649, 3);
            this.pnlminimize.Name = "pnlminimize";
            this.pnlminimize.Size = new System.Drawing.Size(24, 24);
            this.pnlminimize.Clicked += this.pnlminimize_Click;
            // 
            // pnlmaximize
            // 
            this.pnlmaximize.BackgroundColor = System.Drawing.Color.Yellow;
            this.pnlmaximize.Location = new System.Drawing.Point(676, 3);
            this.pnlmaximize.Name = "pnlmaximize";
            this.pnlmaximize.Size = new System.Drawing.Size(24, 24);
            this.pnlmaximize.Clicked += this.pnlmaximize_Click;
            // 
            // pnlclose
            // 
           this.pnlclose.BackgroundColor = System.Drawing.Color.Red;
            this.pnlclose.Location = new System.Drawing.Point(703, 3);
            this.pnlclose.Name = "pnlclose";
            this.pnlclose.Size = new System.Drawing.Size(24, 24);
            this.pnlclose.Clicked += this.pnlclose_Click;
            // 
            // pnltitleleft
            // 
            this.pnltitleleft.Dock = Pos.Left;
            this.pnltitleleft.Location = new System.Drawing.Point(0, 0);
            this.pnltitleleft.Name = "pnltitleleft";
            this.pnltitleleft.Size = new System.Drawing.Size(2, 30);
            // 
            // pnltitleright
            // 
            this.pnltitleright.Dock = Pos.Right;
            this.pnltitleright.Location = new System.Drawing.Point(728, 0);
            this.pnltitleright.Name = "pnltitleright";
            this.pnltitleright.Size = new System.Drawing.Size(2, 30);
            
            // 
            // lbtitletext
            // 
            this.lbtitletext.AutoSizeToContents = true;
            this.lbtitletext.Font = CreateGwenFont(Skin.Renderer, new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold));
            this.lbtitletext.TextColor = System.Drawing.Color.White;
            this.lbtitletext.Location = new System.Drawing.Point(75, 9);
            this.lbtitletext.Name = "lbtitletext";
            this.lbtitletext.Size = new System.Drawing.Size(77, 14);
            this.lbtitletext.Text = "Title text";
            this.lbtitletext.Clicked += this.lbtitletext_Click;
            // 
            // pnlbottom
            // 
            this.pnlbottom.BackgroundColor = System.Drawing.Color.Black;
            this.pnlbottom.Dock = Pos.Bottom;
            this.pnlbottom.Location = new System.Drawing.Point(0, 491);
            this.pnlbottom.Name = "pnlbottom";
            this.pnlbottom.Size = new System.Drawing.Size(730, 2);
            
            // 
            // pnlbottomr
            // 
            this.pnlbottomr.Location = new System.Drawing.Point(728, 0);
            this.pnlbottomr.Name = "pnlbottomr";
            this.pnlbottomr.Size = new System.Drawing.Size(2, 2);
            // 
            // pnlbottoml
            // 
            this.pnlbottoml.Location = new System.Drawing.Point(0, 0);
            this.pnlbottoml.Name = "pnlbottoml";
            this.pnlbottoml.Size = new System.Drawing.Size(2, 2);
            // 
            // pnlleft
            // 
            this.pnlleft.BackgroundColor = System.Drawing.Color.Black;
            this.pnlleft.Dock = Pos.Left;
            this.pnlleft.Location = new System.Drawing.Point(0, 30);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Size = new System.Drawing.Size(2, 461);
            // 
            // pnlright
            // 
            this.pnlright.BackgroundColor = System.Drawing.Color.Black;
            this.pnlright.Dock = Pos.Right;
            this.pnlright.Location = new System.Drawing.Point(728, 30);
            this.pnlright.Name = "pnlright";
            this.pnlright.Size = new System.Drawing.Size(2, 461);
            // 
            // pnlcontents
            // 
            this.pnlcontents.BackgroundColor = System.Drawing.Color.Black;
            this.pnlcontents.Dock = Pos.Fill;
            this.pnlcontents.Location = new System.Drawing.Point(2, 30);
            this.pnlcontents.Name = "pnlcontents";
            this.pnlcontents.Size = new System.Drawing.Size(726, 461);
            // 
            // WindowBorder
            // 
            this.Name = "WindowBorder";
        }

        #endregion

        private ImagePanel pnltitle;
        private Label lbtitletext;
        private ImagePanel pnlminimize;
        private ImagePanel pnlmaximize;
        private ImagePanel pnlclose;
        private ImagePanel pnlbottom;
        private ImagePanel pnlbottomr;
        private ImagePanel pnlbottoml;
        private ImagePanel pnlleft;
        private ImagePanel pnlright;
        private Positioner pnlcontents;
        private ImagePanel pnltitleright;
        private ImagePanel pnltitleleft;
        private ImagePanel pnlicon;
    }
}
