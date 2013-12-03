using System;
using System.Collections;
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
        // HOVERTABLE: DATA
        enum Estados
        {
            Alone,
            Entering,
            Hovering,
            Down,
            Leaving
        }
        Color[,] cursorColor = {
			{ Color.RoyalBlue, Color.DarkBlue },
			{ Color.IndianRed, Color.DarkRed }
		};
        Int32 Jugador = 1;
        Estados currentStatus;
        ColorBlend animationColor;

        // ANIMACIONES
        RectangleBlend ColumnaSeleccionada;

        // TABLERO: DATA
		Brush[] Colores = {
            Brushes.LightGray,
            Brushes.Pink,
            Brushes.LightSkyBlue
        };
		Pen[] Líneas = {
            Pens.DarkGray,
            Pens.DeepPink,
            Pens.RoyalBlue
        };
		Timer animationTimer;
		Int32 SColumna = 20;

        Int32 Margen;
        Int32 Ficha_Alto;
        Int32 Ficha_Ancho;
        Int32 Columna_Ancho;

		Muchacho<Tablero, Int32> MeterFicha;

		public Tablero MiTablero;

		Rectangle Columna (Int32 col)
		{
			return new Rectangle (Columna_Ancho * col + Margen + 2, 0, Columna_Ancho + 2, ClientSize.Height);
		}

		void Tick (object sender, EventArgs e)
		{
			Invalidate ();
		}
        void CambioTurno(object s, Turno e)
        {
            Jugador = e.Jugador - 1;
            animationColor.ChangeBlend(cursorColor[Jugador, 0], 7);
            Rectangle R = new Rectangle(
                Margen + (Ficha_Ancho + 5) * e.Columna + 5,
                -Ficha_Alto,
                Ficha_Ancho,
                Ficha_Alto
            );
            new Ficha(
                e.Columna,
			    e.Fila,
                R,
                new Point(
                    R.X,
                    (Ficha_Alto + 5) * e.Fila + 5
                ),
                Colores[e.Jugador],
                Líneas[e.Jugador]
            );
        }

        public void Ganó(object sender, Ganador e)
        {
            if(!Animable.Animations.ContainsKey(30))
                if (e.Jugador == 1)
                    new Cartel("Ganó el jugador rojo!", Color.DeepPink, FontFamily.GenericSansSerif, 30);
                else
                    new Cartel("Ganó el azul!", Color.RoyalBlue, FontFamily.GenericSerif, 30);
        }

		public Tablero_Graphics ()
		{
			DoubleBuffered = true;

            // HOVERDATA: CTOR
            currentStatus = Estados.Alone;
            animationColor = new ColorBlend(Color.FromArgb(0), Color.Red, 20);

            // TABLERO: CTOR
			ColumnaSeleccionada = new RectangleBlend (new Rectangle (0, 0, 0, 0), new Rectangle (0, 0, 0, 0), 0, 1);
			animationTimer = new Timer ();
			animationTimer.Interval = 10;
			animationTimer.Tick += new EventHandler(Tick);
            animationTimer.Tick += new EventHandler(doMagic);
			animationTimer.Start ();

            MiTablero = new Tablero(Dificultad.PvP);
            MiTablero.CambioTurno += new EventHandler<Turno>(CambioTurno);
            MiTablero.Gano += new EventHandler<Ganador>(Ganó);

			MeterFicha = new Muchacho<Tablero, Int32>.Work(MiTablero.meterFicha);

			InitializeComponent ();

            Margen = (ClientSize.Width - (((ClientSize.Width - 5 * 16) / 15) * 15 + 5 * 16)) / 2;
            Ficha_Alto = (ClientSize.Height - 5 * 16) / 15;
            Ficha_Ancho = (ClientSize.Width - 5 * 16) / 15;
            Columna_Ancho = (ClientSize.Width - Margen * 2) / 15;

			Click += doClick;
		}

        public void CambiarDificultad(Dificultad Dif)
        {
            MiTablero.CambioTurno -= CambioTurno;
            MiTablero.Gano -= Ganó;
            MiTablero = new Tablero(Dif);
            MiTablero.CambioTurno += new EventHandler<Turno>(CambioTurno);
            MiTablero.Gano += new EventHandler<Ganador>(Ganó);
			MeterFicha = new Muchacho<Tablero, int>.Work(MiTablero.meterFicha);
            if(Animable.Animations.ContainsKey(30))
                Animable.Animations.Remove(30);
			animationColor.ChangeBlend(Color.Red, 7);
        }

		protected override void OnPaint (PaintEventArgs pe)
		{
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (KeyValuePair<Int32, Animable> Widget in Animable.Animations)
            {
				if(Widget.Key > 10)
					break;
                Widget.Value.DrawFrame(pe);
            }

			// Data de dibujo
			for (int i = 0; i < 15; i ++)
				for (int j = 0; j < 15; j ++) {
                    Rectangle R = new Rectangle((Ficha_Ancho + 5) * j + Margen + 5, (Ficha_Alto + 5) * i + 5, Ficha_Ancho, Ficha_Alto);
					if(Ficha.Fichas.ContainsKey(new Point(j, i)))
					{
						pe.Graphics.FillEllipse(Colores [0], R);
						pe.Graphics.DrawEllipse(Líneas [0], R);
					}
					else
					{
						pe.Graphics.FillEllipse (Colores [MiTablero [j, i]], R);
						pe.Graphics.DrawEllipse (Líneas [MiTablero [j, i]], R);
					}
				}
			try {
                Rectangle actual = new Rectangle((Ficha_Ancho + 5) * SColumna + Margen + 5, (Ficha_Alto + 5) * MiTablero.ultimoLugar(SColumna) + 5, Ficha_Ancho, Ficha_Alto);
				pe.Graphics.FillEllipse (Brushes.LimeGreen, actual);
				pe.Graphics.DrawEllipse (Pens.Green, actual);
			} catch (Exception) {
			}
			foreach (KeyValuePair<Int32, Animable> Widget in Animable.Animations)
            {
				if(Widget.Key < 10)
					continue;
                Widget.Value.DrawFrame(pe);
            }

            if (Animable.postForEach != null)
                Animable.postForEach();

            // HOVERTABLE: DRAW
            Point start = PointToClient(MousePosition);
            //PROVISORIO
            GraphicsPath area = new GraphicsPath();
            //area.AddEllipse(new Rectangle(start, new Size(50, 50)));
            area.AddEllipse (new Rectangle (new Point (start.X - 25, ClientSize.Height - 25), new Size (50, 50)));

            PathGradientBrush dibu = new PathGradientBrush(area);
            dibu.CenterColor = animationColor.O;
            dibu.SurroundColors = new Color[] { Color.FromArgb(0) };
            dibu.CenterPoint = new Point(start.X, ClientSize.Height);
            //dibu.CenterPoint = new Point (start.X + 25, ClientSize.Height);

            pe.Graphics.FillPath(dibu, area);
            dibu.Dispose();
            area.Dispose();

            base.OnPaint(pe);
		}

        protected override void OnMouseEnter(EventArgs e)
        {
            Rectangle Col = ColumnaSeleccionada.Rect;
            currentStatus = Estados.Entering;
            ColumnaSeleccionada.Change(new Rectangle(Col.X, 0, Col.Width, 0), 1, MiTablero.columnaLlena(SColumna));

            animationColor.ChangeBlend(cursorColor[Jugador, 0], 30);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            currentStatus = Estados.Leaving;
            animationColor.ChangeBlend(Color.FromArgb(0), 7);

            SColumna = 20;
            Rectangle R = ColumnaSeleccionada.Rect;
            ColumnaSeleccionada.Change(new Rectangle(R.Location, new Size(R.Width, 0)), 10, false);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Int32 PColumna = (e.X - Margen) / Columna_Ancho;
            if (PColumna < 15 && PColumna >= 0)
            {
                SColumna = PColumna;
                ColumnaSeleccionada.Change(Columna(SColumna), 10, MiTablero.columnaLlena(SColumna));
            }
            base.OnMouseMove(e);
        }

		protected override void OnSizeChanged (EventArgs e)
		{
			base.OnSizeChanged (e);
            Margen = (ClientSize.Width - (((ClientSize.Width - 5 * 16) / 15) * 15 + 5 * 16)) / 2;
            Ficha_Alto = (ClientSize.Height - 5 * 16) / 15;
            Ficha_Ancho = (ClientSize.Width - 5 * 16) / 15;
            Columna_Ancho = (ClientSize.Width - Margen * 2) / 15;
		}

        void doClick (object sender, EventArgs e)
		{
			if(MeterFicha.IsAlive)
				return;
			MeterFicha.Start(SColumna);
        }
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            if(Dock != DockStyle.None)
                Invalidate();
        }
        void doMagic(object sender, EventArgs e)
        {
            switch (currentStatus)
            {
                case Estados.Alone:
                    return;
                case Estados.Entering:
                    if (animationColor.Ready)
                        currentStatus = Estados.Hovering;
                    else
                        animationColor.Next();
                    break;
                case Estados.Hovering:
                    if (animationColor.Ready)
                        if (animationColor.O.ToArgb() == cursorColor[Jugador, 0].ToArgb())
                            animationColor.ChangeBlend(cursorColor[Jugador, 1], 15);
                        else if (animationColor.O.ToArgb() == cursorColor[Jugador, 1].ToArgb())
                            animationColor.ChangeBlend(cursorColor[Jugador, 0], 15);
                    animationColor.Next();
                    break;
                case Estados.Down:
                    if (!animationColor.Ready)
                        animationColor.Next();
                    break;
                case Estados.Leaving:
                    if (animationColor.Ready)
                        currentStatus = Estados.Alone;
                    else
                        animationColor.Next();
                    break;
            }
        }
	}
}