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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gwen;
using ShiftOS.Engine;
using ShiftOS.WinForms.Tools;

namespace ShiftOS.WinForms.Controls
{
    public class TerminalBox : Gwen.Control.MultilineTextBox, ITerminalWidget
    {
        public TerminalBox(Gwen.Control.Base parent) : base(parent)
        {
            RenderHint = RenderHintConstants.TERMINALBOX;
        }

        public void SelectBottom()
        {
            int y = TotalLines;
            int x = Lines[y-1].Length;
            CursorPosition = new Point(x, y);
        }

        protected override bool OnKeyPressed(Key key, bool down)
        {
            var res =  (bool)KeyDown?.Invoke(key);
            if(res == false)
                res = base.OnKeyPressed(key, down);
            return res;
        }

        public int SelectionStart
        {
            get
            {
                return Convert2Dto1D(CursorPosition);
            }
        }

        private int Convert2Dto1D(Point pt)
        {
            return pt.Y + (pt.X * TotalLines);
        }

        protected override void Render(Gwen.Skin.Base skin) {
            this.DeleteAllChildren(); // they need to die ok? don't question it
            base.Render(skin);
        }

        public string[] Lines
        {
            get
            {
                return Text.Split(new[] { Environment.NewLine.ToString() }, StringSplitOptions.None);
            }
        }

        public event Func<Key, bool> KeyDown;

        protected override void OnMouseClickedLeft(int x, int y, bool down)
        {
            base.OnMouseClickedLeft(x, y, down);
            this.SelectBottom();
        }

        public void Write(string text)
        {
            this.Text += Localization.Parse(text);
            SelectBottom();
        }

        

        public void WriteLine(string text)
        {
            this.Text += Localization.Parse(text) + Environment.NewLine;
            SelectBottom();
        }

        public void Clear()
        {
            this.Text = "";
        }
    }
}
