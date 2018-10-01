using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AplicacionLabMongoDB.Clases
{
    class Compania
    {
        public ObjectId Id { get; private set; }
        public string nombre { get; set; }
        public int ano { get; set; }
        public string sitio { get; set; }
    }
}
