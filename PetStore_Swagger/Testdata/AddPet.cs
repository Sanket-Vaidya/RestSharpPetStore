using AventStack.ExtentReports.Model;
using NUnit.Framework.Constraints;
using PetStore_Swagger.RequestBody.AddPetRequestBody;
using PetStoreRestAPITests.TestSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore_Swagger.Testdata
{
    public class AddPet
    {
        public static Categories cat = new Categories()
        {
            id = 0,
            name = "Commando"
        };

        public static Tag tag = new Tag()
        {
            id = 0,
            name = "Cheetah"
        };

        public static List<Tag> tagList = new List<Tag>() { tag };
        public static List<string> photourl = new List<string>() { "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR_bMkaO58MSdpoLx7DyxKqUztkntGYslvBOQLNQw8o0FbQuTW9" };

        public static AddPetReqBody addPetReqBody = new AddPetReqBody()
        {
            id = 0,
            category = cat,
            name = "Panther",
            photoUrls = photourl,
            tags = tagList,
            status = "Available"
        };

        public static AddPetReqBody updatePetbody = new AddPetReqBody()
        {
            id = Pet_API_Test.id,
            category = cat,
            name = "Black panther",
            photoUrls = photourl,
            tags = tagList,
            status = "Available"
        };

    }
}
