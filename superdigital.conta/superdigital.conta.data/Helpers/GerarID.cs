using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.data.Helpers
{
    public static class GerarID
    {
        public static ObjectId GerarObjectID()
        {
            return ObjectId.GenerateNewId();
        }
    }
}
