using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logica;

namespace cinco_en_linea
{
	public partial class Tablero_Graphics : Control
	{
		Brush[] Colores = {
            Brushes.LightGray,
            Brushes.Pink,
            Brushes.LightBlue
        };
		Pen[] Líneas = {
            Pens.DarkGray,
            Pens.DeepPink,
            Pens.LightSkyBlue
        };
		Brush Selection = new SolidBrush (Color.Gray);
		Animation ColumnaSeleccionada;
		Timer animationTimer;
		Int32 SColumna = 20;
		public Tablero ITablero;

		public void SeleccionarColumna (object sender, HovertableEventArgs e)
		{
			Rectangle Col = Columna (e.Columna);
			if (ColumnaSeleccionada.Rect.Height == 0) {
				ColumnaSeleccionada.Change (new Rectangle (Col.X, 0, Col.Width, 0), 1);
				ColumnaSeleccionada.Next ();
				ColumnaSeleccionada.Change (Col, 10);
			}
			else ColumnaSeleccionada.Change (Col, 10);
			SColumna = e.Columna;
		}

		Rectangle Columna (Int32 col)
		{
			Int32 Margen = (ClientSize.Width - (((ClientSize.Width - 5 * 16) / 15) * 15 + 5 * 16));
			Int32 Ancho = (ClientSize.Width - Margen) / 15;
			return new Rectangle (Ancho * col + Margen / 2 + 2, 0, Ancho + 2, ClientSize.Height);
		}

		void Tick (object sender, EventArgs e)
		{
			Invalidate ();
		}

		public void Hovertable_Leave (object sender, EventArgs e)
		{
			SColumna = 20;
			Rectangle R = ColumnaSeleccionada.Rect;
			ColumnaSeleccionada.Change (new Rectangle (R.Location, new Size (R.Width, 0)), 10);
		}

		public void Hovertable_Click (object sender, EventArgs e)
		{
			try {
				ITablero.meterFicha (SColumna);
			} catch (Exception ex) {
				MessageBox.Show (ex.Message);
			}
		}

		public void NextTurn (object sender, Turno e)
		{
			// TODO: Animaciones and shit 
		}

		public Tablero_Graphics ()
		{
			DoubleBuffered = true;
			ColumnaSeleccionada = new Animation (new Rectangle (0, 0, 0, 0), new Rectangle (0, 0, 0, 0), 0);
			animationTimer = new Timer ();
			animationTimer.Interval = 10;
			animationTimer.Tick += Tick;
			animationTimer.Start ();
			InitializeComponent ();
		}

		protected override void OnPaint (PaintEventArgs pe)
		{
			base.OnPaint (pe);

			// Data de dibujo
			Int32 Alto = (ClientSize.Height - 5 * 16) / 15;
			Int32 Ancho = (ClientSize.Width - 5 * 16) / 15;
			Int32 Margen = (ClientSize.Width - (Ancho * 15 + 5 * 16)) / 2;
			pe.Graphics.FillRectangle (Selection, ColumnaSeleccionada.Next ());
			for (int i = 0; i < 15; i ++)
				for (int j = 0; j < 15; j ++) {
					Rectangle R = new Rectangle ((Ancho + 5) * j + Margen + 5, (Alto + 5) * i + 5, Ancho, Alto);
					pe.Graphics.FillEllipse (Colores [ITablero [j, i]], R);
					pe.Graphics.DrawEllipse (Líneas [ITablero [j, i]], R);
				}
			try {
				Rectangle actual = new Rectangle ((Ancho + 5) * SColumna + Margen + 5, (Alto + 5) * ITablero.ultimoLugar (SColumna) + 5, Ancho, Alto);
				pe.Graphics.FillEllipse (Brushes.LimeGreen, actual);
				pe.Graphics.DrawEllipse (Pens.Green, actual);
			} catch (Exception) {
			}
		}

		protected override void OnSizeChanged (EventArgs e)
		{
			base.OnSizeChanged (e);
			Invalidate ();
		}
	}
}