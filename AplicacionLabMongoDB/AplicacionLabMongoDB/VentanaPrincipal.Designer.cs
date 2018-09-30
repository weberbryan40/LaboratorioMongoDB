namespace AplicacionLabMongoDB
{
    partial class VentanaPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.registroPeliculas1 = new AplicacionLabMongoDB.RegistroPeliculas();
            this.SuspendLayout();
            // 
            // registroPeliculas1
            // 
            this.registroPeliculas1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.registroPeliculas1.Location = new System.Drawing.Point(-9, 1);
            this.registroPeliculas1.Name = "registroPeliculas1";
            this.registroPeliculas1.Size = new System.Drawing.Size(1000, 470);
            this.registroPeliculas1.TabIndex = 0;
            // 
            // VentanaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 471);
            this.Controls.Add(this.registroPeliculas1);
            this.Name = "VentanaPrincipal";
            this.Text = "Laboratorio MongoDB";
            this.ResumeLayout(false);

        }

        #endregion

        private RegistroPeliculas registroPeliculas1;
    }
}

