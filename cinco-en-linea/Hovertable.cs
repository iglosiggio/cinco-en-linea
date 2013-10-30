using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cinco_en_linea
{
    public partial class Hovertable : Control
    {
        enum Estados { Alone, Entering, Hovering, Leaving }
        Estados currentStatus;
        Timer animationControl;
        Color actualColor;
        public Hovertable()
        {
            InitializeComponent();
            actualColor = Color.FromArgb(0, 0, 0);
            animationControl = new Timer();
            animationControl.Interval = 10;
            animationControl.Tick += new EventHandler(doMagic);
            animationControl.Start();
            currentStatus = Estados.Alone;
            DoubleBuffered = true;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            currentStatus = Estados.Entering;
            base.OnMouseEnter(e);
            Cursor.Hide();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            currentStatus = Estados.Leaving;
            base.OnMouseLeave(e);
            Cursor.Show();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            Point start = PointToClient(MousePosition);
            start.X -= 25;
            start.Y -= 25;
            //PROVISORIO
            GraphicsPath area = new GraphicsPath();
            area.AddEllipse(new Rectangle(start, new Size(50, 50)));

            PathGradientBrush dibu = new PathGradientBrush(area);
            dibu.CenterColor = actualColor;
            dibu.SurroundColors = new Color[] { Color.Black };
            dibu.CenterPoint = new Point(start.X + 25, start.Y + 25);

            pe.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
            pe.Graphics.FillPath(dibu, area);
            dibu.Dispose();
            area.Dispose();
        }
        void doMagic(object sender, EventArgs e)
        {
            switch (currentStatus)
            {
                case Estados.Alone:
                    return;
                case Estados.Entering:
                    if (actualColor.R == 255)
                        currentStatus = Estados.Hovering;
                    else
                        actualColor = Color.FromArgb(Math.Min(actualColor.R + 15, 255), 0, 0);
                    break;
                case Estados.Hovering:
                    break;
                case Estados.Leaving:
                    if (actualColor.R == 0)
                        currentStatus = Estados.Alone;
                    else
                        actualColor = Color.FromArgb(Math.Max(actualColor.R - 20, 0), 0, 0);
                    break;
            }
            Invalidate();
        }
    }
}