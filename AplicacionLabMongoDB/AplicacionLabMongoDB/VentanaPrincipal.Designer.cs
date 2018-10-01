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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonPeliculas = new System.Windows.Forms.Button();
            this.buttonCompanias = new System.Windows.Forms.Button();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.registroPeliculas = new AplicacionLabMongoDB.RegistroPeliculas();
            this.registroCompanias = new AplicacionLabMongoDB.RegistroCompanias();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.buttonSalir);
            this.panel1.Controls.Add(this.buttonCompanias);
            this.panel1.Controls.Add(this.buttonPeliculas);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 470);
            this.panel1.TabIndex = 1;
            // 
            // buttonPeliculas
            // 
            this.buttonPeliculas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPeliculas.Font = new System.Drawing.Font("Perpetua", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPeliculas.ForeColor = System.Drawing.Color.White;
            this.buttonPeliculas.Location = new System.Drawing.Point(0, 106);
            this.buttonPeliculas.Name = "buttonPeliculas";
            this.buttonPeliculas.Size = new System.Drawing.Size(177, 87);
            this.buttonPeliculas.TabIndex = 0;
            this.buttonPeliculas.Text = "Registro de Películas";
            this.buttonPeliculas.UseVisualStyleBackColor = true;
            this.buttonPeliculas.Click += new System.EventHandler(this.buttonPeliculas_Click);
            // 
            // buttonCompanias
            // 
            this.buttonCompanias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCompanias.Font = new System.Drawing.Font("Perpetua", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCompanias.ForeColor = System.Drawing.Color.White;
            this.buttonCompanias.Location = new System.Drawing.Point(0, 192);
            this.buttonCompanias.Name = "buttonCompanias";
            this.buttonCompanias.Size = new System.Drawing.Size(177, 87);
            this.buttonCompanias.TabIndex = 1;
            this.buttonCompanias.Text = "Registro de Companías";
            this.buttonCompanias.UseVisualStyleBackColor = true;
            this.buttonCompanias.Click += new System.EventHandler(this.buttonCompanias_Click);
            // 
            // buttonSalir
            // 
            this.buttonSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSalir.Font = new System.Drawing.Font("Perpetua", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSalir.ForeColor = System.Drawing.Color.White;
            this.buttonSalir.Location = new System.Drawing.Point(0, 380);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(177, 87);
            this.buttonSalir.TabIndex = 2;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);
            // 
            // registroPeliculas
            // 
            this.registroPeliculas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.registroPeliculas.Location = new System.Drawing.Point(183, 1);
            this.registroPeliculas.Name = "registroPeliculas";
            this.registroPeliculas.Size = new System.Drawing.Size(1000, 470);
            this.registroPeliculas.TabIndex = 0;
            // 
            // registroCompanias
            // 
            this.registroCompanias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.registroCompanias.Location = new System.Drawing.Point(183, 1);
            this.registroCompanias.Name = "registroCompanias";
            this.registroCompanias.Size = new System.Drawing.Size(1000, 470);
            this.registroCompanias.TabIndex = 2;
            // 
            // VentanaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 471);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.registroPeliculas);
            this.Controls.Add(this.registroCompanias);
            this.Name = "VentanaPrincipal";
            this.Text = "Laboratorio MongoDB";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RegistroPeliculas registroPeliculas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSalir;
        private System.Windows.Forms.Button buttonCompanias;
        private System.Windows.Forms.Button buttonPeliculas;
        private RegistroCompanias registroCompanias;
    }
}

