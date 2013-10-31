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
    public class ColorBlend
    {
        public Color O { get; private set; }
        public Color D { get; private set; }
        public Int32 Pasos { get; private set; }
        public ColorBlend(Color _O, Color _D, Int32 _Pasos)
        {
            O = _O;
            D = _D;
            Pasos = _Pasos;
        }
        public void ChangeBlend(Color _D, Int32 _Pasos)
        {
            D = _D;
            Pasos = _Pasos;
        }
        public bool Ready { get { return Pasos == 0; } }
        public void Next()
        {
            if (Pasos == 0)
                return;
            Int32 A, R, G, B;
            A = O.A + (D.A - O.A) / Pasos;
            R = O.R + (D.R - O.R) / Pasos;
            G = O.G + (D.G - O.G) / Pasos;
            B = O.B + (D.B - O.B) / Pasos;
            Int32[] C = { A, R, G, B };
            for (int i = 0; i < 4; i++)
                if (C[i] < 0)
                    C[i] = 0;
                else if (C[i] > 255)
                    C[i] = 255;
            Pasos--;
            O = Color.FromArgb(C[0], C[1], C[2], C[3]);
        }
    }
    class Animation
    {
        Rectangle Origen;
        public Rectangle Rect { get { return Origen; } }
        public Rectangle Dest { get; private set; }
        public Int32 Pasos { get; private set; }
        public Animation(Rectangle _Rect, Rectangle _Dest, Int32 Duración)
        {
            Origen = _Rect;
            Dest = _Dest;
            Pasos = Duración;
        }
        public void Change(Rectangle _Dest, Int32 Duración)
        {
            Dest = _Dest;
            Pasos = Duración;
        }
        public Rectangle Next()
        {
            if (Pasos != 0)
            {
                Origen.X += (Dest.X - Origen.X) / Pasos;
                Origen.Y += (Dest.Y - Origen.Y) / Pasos;
                Origen.Height += (Dest.Height - Origen.Height) / Pasos;
                Origen.Width += (Dest.Width - Origen.Width) / Pasos;
                Pasos--;
            }
            Console.WriteLine(Origen);
            return Origen;
        }
    }
}