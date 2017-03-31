using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftOS.Engine.Composition.UI;

namespace ShiftOS.Engine.Composition
{
    public class CompositedBorder : IWindowBorder, IPaintable, IDrawable
    {
        private float _x = 0;
        private float _y = 0;
        private float _width = 0;
        private float _height = 0;

        private string _title = "Window";

        private Screen _screenInfo = null;

        public Point ClientLocation
        {
            get
            {
                int x = 0;
                int y = 0;
                if(Decorated == true)
                {
                    x = SkinEngine.LoadedSkin.LeftBorderWidth;
                    y = SkinEngine.LoadedSkin.TitlebarHeight;
                }
                return new Point(x, y);
            }
        }

        private Window _child = null;

        public CompositedBorder(Screen screen, Size clientSize, string title = "My application")
        {
            if (clientSize.Width < 320)
                clientSize.Width = 320;
            if (clientSize.Height < 200)
                clientSize.Height = 200;
            _title = title;
            _screenInfo = screen;
            SetClientSize(clientSize);
            SetBuffer(this.Size.Width, this.Size.Height);
        }

        public void SetClientSize(Size value)
        {
            int x = ClientLocation.X;
            int y = ClientLocation.Y;
            if(Decorated == true)
            {
                x += SkinEngine.LoadedSkin.RightBorderWidth;
                y += SkinEngine.LoadedSkin.BottomBorderWidth;
            }
            this.Size = new Size(value.Width + x, value.Height + y);
        }

        public Size GetClientSize()
        {
            int x = ClientLocation.X;
            int y = ClientLocation.Y;
            if (Decorated == true)
            {
                x += SkinEngine.LoadedSkin.RightBorderWidth;
                y += SkinEngine.LoadedSkin.BottomBorderWidth;
            }
            return new Size(this.Size.Width - x, this.Size.Height - y);

        }

        private Bitmap _backBuffer = null;

        public Image GetBuffer()
        {
            try
            {
                if (_backBuffer != null)
                    _backBuffer.Save("DebugBitmap.bmp");
            }
            catch
            {

            }
            return _backBuffer;
        }

        public Point Location
        {
            get
            {
                return _screenInfo.ConvertCoordinatesFrom(new PointF(_x, _y));
            }
            set
            {
                var f = _screenInfo.ConvertCoordinatesTo(value);
                _x = f.X;
                _y = f.Y;
            }

        }

        public Size Size
        {
            get
            {
                var point =  _screenInfo.ConvertCoordinatesFrom(new PointF(_width, _height));
                return new Size(point.X, point.Y);
            }
            set
            {
                var f = _screenInfo.ConvertCoordinatesTo(new PointF(value.Width, value.Height));
                _width = f.X;
                _height = f.Y;
            }
        }

        public string Text
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public IShiftOSWindow ParentWindow
        {
            get
            {
                return _child;
            }

            set
            {
                if (value is Window || value.GetType().BaseType == typeof(Window))
                {
                    _child = (Window)value;
                    return;
                }
                throw new InvalidOperationException("You cannot set the window of this composited IWIndowBorder to a window of type " + value.GetType().FullName + ". This window border only supports " + typeof(Window).FullName + ".");
            }
        }

        bool _decorated = true;

