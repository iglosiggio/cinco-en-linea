﻿using System;
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
	public partial class Hovertable : Control
	{
		enum Estados
		{
			Alone,
			Entering,
			Hovering,
			Down,
			Leaving
		}
		Color[,] cursorColor = {
			{ Color.IndianRed, Color.DarkRed },
			{ Color.SkyBlue, Color.DarkBlue }
		};
		Int32 Jugador = 0;
		Estados currentStatus;
		Timer animationControl;
		ColorBlend animationColor;

		public event EventHandler<HovertableEventArgs> HoverColumn;

		public Hovertable ()
		{
			InitializeComponent ();
			animationControl = new Timer ();
			animationControl.Interval = 10;
			animationControl.Tick += new EventHandler (doMagic);
			animationControl.Start ();
			currentStatus = Estados.Alone;
			DoubleBuffered = true;
			animationColor = new ColorBlend (BackColor, Color.Red, 20);
		}

		public void nextTurn (object sender, Turno e)
		{
			Jugador = e.Jugador - 1;
			animationColor.ChangeBlend (cursorColor [Jugador, 0], 7);
		}

		protected override void OnMouseEnter (EventArgs e)
		{
			currentStatus = Estados.Entering;
			animationColor.ChangeBlend (cursorColor [Jugador, 0], 30);
			base.OnMouseEnter (e);
			Cursor.Hide ();
		}

		protected override void OnMouseLeave (EventArgs e)
		{
			currentStatus = Estados.Leaving;
			animationColor.ChangeBlend (BackColor, 10);
			base.OnMouseLeave (e);
			Cursor.Show ();
		}

		protected override void OnMouseMove (MouseEventArgs e)
		{
			Int32 Margen = (ClientSize.Width - (((ClientSize.Width - 5 * 16) / 15) * 15 + 5 * 16)) / 2;
			Int32 Ancho = (ClientSize.Width - Margen) / 15;
			Int32 Columna = (e.X - Margen) / Ancho;
			if (Columna < 15 && Columna >= 0 && HoverColumn != null)
				HoverColumn (this, new HovertableEventArgs (Columna));
			base.OnMouseMove (e);
		}

		protected override void OnMouseDown (MouseEventArgs e)
		{
			currentStatus = Estados.Down;
			animationColor.ChangeBlend (cursorColor [Jugador, 1], 7);
			base.OnMouseDown (e);
		}

		protected override void OnMouseUp (MouseEventArgs e)
		{
			currentStatus = Estados.Hovering;
			base.OnMouseDown (e);
		}

		protected override void OnPaint (PaintEventArgs pe)
		{
			Point start = PointToClient (MousePosition);
			start.X -= 25;
			start.Y -= 25;
			//PROVISORIO
			GraphicsPath area = new GraphicsPath ();
			area.AddEllipse(new Rectangle(start, new Size(50, 50)));
			//area.AddEllipse (new Rectangle (new Point (start.X, ClientSize.Height - 25), new Size (50, 50)));

			PathGradientBrush dibu = new PathGradientBrush (area);
			dibu.CenterColor = animationColor.O;
			dibu.SurroundColors = new Color[] { BackColor };
			dibu.CenterPoint = new Point(start.X + 25, start.Y + 25);
			//dibu.CenterPoint = new Point (start.X + 25, ClientSize.Height);

			pe.Graphics.FillPath (dibu, area);
			dibu.Dispose ();
			area.Dispose ();
		}

		void doMagic (object sender, EventArgs e)
		{
			switch (currentStatus) {
			case Estados.Alone:
				return;
			case Estados.Entering:
				if (animationColor.Ready)
					currentStatus = Estados.Hovering;
				else
					animationColor.Next ();
				break;
			case Estados.Hovering:
				if (animationColor.Ready)
				if (animationColor.O.ToArgb () == cursorColor [Jugador, 0].ToArgb ())
					animationColor.ChangeBlend (cursorColor [Jugador, 1], 7);
				else if (animationColor.O.ToArgb () == cursorColor [Jugador, 1].ToArgb ())
					animationColor.ChangeBlend (cursorColor [Jugador, 0], 7);
				animationColor.Next ();
				break;
			case Estados.Down:
				if (!animationColor.Ready)
					animationColor.Next ();
				break;
			case Estados.Leaving:
				if (animationColor.Ready)
					currentStatus = Estados.Alone;
				else
					animationColor.Next ();
				break;
			}
			Invalidate ();
		}
	}

	public class HovertableEventArgs : EventArgs
	{
		public Int32 Columna { get; private set; }

		public HovertableEventArgs (Int32 C)
		{
			Columna = C;
		}
	}
}