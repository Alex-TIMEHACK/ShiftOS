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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftOS.Engine;
using static ShiftOS.Engine.SkinEngine;
using ShiftOS.WinForms.Tools;
using ShiftOS.WinForms.Applications;
using Newtonsoft.Json;
using ShiftOS.Engine.Scripting;
using System.Threading;
using OpenTK;
using Gwen;
using Gwen.Control;
using static Gwen.Control.Base;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

/// <summary>
/// Winforms desktop.
/// </summary>
namespace ShiftOS.WinForms
{
    /// <summary>
    /// Winforms desktop.
    /// </summary>
    public partial class WinformsDesktop : GameWindow, IDesktop
    {
        private bool InScreensaver = false;
        private int millisecondsUntilScreensaver = 300000;
        private Queue<Action> actionsToRun = new Queue<Action>();

        public void Invoke(Action act)
        {
            actionsToRun.Enqueue(act);

        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ShiftOS.WinForms.WinformsDesktop"/> class.
        /// </summary>
        public WinformsDesktop() : base(2560,1440, OpenTK.Graphics.GraphicsMode.Default, "ShiftOS")
        {
            InitializeComponent();
            NotificationDaemon.NotificationMade += (note) =>
            {
                //Soon this will pop a balloon note.
                this.Invoke(new Action(() =>
                {
                    btnnotifications.Text = "Notifications (" + NotificationDaemon.GetUnreadCount().ToString() + ")";
                }));
            };

            NotificationDaemon.NotificationRead += () =>
            {
                //Soon this will pop a balloon note.
                this.Invoke(new Action(() =>
                {
                    btnnotifications.Text = "Notifications (" + NotificationDaemon.GetUnreadCount().ToString() + ")";
                }));
            };

            SaveSystem.GameReady += () =>
            {
                SetupDesktop();
                btnnotifications.Text = "Notifications (" + NotificationDaemon.GetUnreadCount().ToString() + ")";
            };
            Shiftorium.Installed += () =>
            {
                SetupDesktop();
            };
            var time = new System.Timers.Timer();
            time.Interval = 100;
            this.KeyDown += (o, a) =>
            {
                if (a.Control && a.Key == OpenTK.Input.Key.T)
                {
                    Engine.AppearanceManager.SetupWindow(new Applications.Terminal());
                }
                /*if (a.Control && a.KeyCode == Keys.Tab)
                {
                    // CtrlTabMenu 
                    CtrlTabMenu.Show();
                    if (a.Shift) CtrlTabMenu.CycleBack();
                    else CtrlTabMenu.CycleForwards();
                }*/ //nyi

                ShiftOS.Engine.Scripting.LuaInterpreter.RaiseEvent("on_key_down", a);
            };
            SkinEngine.SkinLoaded += () =>
            {
                SetupDesktop();
            };
        }

        public void HideScreensaver()
        {
            if (ResetDesktop == true)
            {
                Cursor = MouseCursor.Default;
                SetupDesktop();
                ResetDesktop = false;

            }
        }

        private bool ResetDesktop = false;

        private void ShowScreensaver()
        {
            if (Shiftorium.UpgradeInstalled("screensavers"))
            {
                var icn = GetImage("screensaver");
                
                Cursor = MouseCursor.Empty;

                var t = new Thread(() =>
                {
                    var rnd = new Random();
                    while (InScreensaver == true)
                    {
                        int x = rnd.Next(0, this.Width);
                        int y = rnd.Next(0, this.Height);

                        
                        Thread.Sleep(5000);
                    }
                    ResetDesktop = true;
                });
                t.IsBackground = true;
                t.Start();

            }
        }


        

        /// <summary>
        /// Populates the panel buttons.
        /// </summary>
        /// <returns>The panel buttons.</returns>
        public void PopulatePanelButtons()
        {
            while(PanelButtonElements.Count > 0)
            {
                PanelButtonElements.Dequeue().Dispose();
            }
            if (DesktopFunctions.ShowDefaultElements == true)
            {
                if (Shiftorium.IsInitiated == true)
                {
                    if (Shiftorium.UpgradeInstalled("wm_panel_buttons"))
                    {
                        int pnlbtnLeft = 0;
                        foreach (WindowBorder form in Engine.AppearanceManager.OpenForms)
                        {
                            if (form != null)
                            {
                                if (form.Visible == true)
                                {
                                    GwenEventHandler<ClickedEventArgs> onClick = (o, a) =>
                                    {
                                        if (form == focused)
                                        {
                                            if (form.IsMinimized)
                                            {
                                                RestoreWindow(form);
                                            }
                                            else
                                            {
                                                MinimizeWindow(form);
                                            }
                                        }
                                        else
                                        {
                                            form.BringToFront();
                                            focused = form;
                                        }
                                    };

                                    var pnlbtn = new ImagePanel(toplevel);
                                    pnlbtn.Y = (LoadedSkin.DesktopPanelPosition == 1) ? (this.Height - desktoppanel.Height) : 0 + LoadedSkin.PanelButtonFromTop;
                                    pnlbtn.X = LoadedSkin.PanelButtonHolderFromLeft + pnlbtnLeft;
                                    pnlbtn.BackgroundColor = LoadedSkin.PanelButtonColor;
                                    var img = GetImage("panelbutton");
                                    if (img != null)
                                    {
                                        var tex = new Texture(renderer);
                                        tex.LoadRaw(img.Width, img.Height, ImageToBinary(img));
                                        pnlbtn.Image = tex;
                                    }
                                    pnlbtn.BackgroundImageLayout = (int)GetImageLayout("panelbutton");

                                    var pnlbtntext = new Label(toplevel);
                                    pnlbtntext.AutoSizeToContents = true;
                                    pnlbtntext.Text = NameChangerBackend.GetName(form.ParentWindow);
                                    pnlbtntext.X = pnlbtn.X + LoadedSkin.PanelButtonFromLeft.X;
                                    pnlbtntext.Y = pnlbtn.Y + LoadedSkin.PanelButtonFromLeft.Y;
                                    pnlbtntext.TextColor = LoadedSkin.PanelButtonTextColor;
                                    
                                    pnlbtn.BackgroundColor = LoadedSkin.PanelButtonColor;
                                    pnlbtn.Size = LoadedSkin.PanelButtonSize;
                                    pnlbtn.Show();
                                    pnlbtntext.Show();

                                    if (Shiftorium.UpgradeInstalled("useful_panel_buttons"))
                                    {
                                        pnlbtn.Clicked += onClick;
                                        pnlbtntext.Clicked += onClick;
                                    }
                                    pnlbtntext.Font = ControlManager.CreateGwenFont(renderer, LoadedSkin.PanelButtonFont);
                                    pnlbtnLeft += pnlbtn.Width + 7;

                                    PanelButtonElements.Enqueue(pnlbtn);
                                    PanelButtonElements.Enqueue(pnlbtntext);

                                }
                            }
                        }
                    }
                }
            }

            LuaInterpreter.RaiseEvent("on_panelbutton_populate", this);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //We'll run all the queued methods.
            while(actionsToRun.Count > 0)
            {
                actionsToRun.Dequeue()?.Invoke();
            }

            this.WindowState = WindowState.Fullscreen;
            desktoppanel.Width = this.ClientRectangle.Width;
            int dpStart = (LoadedSkin.DesktopPanelPosition == 0) ? 0 : this.Height - desktoppanel.Height;
            if (Shiftorium.IsInitiated == true)
            {
                if (SaveSystem.CurrentSave != null && TutorialManager.IsInTutorial == false)
                {
                    lbtime.Text = Applications.Terminal.GetTime();
                    lbtime.X = desktoppanel.Width - lbtime.Width - LoadedSkin.DesktopPanelClockFromRight.X;
                    lbtime.Y = dpStart + LoadedSkin.DesktopPanelClockFromRight.Y;
                }
            }



            try
            {
                btnnotifications.X = lbtime.X - btnnotifications.Width - 2;
                btnnotifications.Y = dpStart + ((desktoppanel.Height - btnnotifications.Height) / 2);
                btnnotifications.TextColor = LoadedSkin.ControlTextColor;
            }
            catch { }
            GL.Viewport(ClientRectangle);
            GL.Scale(1, 1, 1);
            base.OnUpdateFrame(e);

        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == OpenTK.Input.Key.Escape)
                Exit();

            input.ProcessKeyDown(e);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            input.ProcessKeyUp(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            input.ProcessMouseMessage(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            input.ProcessMouseMessage(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            input.ProcessMouseMessage(e);
            
        }

        private readonly Queue<Base> PanelButtonElements = new Queue<Base>();


        /// <summary>
        /// Setups the desktop.
        /// </summary>
        /// <returns>The desktop.</returns>
        public void SetupDesktop()
        {
            this.WindowState = WindowState.Fullscreen;

            if (DesktopFunctions.ShowDefaultElements == true)
            {
                this.WindowState = WindowState.Fullscreen;
                desktoppanel.BackgroundColor = Color.Green;

                //upgrades

                if (Shiftorium.IsInitiated == true)
                {
                    desktoppanel.IsVisible = Shiftorium.UpgradeInstalled("desktop");
                    lbtime.IsVisible = Shiftorium.UpgradeInstalled("desktop_clock_widget");

                    if (Shiftorium.UpgradeInstalled("panel_notifications"))
                        btnnotifications.Show();
                    else
                        btnnotifications.Hide();

                    //skinning
                    lbtime.TextColor = LoadedSkin.DesktopPanelClockColor;

                    appButton.IsVisible = Shiftorium.UpgradeInstalled("app_launcher");

                    desktoppanel.BackgroundColor = LoadedSkin.DesktopPanelColor;

                    var pnlimg = GetImage("desktoppanel");
                    if (pnlimg != null)
                    {
                        var bmp = new Bitmap(pnlimg);
                        bmp.MakeTransparent(Color.FromArgb(1, 0, 1));
                        pnlimg = bmp;
                    }

                    if (pnlimg != null)
                    {
                        var tex = new Texture(renderer);
                        tex.LoadRaw(pnlimg.Width, pnlimg.Height, ImageToBinary(pnlimg));
                        desktoppanel.Image = tex;
                    }
                    var appimg = GetImage("applauncher");
                    if (appimg != null)
                    {
                        var bmp = new Bitmap(appimg);
                        bmp.MakeTransparent(Color.FromArgb(1, 0, 1));
                        appimg = bmp;
                    }
                    if (appimg != null)
                    {
                        var tex = new Texture(renderer);
                        tex.LoadRaw(appimg.Width, appimg.Height, SkinEngine.ImageToBinary(appimg));
                        appButton.Image = tex;
                    }
                    lbtime.TextColor = LoadedSkin.DesktopPanelClockColor;
                    lbtime.Font = ControlManager.CreateGwenFont(renderer, LoadedSkin.DesktopPanelClockFont);
                    appButton.Size = LoadedSkin.AppLauncherHolderSize;
                    desktoppanel.BackgroundImageLayout = (int)GetImageLayout("desktoppanel");
                    desktoppanel.Height = LoadedSkin.DesktopPanelHeight;
                    if (LoadedSkin.DesktopPanelPosition == 1)
                    {
                        desktoppanel.Dock = Pos.Bottom;
                    }
                    else
                    {
                        desktoppanel.Dock = Pos.Top;
                    }
                    int dpStart = (LoadedSkin.DesktopPanelPosition == 0) ? 0 : this.Height - desktoppanel.Height;
                    appButton.Location = new Point(desktoppanel.X + LoadedSkin.AppLauncherFromLeft.X, dpStart + LoadedSkin.AppLauncherFromLeft.Y);

                    appButton.Clicked += new GwenEventHandler<ClickedEventArgs>(al_clicked);
                }
            }
            else
            {
                desktoppanel.Hide();
            }

            LuaInterpreter.RaiseEvent("on_desktop_skin", this);
            
            PopulatePanelButtons();
            toplevel.RenderCanvas();
        }

        public void al_clicked (object o, ClickedEventArgs e){
            // called when the applauncher button is clicked

            /*renderer.Begin();
            renderer.DrawColor = Color.FromArgb(255, 255, 255, 255);

            Menu menu = new Menu(appButton);
            Point local = appButton.CanvasPosToLocal(new Point(e.X, e.Y));
            menu.SetPosition(local.X, local.Y);
            menu.BackgroundColor = Color.FromArgb(255, 0, 0, 0);
            menu.AddItem("Test");
            menu.AddItem("Clickable").Clicked += delegate (Base sender2, ClickedEventArgs args2) {
                Infobox.Show("clicked", "yes");
            };
            menu.Show();
            renderer.End();*/

            ShiftMenu alPanel = new ShiftMenu(toplevel);
            alPanel.BackgroundColor = LoadedSkin.Menu_ToolStripDropDownBackground;
            int maxX = 0;
            desktoppanel.AddChild(alPanel);

            Action c = null;

            foreach (var itm in AppLauncherDaemon.Available()) {


                Button alButton = new Button(alPanel);

                

                alButton.RenderHint = RenderHintConstants.AL_ITEM;

                
                alButton.Clicked += (obj, a) =>
                {
                    if (itm is LuaLauncherItem)
                    {
                        var i = new LuaInterpreter();
                        i.ExecuteFile((itm as LuaLauncherItem).LaunchPath);
                    }
                    else
                    {
                        var win = (IShiftOSWindow)Activator.CreateInstance(itm.LaunchType, null);
                        AppearanceManager.SetupWindow(win);
                    }
                    buttonClicked?.Invoke();
                };

                alButton.TextColor = LoadedSkin.Menu_SelectedTextColor;
                alButton.Redraw();
                if (itm is LuaLauncherItem)
                {
                    alButton.Text = itm.DisplayData.Name;
                }
                else
                {
                    alButton.Text = NameChangerBackend.GetNameRaw(itm.LaunchType);

                    var icn = GetIcon(itm.LaunchType.Name);
                    if (icn != null)
                    {
                        var tex = new Texture(renderer);
                        tex.LoadRaw(icn.Width, icn.Height, ImageToBinary(icn));
                        alButton.SetImage(tex);
                    }
                }

                if (c == null)
                {
                    c = () =>
                    {
                        alPanel.Hide();
                        alPanel.Dispose();
                        
                        buttonClicked -= c;
                    };
                    buttonClicked += c;
                }
                alButton.Text = alButton.Text; //update the text so that it refreshes, and puts the text over the image
                alButton.Width = alButton.TextWidth + 24;
                int dpStart = (LoadedSkin.DesktopPanelPosition == 0) ? appButton.Height : this.Height - desktoppanel.Height;
                alPanel.AddMenuItem(alButton);

                Point pos = new Point(0, dpStart);
                alPanel.SetPosition(pos.X, pos.Y);
            }
        }

        private event Action buttonClicked;

        public MenuItem GetALCategoryWithName(string text)
        {
            var itm = new MenuItem(apps);
            itm.Text = text;
            return itm;
        }

        /// <summary>
        /// Populates the app launcher.
        /// </summary>
        /// <returns>The app launcher.</returns>
        /// <param name="items">Items.</param>
        public void PopulateAppLauncher(LauncherItem[] items)
        {
            if(SkinEngine.LoadedSkin != null)
                apps.Repopulate(items);
        }


        /// <summary>
        /// Desktops the load.
        /// </summary>
        /// <returns>The load.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void Desktop_Load(object sender, EventArgs e)
        {

            SaveSystem.Begin();

            SetupDesktop();

            SaveSystem.GameReady += () =>
            {
                LuaInterpreter.RaiseEvent("on_desktop_load", this);
            };
        }

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <returns>The window.</returns>
        /// <param name="border">Border.</param>
        public void ShowWindow(IWindowBorder border)
        {
            var brdr = border as System.Windows.Forms.Form;
            focused = border;
            brdr.GotFocus += (o, a) =>
            {
                focused = border;
            };
            brdr.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            brdr.Show();
            brdr.TopMost = true;
        }

        /// <summary>
        /// Kills the window.
        /// </summary>
        /// <returns>The window.</returns>
        /// <param name="border">Border.</param>
        public void KillWindow(IWindowBorder border)
        {
            border.Close();
        }

        private IWindowBorder focused = null;

        public string DesktopName
        {
            get
            {
                return "ShiftOS Desktop";
            }
        }


        /// <summary>
        /// Minimizes the window.
        /// </summary>
        /// <param name="brdr">Brdr.</param>
        public void MinimizeWindow(IWindowBorder brdr)
        {
            var loc = (brdr as WindowBorder).Location;
            var sz = (brdr as WindowBorder).Size;
            (brdr as WindowBorder).Tag = JsonConvert.SerializeObject(new
            {
                Size = sz,
                Location = loc
            });
            (brdr as WindowBorder).Location = new Point(this.GetSize().Width * 2, this.GetSize().Height * 2);
        }

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        /// <returns>The window.</returns>
        /// <param name="brdr">Brdr.</param>
        public void MaximizeWindow(IWindowBorder brdr)
        {
            int startY = (LoadedSkin.DesktopPanelPosition == 1) ? 0 : LoadedSkin.DesktopPanelHeight;
            int h = this.GetSize().Height - LoadedSkin.DesktopPanelHeight;
            var loc = (brdr as WindowBorder).Location;
            var sz = (brdr as WindowBorder).Size;
            (brdr as WindowBorder).Tag = JsonConvert.SerializeObject(new
            {
                Size = sz,
                Location = loc
            });
            (brdr as WindowBorder).Location = new Point(0, startY);
            (brdr as WindowBorder).Size = new Size(this.GetSize().Width, h);

        }

        /// <summary>
        /// Restores the window.
        /// </summary>
        /// <returns>The window.</returns>
        /// <param name="brdr">Brdr.</param>
        public void RestoreWindow(IWindowBorder brdr)
        {
            dynamic tag = JsonConvert.DeserializeObject<dynamic>((brdr as WindowBorder).Tag.ToString());
            (brdr as WindowBorder).Location = tag.Location;
            (brdr as WindowBorder).Size = tag.Size;

        }

        /// <summary>
        /// Invokes the on worker thread.
        /// </summary>
        /// <returns>The on worker thread.</returns>
        /// <param name="act">Act.</param>
        public void InvokeOnWorkerThread(Action act)
        {
            this.Invoke(act);
        }

        public void OpenAppLauncher(Point loc)
        {
            apps.MenuVisible = true;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <returns>The size.</returns>
        public Size GetSize()
        {
            return this.Size;
        }

        private void btnnotifications_Click(object sender, EventArgs e)
        {
            AppearanceManager.SetupWindow(new Applications.Notifications());
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }

    [ShiftOS.Engine.Scripting.Exposed("desktop")]
    public class DesktopFunctions
    {
        public static bool ShowDefaultElements = true;

        public dynamic getWindow()
        {
            return Desktop.CurrentDesktop;
        }

        public void showDefaultElements(bool val)
        {
            ShowDefaultElements = val;
            SkinEngine.LoadSkin();
        }

        public dynamic getOpenWindows()
        {
            return AppearanceManager.OpenForms;
        }

        public string getALItemName(LauncherItem kv)
        {
            return (kv.LaunchType == null) ? kv.DisplayData.Name : Applications.NameChangerBackend.GetNameRaw(kv.LaunchType);
        }

        public void openAppLauncher(Point loc)
        {
            Desktop.OpenAppLauncher(loc);
        }

        public string getWindowTitle(IWindowBorder form)
        {
            return NameChangerBackend.GetName(form.ParentWindow);
        }

        public void openApp(LauncherItem kv)
        {
            if (kv is LuaLauncherItem)
            {
                var interpreter = new Engine.Scripting.LuaInterpreter();
                interpreter.ExecuteFile((kv as LuaLauncherItem).LaunchPath);
            }
            else
            {
                Engine.AppearanceManager.SetupWindow(Activator.CreateInstance(kv.LaunchType) as IShiftOSWindow);
            }
        }
    }
}