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
            Brushes.LightGray,
            Brushes.Pink,
            Brushes.LightBlue
        };
        Pen[] Líneas =  {
            Pens.DarkGray,
            Pens.DeepPink,
            Pens.LightSkyBlue
        };
		Brush Selection = new SolidBrush(Color.Gray);
		Int32 ColumnaSeleccionada = 20;
		Rectangle Columna (Int32 col)
		{
			Int32 Margen = (ClientSize.Width -(((ClientSize.Width - 5 * 16) / 15) * 15 + 5 * 16));
            Int32 Ancho = (ClientSize.Width - Margen) / 15;
			return new Rectangle(Ancho * col + Margen / 2 + 2, 0, Ancho, ClientSize.Height);
		}

        public Tablero_Graphics()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint (PaintEventArgs pe)
		{
			base.OnPaint (pe);

			// Data de dibujo
			Int32 Alto = (ClientSize.Height - 5 * 16) / 15;
			Int32 Ancho = (ClientSize.Width - 5 * 16) / 15;
			Int32 Margen = (ClientSize.Width - (Ancho * 15 + 5 * 16)) / 2;
			pe.Graphics.FillRectangle (Selection, Columna (ColumnaSeleccionada));

			for (int i = 5; i < (Alto + 5) * 15; i += Alto + 5)
				for (int j = Margen + 5; j < (Ancho + 5) * 15; j += Ancho + 5)
				{
					pe.Graphics.FillEllipse (Colores [2], j, i, Ancho, Alto);
					pe.Graphics.DrawEllipse (Líneas [2], j, i, Ancho, Alto);
				}

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
}