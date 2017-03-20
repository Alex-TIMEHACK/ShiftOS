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
    partial class Pong
    {
        private void InitializeComponent()
        {
            this.gameTimer = new System.Timers.Timer();
            this.counter = new System.Windows.Forms.Timer(this.components);
            this.tmrcountdown = new System.Windows.Forms.Timer(this.components);
            this.tmrstoryline = new System.Windows.Forms.Timer(this.components);
            this.pgcontents = new Canvas(this.Skin);
            this.pnlgamestats = new System.Windows.Forms.Panel();
            this.button1 = new Button(this);
            this.label12 = new Label(this);
            this.lblnextstats = new Label(this);
            this.Label7 = new Label(this);
            this.lblpreviousstats = new Label(this);
            this.Label4 = new Label(this);
            this.btnplayon = new Button(this);
            this.Label3 = new Label(this);
            this.btncashout = new Button(this);
            this.Label2 = new Label(this);
            this.lbllevelreached = new Label(this);
            this.pnlhighscore = new System.Windows.Forms.Panel();
            this.lbhighscore = new System.Windows.Forms.ListBox();
            this.label10 = new Label(this);
            this.pnlfinalstats = new System.Windows.Forms.Panel();
            this.btnplayagain = new Button(this);
            this.lblfinalcodepoints = new Label(this);
            this.Label11 = new Label(this);
            this.lblfinalcomputerreward = new Label(this);
            this.Label9 = new Label(this);
            this.lblfinallevelreward = new Label(this);
            this.lblfinallevelreached = new Label(this);
            this.lblfinalcodepointswithtext = new Label(this);
            this.pnllose = new System.Windows.Forms.Panel();
            this.lblmissedout = new Label(this);
            this.lblbutyougained = new Label(this);
            this.btnlosetryagain = new Button(this);
            this.Label5 = new Label(this);
            this.Label1 = new Label(this);
            this.pnlintro = new System.Windows.Forms.Panel();
            this.Label6 = new Label(this);
            this.btnstartgame = new Button(this);
            this.Label8 = new Label(this);
            this.lblbeatai = new Label(this);
            this.lblcountdown = new Label(this);
            this.ball = new Canvas(this.Skin);
            this.paddleHuman = new ImagePanel(this);
            this.paddleComputer = new System.Windows.Forms.Panel();
            this.lbllevelandtime = new Label(this);
            this.lblstatscodepoints = new Label(this);
            this.lblstatsY = new Label(this);
            this.lblstatsX = new Label(this);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new Button(this);
            ((System.ComponentModel.ISupportInitialize)(this.paddleHuman)).BeginInit();
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 30;
            this.gameTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.gameTimer_Tick);
            // 
            // counter
            // 
            this.counter.Interval = 1000;
            this.counter.Tick += new System.EventHandler(this.counter_Tick);
            // 
            // tmrcountdown
            // 
            this.tmrcountdown.Interval = 1000;
            this.tmrcountdown.Tick += new System.EventHandler(this.countdown_Tick);
            // 
            // tmrstoryline
            // 
            this.tmrstoryline.Interval = 1000;
            this.tmrstoryline.Tick += new System.EventHandler(this.tmrstoryline_Tick);
            // 
            // pgcontents
            // 
            this.pgcontents.BackgroundColor = System.Drawing.Color.White;
            this.pgcontents.AddChild(this.pnlhighscore);
            this.pgcontents.AddChild(this.pnlgamestats);
            this.pgcontents.AddChild(this.pnlfinalstats);
            this.pgcontents.AddChild(this.pnllose);
            this.pgcontents.AddChild(this.pnlintro);
            this.pgcontents.AddChild(this.lblbeatai);
            this.pgcontents.AddChild(this.lblcountdown);
            this.pgcontents.AddChild(this.ball);
            this.pgcontents.AddChild(this.paddleHuman);
            this.pgcontents.AddChild(this.paddleComputer);
            this.pgcontents.AddChild(this.lbllevelandtime);
            this.pgcontents.AddChild(this.lblstatscodepoints);
            this.pgcontents.AddChild(this.lblstatsY);
            this.pgcontents.AddChild(this.lblstatsX);
            this.pgcontents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgcontents.Location = new System.Drawing.Point(0, 0);
            this.pgcontents.Name = "pgcontents";
            this.pgcontents.Size = new System.Drawing.Size(700, 400);
            this.pgcontents.TabIndex = 20;
            this.pgcontents.Paint += new System.Windows.Forms.PaintEventHandler(this.pgcontents_Paint);
            this.pgcontents.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pongMain_MouseMove);
            // 
            // pnlgamestats
            // 
            this.pnlgamestats.AddChild(this.button1);
            this.pnlgamestats.AddChild(this.label12);
            this.pnlgamestats.AddChild(this.lblnextstats);
            this.pnlgamestats.AddChild(this.Label7);
            this.pnlgamestats.AddChild(this.lblpreviousstats);
            this.pnlgamestats.AddChild(this.Label4);
            this.pnlgamestats.AddChild(this.btnplayon);
            this.pnlgamestats.AddChild(this.Label3);
            this.pnlgamestats.AddChild(this.btncashout);
            this.pnlgamestats.AddChild(this.Label2);
            this.pnlgamestats.AddChild(this.lbllevelreached);
            this.pnlgamestats.Location = new System.Drawing.Point(56, 76);
            this.pnlgamestats.Name = "pnlgamestats";
            this.pnlgamestats.Size = new System.Drawing.Size(466, 284);
            this.pnlgamestats.TabIndex = 6;
            this.pnlgamestats.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(32, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 35);
            this.button1.TabIndex = 10;
            this.button1.Text = "{PONG_VIEW_HIGHSCORES}";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnhighscore_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(245, 33);
            this.label12.Text = "{PONG_HIGHSCORE_EXP}";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblnextstats
            // 
            this.lblnextstats.AutoSize = true;
            this.lblnextstats.Location = new System.Drawing.Point(278, 136);
            this.lblnextstats.Name = "lblnextstats";
            this.lblnextstats.Size = new System.Drawing.Size(0, 13);
            // 
            // Label7
            // 
            // this.Label7.AutoSize = true;
            this.Label7.Font = Tools.ControlManager.CreateGwenFont(Label7.Skin.Renderer, new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            this.Label7.Location = new System.Drawing.Point(278, 119);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(124, 16);
            this.Label7.Text = "Next Level Stats:";
            // 
            // lblpreviousstats
            // 
            this.lblpreviousstats.AutoSize = true;
            this.lblpreviousstats.Location = new System.Drawing.Point(278, 54);
            this.lblpreviousstats.Name = "lblpreviousstats";
            this.lblpreviousstats.Size = new System.Drawing.Size(0, 13);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(278, 37);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(154, 16);
            this.Label4.Text = "Previous Level Stats:";
            // 
            // btnplayon
            // 
            this.btnplayon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnplayon.Location = new System.Drawing.Point(32, 147);
            this.btnplayon.Name = "btnplayon";
            this.btnplayon.Size = new System.Drawing.Size(191, 35);
            this.btnplayon.Text = "Play on for 3 codepoints!";
            this.btnplayon.UseVisualStyleBackColor = true;
            this.btnplayon.Click += new System.EventHandler(this.btnplayon_Click);
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(8, 111);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(245, 33);
            this.Label3.Text = "{PONG_PLAYON_DESC}";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btncashout
            // 
            this.btncashout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncashout.Location = new System.Drawing.Point(32, 73);
            this.btncashout.Name = "btncashout";
            this.btncashout.Size = new System.Drawing.Size(191, 35);
            this.btncashout.Text = "Cash out with 1 codepoint!";
            this.btncashout.UseVisualStyleBackColor = true;
            this.btncashout.Click += new System.EventHandler(this.btncashout_Click);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(8, 37);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(245, 33);
            this.Label2.Text = "{PONG_CASHOUT_DESC}";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbllevelreached
            // 
            this.lbllevelreached.AutoSize = true;
            this.lbllevelreached.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllevelreached.Location = new System.Drawing.Point(149, 6);
            this.lbllevelreached.Name = "lbllevelreached";
            this.lbllevelreached.Size = new System.Drawing.Size(185, 20);
            this.lbllevelreached.Text = "You Reached Level 2!";
            // 
            // pnlhighscore
            // 
            this.pnlhighscore.AddChild(this.lbhighscore);
            this.pnlhighscore.AddChild(this.flowLayoutPanel1);
            this.pnlhighscore.AddChild(this.label10);
            this.pnlhighscore.Location = new System.Drawing.Point(67, 29);
            this.pnlhighscore.Name = "pnlhighscore";
            this.pnlhighscore.Size = new System.Drawing.Size(539, 311);
            this.pnlhighscore.Visible = false;
            // 
            // lbhighscore
            // 
            this.lbhighscore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbhighscore.FormattingEnabled = true;
            this.lbhighscore.Location = new System.Drawing.Point(0, 36);
            this.lbhighscore.MultiColumn = true;
            this.lbhighscore.Name = "lbhighscore";
            this.lbhighscore.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbhighscore.Size = new System.Drawing.Size(539, 246);
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(539, 36);
            this.label10.Text = "{HIGH_SCORES}";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlfinalstats
            // 
            this.pnlfinalstats.AddChild(this.btnplayagain);
            this.pnlfinalstats.AddChild(this.lblfinalcodepoints);
            this.pnlfinalstats.AddChild(this.Label11);
            this.pnlfinalstats.AddChild(this.lblfinalcomputerreward);
            this.pnlfinalstats.AddChild(this.Label9);
            this.pnlfinalstats.AddChild(this.lblfinallevelreward);
            this.pnlfinalstats.AddChild(this.lblfinallevelreached);
            this.pnlfinalstats.AddChild(this.lblfinalcodepointswithtext);
            this.pnlfinalstats.Location = new System.Drawing.Point(172, 74);
            this.pnlfinalstats.Name = "pnlfinalstats";
            this.pnlfinalstats.Size = new System.Drawing.Size(362, 226);
            this.pnlfinalstats.Visible = false;
            // 
            // btnplayagain
            // 
            this.btnplayagain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnplayagain.Location = new System.Drawing.Point(5, 194);
            this.btnplayagain.Name = "btnplayagain";
            this.btnplayagain.Size = new System.Drawing.Size(352, 29);
            this.btnplayagain.Text = "{PLAY}";
            this.btnplayagain.UseVisualStyleBackColor = true;
            this.btnplayagain.Click += new System.EventHandler(this.btnplayagain_Click);
            // 
            // lblfinalcodepoints
            // 
            this.lblfinalcodepoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfinalcodepoints.Location = new System.Drawing.Point(3, 124);
            this.lblfinalcodepoints.Name = "lblfinalcodepoints";
            this.lblfinalcodepoints.Size = new System.Drawing.Size(356, 73);
            this.lblfinalcodepoints.Text = "134 CP";
            this.lblfinalcodepoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(162, 82);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(33, 33);
            this.Label11.Text = "+";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblfinalcomputerreward
            // 
            this.lblfinalcomputerreward.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfinalcomputerreward.Location = new System.Drawing.Point(193, 72);
            this.lblfinalcomputerreward.Name = "lblfinalcomputerreward";
            this.lblfinalcomputerreward.Size = new System.Drawing.Size(151, 52);
            this.lblfinalcomputerreward.Text = "34";
            this.lblfinalcomputerreward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(179, 31);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(180, 49);
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblfinallevelreward
            // 
            this.lblfinallevelreward.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfinallevelreward.Location = new System.Drawing.Point(12, 72);
            this.lblfinallevelreward.Name = "lblfinallevelreward";
            this.lblfinallevelreward.Size = new System.Drawing.Size(151, 52);
            this.lblfinallevelreward.Text = "100";
            this.lblfinallevelreward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblfinallevelreached
            // 
            this.lblfinallevelreached.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfinallevelreached.Location = new System.Drawing.Point(3, 31);
            this.lblfinallevelreached.Name = "lblfinallevelreached";
            this.lblfinallevelreached.Size = new System.Drawing.Size(170, 49);
            this.lblfinallevelreached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblfinalcodepointswithtext
            // 
            this.lblfinalcodepointswithtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfinalcodepointswithtext.Location = new System.Drawing.Point(3, 2);
            this.lblfinalcodepointswithtext.Name = "lblfinalcodepointswithtext";
            this.lblfinalcodepointswithtext.Size = new System.Drawing.Size(356, 26);
            this.lblfinalcodepointswithtext.Text = "You cashed out with 134 codepoints!";
            this.lblfinalcodepointswithtext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnllose
            // 
            this.pnllose.AddChild(this.lblmissedout);
            this.pnllose.AddChild(this.lblbutyougained);
            this.pnllose.AddChild(this.btnlosetryagain);
            this.pnllose.AddChild(this.Label5);
            this.pnllose.AddChild(this.Label1);
            this.pnllose.Location = new System.Drawing.Point(209, 71);
            this.pnllose.Name = "pnllose";
            this.pnllose.Size = new System.Drawing.Size(266, 214);
            this.pnllose.Visible = false;
            // 
            // lblmissedout
            // 
            this.lblmissedout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmissedout.Location = new System.Drawing.Point(3, 175);
            this.lblmissedout.Name = "lblmissedout";
            this.lblmissedout.Size = new System.Drawing.Size(146, 35);
            this.lblmissedout.Text = "You Missed Out On: 500 Codepoints";
            this.lblmissedout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblbutyougained
            // 
            this.lblbutyougained.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbutyougained.Location = new System.Drawing.Point(3, 125);
            this.lblbutyougained.Name = "lblbutyougained";
            this.lblbutyougained.Size = new System.Drawing.Size(146, 35);
            this.lblbutyougained.Text = "But you gained 5 Codepoints";
            this.lblbutyougained.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnlosetryagain
            // 
            this.btnlosetryagain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlosetryagain.Location = new System.Drawing.Point(155, 176);
            this.btnlosetryagain.Name = "btnlosetryagain";
            this.btnlosetryagain.Size = new System.Drawing.Size(106, 35);
            this.btnlosetryagain.Text = "Try Again";
            this.btnlosetryagain.UseVisualStyleBackColor = true;
            this.btnlosetryagain.Click += new System.EventHandler(this.btnlosetryagain_Click);
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(7, 26);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(260, 163);
            // 
            // Label1
            // 
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(266, 16);
            this.Label1.Text = "You lose!";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlintro
            // 
            this.pnlintro.AddChild(this.Label6);
            this.pnlintro.AddChild(this.btnstartgame);
            this.pnlintro.AddChild(this.Label8);
            this.pnlintro.Location = new System.Drawing.Point(52, 29);
            this.pnlintro.Name = "pnlintro";
            this.pnlintro.Size = new System.Drawing.Size(595, 303);
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 39);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(589, 227);
            this.Label6.Text = "{PONG_DESC}";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label6.Click += new System.EventHandler(this.Label6_Click);
            // 
            // btnstartgame
            // 
            this.btnstartgame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstartgame.Location = new System.Drawing.Point(186, 273);
            this.btnstartgame.Name = "btnstartgame";
            this.btnstartgame.Size = new System.Drawing.Size(242, 28);
            this.btnstartgame.Text = "{PLAY}";
            this.btnstartgame.UseVisualStyleBackColor = true;
            this.btnstartgame.Click += new System.EventHandler(this.btnstartgame_Click);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.Color.Black;
            this.Label8.Location = new System.Drawing.Point(250, 5);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(280, 31);
            this.Label8.Text = "{PONG_WELCOME}";
            this.Label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // lblbeatai
            // 
            this.lblbeatai.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbeatai.Location = new System.Drawing.Point(47, 41);
            this.lblbeatai.Name = "lblbeatai";
            this.lblbeatai.Size = new System.Drawing.Size(600, 30);
            this.lblbeatai.Text = "You got 2 codepoints for beating the Computer!";
            this.lblbeatai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblbeatai.IsVisible = false;
            // 
            // lblcountdown
            // 
            this.lblcountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcountdown.Location = new System.Drawing.Point(182, 152);
            this.lblcountdown.Name = "lblcountdown";
            this.lblcountdown.Size = new System.Drawing.Size(315, 49);
            this.lblcountdown.Text = "3";
            this.lblcountdown.Alignment = (Gwen.Pos)System.Drawing.ContentAlignment.MiddleCenter;
            this.lblcountdown.IsVisible = false;
            // 
            // ball
            // 
            this.ball.BackgroundColor = System.Drawing.Color.Black;
            this.ball.Location = new System.Drawing.Point(300, 152);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(20, 20);
            this.ball.HoverEnter += new GwenEventHandler<System.EventArgs>(this.ball_MouseEnter);
            this.ball.HoverLeave += new GwenEventHandler<System.EventArgs>(this.ball_MouseLeave);
            // 
            // paddleHuman
            // 
            this.paddleHuman.BackgroundColor = System.Drawing.Color.Black;
            this.paddleHuman.Location = new System.Drawing.Point(10, 134);
            this.paddleComputer.MaximumSize = new System.Drawing.Size(20, 150);
            this.paddleHuman.Name = "paddleHuman";
            this.paddleHuman.Size = new System.Drawing.Size(20, 100);
            // 
            // paddleComputer
            // 
            this.paddleComputer.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paddleComputer.BackgroundColor = System.Drawing.Color.Black;
            this.paddleComputer.Location = new System.Drawing.Point(666, 134);
            this.paddleComputer.MaximumSize = new System.Drawing.Size(20, 150);
            this.paddleComputer.Name = "paddleComputer";
            this.paddleComputer.Size = new System.Drawing.Size(20, 100);
            // 
            // lbllevelandtime
            // 
            this.lbllevelandtime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbllevelandtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllevelandtime.Location = new System.Drawing.Point(0, 0);
            this.lbllevelandtime.Name = "lbllevelandtime";
            this.lbllevelandtime.Size = new System.Drawing.Size(700, 22);
            this.lbllevelandtime.Text = "Level: 1 - 58 Seconds Left";
            this.lbllevelandtime.Alignment = (Gwen.Pos)System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblstatscodepoints
            // 
            this.lblstatscodepoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblstatscodepoints.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstatscodepoints.Location = new System.Drawing.Point(239, 356);
            this.lblstatscodepoints.Name = "lblstatscodepoints";
            this.lblstatscodepoints.Size = new System.Drawing.Size(219, 35);
            this.lblstatscodepoints.Text = "Codepoints: ";
            this.lblstatscodepoints.Alignment = (Gwen.Pos)System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblstatsY
            // 
            this.lblstatsY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblstatsY.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstatsY.Location = new System.Drawing.Point(542, 356);
            this.lblstatsY.Name = "lblstatsY";
            this.lblstatsY.Size = new System.Drawing.Size(144, 35);
            this.lblstatsY.Text = "Yspeed:";
            this.lblstatsY.Alignment = (Gwen.Pos)System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblstatsX
            // 
            this.lblstatsX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblstatsX.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstatsX.Location = new System.Drawing.Point(3, 356);
            this.lblstatsX.Name = "lblstatsX";
            this.lblstatsX.Size = new System.Drawing.Size(144, 35);
            this.lblstatsX.Text = "Xspeed: ";
            this.lblstatsX.Alignment = (Gwen.Pos)System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.AddChild(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 282);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(539, 29);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.Location = new System.Drawing.Point(476, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.Text = "{CLOSE}";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Clicked += new GwenEventHandler<ClickedEventArgs>(this.button2_Click);
            // 
            // Pong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.White;
            this.AddChild(this.pgcontents);
            this.DoubleBuffered = true;
            this.Name = "Pong";
            this.Text = "{PONG_NAME}";
            this.Size = new System.Drawing.Size(820, 500);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pongMain_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.paddleHuman)).EndInit();

        }
        internal System.Windows.Forms.Panel paddleComputer;
        internal System.Timers.Timer gameTimer;
        internal ImagePanel paddleHuman;
        internal Label lbllevelandtime;
        internal Label lblstatsX;
        internal System.Windows.Forms.Timer counter;
        internal System.Windows.Forms.Panel pnlgamestats;
        internal Label lblnextstats;
        internal Label Label7;
        internal Label lblpreviousstats;
        internal Label Label4;
        internal Button btnplayon;
        internal Label Label3;
        internal Button btncashout;
        internal Label Label2;
        internal Label lbllevelreached;
        internal Label lblcountdown;
        internal System.Windows.Forms.Timer tmrcountdown;
        internal Label lblbeatai;
        internal System.Windows.Forms.Panel pnlfinalstats;
        internal Button btnplayagain;
        internal Label lblfinalcodepoints;
        internal Label Label11;
        internal Label lblfinalcomputerreward;
        internal Label Label9;
        internal Label lblfinallevelreward;
        internal Label lblfinallevelreached;
        internal Label lblfinalcodepointswithtext;
        internal System.Windows.Forms.Panel pnllose;
        internal Label lblmissedout;
        internal Label lblbutyougained;
        internal Button btnlosetryagain;
        internal Label Label5;
        internal Label Label1;
        internal Label lblstatscodepoints;
        internal Label lblstatsY;
        internal System.Windows.Forms.Panel pnlintro;
        internal Label Label6;
        internal Button btnstartgame;
        internal Label Label8;
        internal System.Windows.Forms.Timer tmrstoryline;
        private System.Windows.Forms.Panel pnlhighscore;
        private System.Windows.Forms.ListBox lbhighscore;
        private Label label10;
        internal Gwen.Control.Canvas pgcontents;
        internal Gwen.Control.Canvas ball;
        internal Button button1;
        internal Label label12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Button button2;
    }
}
