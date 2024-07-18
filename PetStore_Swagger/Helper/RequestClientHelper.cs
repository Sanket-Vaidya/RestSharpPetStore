using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreRestAPITests.Helper
{
    public class RequestClientHelper
    {
        private IRestClient GetResClient()
        {
            IRestClient restClient = new RestClient();
            return restClient;
        }

        private IRestRequest CreateRestRequest(string url, Method method, Dictionary<string, string> headers, object body, DataFormat format, string filePath,Dictionary<string,string> queryParameter)
        {
            IRestRequest restRequest = new RestRequest()
            {
                Resource = url,
                Method = method,
            };
            if (queryParameter != null)
            {
                foreach(string query in queryParameter.Keys)
                {
                    restRequest.AddQueryParameter(query, queryParameter[query]);
                }
            }
           
            foreach (string key in headers.Keys)
            {
                restRequest.AddHeader(key, headers[key]);
            }
            restRequest.RequestFormat = format;

            switch (format)
            {
                case DataFormat.Json:
                    restRequest.AddBody(body);
                    break;
                case DataFormat.Xml:
                    restRequest.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
                    restRequest.AddParameter("xmlData", body.GetType().Equals(typeof(string)) ? body : restRequest.XmlSerializer.Serialize(body), ParameterType.RequestBody);
                    break;
            }
            if (filePath != null)
            {
                restRequest.AddFile("file", filePath, "multipart/form-data");
            }
            return restRequest;
        }

        private IRestResponse SendRequest(IRestRequest restRequest)
        {
            IRestClient client = GetResClient();
            IRestResponse restResponse = client.Execute(restRequest);
            return restResponse;
        }

        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest) where T : new()
        {
            IRestClient client = GetResClient();
            IRestResponse<T> restResponse = client.Execute<T>(restRequest);
            if (restResponse.ContentType.Equals("application/xml"))
            {
                var desrialize = new RestSharp.Deserializers.DotNetXmlDeserializer();
                restResponse.Data = desrialize.Deserialize<T>(restResponse);
            }
            return restResponse;
        }

        public IRestResponse PerformGetRequest(string url, Dictionary<string, string> headers,Dictionary<string,string> queryParameter)
        {
            IRestRequest restRequest = CreateRestRequest(url, Method.GET, headers, null, DataFormat.None, null,queryParameter);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse<T> PerformGetRequest<T>(string url, Dictionary<string, string> headers, Dictionary<string, string> queryParameter) where T : new()
        {
            IRestClient restClient = GetResClient();
            IRestRequest restRequest = CreateRestRequest(url, Method.GET, headers, null, DataFormat.None, null, queryParameter);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        public IRestResponse PerformPostRequest(string url, Dictionary<string, string> headers, object body, DataFormat format, string filePath, Dictionary<string, string> queryParameter)
        {
            IRestRequest restRequest = CreateRestRequest(url, Method.POST, headers, body, format, filePath, queryParameter);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse<T> PerformPostRequest<T>(string url, Dictionary<string, string> headers, object body, DataFormat format, string filePath, Dictionary<string, string> queryParameter) where T:new() 
        {
            IRestRequest restRequest = CreateRestRequest(url, Method.POST, headers, body, format, filePath, queryParameter);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        public IRestResponse PerformPutRequest(string url, Dictionary<string, string> headers, object body, DataFormat format, Dictionary<string, string> queryParameter)
        {
            IRestRequest restRequest = CreateRestRequest(url, Method.PUT, headers, body, format, null, queryParameter);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse<T> PerformPutRequest<T>(string url, Dictionary<string, string> headers, object body, DataFormat format, Dictionary<string, string> queryParameter) where T : new()
        {
            IRestRequest restRequest = CreateRestRequest(url, Method.PUT, headers, body, format, null, queryParameter);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        public IRestResponse PerformDeleteRequest(string url, Dictionary<string, string> headers, Dictionary<string, string> queryParameter)
        {
            IRestRequest restRequest = CreateRestRequest(url, Method.DELETE, headers, null, DataFormat.None, null, queryParameter);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse<T> PerformDeleteRequest<T>(string url, Dictionary<string, string> headers, Dictionary<string, string> queryParameter) where T : new()
        {
            IRestClient restClient = GetResClient();
            IRestRequest restRequest = CreateRestRequest(url, Method.DELETE, headers, null, DataFormat.None, null, queryParameter);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

    }
}


