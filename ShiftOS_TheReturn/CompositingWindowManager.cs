using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftOS.Engine
{
    public class CompositingWindowManager : WindowManager
    {



        public override void Close(IShiftOSWindow win)
        {
            throw new NotImplementedException();
        }

        public override void InvokeAction(Action act)
        {
            throw new NotImplementedException();
        }

        public override void Maximize(IWindowBorder border)
        {
            throw new NotImplementedException();
        }

        public override void Minimize(IWindowBorder border)
        {
            throw new NotImplementedException();
        }

        public override void SetTitle(IShiftOSWindow win, string title)
        {
            throw new NotImplementedException();
        }

        public override void SetupDialog(IShiftOSWindow win)
        {
            throw new NotImplementedException();
        }

        public override void SetupWindow(IShiftOSWindow win)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class GraphicsAPI
    {
        private int _width = 0;
        private int _height = 0;

        public GraphicsAPI(int width, int height)
        {
            _width = width;
            _height = height;
            Init(); //Set up the low-level API.
        }


        /// <summary>
        /// When overidden, this method sets up the low-level 3D graphics API for compositing window managers to use. This should do things like set up graphics contexts, register devices, etc, set the screen to the desired resolution, and clear it to black.
        /// </summary>
        protected virtual void Init()
        {
            //Engine bootstrapper should go here eventually.
        }

        /// <summary>
        /// Draws a polygon onto the 3D world using the specified color and 3D points.
        /// </summary>
        /// <param name="color">The color of the polygon.</param>
        /// <param name="points">The vertices of the polygon.</param>
        public abstract void DrawPolygon(System.Drawing.Color color, params Vector3D[] points);

        /// <summary>
        /// Draws a polygon onto the 3D world using the specified 32-bit ARGB bitmap data and 3D points.
        /// </summary>
        /// <param name="textureRaw">A byte[] array representing a 32-bit ARGB bitmap.</param>
        /// <param name="w">The width, in pixels, of the texture.</param>
        /// <param name="h">The height, in pixels, of the texture.</param>
        /// <param name="points">The vertices of the polygon.</param>
        public abstract void DrawPolygon(byte[] textureRaw, int w, int h, params Vector3D[] points);


        /// <summary>
        /// Draws a polygon onto the 3D world using the specified <see cref="System.Drawing.Image"/>  and 3D points.
        /// </summary>
        /// <param name="texture">A <see cref="System.Drawing.Image"/> to be drawn as a texture.</param>
        /// <param name="points">The vertices of the polygon.</param>
        ///<remarks>
        ///     <para>This method doesn't need to be overidden as it will simply use the <see cref="ShiftOS.Engine.SkinEngine"/> ImageToBinary method to extract the binary data of the image and use the DrawPolygon(byte[]) overload. However, if your graphics API supports direct drawing of <see cref="System.Drawing.Image"/> textures, use its method instead, as it may be faster!</para>
        /// </remarks>
        public virtual void DrawPolygon(System.Drawing.Image texture, params Vector3D[] points)
        {
            var width = texture.Width;
            var height = texture.Height;
            var binary = SkinEngine.ImageToBinary(texture);
            DrawPolygon(binary, width, height, points);
        }

        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// This method should be called each frame - and should be used to render objects.
        /// </summary>
        public virtual void OnRenderFrame()
        {

        }
    }

    public struct Vector3D
    {
        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X
        {
            get; set;
        }

        public float Y
        {
            get; set;
        }

        public float Z
        {
            get; set;
        }

    }
}
