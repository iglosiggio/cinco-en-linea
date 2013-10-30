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
}