        public void Draw()
        {
            if (Decorated == true)
            {
                int title_x = 0;
                int title_width = this.Size.Width;
                int title_height = SkinEngine.LoadedSkin.TitlebarHeight;

                if (AppearanceManager.DecoratorConfig.ShowLeftTitleCorner)
                {
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.TitleLeftBG))
                    {
                        title_x = SkinEngine.LoadedSkin.TitleLeftCornerWidth;
                        TextureRectangle(new Point(0, 0), new Size(title_x, title_height), img);
                        title_width -= title_x;
                    }
                }
                if (AppearanceManager.DecoratorConfig.ShowRightTitleCorner)
                {
                    int width = SkinEngine.LoadedSkin.TitleRightCornerWidth;
                    title_width -= width;
                    int x = title_x + title_width;
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.TitleRightBG))
                    {
                        TextureRectangle(new Point(x, 0), new Size(width, title_height), img);
                    }
                }
                if (AppearanceManager.DecoratorConfig.ShowTitleBar)
                {
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.TitleBarBackground))
                    {
                        TextureRectangle(new Point(title_x, 0), new Size(title_width, title_height), img);
                    }

                    if (AppearanceManager.DecoratorConfig.ShowTitleText)
                    {
                        Font font = SkinEngine.LoadedSkin.TitleFont;
                        bool centered = SkinEngine.LoadedSkin.TitleTextCentered;
                        Color c = SkinEngine.LoadedSkin.TitleTextColor;
                        var point = SkinEngine.LoadedSkin.TitleTextLeft;
                        if (centered)
                        {
                            var textSize = MeasureString(this._title, font);
                            point = new Point(
                                    (title_width - (int)textSize.Width) / 2,
                                    (point.Y)
                                );

                        }
                        DrawString(_title, font, point, c);
                    }
                }

                //By now, we have the titlebar fully drawn. We can now work on the bottom border and corners.

                //First we set up some variables and initialize them with values from the current skin.
                //These variables will be used for the widths/heights of the bottom elements.

                int bottom_height = SkinEngine.LoadedSkin.BottomBorderWidth;
                //We'll also grab the left and right widths - as we'll need it for the bottom corners.
                int left_width = SkinEngine.LoadedSkin.LeftBorderWidth;
                int right_width = SkinEngine.LoadedSkin.RightBorderWidth;
                //Alright, now we can create the variables for the corner sizes.
                //The current skin specification does not allow explicit setting of these variables within the skin
                //so implementations must calculate these values on their own based on the left, right and bottom widths.
                //This is why we needed all three of those values.

                //We'll start by initiating the variables as 0s as we'll need to populate them later based on whether the decorator
                //supports this portion of the skin spec.
                Size cleft_size = new Size(0, 0);
                Size cright_size = new Size(0, 0);

                //Now we determine the actual values.
                if (AppearanceManager.DecoratorConfig.ShowLeftBottomCorner)
                {
                    //Set the cleft_size.
                    cleft_size.Height = bottom_height;
                    cleft_size.Width = left_width;

                    //Now we can draw this corner.
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.BottomLBorderBG))
                    {
                        if (img != null)
                        {
                            //Draw the image as a textured rectangle.
                            TextureRectangle(new Point(0, this.Size.Height - bottom_height), cleft_size, img);
                        }
                        else
                        {
                            FillRectangle(new Point(0, this.Size.Height - bottom_height), cleft_size, SkinEngine.LoadedSkin.BorderBottomLeftBackground); //This fills the solid background if we don't have an image.
                        }
                    }
                }

                //Let's do the same for the right corner.
                if (AppearanceManager.DecoratorConfig.ShowRightBottomCorner)
                {
                    //Set the cright_size
                    cright_size.Width = right_width;
                    cright_size.Height = bottom_height;

                    //Now we can draw this corner.
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.BottomRBorderBG))
                    {
                        if (img != null)
                        {
                            //Draw the image as a textured rectangle.
                            TextureRectangle(new Point(this.Size.Width - cright_size.Width, this.Size.Height - bottom_height), cright_size, img);
                        }
                        else
                        {
                            FillRectangle(new Point(this.Size.Width - cright_size.Width, this.Size.Height - bottom_height), cright_size, SkinEngine.LoadedSkin.BorderBottomRightBackground); //This fills the solid background if we don't have an image.
                        }
                    }
                }

                //So the border's corners have been drawn in. Now we need to do the borders themselves.

                //We'll start with the bottom border.

                //First we need the width that'll get taken up by this rectangle.
                int bottom_width = this.Size.Width - cleft_size.Width - cright_size.Width; //That should take care of things.

                //Next, for convenience, the Y coordinate.
                int bottom_y = this.Size.Height - bottom_height;

                //And the X coordinate - we can steal this from the left corner. This is just for convenience.
                int bottom_x = cleft_size.Width;

                //Now we draw it!
                if (AppearanceManager.DecoratorConfig.ShowBottomBorder)
                {
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.BottomBorderBG))
                    {
                        //If not null, draw as a texture..
                        if (img != null)
                            TextureRectangle(new Point(bottom_x, bottom_y), new Size(bottom_width, bottom_height), img);
                        else //That was simple.
                            FillRectangle(new Point(bottom_x, bottom_y), new Size(bottom_width, bottom_height), SkinEngine.LoadedSkin.BorderBottomBackground);
                    }
                }
                //Titlebar... check.
                //Title text... check.
                //Bottom border corners... check.
                //Top border corners... check.
                //Bottom border... check.

                //Ahhhh yes, I still need the left and right borders - and the client area.

                //Let's do the left one. All we need is it's Y coordinate and height.
                int left_y = title_height;
                int left_height = this.Size.Height - title_height - bottom_height;

                //Cool! Now, draw.

                //Now we draw it!
                if (AppearanceManager.DecoratorConfig.ShowLeftBorder)
                {
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.LeftBorderBG))
                    {
                        //If not null, draw as a texture..
                        if (img != null)
                            TextureRectangle(new Point(0, left_y), new Size(left_width, left_height), img);
                        else //That was simple.
                            FillRectangle(new Point(0, left_y), new Size(left_width, left_height), SkinEngine.LoadedSkin.BorderLeftBackground);
                    }
                }

                //This gets far easier and far more repetitive as more math is done.
                //Let's move onto the right border. We need the Y, height, and X values.
                int right_y = left_y;
                int right_x = this.Size.Width - right_width;
                int right_height = left_height;

                //Wow... a lot of that got stolen from the left border.

                if (AppearanceManager.DecoratorConfig.ShowRightBorder)
                {
                    using (var img = SkinEngine.ImageFromBinary(SkinEngine.LoadedSkin.RightBorderBG))
                    {
                        //If not null, draw as a texture..
                        if (img != null)
                            TextureRectangle(new Point(right_x, right_y), new Size(right_width, right_height), img);
                        else //That was simple.
                            FillRectangle(new Point(right_x, right_y), new Size(right_width, right_height), SkinEngine.LoadedSkin.BorderRightBackground);
                    }
                }
                                //Now we can draw the client area!
                _child.Draw(); //That'll get the client to draw onto its buffer.
                //Once that's done, which it is by the time we do this, we can draw the window buffer onto OUR buffer as a texture rectangle.

                TextureRectangle(new Point(left_width, title_height), _child.ClientSize, _child.GetBuffer());
                //That's all we need to do. The rest is up to the window manager!

            }
            else
            {
                            //Now we can draw the client area!
                _child.Draw(); //That'll get the client to draw onto its buffer.
                //Once that's done, which it is by the time we do this, we can draw the window buffer onto OUR buffer as a texture rectangle.

                TextureRectangle(new Point(0, 0), _child.ClientSize, _child.GetBuffer());
                //That's all we need to do. The rest is up to the window manager!

            }
        }

        public bool Decorated
        {
            get
            {
                return _decorated;
            }

            set
            {
                _decorated = true;
            }
        }

        public void Close()
        {
            //only draw borders if we are decorated.
            if(Decorated == true)
            {
                //Should we draw the titlebar?

            }
        }

        public void DrawLine(Point a, Point b, int thickness, Color color)
        {
            _gfx.DrawLine(new Pen(new SolidBrush(color), thickness), a, b);
        }

        public void DrawRectangle(Point pos, Size size, int lineThickness, Color color)
        {
            _gfx.DrawRectangle(new Pen(new SolidBrush(color), lineThickness), new Rectangle(pos, size));
        }

        public void FillRectangle(Point pos, Size size, Color color)
        {
            _gfx.FillRectangle(new SolidBrush(color), new Rectangle(pos, size));
        }

        public void TextureRectangle(Point pos, Size size, Image img)
        {
            _gfx.FillRectangle(new TextureBrush(img), new Rectangle(pos, size));
        }

        public void DrawOval(Point pos, Size size, int lineThickness, Color color)
        {
            _gfx.DrawEllipse(new Pen(new SolidBrush(color), lineThickness), new Rectangle(pos, size));
        }

        public void FillOval(Point pos, Size size, Color color)
        {
            _gfx.FillEllipse(new SolidBrush(color), new Rectangle(pos, size));
        }

        public void TextureOval(Point pos, Size size, Image img)
        {
            _gfx.FillEllipse(new TextureBrush(img), new Rectangle(pos, size));
        }

        public SizeF MeasureString(string text, Font font)
        {
            return _gfx.MeasureString(text, font);
        }

        public void DrawString(string text, Font font, Point pos, Color color)
        {
            _gfx.DrawString(text, font, new SolidBrush(color), new PointF(pos.X, pos.Y));
        }

        public void DrawPolygon(Color color, int lineThickness, params Point[] points)
        {
            _gfx.DrawPolygon(new Pen(new SolidBrush(color), lineThickness), points);
        }

        public void FillPolygon(Color color, params Point[] points)
        {
            _gfx.FillPolygon(new SolidBrush(color), points);
        }

        public void Clear(Color color)
        {
            _gfx.Clear(color);
        }

        private Graphics _gfx = null;

        public void SetBuffer(int width, int height)
        {
            if (_backBuffer != null)
            {
                _backBuffer.Dispose();
                _backBuffer = null;
            }
            if (_gfx != null)
            {
                _gfx.Dispose();
                _gfx = null;
            }
            _backBuffer = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _gfx = Graphics.FromImage(_backBuffer);
        }

        static public double linear(double x, double x0, double x1, double y0, double y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }

        public Point PointToClient(Rectangle bounds, Point value)
        {
            return new Point(
                    (int)linear(value.X, bounds.X, bounds.X + bounds.Width, this.Location.X, this.Size.Width),
                    (int)linear(value.Y, bounds.Y, bounds.Y + bounds.Height, this.Location.Y, this.Size.Height)
                );
        }

        public Point PointToScreen(Rectangle bounds, Point value)
        {
            return new Point(
                    (int)linear(value.X, bounds.X, bounds.X + bounds.Width, 0, _screenInfo.Width),
                    (int)linear(value.Y, bounds.Y, bounds.Y + bounds.Height, 0, _screenInfo.Height)
                );

        }

    }

    public class Screen
    {
        private GraphicsAPI _api = null;

        /// <summary>
        /// Creates a new instance of the <see cref="Screen"/> class using the given API. 
        /// </summary>
        /// <param name="api">The graphics backend to use.</param>
        public Screen(GraphicsAPI api)
        {
            if (api == null)
                throw new ArgumentNullException("The specified graphics backend is null.");
            _api = api;
            api.FrameRenderStarted += () =>
            {
                FrameRendering?.Invoke();
            };
        }

        public event Action FrameRendering;

        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        public int Width
        {
            get
            {
                return _api.Width;
            }
        }

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        public int Height
        {
            get
            {
                return _api.Height;
            }
        }

        /// <summary>
        /// Gets both the width and height of the screen.
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
        }

        public GraphicsAPI API
        {
            get
            {
                return _api;
            }
        }

        /// <summary>
        /// Converts a specified coordinate set from the universal ShiftOS coordinate system to the driver coordinate system and returns the value.
        /// </summary>
        /// <param name="point">The ShiftOS coordinate pair, where (0,0) is the top-left of the screen.</param>
        /// <returns>The driver-converted coordinate pair.</returns>
        public PointF ConvertCoordinatesTo(PointF point)
        {
            return _api.ConvertCoordsToDriver(point);
        }

        /// <summary>
        /// Converts a specified coordinate set from the underlying driver's coordinate system to the ShiftOS coordinate system and returns the value.
        /// </summary>
        /// <param name="point">The driver-specific coordinate pair.</param>
        /// <returns>The ShiftOS coordinate pair, where (0,0) is the top-left of the screen.</returns>
        public Point ConvertCoordinatesFrom(PointF point)
        {
            var f = _api.ConvertCoordsToEngine(point);
            return new Point((int)f.X, (int)f.Y);
        }
    }


    public class CompositingWindowManager : WindowManager
    {
        private List<CompositedBorder> Windows = new List<CompositedBorder>();


        public override void Close(IShiftOSWindow win)
        {
            var wb = Windows.FirstOrDefault(x => x.ParentWindow == win);
            if (wb != null)
                Windows.Remove(wb);
        }

        public override void InvokeAction(Action act)
        {
            act?.Invoke();
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
            Windows.FirstOrDefault(x => x.ParentWindow == win).Text = title;
        }

        private Screen _backend = null;

        public CompositingWindowManager(Screen backend)
        {
            _backend = backend;
            _backend.FrameRendering += () =>
            {
                //Render each window.
                foreach(var win in Windows)
                {
                    win.Draw();
                    var topleft = new PointF(win.Location.X, win.Location.Y);
                    var topright = new PointF(topleft.X + win.Size.Width, topleft.Y);
                    var bottomright = new PointF(topright.X, topright.Y + win.Size.Height);
                    var bottomleft = new PointF(topleft.X, bottomright.Y);

                    backend.API.DrawPolygon(win.GetBuffer(), new Vector3D(topright.X, 1, topright.Y), new Vector3D(topleft.X, 1, topleft.Y), new Vector3D(bottomright.X, 1, bottomright.Y), new Vector3D(bottomleft.X, 1, bottomleft.Y));
                }
            };
        }

        public override void SetupDialog(IShiftOSWindow win, bool decorated)
        {
            var wb = new CompositedBorder(_backend, win.Size);
            wb.Decorated = decorated;
            wb.ParentWindow = win;
            (win as Window).Parent = wb;
            bool fInit = false;

            if (Windows.Count == 0)
                fInit = true;
                Windows.Add(wb);
            if(fInit == true)
                this._backend.API.ForceInit();
        }

        public override void SetupWindow(IShiftOSWindow win, bool decorated)
        {
            var wb = new CompositedBorder(_backend, win.Size);
            wb.Decorated = decorated;
            wb.ParentWindow = win;
            (win as Window).Parent = wb; bool fInit = false;
            if (Windows.Count == 0)
                fInit = true;
            Windows.Add(wb);
            if (fInit == true)
                this._backend.API.ForceInit();
        }
    }

    public abstract class GraphicsAPI
    {
        private int _width = 0;
        private int _height = 0;

        public virtual PointF ConvertCoordsToDriver(PointF point)
        {
            return point;
        }

        public virtual PointF ConvertCoordsToEngine(PointF point)
        {
            return point;
        }

        public GraphicsAPI(int width, int height)
        {
            _width = width;
            _height = height;
            
        }

        public void ForceInit()
        {
            Init();
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
        public abstract void DrawPolygon(System.Drawing.Color color, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft);

        /// <summary>
        /// Draws a polygon onto the 3D world using the specified 32-bit ARGB bitmap data and 3D points.
        /// </summary>
        /// <param name="textureRaw">A byte[] array representing a 32-bit ARGB bitmap.</param>
        /// <param name="w">The width, in pixels, of the texture.</param>
        /// <param name="h">The height, in pixels, of the texture.</param>
        /// <param name="points">The vertices of the polygon.</param>
        public abstract void DrawPolygon(byte[] textureRaw, int w, int h, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft);


        /// <summary>
        /// Draws a polygon onto the 3D world using the specified <see cref="System.Drawing.Image"/>  and 3D points.
        /// </summary>
        /// <param name="texture">A <see cref="System.Drawing.Image"/> to be drawn as a texture.</param>
        /// <param name="points">The vertices of the polygon.</param>
        ///<remarks>
        ///     <para>This method doesn't need to be overidden as it will simply use the <see cref="ShiftOS.Engine.SkinEngine"/> ImageToBinary method to extract the binary data of the image and use the DrawPolygon(byte[]) overload. However, if your graphics API supports direct drawing of <see cref="System.Drawing.Image"/> textures, use its method instead, as it may be faster!</para>
        /// </remarks>
        public virtual void DrawPolygon(System.Drawing.Image texture, Vector3D topright, Vector3D topleft, Vector3D bottomright, Vector3D bottomleft)
        {
            var width = texture.Width;
            var height = texture.Height;
            var binary = SkinEngine.ImageToBinary(texture);
            DrawPolygon(binary, width, height, topright, topleft, bottomright, bottomleft);
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
            FrameRenderStarted?.Invoke();
        }

        public event Action FrameRenderStarted;
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
