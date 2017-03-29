using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;
using D3D11 = SharpDX.Direct3D11;

namespace ShiftOS.Engine
{
    public class DirectXGraphicsAPI : GraphicsAPI, IDisposable
    {
        private D3D11.Device d3dDevice;
        private D3D11.DeviceContext d3dDeviceContext;
        private SwapChain swapChain;
        private D3D11.RenderTargetView renderTargetView;

        ModeDescription backBufferDesc;

        SwapChainDescription swapChainDesc;

        private RenderForm _renderForm;

        public DirectXGraphicsAPI(int width, int height) : base(width, height)
        {
        }

        public void Dispose()
        {
            renderTargetView.Dispose();
            swapChain.Dispose();
            d3dDevice.Dispose();
            d3dDeviceContext.Dispose();
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

            backBufferDesc = new ModeDescription(Width, Height, new Rational(60, 1), Format.R8G8B8A8_UNorm);
            swapChainDesc = new SwapChainDescription()
            {
                ModeDescription = backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = _renderForm.Handle,
                IsWindowed = false
            };

            D3D11.Device.CreateWithSwapChain(DriverType.Hardware, D3D11.DeviceCreationFlags.None, swapChainDesc, out d3dDevice, out swapChain);
            d3dDeviceContext = d3dDevice.ImmediateContext;

            using (D3D11.Texture2D backBuffer = swapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                renderTargetView = new D3D11.RenderTargetView(d3dDevice, backBuffer);
            }

            base.Init();
            RenderLoop.Run(_renderForm, this.OnRenderFrame);
        }
    }
}
