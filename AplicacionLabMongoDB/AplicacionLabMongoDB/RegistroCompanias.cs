using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using AplicacionLabMongoDB.Clases;

namespace AplicacionLabMongoDB
{
    public partial class RegistroCompanias : UserControl
    {
        MongoDatabase db = new MongoClient().GetServer().GetDatabase("labMongo");

        public RegistroCompanias()
        {
            InitializeComponent();
            anoCompania.Format = DateTimePickerFormat.Custom;
            anoCompania.CustomFormat = "yyyy";
            anoCompania.ShowUpDown = true;
        }

        private void RegistroCompanias_Load(object sender, EventArgs e)
        {
            cargarCompanias();
        }

        private void cargarCompanias()
        {
            List<Compania> peliculas = db.GetCollection<Compania>("companias").FindAll().ToList();
            tablaCompanias.DataSource = peliculas;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Compania peli = new Compania
            {
                nombre = textNombre.Text,
                ano = Int32.Parse(anoCompania.Text),                
                sitio = textSitio.Text
            };

            MongoCollection companias = db.GetCollection<Compania>("companias");
            companias.Insert(peli);
            cargarCompanias();

            textNombre.Text = "";
            textSitio.Text = "";            
        }
    }
}
