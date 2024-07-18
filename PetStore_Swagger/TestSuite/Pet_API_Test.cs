using NUnit.Framework;
using PetStoreRestAPITests.Helper;
using PetStoreRestAPITests.ResponseFolder;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using System.IO;
using PetStore_Swagger.RequestBody;
using PetStore_Swagger.RequestBody.AddPetRequestBody;
using PetStore_Swagger.Testdata;

namespace PetStoreRestAPITests.TestSuite
{

    [TestFixture]
    public class Pet_API_Test : BaseClass
    {
        private readonly object syslock=new object();
        public static long id = 0;
        string base_url = "https://petstore.swagger.io/v2/";
        string post_upload_image = @"pet/9222968140497182000/uploadImage";
        string addPetUrl = "pet";
        string getPetById = "pet/";
        string getPetByStatus = "pet/findByStatus";
        public static string workingDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
        string imageToUpload = projectDirectory + "\\Images\\Image1.jpg";

        [Test, Order(0)]
        public void PostPetImage_001()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Accept", "application/json");
            RequestClientHelper client = new RequestClientHelper();
            IRestResponse<PetStore_Upload_Image_Response> restResponse = client.PerformPostRequest<PetStore_Upload_Image_Response>(base_url + post_upload_image, headers, null, DataFormat.None, imageToUpload, null);
            test.GenerateLog(Status.Pass, "PetStore Image upload done");
            Assert.That(200, Is.EqualTo((int)restResponse.StatusCode));
        }

        [Test, Order(1)]
        public void AddPet_002()
        {
            //  AddPet addPet = new AddPet();
            RequestClientHelper client = new RequestClientHelper();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Accept", "application/json");
            IRestResponse<AddPetReqBody> restResponse = client.PerformPostRequest<AddPetReqBody>(base_url + addPetUrl, headers, AddPet.addPetReqBody, DataFormat.Json, null, null);
            id = restResponse.Data.id;
            string petName = restResponse.Data.name;
            test.GenerateLog(Status.Info, "ID of the pet created is " + id);
            test.GenerateLog(Status.Info, "name of the pet " + petName);
            test.GenerateLog(Status.Pass, "PetStore pet added done");
            Assert.That("Panther", Is.EqualTo(petName));
            Assert.That(200, Is.EqualTo((int)restResponse.StatusCode));
        }

        [Test, Order(2)]
        public void UpdatePet_003()
        {
            //  AddPet addPet = new AddPet();
            RequestClientHelper client = new RequestClientHelper();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Accept", "application/json");
            IRestResponse<AddPetReqBody> restResponse = client.PerformPutRequest<AddPetReqBody>(base_url + addPetUrl, headers, AddPet.updatePetbody, DataFormat.Json, null);
            id = restResponse.Data.id;
            string petName = restResponse.Data.name;
            test.GenerateLog(Status.Info, "ID of the pet created is " + id);
            test.GenerateLog(Status.Info, "name of the pet " + petName);
            Assert.That("Black panther", Is.EqualTo(petName));
            Assert.That(200, Is.EqualTo((int)restResponse.StatusCode));
        }

        [Test, Order(3)]
        public void GetPetById()
        {
            RequestClientHelper client = new RequestClientHelper();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Accept", "application/json");
            IRestResponse<AddPetReqBody> restResponse = client.PerformGetRequest<AddPetReqBody>(base_url + getPetById + id, headers, null);
            string petName = restResponse.Data.name;
            test.GenerateLog(Status.Info, "Pet name is " + petName);
            test.GenerateLog(Status.Pass, "get pet by ID is passed");
            Assert.That("Black panther", Is.EqualTo(petName));
            Assert.That(200, Is.EqualTo((int)restResponse.StatusCode));
        }

        [Test, Order(4)]
        public void GetPetByStatus()
        {
            RequestClientHelper client = new RequestClientHelper();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Accept", "application/json");
            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("status", "available");

            IRestResponse<List<AddPetReqBody>> restResponse = client.PerformGetRequest<List<AddPetReqBody>>(base_url + getPetByStatus, headers, queryParameter);
            int count = restResponse.Data.Count;
            test.GenerateLog(Status.Info, "Total count of avavilable pets are " + count);
            test.GenerateLog(Status.Pass, "get pet by ID is passed");

            Assert.That(200, Is.EqualTo((int)restResponse.StatusCode));
        }


    }

}
