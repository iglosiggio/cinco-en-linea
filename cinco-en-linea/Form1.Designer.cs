namespace cinco_en_linea
{
	partial class Form1
	{
		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

        #region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent ()
		{
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nuevaPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humanoVsComputadoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fácilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.difícilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conUnAmigoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablero_Graphics1 = new cinco_en_linea.Tablero_Graphics();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaPartidaToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(329, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nuevaPartidaToolStripMenuItem
            // 
            this.nuevaPartidaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.humanoVsComputadoraToolStripMenuItem,
            this.conUnAmigoToolStripMenuItem});
            this.nuevaPartidaToolStripMenuItem.Name = "nuevaPartidaToolStripMenuItem";
            this.nuevaPartidaToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.nuevaPartidaToolStripMenuItem.Text = "Nueva partida";
            // 
            // humanoVsComputadoraToolStripMenuItem
            // 
            this.humanoVsComputadoraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fácilToolStripMenuItem,
            this.medioToolStripMenuItem,
            this.difícilToolStripMenuItem});
            this.humanoVsComputadoraToolStripMenuItem.Name = "humanoVsComputadoraToolStripMenuItem";
            this.humanoVsComputadoraToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.humanoVsComputadoraToolStripMenuItem.Text = "Sólo (PvC)";
            // 
            // fácilToolStripMenuItem
            // 
            this.fácilToolStripMenuItem.Name = "fácilToolStripMenuItem";
            this.fácilToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.fácilToolStripMenuItem.Text = "Fácil";
            this.fácilToolStripMenuItem.Click += new System.EventHandler(this.fácilToolStripMenuItem_Click);
            // 
            // medioToolStripMenuItem
            // 
            this.medioToolStripMenuItem.Name = "medioToolStripMenuItem";
            this.medioToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.medioToolStripMenuItem.Text = "Medio";
            this.medioToolStripMenuItem.Click += new System.EventHandler(this.medioToolStripMenuItem_Click);
            // 
            // difícilToolStripMenuItem
            // 
            this.difícilToolStripMenuItem.Name = "difícilToolStripMenuItem";
            this.difícilToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.difícilToolStripMenuItem.Text = "Difícil";
            this.difícilToolStripMenuItem.Click += new System.EventHandler(this.difícilToolStripMenuItem_Click);
            // 
            // conUnAmigoToolStripMenuItem
            // 
            this.conUnAmigoToolStripMenuItem.Name = "conUnAmigoToolStripMenuItem";
            this.conUnAmigoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.conUnAmigoToolStripMenuItem.Text = "Con un amigo (PvP)";
            this.conUnAmigoToolStripMenuItem.Click += new System.EventHandler(this.conUnAmigoToolStripMenuItem_Click);
            // 
            // tablero_Graphics1
            // 
            this.tablero_Graphics1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablero_Graphics1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablero_Graphics1.Location = new System.Drawing.Point(0, 24);
            this.tablero_Graphics1.Name = "tablero_Graphics1";
            this.tablero_Graphics1.Size = new System.Drawing.Size(329, 301);
            this.tablero_Graphics1.TabIndex = 0;
            this.tablero_Graphics1.Text = "tablero_Graphics1";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 325);
            this.Controls.Add(this.tablero_Graphics1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "cinco-en-linea-form1";
			this.Load += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            this.Text = "Cinco en línea";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private Tablero_Graphics tablero_Graphics1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevaPartidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem humanoVsComputadoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fácilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem difícilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conUnAmigoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
	}
}

