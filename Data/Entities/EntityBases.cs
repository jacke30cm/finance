using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Data.Entities
{
    public abstract class RelationalBase
    {
        public int Id { get; set; }
    }

    public abstract class MongoBase
    {
        public ObjectId Id { get; set; }
    }
}
