using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace cinco_en_linea
{
    abstract class Animable
    {
		public readonly Int32 ID;
        public static SortedList<Int32, Animable> Animations = new SortedList<Int32, Animable>();
        public Animable(Int32 Prioridad)
        {
			ID = Prioridad;
            Animations.Add(Prioridad, this);
        }
        abstract public void DrawFrame(PaintEventArgs e);
		abstract public void Next();
    }
    class RectangleBlend : Animable
    {
        Rectangle Origen;
        Rectangle Dest;
        ColorBlend RColor;
        public Int32 Pasos { get; private set; }
        public Rectangle Rect { get { return Origen; } }
        public RectangleBlend(Rectangle _Rect, Rectangle _Dest, Int32 Duración, Int32 Prioridad)
			: base(Prioridad)
        {
            Origen = _Rect;
            Dest = _Dest;
            Pasos = Duración;
            RColor = new ColorBlend(Color.Gray, Color.Gray, 0);
        }

        public void Change(Rectangle _Dest, Int32 Duración, Boolean Full)
        {
            Dest = _Dest;
            Pasos = Duración;
            if (Full)
                RColor.ChangeBlend(Color.Red, 10);
            else
                RColor.ChangeBlend(Color.Gray, 10);
        }
		public override void Next ()
		{
			if (Pasos != 0)
            {
                Origen.X += (Dest.X - Origen.X) / Pasos;
                Origen.Y += (Dest.Y - Origen.Y) / Pasos;
                Origen.Height += (Dest.Height - Origen.Height) / Pasos;
                Origen.Width += (Dest.Width - Origen.Width) / Pasos;
                Pasos--;
            }

		}
        public override void DrawFrame(PaintEventArgs e)
        {
			Next();
            RColor.Next();
            Brush b = new SolidBrush(RColor.O);
            e.Graphics.FillRectangle(b, Origen);
            b.Dispose();
        }
    }
	class Ficha : RectangleBlend
	{
		Int32 Col;
		Brush Color;
		Pen Línea;
		public static Dictionary<Int32, Ficha> Fichas = new Dictionary<int, Ficha>();
		public Ficha(Int32 Columna, Rectangle _Ficha, Point Final, Int32 Prioridad, Brush FColor, Pen FLínea)
			: base(_Ficha, new Rectangle(Final, _Ficha.Size), 10, Prioridad)
		{
			Col = Columna;
			Color = FColor;
			Línea = FLínea;
			Fichas.Add(Columna, this);
		}
		public override void DrawFrame (PaintEventArgs e)
		{
			base.Next ();
			e.Graphics.FillEllipse (Color, Rect);
			e.Graphics.DrawEllipse (Línea, Rect);
			if (Pasos == 0) {
				Animations.Remove(ID);
				Fichas.Remove(Col);
			}
		}
	}
}
