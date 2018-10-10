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
        //MongoDatabase db = new MongoClient().GetServer().GetDatabase("labMongo");
        static MongoClientSettings settings = MongoClientSettings.FromConnectionString("mongodb://localhost");
        static MongoClient mongoClient = new MongoClient(settings);
        IMongoDatabase db = mongoClient.GetDatabase("labMongo");
        string peliculaSeleccionada = "";
        int filtroSeleccionado = 0;

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
            filtroSeleccionado = comboFiltro.SelectedIndex+1;
            switch (filtroSeleccionado) {
                case 1:
                    cargarPeliculas();
                    break;
                case 4:
                    filtrar("rangoAno", textDato.Text);
                    break;
                case 6:
                    filtrar("cantidadPeliculas", textDato.Text);
                    break;
                case 7:
                    filtrar("peliculaMenosDura", textDato.Text);
                    break;
                case 8:
                    filtrar("peliculaMasDura", textDato.Text);
                    break;
                case 9:
                    var peliculas = db.GetCollection<Pelicula>("peliculas");
                    var result = peliculas.Aggregate()                        
                        .Group(b => b.compania, g =>
                        new {
                            Compania = g.Key,
                            AverageDuration = g.Average(p => p.duracion)
                        }).ToList();
                    tablaConsultas.DataSource = result;
                    break;
                default:
                    cargarPeliculas();
                    break;
            }
        }

        private void RegistroPeliculas_Load(object sender, EventArgs e)
        {
            cargarPeliculas();
            tablaConsultas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            comboFiltro.SelectedIndex = 0;            
        }

        private void cargarPeliculas() {
            List<Pelicula> peliculas = db.GetCollection<Pelicula>("peliculas").Find("{}").ToList();
            Debug.Print(peliculas.Last().reparto[0].ToString());
            tablaConsultas.DataSource = peliculas;
            tablaConsultas.Columns["Id"].Visible = false;
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
                compania = companiaBox.Text               
            };
            string[] listaReparto = textReparto.Text.Split(new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
            peli.reparto = new BsonArray(listaReparto.ToList());
            var peliculas = db.GetCollection<Pelicula>("peliculas");
            peliculas.InsertOne(peli);
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
                    ObjectId objectId = ObjectId.Parse(peliculaSeleccionada);
                    var filter = Builders<Pelicula>.Filter.Eq("_id", objectId);
                    List<Pelicula> pelicula = db.GetCollection<Pelicula>("peliculas").Find(filter).ToList();
                    Debug.Print(peliculaSeleccionada.ToString());
                    foreach (var peli in pelicula) {
                        textNombre.Text = peli.nombre;                       
                        textGenero.Text = peli.genero;
                        textDirector.Text = peli.director;
                        textFranquicia.Text = peli.franquicia;
                        textPais.Text = peli.pais;
                        textDuracion.Text = peli.duracion.ToString();
                        companiaBox.Text = peli.compania;
                        anoPeli.Value = new DateTime(peli.ano, 01, 01);
                        BsonArray reparto = peli.reparto;
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
                    var pelicula = db.GetCollection<Pelicula>("peliculas");
                    ObjectId objectId = ObjectId.Parse(peliculaSeleccionada);
                    pelicula.DeleteOne((a => a.Id == objectId));                    
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
                        { "compania" , companiaBox.Text },
                        { "reparto" , new BsonArray(obtenerListaActores())}
                    }                    
                } 
            };
            peliculas.UpdateOne(query,update);
            cargarPeliculas();
            peliculaSeleccionada = "";
            vaciarCampos();
            buttonGuardar.Enabled = true;
            buttonActualizar.Enabled = false;

        }

        private List<string> obtenerListaActores()
        {
            string[] listaReparto = textReparto.Text.Split(new[] { "\r\n", "\r", "\n" },
                 StringSplitOptions.None);
            return listaReparto.ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textDato.Text == "" && filtroSeleccionado!=6 && filtroSeleccionado != 7
                && filtroSeleccionado != 8 && filtroSeleccionado != 9 && filtroSeleccionado != 4)
            {
                cargarPeliculas();
            }
            else
            {
                switch (filtroSeleccionado)
                {                    
                    case 2:
                        filtrar("nombrePelicula", textDato.Text);
                        break;
                    case 3:
                        filtrar("franquiciaPelicula", textDato.Text);
                        break;                    
                    case 5:
                        filtrar("companiaPelicula", textDato.Text);
                        break;
                    case 9:
                        filtrar("duracionPromedioPorCompania", textDato.Text);
                        break;
                    default:
                        cargarPeliculas();
                        break;
                }
            }
        }

        private void filtrar(string filtrarPor, string dato) {
            var peliculas = db.GetCollection<Pelicula>("peliculas");
            switch (filtrarPor)
            {
                case "nombrePelicula":
                    FilterDefinition<Pelicula> filter1 = "{ nombre : { $regex : \"" + dato.ToString() + "\" } }";
                    var list1 = peliculas.Find(filter1).ToList();
                    tablaConsultas.DataSource = list1;
                    break;
                case "franquiciaPelicula":
                    FilterDefinition<Pelicula> filter2 = "{ franquicia : { $regex : \"" + dato.ToString() + "\" } }";
                    var list2 = peliculas.Find(filter2).ToList();
                    tablaConsultas.DataSource = list2;
                    break;
                case "rangoAno":
                    FilterDefinition<Pelicula> filter3 = "{$and:[{ ano : { $gte : " + anoInicio.Value.Year.ToString() + " }} , {ano : { $lte : " + anoFinal.Value.Year.ToString() + " }}]}";                   
                    List<Pelicula> list3 = peliculas.Find(filter3).ToList();                   
                    tablaConsultas.DataSource = list3;
                    break;
                case "companiaPelicula":
                    FilterDefinition<Pelicula> filter4 = "{ compania : { $regex : \"" + dato.ToString() + "\" } }";
                    var list4 = peliculas.Find(filter4).ToList();
                    tablaConsultas.DataSource = list4;
                    break;
                case "cantidadPeliculas":
                    var list5 = peliculas.Aggregate().Count().ToList();
                    tablaConsultas.DataSource = list5;
                    break;
                case "peliculaMenosDura":
                    List<Pelicula> list6 = peliculas.Find(x => true).SortByDescending(d => d.duracion).ToList();
                    List<Pelicula> listAux = new List<Pelicula>();
                    listAux.Insert(0, list6.Last());
                    tablaConsultas.DataSource = listAux;
                    break;
                case "peliculaMasDura":
                    List<Pelicula> list7 = peliculas.Find(x => true).SortByDescending(d => d.duracion).Limit(1).ToList();
                    tablaConsultas.DataSource = list7;
                    break;
                case "duracionPromedioPorCompania":

                    FilterDefinition<Pelicula> filter8 = "{ compania : { $regex : \"" + dato.ToString() + "\" } }";
                    var list8 =  peliculas.Aggregate()
                        .Match(filter8)
                        .Group(b => b.compania, g =>
                        new {
                            Compania = g.Key,
                            AverageDuration = g.Average(p => p.duracion)
                        }).ToList();                    
                    tablaConsultas.DataSource = list8;
                    break;
                default:
                    cargarPeliculas();
                    break;
            }     
        }

        private void vaciarCampos() {
            textNombre.Text = "";
            textGenero.Text = "";
            textDirector.Text = "";
            textFranquicia.Text = "";
            textPais.Text = "";
            textDuracion.Text = "";
            companiaBox.Text = "";
            textReparto.Text = "";
        }

        private void anoInicio_ValueChanged(object sender, EventArgs e)
        {
            var peliculas = db.GetCollection<Pelicula>("peliculas");
            FilterDefinition<Pelicula> filter3 = "{$and:[{ ano : { $gte : " + anoInicio.Value.Year.ToString() + " }} , {ano : { $lte : " + anoFinal.Value.Year.ToString() + " }}]}";
            List<Pelicula> list3 = peliculas.Find(filter3).ToList();
            tablaConsultas.DataSource = list3;
        }

        private void anoFinal_ValueChanged(object sender, EventArgs e)
        {
            var peliculas = db.GetCollection<Pelicula>("peliculas");
            FilterDefinition<Pelicula> filter3 = "{$and:[{ ano : { $gte : " + anoInicio.Value.Year.ToString() + " }} , {ano : { $lte : " + anoFinal.Value.Year.ToString() + " }}]}";
            List<Pelicula> list3 = peliculas.Find(filter3).ToList();
            tablaConsultas.DataSource = list3;
        }

        private void textDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void companiaBox_MouseClick(object sender, MouseEventArgs e)
        {
            List<Compania> companias = db.GetCollection<Compania>("companias").Find("{}").ToList();
            companiaBox.Items.Clear();
            foreach (var compa in companias)
            {
                companiaBox.Items.Add(compa.nombre);
            }
        }
    }
}
