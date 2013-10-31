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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tablero_Graphics1 = new cinco_en_linea.Tablero_Graphics();
            this.hovertable1 = new cinco_en_linea.Hovertable();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tablero_Graphics1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.hovertable1);
            this.splitContainer1.Size = new System.Drawing.Size(298, 319);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 0;
            // 
            // tablero_Graphics1
            // 
            this.tablero_Graphics1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablero_Graphics1.Location = new System.Drawing.Point(0, 0);
            this.tablero_Graphics1.Name = "tablero_Graphics1";
            this.tablero_Graphics1.Size = new System.Drawing.Size(298, 278);
            this.tablero_Graphics1.TabIndex = 0;
            this.tablero_Graphics1.Text = "tablero_Graphics1";
            // 
            // hovertable1
            // 
            this.hovertable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hovertable1.Location = new System.Drawing.Point(0, 0);
            this.hovertable1.Name = "hovertable1";
            this.hovertable1.Size = new System.Drawing.Size(298, 37);
            this.hovertable1.TabIndex = 0;
            this.hovertable1.Text = "hovertable1";
			this.hovertable1.HoverColumn += tablero_Graphics1.SeleccionarColumna;
            this.hovertable1.MouseLeave += tablero_Graphics1.Hovertable_Leave;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 319);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Hovertable hovertable1;
        private Tablero_Graphics tablero_Graphics1;


    }
}

