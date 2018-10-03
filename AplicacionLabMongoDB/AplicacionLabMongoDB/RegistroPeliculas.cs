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
using MongoDB.Driver.Builders;
using System.Diagnostics;

namespace AplicacionLabMongoDB
{
    public partial class RegistroPeliculas : UserControl
    {
        MongoDatabase db = new MongoClient().GetServer().GetDatabase("labMongo");
        string peliculaSeleccionada = "";

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
            tablaConsultas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void cargarPeliculas() {
            List<Pelicula> peliculas = db.GetCollection<Pelicula>("peliculas").FindAll().ToList();
            tablaConsultas.DataSource = peliculas;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            int dura = 0;
            if (textDuracion.Text != "") { dura = Int32.Parse(textDuracion.Text); }
            Pelicula peli = new Pelicula
            {
                nombre = textNombre.Text,
                genero = textGenero.Text,
                director = textDirector.Text,
                franquicia = textFranquicia.Text,
                pais = textPais.Text,
                ano = Int32.Parse(anoPeli.Text),
                duracion = dura,
                compania = textCompania.Text               
            };
            string[] listaReparto = textReparto.Text.Split(new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
            peli.reparto = listaReparto.ToList();
            MongoCollection peliculas = db.GetCollection<Pelicula>("peliculas");
            peliculas.Insert(peli);
            cargarPeliculas();

            vaciarCampos();
        }

        private void tablaConsultas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
                int position = tablaConsultas.HitTest(e.X, e.Y).RowIndex;

                if (position >= 0)
                {
                    menu.Items.Add("Actualizar").Name = "Actualizar";
                    menu.Items.Add("Eliminar").Name = "Eliminar";
                    peliculaSeleccionada = (tablaConsultas[0, position].Value.ToString());

                }
                menu.Show(tablaConsultas, new Point(e.X, e.Y));
                menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
            }
        }
        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.ClickedItem.Name.ToString() == "Actualizar")
            {
                if (peliculaSeleccionada != "")
                {
                    var query = Query.EQ("_id", ObjectId.Parse(peliculaSeleccionada));
                    var pelicula = db.GetCollection<Pelicula>("peliculas").Find(query);
                    foreach (var peli in pelicula) {
                        textNombre.Text = peli.nombre;                       
                        textGenero.Text = peli.genero;
                        textDirector.Text = peli.director;
                        textFranquicia.Text = peli.franquicia;
                        textPais.Text = peli.pais;
                        textDuracion.Text = peli.duracion.ToString();
                        textCompania.Text = peli.compania;
                        anoPeli.Value = new DateTime(peli.ano, 01, 01);
                        List<String> reparto = peli.reparto;
                        string stringReparto = "";
                        foreach (string actor in reparto) {
                            stringReparto += actor + "\r\n";
                        }
                        textReparto.Text = stringReparto;
                    }
                }
                buttonActualizar.Enabled = true;
                buttonGuardar.Enabled = false;
            } else if (e.ClickedItem.Name.ToString() == "Eliminar") {
                if (peliculaSeleccionada != "")
                {
                    var query = Query.EQ("_id", ObjectId.Parse(peliculaSeleccionada));
                    var peliculas = db.GetCollection<Pelicula>("peliculas");
                    peliculas.Remove(query);
                    cargarPeliculas();
                }
            }            
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {            
            var peliculas = db.GetCollection<Pelicula>("peliculas");
            var query = new QueryDocument {
                {"_id",ObjectId.Parse(peliculaSeleccionada) }
            };
            var update = new UpdateDocument
            {
                {"$set", new BsonDocument                
                    {
                        { "nombre" ,textNombre.Text},
                        { "genero",  textGenero.Text },
                        { "director",  textDirector.Text },
                        { "franquicia",  textFranquicia.Text },
                        { "pais",  textPais.Text },
                        { "ano" , Int32.Parse(anoPeli.Text) },
                        { "duracion" , Int32.Parse(textDuracion.Text) },
                        { "compania" , textCompania.Text }                       
                    }                    
                } 
            };
            peliculas.Update(query,update);
            cargarPeliculas();
            peliculaSeleccionada = "";
            vaciarCampos();
            buttonGuardar.Enabled = true;
            buttonActualizar.Enabled = false;

        }

        private void vaciarCampos() {
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
