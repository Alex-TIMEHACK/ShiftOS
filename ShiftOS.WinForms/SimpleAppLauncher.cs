using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwen;
using ShiftOS.Engine;
using ShiftOS.WinForms.Tools;
using static ShiftOS.Engine.SkinEngine;
namespace ShiftOS.WinForms
{
    public class SimpleAppLauncher : Gwen.Control.Base
    {
        private int desktop_height = 0;
        

        public SimpleAppLauncher(Gwen.Control.Canvas win) : base(win)
        {
            this.IsVisible = false;
            desktop_height = win.Height;    
        }

        /// <summary>
        /// Calculates the start position of the App Launcher menu.
        /// </summary>
        /// <returns>The Y coordinate of the start pos.</returns>
        private int GetStartPosition(int itemCount)
        {
            if (LoadedSkin.DesktopPanelPosition == 0)
                return LoadedSkin.DesktopPanelHeight;
            else
            {
                int totalAppHeight = 0;
                if (Shiftorium.UpgradeInstalled("al_shutdown"))
                {
                    totalAppHeight += 30;
                }
                totalAppHeight += 24 * itemCount;
                return desktop_height - LoadedSkin.DesktopPanelHeight - totalAppHeight;
            }

        }

        private int GetLongestWidth()
        {
            int width = 24; //total border width + icon space

            int last = 0;

            foreach(var itm in items)
            {
                var r = Skin.Renderer;
                int w = r.MeasureText(ControlManager.CreateGwenFont(r, LoadedSkin.MainFont), itm.Text).X;
                if (w > last)
                    last = w;
            }

            return width;
        }

        public int GetHeight()
        {
            return 30 * AppLauncherDaemon.Available().Count;
        }

        private readonly List<AppLauncherMenuItem> items = new List<AppLauncherMenuItem>();

        private bool _MenuVisible = false;

        public bool MenuVisible
        {
            get
            {
                return _MenuVisible;
            }
            set
            {
                _MenuVisible = value;
                Redraw();
            }
        }

        public void Repopulate(LauncherItem[] itms)
        {
            this.X = 0;
            this.Y = GetStartPosition(itms.Length);

            this.items.Clear();
            foreach(var itm in itms)
            {
                items.Add(new AppLauncherMenuItem(this, itm));
            }
            this.Width = GetLongestWidth();
            this.Height = GetHeight();
        }

        public override void Redraw()
        {
            if(MenuVisible == true)
            {
                var r = Skin.Renderer;
                r.Begin();
                r.DrawColor = LoadedSkin.Menu_MenuBorder;
                var rect = new System.Drawing.Rectangle(0, this.GetStartPosition(AppLauncherDaemon.Available().Count), this.GetLongestWidth(), this.GetHeight() + 4/*padding*/);
                r.DrawFilledRect(rect);
                r.DrawColor = LoadedSkin.Menu_ToolStripDropDownBackground;
                var dropRect = new System.Drawing.Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
                r.DrawFilledRect(dropRect);

                r.End();
            }
            else
            {

            }
        }
    }

    public class AppLauncherMenuItem : Gwen.Control.Base
    {
        private string item_text = "Item";
        private System.Drawing.Image icon = null;
        private Type launch_type = null;
        private bool is_lua = false;
        private string lua_path = "";
        private SimpleAppLauncher parentLauncher = null;
        private bool isMouseOver = false;
        
        public string Text
        {
            get
            {
                return item_text;
            }
        }

        protected override void OnMouseEntered()
        {
            isMouseOver = true;
            base.OnMouseEntered();
        }

        protected override void OnMouseLeft()
        {
            isMouseOver = false;
            base.OnMouseEntered();
        }

        public AppLauncherMenuItem(SimpleAppLauncher launcher, LauncherItem itemData) : base(launcher)
        {
            is_lua = (itemData is LuaLauncherItem);

            if (is_lua == false)
            {
                item_text = Applications.NameChangerBackend.GetNameRaw(itemData.LaunchType);
            }
            else
            {
                item_text = itemData.DisplayData.Name;
            }

            icon = GetIcon(itemData.LaunchType.Name);

            lua_path = (is_lua == true) ? (itemData as LuaLauncherItem).LaunchPath : null;

            launch_type = itemData.LaunchType;

            parentLauncher = launcher;
        }

        protected override void OnMouseClickedLeft(int x, int y, bool down)
        {
            if(is_lua == true)
            {
                var interp = new ShiftOS.Engine.Scripting.LuaInterpreter();
                interp.ExecuteFile(lua_path);
            }
            else
            {
                if(launch_type is IShiftOSWindow)
                {
                    IShiftOSWindow win = (IShiftOSWindow)Activator.CreateInstance(launch_type, null);
                    AppearanceManager.SetupWindow(win);
                }
            }
            parentLauncher.MenuVisible = false;
            base.OnMouseClickedLeft(x, y, down);
        }


    }
}
