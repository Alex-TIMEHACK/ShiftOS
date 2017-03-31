using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftOS.Engine;
using ShiftOS.Engine.Composition;
using ShiftOS.Engine.Composition.UI;
using static ShiftOS.Engine.SkinEngine;

namespace ShiftOS.WinForms
{
    public class CompositingDesktop : Engine.Composition.UI.Window, IDesktop
    {
        public override void Draw()
        {
            SetupDesktop();
        }

        private Screen screen;

        public void Show()
        {
            SaveSystem.GameReady += () =>
            {
                SetupDesktop();
            };
            SaveSystem.Begin(false);
        }

        public CompositingDesktop(Screen scn) : base(scn.Width, scn.Height)
        {
            screen = scn;
            this.Location = new Point(0, 0);
        }

        

        public string DesktopName
        {
            get
            {
                return "Compositing Desktop";
            }
        }

        public void Close()
        {
            //Shouldn't this just...uhhhh....shut down the OS? xD
        }

        public Size GetSize()
        {
            return this.Size;
        }

        public void InvokeOnWorkerThread(Action act)
        {
            act?.Invoke();
        }

        public void KillWindow(IWindowBorder border)
        {
            
        }

        public void MaximizeWindow(IWindowBorder brdr)
        {
        }

        public void MinimizeWindow(IWindowBorder brdr)
        {
        }

        public void OpenAppLauncher(Point loc)
        {
        }

        public void PopulateAppLauncher(LauncherItem[] items)
        {
        }

        public void PopulatePanelButtons()
        {
        }

        public void RestoreWindow(IWindowBorder brdr)
        {
        }

        public void SetupDesktop()
        {
            //First, we draw the desktop background.
            Clear(Color.White);
            
        }

        public void ShowWindow(IWindowBorder border)
        {
        }
    }
}
