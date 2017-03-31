//#define DIRECTXDRIVER

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using D3D11 = SharpDX.Direct3D11;

namespace ShiftOS.Engine
{
#if DIRECTXDRIVER
    [Obsolete("If one can fix this, go ahead.")]
    public class DirectXGraphicsAPI : GraphicsAPI, IDisposable
    {
        private D3D11.Device d3dDevice;
        private D3D11.DeviceContext d3dDeviceContext;
        private SwapChain swapChain;
        private D3D11.RenderTargetView renderTargetView;
        private D3D11.VertexShader vertexShader;
        private D3D11.PixelShader pixelShader;

        private void InitializeShaders()
        {
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile("defaultShader.hlsl", "vertex_main", "vs_4_0", ShaderFlags.Debug))
            {
                vertexShader = new D3D11.VertexShader(d3dDevice, vertexShaderByteCode);
            }
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile("defaultShader.hlsl", "pixel_main", "ps_4_0", ShaderFlags.Debug))
            {
                pixelShader = new D3D11.PixelShader(d3dDevice, pixelShaderByteCode);
            }
        }

        ModeDescription backBufferDesc;

        SwapChainDescription swapChainDesc;

        private SharpDX.Windows.RenderForm _renderForm;

        public DirectXGraphicsAPI(int width, int height) : base(width, height)
        {
        }

        public void Dispose()
        {
            try
            {
                renderTargetView.Dispose();
                swapChain.Dispose();
                d3dDevice.Dispose();
                d3dDeviceContext.Dispose();
                _renderForm.Dispose();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Print($"[Engine] <DX11/WARN> " + ex.ToString());
            }
            }

        public override void OnRenderFrame()
        {
            d3dDeviceContext.OutputMerger.SetRenderTargets(renderTargetView);
            d3dDeviceContext.ClearRenderTargetView(renderTargetView, new SharpDX.Color4(1.0f, 0f, 0f, 1.0f));
            swapChain.Present(1, PresentFlags.None);

            base.OnRenderFrame();
        }

        public override void DrawPolygon(Color color, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft)
        {
           
        }

        public override void DrawPolygon(byte[] textureRaw, int w, int h, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft)
        {
            
        }

        protected override void Init()
        {
            _renderForm = new SharpDX.Windows.RenderForm("ShiftOS");
            _renderForm.ClientSize = new Size(Width, Height);
            _renderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            _renderForm.WindowState = FormWindowState.Maximized;
            
            backBufferDesc = new ModeDescription(Width, Height, new Rational(60, 1), Format.R8G8B8A8_UNorm);
            swapChainDesc = new SwapChainDescription()
            {
                ModeDescription = backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = _renderForm.Handle,
            };
            
            D3D11.Device.CreateWithSwapChain(DriverType.Hardware, D3D11.DeviceCreationFlags.None, swapChainDesc, out d3dDevice, out swapChain);
            d3dDeviceContext = d3dDevice.ImmediateContext;

            using (D3D11.Texture2D backBuffer = swapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                renderTargetView = new D3D11.RenderTargetView(d3dDevice, backBuffer);
            }

            base.Init();
            SharpDX.Windows.RenderLoop.Run(_renderForm, OnRenderFrame);
        }

        public void StartRenderLoop()
        {
            int milliseconds = 1000 / 60;
            while (_renderForm.Visible == true)
            {
                base.OnRenderFrame();
                Thread.Sleep(milliseconds);
            }
        }
    }
#endif
}
