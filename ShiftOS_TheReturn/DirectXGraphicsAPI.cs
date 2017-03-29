using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Windows;

namespace ShiftOS.Engine
{
    public class DirectXGraphicsAPI : GraphicsAPI, IDisposable
    {
        private RenderForm _renderForm;

        public DirectXGraphicsAPI(int width, int height) : base(width, height)
        {
        }

        public void Dispose()
        {
            _renderForm.Dispose();
        }

        public override void DrawPolygon(Color color, params Vector3D[] points)
        {
            throw new NotImplementedException();
        }

        public override void DrawPolygon(byte[] textureRaw, int w, int h, params Vector3D[] points)
        {
            throw new NotImplementedException();
        }

        protected override void Init()
        {
            _renderForm = new RenderForm("ShiftOS.Engine - Compositing Window Manager");
            _renderForm.ClientSize = new Size(Width, Height);
            _renderForm.AllowUserResizing = false;
            _renderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            _renderForm.IsFullscreen = true;
            base.Init();
            RenderLoop.Run(_renderForm, this.OnRenderFrame);
        }
    }
}
