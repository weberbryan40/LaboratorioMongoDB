using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using AplicacionLabMongoDB.Clases;

namespace AplicacionLabMongoDB
{
    public partial class RegistroPeliculas : UserControl
    {
        MongoDatabase db = new MongoClient().GetServer().GetDatabase("labMongo");

        public RegistroPeliculas()
        {
            InitializeComponent();
            anoInicio.Format = DateTimePickerFormat.Custom;
            anoInicio.CustomFormat = "yyyy";
            anoInicio.ShowUpDown = true;
            anoFinal.Format = DateTimePickerFormat.Custom;
            anoFinal.CustomFormat = "yyyy";
            anoFinal.ShowUpDown = true;
            anoPeli.Format = DateTimePickerFormat.Custom;
            anoPeli.CustomFormat = "yyyy";
            anoPeli.ShowUpDown = true;
        }
         

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RegistroPeliculas_Load(object sender, EventArgs e)
        {
            cargarPeliculas();
        }

        private void cargarPeliculas() {
            List<Pelicula> peliculas = db.GetCollection<Pelicula>("peliculas").FindAll().ToList();
            tablaConsultas.DataSource = peliculas;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Pelicula peli = new Pelicula
            {
                nombre = textNombre.Text,
                genero = textGenero.Text,
                director = textDirector.Text,
                franquicia = textFranquicia.Text,
                pais = textPais.Text,
                ano = Int32.Parse(anoPeli.Text),
                duracion = Int32.Parse(textDuracion.Text),
                compania = textCompania.Text               
            };
            string[] listaReparto = textReparto.Text.Split(null);
            peli.reparto = listaReparto.ToList();
            MongoCollection peliculas = db.GetCollection<Pelicula>("peliculas");
            peliculas.Insert(peli);
            cargarPeliculas();

            textNombre.Text = "";
            textGenero.Text = "";
            textDirector.Text = "";
            textFranquicia.Text = "";
            textPais.Text = "";
            textDuracion.Text = "";
            textCompania.Text = "";
            textReparto.Text = "";
        }
    }
}
