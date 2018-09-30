using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionLabMongoDB
{
    public partial class RegistroPeliculas : UserControl
    {
        public RegistroPeliculas()
        {
            InitializeComponent();
            anoInicio.Format = DateTimePickerFormat.Custom;
            anoInicio.CustomFormat = "yyyy";
            anoInicio.ShowUpDown = true;
            anoFinal.Format = DateTimePickerFormat.Custom;
            anoFinal.CustomFormat = "yyyy";
            anoFinal.ShowUpDown = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
