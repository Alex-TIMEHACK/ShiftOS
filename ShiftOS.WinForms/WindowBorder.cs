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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ShiftOS.WinForms.Tools.ControlManager;
using static ShiftOS.Engine.SkinEngine;
using System.Runtime.InteropServices;
using ShiftOS.Engine;
using ShiftOS.WinForms.Tools;
using ShiftOS.WinForms.Applications;
using Gwen;

/// <summary>
/// Window border.
/// </summary>
namespace ShiftOS.WinForms
{
	/// <summary>
	/// Window border.
	/// </summary>
    public partial class WindowBorder : Gwen.Control.Base, IWindowBorder
    {
        public string Text
        {
            get
            {
                return lbtitletext.Text;
            }
            set
            {
                lbtitletext.Text = value;
            }
        }

		/// <summary>
		/// The parent window.
		/// </summary>
        private Gwen.Control.Base _parentWindow = null;

        /// <summary>
        /// Gets or sets the parent window.
        /// </summary>
        /// <value>The parent window.</value>
        public IShiftOSWindow ParentWindow
        {
            get
            {
                return (IShiftOSWindow)_parentWindow;
            }
            set
            {
                _parentWindow = (Gwen.Control.Base)value;
            }
        }

        internal void SetTitle(string title)
        {
            lbtitletext.Text = title;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShiftOS.WinForms.WindowBorder"/> class.
        /// </summary>
        /// <param name="win">Window.</param>
        public WindowBorder(IShiftOSWindow win)
        {
            _parentWindow = win as Gwen.Control.Base;
        }

        

        /// <summary>
        /// Universals the key down.
        /// </summary>
        /// <returns>The key down.</returns>
        /// <param name="o">O.</param>
        /// <param name="a">The alpha component.</param>
        public static void Universal_KeyDown(object o, KeyEventArgs a)
        {
            if (a.Control && a.KeyCode == Keys.T)
            {
                a.SuppressKeyPress = true;


                if (SaveSystem.CurrentSave != null)
                {
                    if (Shiftorium.UpgradeInstalled("window_manager"))
                    {
                        Engine.AppearanceManager.SetupWindow(new Applications.Terminal());
                    }
                }
            }

            ShiftOS.Engine.Scripting.LuaInterpreter.RaiseEvent("on_key_down", a);
        }

        public override void Show()
        {
            base.Show();
            Desktop.ShowWindow(this);
            InitializeComponent();
            _parentWindow.Show();
            WindowBorder_Load(this, EventArgs.Empty);
        }

        /// <summary>
        /// Windows the border load.
        /// </summary>
        /// <returns>The border load.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public void WindowBorder_Load(object sender, EventArgs e)
        {
            this.X = (Desktop.Size.Width - this.Width) / 2;
            this.Y = (Desktop.Size.Height - this.Height) / 2;

            if (!this.IsDialog)
            {
                Engine.AppearanceManager.OpenForms.Add(this);
            }

            SaveSystem.GameReady += () =>
            {
                if (Shiftorium.UpgradeInstalled("wm_free_placement"))
                {
                    AppearanceManager.Invoke(new Action(() =>
                    {
                        this.X = (Desktop.Size.Width - this.Width) / 2;
                        this.Y = (Desktop.Size.Height - this.Height) / 2;

                    }));
                }
                AppearanceManager.Invoke(new Action(() =>
                {
                    Setup();
                }));
            };

            Setup();

            var sWin = (IShiftOSWindow)ParentWindow;

            sWin.OnLoad();

            SkinLoaded += () =>
            {
                sWin.OnSkinLoad();
            };

            Shiftorium.Installed += () =>
            {
                sWin.OnUpgrade();
            };

            sWin.OnUpgrade();
            sWin.OnSkinLoad();

            SizeChanged += (Size) =>
            {
                sWin.OnSkinLoad();
                Setup();
            };
            
        }

        

        /// <summary>
        /// Setup this instance.
        /// </summary>
        public void Setup()
        {
            SetupInternal();
        }

        internal void SetupInternal()
        {
            this.lbtitletext.Text = NameChangerBackend.GetName(ParentWindow);

            if (SaveSystem.CurrentSave != null)
            {
                this.pnltitle.IsVisible = Shiftorium.UpgradeInstalled("wm_titlebar");
                this.pnlclose.IsVisible = Shiftorium.UpgradeInstalled("close_button");
                this.pnlminimize.IsVisible = (IsDialog == false) && Shiftorium.UpgradeInstalled("minimize_button");
                this.pnlmaximize.IsVisible = (IsDialog == false) && Shiftorium.UpgradeInstalled("maximize_button");
                SetupSkin();
            }
            else
            {
                this.pnltitle.IsVisible = false;
                this.pnlclose.IsVisible = false;
                this.pnlminimize.IsVisible = false;
                this.pnlmaximize.IsVisible = false;

            }

        }

        /// <summary>
        /// Setups the skin.
        /// </summary>
        /// <returns>The skin.</returns>
        public void SetupSkin()
        {
            pnltitle.Height = LoadedSkin.TitlebarHeight;
            pnltitle.BackgroundColor = LoadedSkin.TitleBackgroundColor;
            pnltitle.BackgroundImage = GetTexture("titlebar");
            pnltitleleft.IsVisible = LoadedSkin.ShowTitleCorners;
            pnltitleright.IsVisible = LoadedSkin.ShowTitleCorners;
            pnltitleleft.BackgroundColor = LoadedSkin.TitleLeftCornerBackground;
            pnltitleright.BackgroundColor = LoadedSkin.TitleRightCornerBackground;
            pnltitleleft.Width = LoadedSkin.TitleLeftCornerWidth;
            pnltitleright.Width = LoadedSkin.TitleRightCornerWidth;
            pnltitleleft.BackgroundImage = GetTexture("titleleft");
            pnltitleleft.BackgroundImageLayout = (int)GetImageLayout("titleleft");
            pnltitleright.BackgroundImage = GetTexture("titleright");
            pnltitleright.BackgroundImageLayout = (int)GetImageLayout("titleright");
            pnltitle.BackgroundImageLayout = (int)GetImageLayout("titlebar"); //RETARD ALERT. WHY WASN'T THIS THERE WHEN IMAGELAYOUTS WERE FIRST IMPLEMENTED?

            lbtitletext.BackgroundColor = (pnltitle.BackgroundImage != null) ? Color.Transparent : LoadedSkin.TitleBackgroundColor;
            lbtitletext.TextColor = LoadedSkin.TitleTextColor;
            lbtitletext.Font = ControlManager.CreateGwenFont(Skin.Renderer, LoadedSkin.TitleFont);

            pnlleft.BackgroundColor = LoadedSkin.BorderLeftBackground;
            pnlleft.BackgroundImage = GetTexture("leftborder");
            pnlleft.BackgroundImageLayout = (int)GetImageLayout("leftborder");
            pnlleft.Width = LoadedSkin.LeftBorderWidth;
            pnlright.BackgroundColor = LoadedSkin.BorderRightBackground;
            pnlright.BackgroundImage = GetTexture("rightborder");
            pnlright.BackgroundImageLayout = (int)GetImageLayout("rightborder");
            pnlright.Width = LoadedSkin.RightBorderWidth;

            pnlbottom.BackgroundColor = LoadedSkin.BorderBottomBackground;
            pnlbottom.BackgroundImage = GetTexture("bottomborder");
            pnlbottom.BackgroundImageLayout = (int)GetImageLayout("bottomborder");
            pnlbottom.Height = LoadedSkin.BottomBorderWidth;

            pnlbottomr.BackgroundColor = LoadedSkin.BorderBottomRightBackground;
            pnlbottomr.BackgroundImage = GetTexture("bottomrborder");
            pnlbottomr.BackgroundImageLayout = (int)GetImageLayout("bottomrborder");
            pnlbottoml.BackgroundColor = LoadedSkin.BorderBottomLeftBackground;
            pnlbottoml.BackgroundImage = GetTexture("bottomlborder");
            pnlbottoml.BackgroundImageLayout = (int)GetImageLayout("bottomlborder");

            lbtitletext.TextColor = LoadedSkin.TitleTextColor;
            lbtitletext.Font = CreateGwenFont(Skin.Renderer, LoadedSkin.TitleFont);
            pnlclose.BackgroundColor = LoadedSkin.CloseButtonColor;
            pnlclose.BackgroundImage = GetTexture("closebutton");
            pnlclose.BackgroundImageLayout = (int)GetImageLayout("closebutton");
            pnlminimize.BackgroundColor = LoadedSkin.MinimizeButtonColor;
            pnlminimize.BackgroundImage = GetTexture("minimizebutton");
            pnlminimize.BackgroundImageLayout = (int)GetImageLayout("minimizebutton");
            pnlmaximize.BackgroundColor = LoadedSkin.MaximizeButtonColor;
            pnlmaximize.BackgroundImage = GetTexture("maximizebutton");
            pnlmaximize.BackgroundImageLayout = (int)GetImageLayout("maximizebutton");

            pnlclose.Size = LoadedSkin.CloseButtonSize;
            pnlminimize.Size = LoadedSkin.MinimizeButtonSize;
            pnlmaximize.Size = LoadedSkin.MaximizeButtonSize;
            pnlclose.Location = FromRight(LoadedSkin.CloseButtonFromSide);
            pnlminimize.Location = FromRight(LoadedSkin.MinimizeButtonFromSide);
            pnlmaximize.Location = FromRight(LoadedSkin.MaximizeButtonFromSide);
            pnlclose.X -= pnlclose.Width;
            pnlmaximize.X -= pnlmaximize.Width;
            pnlminimize.X -= pnlminimize.Width;

            switch (LoadedSkin.TitleTextCentered)
            {
                case false:
                    lbtitletext.Location = new Point(16 + LoadedSkin.TitlebarIconFromSide.X + LoadedSkin.TitleTextLeft.X,
                            LoadedSkin.TitleTextLeft.Y);
                    break;
                default:
                    lbtitletext.X = (pnltitle.Width - lbtitletext.Width) / 2;
                    lbtitletext.Y = LoadedSkin.TitleTextLeft.Y;
                    break;
            }

            if (Shiftorium.UpgradeInstalled("app_icons"))
            {
                pnlicon.Show();
                pnlicon.Size = new Size(16, 16);
                pnlicon.BackgroundColor = Color.Transparent;
                pnlicon.BackgroundImage = GetIconTexture(this.ParentWindow.GetType().Name);
                pnlicon.BackgroundImageLayout = (int)ImageLayout.Stretch;
                pnlicon.Location = LoadedSkin.TitlebarIconFromSide;
            }
            else
            {
                pnlicon.Hide();
            }
        }

        public Texture GetTexture(string id)
        {
            var img = GetImage(id);
            if (img == null)
                return null;

            var tex = new Texture(this.Skin.Renderer);
            tex.LoadRaw(img.Width, img.Height, ImageToBinary(img));
            return tex;
        }

        public Texture GetIconTexture(string id)
        {
            var img = GetIcon(id);
            if (img == null)
                return null;

            var tex = new Texture(this.Skin.Renderer);
            tex.LoadRaw(img.Width, img.Height, ImageToBinary(img));
            return tex;
        }


        /// <summary>
        /// Froms the right.
        /// </summary>
        /// <returns>The right.</returns>
        /// <param name="input">Input.</param>
        public Point FromRight(Point input)
        {
            return new Point(pnltitle.Width - input.X, input.Y);
        }

		/// <summary>
		/// Lbtitletexts the click.
		/// </summary>
		/// <returns>The click.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void lbtitletext_Click(object sender, EventArgs e)
        {

        }

		/// <summary>
		/// Pnlcloses the click.
		/// </summary>
		/// <returns>The click.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void pnlclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		/// <summary>
		/// Pnlmaximizes the click.
		/// </summary>
		/// <returns>The click.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void pnlmaximize_Click(object sender, EventArgs e)
        {
            if (maximized == false)
                Desktop.MaximizeWindow(this);
            else
                Desktop.RestoreWindow(this);
            maximized = !maximized;
            SetupSkin();
        }

        bool minimized = false;
        bool maximized = false;

        public bool IsMinimized
        {
            get
            {
                return minimized;
            }
        }

        public bool IsMaximized
        {
            get
            {
                return maximized;
            }
        }


        /// <summary>
        /// Pnlminimizes the click.
        /// </summary>
        /// <returns>The click.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void pnlminimize_Click(object sender, EventArgs e)
        {
            if (minimized == false)
                Desktop.MinimizeWindow(this);
            else
                Desktop.RestoreWindow(this);
            minimized = !minimized;
        }


        /// <summary>
        /// The W m NCLBUTTONDOW.
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        /// <summary>
        /// The H t CAPTIO.
        /// </summary>
        public const int HT_CAPTION = 0x2;

        /// <summary>
        /// The is dialog.
        /// </summary>
        public bool IsDialog = false;


        [DllImportAttribute("user32.dll")]
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <returns>The message.</returns>
        /// <param name="hWnd">H window.</param>
        /// <param name="Msg">Message.</param>
        /// <param name="wParam">W parameter.</param>
        /// <param name="lParam">L parameter.</param>
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        /// <summary>
        /// Releases the capture.
        /// </summary>
        /// <returns>The capture.</returns>
        public static extern bool ReleaseCapture();

		/// <summary>
		/// Pnltitles the mouse move.
		/// </summary>
		/// <returns>The mouse move.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void pnltitle_MouseMove(object sender, MouseEventArgs e)
        {
        }

		/// <summary>
		/// Pnltitles the paint.
		/// </summary>
		/// <returns>The paint.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void pnltitle_Paint(object sender, PaintEventArgs e) {

        }

		/// <summary>
		/// Lbtitletexts the mouse move.
		/// </summary>
		/// <returns>The mouse move.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void lbtitletext_MouseMove(object sender, MouseEventArgs e) {
            pnltitle_MouseMove(sender, e);
        }

        public void Close()
        {
            if ((ParentWindow as IShiftOSWindow).OnUnload())
            {
                if (!SaveSystem.ShuttingDown)
                {
                    if (Engine.AppearanceManager.OpenForms.Contains(this))
                        Engine.AppearanceManager.OpenForms.Remove(this);
                    Desktop.ResetPanelButtons();
                    this._parentWindow.Dispose();
                    this.Dispose();
                }
            }

        }
    }
}
