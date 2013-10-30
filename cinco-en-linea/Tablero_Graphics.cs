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
    public partial class Tablero_Graphics : Control
    {
        Brush[] Colores =  {
            new SolidBrush(Color.LightGray),
            new SolidBrush(Color.Red),
            new SolidBrush(Color.Blue)
        };

        public Tablero_Graphics()
        {

            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Data de dibujo
            Int32 Alto = (ClientSize.Height - 5 * 16) / 15;
            Int32 Ancho = (ClientSize.Width - 5 * 16) / 15;
            for (int i = 5; i < (Alto + 5) * 15; i += Alto + 5)
                for (int j = 5; j < (Ancho + 5) * 15; j += Ancho + 5)
                    pe.Graphics.FillRectangle(Colores[((j*i) + (j/i)) % 3], j, i, Ancho, Alto);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }
    }
    public class TableroClickEventArgs : EventArgs
    {
        public Int32 Columna { get; private set; }
        public Int32 Fila { get; private set; }
        public TableroClickEventArgs(Int32 C, Int32 F)
        {
            Columna = C;
            Fila = F;
        }
    }
	public partial class Hovertable : Control
	{
		enum Estados { Alone, Entering, Hovering, Leaving }
		Estados currentStatus;
		Timer animationControl;
		Color actualColor;
		public Hovertable()
		{
			actualColor = Color.FromArgb(0, 0, 0);
			animationControl = new Timer();
			animationControl.Interval = 10;
			animationControl.Tick += new EventHandler(doMagic);
			animationControl.Start();
			currentStatus = Estados.Alone;
			DoubleBuffered = true;
			Cursor.Hide();
		}
		protected override void OnMouseEnter (EventArgs e)
		{
			currentStatus = Estados.Entering;
			base.OnMouseEnter (e);
		}
		protected override void OnMouseLeave (EventArgs e)
		{
			currentStatus = Estados.Leaving;
			base.OnMouseLeave (e);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			//PROVISORIO
			GraphicsPath area = new GraphicsPath();
			area.AddEllipse(new Rectangle(0, 0, 50, 50));

			PathGradientBrush dibu = new PathGradientBrush(area);
			dibu.CenterColor = actualColor;
			dibu.SurroundColors = new Color[] { Color.Black };
			dibu.CenterPoint = PointToClient (MousePosition);

			pe.Graphics.FillRectangle(dibu, ClientRectangle);
			dibu.Dispose();
			area.Dispose();
		}
		void doMagic(object sender, EventArgs e)
		{
			switch (currentStatus) {
			case Estados.Alone:
				return;
			case Estados.Entering:
				if(actualColor.R == 255)
					currentStatus = Estados.Hovering;
				else
					actualColor = Color.FromArgb(Math.Min(actualColor.R + 15, 255), 0, 0);
				break;
			case Estados.Hovering:
				break;
			case Estados.Leaving:
				if(actualColor.R == 0)
					currentStatus = Estados.Alone;
				else
					actualColor = Color.FromArgb(Math.Max(actualColor.R - 20, 0), 0, 0);
				break;
			}
			Invalidate();
		}
	}
}