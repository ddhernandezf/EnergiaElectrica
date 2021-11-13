using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace EnergiaElectrica.ui
{
    public class Api
    {
        private string urlApi { get; set; }
        private RestClient client { get; set; }
        private RestRequest request { get; set; }

        public Api(string urlapi)
        {
            this.urlApi = urlapi;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        }

        public IRestResponse Process(Method requestmethod, string apipart, object body = null)
        {
            this.client = new RestClient(this.urlApi);
            this.request = new RestRequest(apipart, requestmethod) { Timeout = 3600000 };

            if (requestmethod != Method.GET)
                this.request.AddJsonBody(body);

            return Task.Run(() => this.client.Execute(this.request)).Result;
        }
    }
}
