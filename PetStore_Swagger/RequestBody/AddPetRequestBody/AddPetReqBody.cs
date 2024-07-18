using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore_Swagger.RequestBody.AddPetRequestBody
{
    public class AddPetReqBody
    {
        public long id { get; set; }
        public Categories category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }
    }
}
