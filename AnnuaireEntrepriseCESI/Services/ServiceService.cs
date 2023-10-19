using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntrepriseCESI.Services
{
    public class ServiceService : IServiceService
    {
        #region Champs
        #endregion

        #region Constructeur
        public ServiceService() 
        { 
        
        }
        #endregion

        #region Méthodes publiques

        #region Create (ajout)
        public async Task AddService(Service service)
        {
            string json = JsonConvert.SerializeObject(service); /// on transorme notre objet en json

            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7089/api/Service/AddService/{service.Name}")) // HERE CHANGE POST TO PUT FOR A CREATION DOC
            {
                using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = stringContent;

                using var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false);

                if (!send.IsSuccessStatusCode)
                    throw new Exception();

                var response = send.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #region Read (Lecture)
        public async Task<List<Service>> GetAllService()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/Service/GetAll").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Service>>(json) ?? new List<Service>();
            }
        }

        public async Task<Service> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/Service/GetServiceById/{id}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Service>(json) ?? new Service();
            }
        }

        public async Task<Service> GetByName(string name)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/Service/GetServiceByName/{name}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Service>(json) ?? new Service();
            }
        }
        #endregion

        #region Update (Modification)
        public async Task UpdateService(string name, Service service)
        {
            string json = JsonConvert.SerializeObject(service); // on transorme notre objet en json

            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7089/api/Service/UpdateService/{name}")) // HERE CHANGE POST TO PUT FOR A CREATION DOC
            {
                using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = stringContent;

                using var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false);

                if (!send.IsSuccessStatusCode)
                    throw new Exception();

                var response = send.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #region Delete (Suppression)
        public async Task DeleteService(string name)
        {
            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7089/api/Service/DeleteService/{name}"))
            {
                using var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (!send.IsSuccessStatusCode)
                    throw new Exception();

                var response = send.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #endregion

    }
}
