using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftOS.Engine.Composition.UI
{
    /// <summary>
    /// Provides a simple draw method.
    /// </summary>
    public interface IDrawable
    {
        void Draw();
    }

    /// <summary>
    /// Provides an abstraction layer for 2-dimensional painting on a composited control.
    /// </summary>
    public interface IPaintable
    {
        void DrawLine(Point a, Point b, int thickness, Color color);
        void DrawRectangle(Point pos, Size size, int lineThickness, Color color);
        void FillRectangle(Point pos, Size size, Color color);
        void TextureRectangle(Point pos, Size size, Image img);
        void DrawOval(Point pos, Size size, int lineThickness, Color color);
        void FillOval(Point pos, Size size, Color color);
        void TextureOval(Point pos, Size size, Image img);
        SizeF MeasureString(string text, Font font);
        void DrawString(string text, Font font, Point pos, Color color);
        void DrawPolygon(Color color, int lineThickness, params Point[] points);
        void FillPolygon(Color color, params Point[] points);



        void Clear(Color color);

        void SetBuffer(int width, int height);

        Point PointToClient(Rectangle bounds, Point value);

        Point PointToScreen(Rectangle bounds, Point value);

        Image GetBuffer();
    }

    /// <summary>
    /// The root (client area) of a ShiftOS composited window. This is where the user can add controls - and should be inherited by application classes.
    /// </summary>
    public abstract class Window : IShiftOSWindow, IPaintable, IDrawable
    {
        public void Draw()
        {
            SetBuffer(ClientSize.Width, ClientSize.Height);
            Clear(SkinEngine.LoadedSkin.ControlColor);
        }

        private CompositedBorder _parent = null;

        private Bitmap _backBuffer = null;

        public Image GetBuffer()
        {
            return _backBuffer;
        }

        public Window(int width, int height)
        {
            this.ClientSize = new Size(width, height);
        }

        public Size ClientSize
        {
            get
            {
                return _parent.GetClientSize();
            }
            set
            {
                _parent.SetClientSize(value);
                SetBuffer(value.Width, value.Height);
            }
        }

        public Point Location
        {
            get
            {
                return _parent.Location;
            }

            set
            {
                _parent.Location = value;
            }
        }

        public Size Size
        {
            get
            {
                return _parent.Size;
            }

            set
            {
                _parent.Size = value;
                SetBuffer(ClientSize.Width, ClientSize.Height);

            }
        }

        public string Text
        {
            get
            {
                return _parent.Text;
            }
            set
            {
                _parent.Text = value;
            }
        }

        public event Action Loaded;
        public event Action SkinLoaded;
        public event Func<bool> Closing;
        public event Action Upgraded;

        public void OnLoad()
        {
            Loaded?.Invoke();
        }

        public void OnSkinLoad()
        {
            SkinLoaded?.Invoke();
        }

        public bool OnUnload()
        {
            return (bool)Closing?.Invoke();
        }

        public void OnUpgrade()
        {
            Upgraded?.Invoke();
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
            return _parent.PointToScreen(bounds, value);
        }
    }
}
