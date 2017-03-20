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

using Gwen.Control;

namespace ShiftOS.WinForms.Applications
{
    partial class About
    {

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new ImagePanel(this);
            this.label1 = new Label(this);
            this.lbshiftit = new Label(this);
            this.lbaboutdesc = new Label(this);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ShiftOS.WinForms.Properties.Resources.justthes;
            this.pictureBox1.Location = new System.Drawing.Point(14, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label1
            // 
            // this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.Text = "ShiftOS";
            // 
            // lbshiftit
            // 
            // this.lbshiftit.AutoSize = true;
            this.lbshiftit.Location = new System.Drawing.Point(140, 73);
            this.lbshiftit.Name = "lbshiftit";
            this.lbshiftit.Size = new System.Drawing.Size(84, 13);
            this.lbshiftit.Text = "Shift it your way.";
            // 
            // lbaboutdesc
            // 
            this.lbaboutdesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbaboutdesc.Location = new System.Drawing.Point(14, 126);
            this.lbaboutdesc.Name = "lbaboutdesc";
            this.lbaboutdesc.Size = new System.Drawing.Size(498, 328);
            this.lbaboutdesc.Text = "label2";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AddChild(this.lbaboutdesc);
            this.AddChild(this.lbshiftit);
            this.AddChild(this.label1);
            this.AddChild(this.pictureBox1);
            this.Name = "About";
            this.Size = new System.Drawing.Size(532, 474);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();

        }

        #endregion

        private ImagePanel pictureBox1;
        private Label label1;
        private Label lbshiftit;
        private Label lbaboutdesc;
    }
}
