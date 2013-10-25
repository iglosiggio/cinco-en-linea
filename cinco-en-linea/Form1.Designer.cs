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
            this.tablero_Graphics1 = new cinco_en_linea.Tablero_Graphics();
            this.SuspendLayout();
            // 
            // tablero_Graphics1
            // 
            this.tablero_Graphics1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablero_Graphics1.Location = new System.Drawing.Point(0, 0);
            this.tablero_Graphics1.Name = "tablero_Graphics1";
            this.tablero_Graphics1.Size = new System.Drawing.Size(298, 319);
            this.tablero_Graphics1.TabIndex = 0;
            this.tablero_Graphics1.Text = "tablero_Graphics1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 319);
            this.Controls.Add(this.tablero_Graphics1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Tablero_Graphics tablero_Graphics1;
    }
}

