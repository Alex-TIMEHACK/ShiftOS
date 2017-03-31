using System;
using System.Collections.Generic;
using System.Drawing;
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

        private int LoadTexture(byte[] binary, int quality = 0, bool repeat = true, bool flip_y = false)
        {
            Bitmap bitmap = (Bitmap)SkinEngine.ImageFromBinary(binary);

            //Flip the image
            if (flip_y)
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            //Generate a new texture target in gl
            int texture = GL.GenTexture();

            //Will bind the texture newly/empty created with GL.GenTexture
            //All gl texture methods targeting Texture2D will relate to this texture
            GL.BindTexture(TextureTarget.Texture2D, texture);

            //The reason why your texture will show up glColor without setting these parameters is actually
            //TextureMinFilters fault as its default is NearestMipmapLinear but we have not established mipmapping
            //We are only using one texture at the moment since mipmapping is a collection of textures pre filtered
            //I'm assuming it stops after not having a collection to check.
            switch (quality)
            {
                case 0:
                default://Low quality
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
                    break;
                case 1://High quality
                       //This is in my opinion the best since it doesnt average the result and not blurred to shit
                       //but most consider this low quality...
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
                    break;
            }

            if (repeat)
            {
                //This will repeat the texture past its bounds set by TexImage2D
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);
            }
            else
            {
                //This will clamp the texture to the edge, so manipulation will result in skewing
                //It can also be useful for getting rid of repeating texture bits at the borders
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
            }

            //Creates a definition of a texture object in opengl
            /* Parameters
             * Target - Since we are using a 2D image we specify the target Texture2D
             * MipMap Count / LOD - 0 as we are not using mipmapping at the moment
             * InternalFormat - The format of the gl texture, Rgba is a base format it works all around
             * Width;
             * Height;
             * Border - must be 0;
             * 
             * Format - this is the images format not gl's the format Bgra i believe is only language specific
             *          C# uses little-endian so you have ARGB on the image A 24 R 16 G 8 B, B is the lowest
             *          So it gets counted first, as with a language like Java it would be PixelFormat.Rgba
             *          since Java is big-endian default meaning A is counted first.
             *          but i could be wrong here it could be cpu specific :P
             *          
             * PixelType - The type we are using, eh in short UnsignedByte will just fill each 8 bit till the pixelformat is full
             *             (don't quote me on that...)
             *             you can be more specific and say for are RGBA to little-endian BGRA -> PixelType.UnsignedInt8888Reversed
             *             this will mimic are 32bit uint in little-endian.
             *             
             * Data - No data at the moment it will be written with TexSubImage2D
             */
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            //Load the data from are loaded image into virtual memory so it can be read at runtime
            System.Drawing.Imaging.BitmapData bitmap_data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //Writes data to are texture target
            /* Target;
             * MipMap;
             * X Offset - Offset of the data on the x axis
             * Y Offset - Offset of the data on the y axis
             * Width;
             * Height;
             * Format;
             * Type;
             * Data - Now we have data from the loaded bitmap image we can load it into are texture data
             */
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, bitmap.Width, bitmap.Height, PixelFormat.Bgra, PixelType.UnsignedByte, bitmap_data.Scan0);

            //Release from memory
            bitmap.UnlockBits(bitmap_data);

            //get rid of bitmap object its no longer needed in this method
            bitmap.Dispose();

            /*Binding to 0 is telling gl to use the default or null texture target
            *This is useful to remember as you may forget that a texture is targeted
            *And may overflow to functions that you dont necessarily want to
            *Say you bind a texture
            *
            * Bind(Texture);
            * DrawObject1();
            *                <-- Insert Bind(NewTexture) or Bind(0)
            * DrawObject2();
            * 
            * Object2 will use Texture if not set to 0 or another.
            */
            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texture;
        }

        public override void DrawPolygon(byte[] textureRaw, int w, int h, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft)
        {
            var current_texture = LoadTexture(textureRaw);
            
            GL.Enable(EnableCap.Texture2D);
            //Basically enables the alpha channel to be used in the color buffer
            GL.Enable(EnableCap.Blend);
            //The operation/order to blend
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //Use for pixel depth comparing before storing in the depth buffer
            GL.Enable(EnableCap.DepthTest);

            GL.Color4(Color.White);

            GL.BindTexture(TextureTarget.Texture2D, current_texture);

            GL.Begin(PrimitiveType.Polygon);

            //Bind texture coordinates to vertices in ccw order
            GL.Color4(Color.White);
            float x = 0;
            float y = 0;
            float z = 0;

            x = (float)linear(topleft.X, 0, this.Width, -1, 1);
            z = (float)linear(topleft.Z, 0, this.Height, -1, 1);
            y = (float)topleft.Y;
            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x, y, z);

            x = (float)linear(topright.X, 0, this.Width, -1, 1);
            z = (float)linear(topright.Z, 0, this.Height, -1, 1);
            y = (float)topright.Y;
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(x, y, z);

            x = (float)linear(bottomleft.X, 0, this.Width, -1, 1);
            z = (float)linear(bottomleft.Z, 0, this.Height, -1, 1);
            y = (float)bottomleft.Y;
            GL.TexCoord2(0f, 1f);
            GL.Vertex3(x, y, z);

            x = (float)linear(bottomright.X, 0, this.Width, -1, 1);
            z = (float)linear(bottomright.Z, 0, this.Height, -1, 1);
            y = (float)bottomright.Y;
            GL.TexCoord2(1f, 1f);
            GL.Vertex3(x, y, z);

            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.PopMatrix();

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
