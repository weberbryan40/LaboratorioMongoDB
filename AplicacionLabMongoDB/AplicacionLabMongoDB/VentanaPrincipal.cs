using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionLabMongoDB
{
    public partial class VentanaPrincipal : Form
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private void buttonPeliculas_Click(object sender, EventArgs e)
        {
            registroPeliculas.BringToFront();
        }

        private void buttonCompanias_Click(object sender, EventArgs e)
        {
            registroCompanias.BringToFront();
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
