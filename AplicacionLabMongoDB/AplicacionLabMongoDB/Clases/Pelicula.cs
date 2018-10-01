using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AplicacionLabMongoDB.Clases
{
    class Pelicula
    {
        public ObjectId Id { get; private set; }
        public string nombre { get; set; }
        public string genero { get; set; }
        public string director { get; set; }
        public string franquicia { get; set; }
        public string pais { get; set; }
        public int ano { get; set; }
        public int duracion { get; set; }
        public string compania { get; set; }
        public List<string> reparto { get; set; }
    }
}
