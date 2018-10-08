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
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace AplicacionLabMongoDB
{
    public partial class RegistroCompanias : UserControl
    {
        //MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://usermehmet:inno12345@localhost/testDB"));
        static MongoClientSettings settings = MongoClientSettings.FromConnectionString("mongodb://localhost");
        static MongoClient mongoClient = new MongoClient(settings);
        IMongoDatabase db = mongoClient.GetDatabase("labMongo");

        //MongoDatabase db = new MongoClient().GetServer().GetDatabase("labMongo");
        string companiaSeleccionada = "";

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
            tablaCompanias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void cargarCompanias()
        {
            List<Compania> companias = db.GetCollection<Compania>("companias").Find("{}").ToList();            
            tablaCompanias.DataSource = companias;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Compania comp = new Compania
            {
                nombre = textNombre.Text,
                ano = Int32.Parse(anoCompania.Text),                
                sitio = textSitio.Text
            };

            var companias = db.GetCollection<Compania>("companias");
            companias.InsertOne(comp);
            cargarCompanias();

            textNombre.Text = "";
            textSitio.Text = "";            
        }

        private void tablaCompanias_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
                int position = tablaCompanias.HitTest(e.X, e.Y).RowIndex;

                if (position >= 0)
                {
                    menu.Items.Add("Actualizar").Name = "Actualizar";
                    menu.Items.Add("Eliminar").Name = "Eliminar";
                    companiaSeleccionada = (tablaCompanias[0, position].Value.ToString());

                }
                menu.Show(tablaCompanias, new Point(e.X, e.Y));
                menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
            }
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.ClickedItem.Name.ToString() == "Actualizar")
            {
                if (companiaSeleccionada != "")
                {
                    //var query = Query.EQ("_id", ObjectId.Parse(companiaSeleccionada)); 
                    ObjectId objectId = ObjectId.Parse(companiaSeleccionada);
                    var filter = Builders<Compania>.Filter.Eq("_id", objectId);
                    List<Compania> compania = db.GetCollection<Compania>("companias").Find(filter).ToList();
                    foreach (var comp in compania)
                    {
                        textNombre.Text = comp.nombre;                       
                        textSitio.Text = comp.sitio;
                        anoCompania.Value = new DateTime(comp.ano, 01, 01);                       
                    }
                }
                buttonActualizar.Enabled = true;
                buttonGuardar.Enabled = false;
            }
            else if (e.ClickedItem.Name.ToString() == "Eliminar")
            {
                if (companiaSeleccionada != "")
                {                    
                    var compania = db.GetCollection<Compania>("companias");
                    ObjectId objectId = ObjectId.Parse(companiaSeleccionada);
                    compania.DeleteOne((a => a.Id ==objectId));
                    cargarCompanias();
                }
            }
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            var companias = db.GetCollection<Compania>("companias");
            var query = new QueryDocument {
                {"_id",ObjectId.Parse(companiaSeleccionada) }
            };
            var update = new UpdateDocument
            {
                {"$set", new BsonDocument
                    {
                        { "nombre" ,textNombre.Text},
                        { "ano",  Int32.Parse(anoCompania.Text) },
                        { "sitio",  textSitio.Text }                       
                    }
                }
            };
            companias.UpdateOne(query, update);
            cargarCompanias();
            companiaSeleccionada = "";
            textNombre.Text = "";
            textSitio.Text = "";
            buttonGuardar.Enabled = true;
            buttonActualizar.Enabled = false;
        }
    }
}
