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
		Bend ColorBend;
		public event HoverColumn;
        public Hovertable()
        {
            InitializeComponent();
            animationControl = new Timer();
            animationControl.Interval = 10;
            animationControl.Tick += new EventHandler(doMagic);
            animationControl.Start();
            currentStatus = Estados.Alone;
            DoubleBuffered = true;
			ColorBend = new Bend(BackColor, Color.Red, 20);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            currentStatus = Estados.Entering;
			ColorBend.ChangeBend(Color.Red, 30);
            base.OnMouseEnter(e);
            Cursor.Hide();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            currentStatus = Estados.Leaving;
			ColorBend.ChangeBend(BackColor, 10);
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
            dibu.CenterColor = ColorBend.O;
            dibu.SurroundColors = new Color[] { BackColor };
            dibu.CenterPoint = new Point(start.X + 25, start.Y + 25);

            pe.Graphics.FillPath(dibu, area);
            dibu.Dispose();
            area.Dispose();
        }
        void doMagic (object sender, EventArgs e)
		{
			switch (currentStatus) {
			case Estados.Alone:
				return;
			case Estados.Entering:
				if (ColorBend.Ready)
					currentStatus = Estados.Hovering;
				else ColorBend.Next();
				break;
			case Estados.Hovering:
				if(ColorBend.Ready)
					if(ColorBend.O.ToArgb() == Color.Red.ToArgb())
						ColorBend.ChangeBend(Color.DarkRed, 7);
					else if(ColorBend.O.ToArgb() == Color.DarkRed.ToArgb())
						ColorBend.ChangeBend(Color.Red, 7);
				ColorBend.Next();
				break;
			case Estados.Leaving:
				if (ColorBend.Ready)
					currentStatus = Estados.Alone;
				else ColorBend.Next();
				break;
			}
			Invalidate ();
		}
		class Bend {
			public Color O { get; private set; }
			public Color D { get; private set; }
			public Int32 Pasos { get; private set; }
			public Bend(Color _O, Color _D, Int32 _Pasos)
			{
				O = _O;
				D = _D;
				Pasos = _Pasos;
			}
			public void ChangeBend(Color _D, Int32 _Pasos)
			{
				D = _D;
				Pasos = _Pasos;
			}
			public bool Ready { get { return Pasos == 0; } }
			public void Next() {
				if(Pasos == 0)
					return;
				Int32 A, R, G, B;
				A = O.A + (D.A - O.A) / Pasos;
				R = O.R + (D.R - O.R) / Pasos;
				G = O.G + (D.G - O.G) / Pasos;
				B = O.B + (D.B - O.B) / Pasos;
				Int32[] C = { A, R, G, B };
				for (int i = 0; i < 4; i++)
					if(C[i] < 0)
						C[i] = 0;
					else if(C[i] > 255)
						C[i] = 255;
				Pasos--;
				O = Color.FromArgb(C[0], C[1], C[2], C[3]);
			}
		}
    }
}