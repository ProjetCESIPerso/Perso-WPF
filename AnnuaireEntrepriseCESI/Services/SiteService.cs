using AnnuaireEntrepriseCESI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.DTOs;
using System.Xml.Linq;

namespace AnnuaireEntrepriseCESI.Services
{
    public class SiteService : ISiteService
    {
        #region Champs
        #endregion

        #region Constructeur
        public SiteService() 
        {

        }

        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task AddSite(Site site)
        {
            string json = JsonConvert.SerializeObject(site); /// on transorme notre objet en json

            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7089/api/Site/AddSite/{site.Town}")) // HERE CHANGE POST TO PUT FOR A CREATION DOC
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
        public async Task<List<SiteDTO>> GetAllSite()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/Site/GetAll").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SiteDTO>>(json) ?? new List<SiteDTO>();
            }
        }

        public async Task<SiteDTO> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/User/GetSiteById/{id}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SiteDTO>(json) ?? new SiteDTO();
            }
        }

        public async Task<SiteDTO> GetByName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/Site/GetSiteByName/{name}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SiteDTO>(json) ?? new SiteDTO();
            }
        }
        #endregion

        #region Update (Modification)
        public async Task UpdateSite(string town, Site site)
        {
            string json = JsonConvert.SerializeObject(site); // on transorme notre objet en json

            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7089/api/Site/UpdateSite/{town}")) // HERE CHANGE POST TO PUT FOR A CREATION DOC
            {
                using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = stringContent;

                using var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (!send.IsSuccessStatusCode)
                    throw new Exception();

                var response = send.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #region Delete (Suppression)
        public async Task DeleteSite(string town)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7089/api/Site/DeleteSite/{town}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var result = response.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #endregion
    }
}
