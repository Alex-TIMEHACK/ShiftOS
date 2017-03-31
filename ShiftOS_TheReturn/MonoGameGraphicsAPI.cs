using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ShiftOS.Engine.Composition
{
    public class OpenTKGraphicsAPI : GraphicsAPI
    {
        public OpenTKGraphicsAPI(int width, int height) : base(width, height)
        {
        }

        public override PointF ConvertCoordsToDriver(PointF point)
        {
            return new PointF(
                    (float)linear(point.X, 0, this.Width, -1, 1),
                    (float)linear(point.Y, 0, this.Height, -1, 1)
                );
        }

        public override PointF ConvertCoordsToEngine(PointF point)
        {
            return new PointF(
                    (float)linear(point.X, -1, 1, 0, this.Width),
                    (float)linear(point.Y, -1, 1, 0, this.Height)
                );
        }

        public override void OnRenderFrame()
        {
            base.OnRenderFrame();
        }

        protected override void Init()
        {
            using(var game = new OpenTKGameWindow(Width, Height))
            {
                game.FrameRenderBegun += (f) => { this.OnRenderFrame(); };
                game.Run();
            }
        }

        public override void DrawPolygon(Color color, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft)
        {
            GL.Begin(PrimitiveType.Polygon);
            float x = 0;
            float y = 0;
            float z = 0;

            x = (float)linear(topleft.X, 0, this.Width, -1, 1);
            z = (float)linear(topleft.Z, 0, this.Height, -1, 1);
            y = (float)topleft.Y;
            GL.Color4(color);
            GL.Vertex3(x, y, z);

            x = (float)linear(topright.X, 0, this.Width, -1, 1);
            z = (float)linear(topright.Z, 0, this.Height, -1, 1);
            y = (float)topright.Y;
            GL.Color4(color);
            GL.Vertex3(x, y, z);

            x = (float)linear(bottomleft.X, 0, this.Width, -1, 1);
            z = (float)linear(bottomleft.Z, 0, this.Height, -1, 1);
            y = (float)bottomleft.Y;
            GL.Color4(color);
            GL.Vertex3(x, y, z);

            x = (float)linear(bottomright.X, 0, this.Width, -1, 1);
            z = (float)linear(bottomright.Z, 0, this.Height, -1, 1);
            y = (float)bottomright.Y;
            GL.Color4(color);
            GL.Vertex3(x, y, z);
            
            GL.End();
        }

        static public double linear(double x, double x0, double x1, double y0, double y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }

        private int LoadTexture(byte[] binary)
        {
            Bitmap bitmap = (Bitmap)SkinEngine.ImageFromBinary(binary);

            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return tex;
        }

        public override void DrawPolygon(byte[] textureRaw, int w, int h, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft)
        {
            var image = LoadTexture(textureRaw);

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();

            GL.Ortho(0, 800, 0, 600, -1, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();

            GL.Disable(EnableCap.Lighting);

            GL.Enable(EnableCap.Texture2D);

            GL.Color4(Color.White);

            GL.BindTexture(TextureTarget.Texture2D, image);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex3(topleft.X, topleft.Z, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex3(topright.X, topright.Z, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex3(bottomright.X, bottomright.Z, 0);

            GL.TexCoord2(0, 1);
            GL.Vertex3(bottomleft.X, bottomleft.Z, 0);

            GL.End();

            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();

            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();

            GL.MatrixMode(MatrixMode.Modelview);
        }
    }

    public class OpenTKGameWindow : OpenTK.GameWindow
    {
        public OpenTKGameWindow(int w, int h) : base(w, h, OpenTK.Graphics.GraphicsMode.Default, "ShiftOS")
        {
            VSync = OpenTK.VSyncMode.Adaptive;
            this.WindowState = WindowState.Fullscreen;
        }

        public event Action<FrameEventArgs> FrameRenderBegun;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.Ortho(0, this.Width, this.Height, 0, 0, 1);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            base.OnUpdateFrame(e);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.PushMatrix();
            FrameRenderBegun?.Invoke(e);

            SwapBuffers();
        }
    }
}
