using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using AnnuaireEntrepriseCESI.DTOs;
using Site = AnnuaireEntrepriseCESI.Models.Site;

namespace AnnuaireEntrepriseCESI.Services
{
    public class UserService : IUserService
    {
        #region Champs
        #endregion

        #region Constructeur
        public UserService() 
        {

        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task AddUser(User user)
        {
            string json = JsonConvert.SerializeObject(user); /// on transorme notre objet en json

            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7089/api/User/AddUser"))
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
        public async Task<List<UserDTO>> GetAllUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/User/GetAll").Result;

                var json = await response.Content.ReadAsStringAsync();

                List<User> listeUser = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();

                List<UserDTO> users = new List<UserDTO>();

                foreach (var item in listeUser)
                {
                    response = client.GetAsync($"https://localhost:7089/api/Service/GetServiceById/{item.ServiceId}").Result;
                    json = await response.Content.ReadAsStringAsync();
                    Service serviceUser = JsonConvert.DeserializeObject<Service>(json) ?? new Service();

                    response = client.GetAsync($"https://localhost:7089/api/Site/GetSiteById/{item.SiteId}").Result;
                    json = await response.Content.ReadAsStringAsync();
                    Site siteUser = JsonConvert.DeserializeObject<Site>(json) ?? new Site();

                    users.Add(new UserDTO() 
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Surname = item.Surname,
                        Email = item.Email,
                        MobilePhone = item.MobilePhone,
                        PhoneNumber = item.PhoneNumber,
                        ServiceId = item.ServiceId,
                        SiteId = item.SiteId,
                        ServiceName = serviceUser.Name,
                        SiteName = siteUser.Town
                 
                    });
                }

                return users;
            }
        }

        public async Task<UserDTO> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/User/GetUserById/{id}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDTO>(json) ?? new UserDTO();
            }
        }

        public async Task<UserDTO> GetByName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/User/GetUserByName/{name}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDTO>(json) ?? new UserDTO();
            }
        }

        public async Task<int> GetNbOfAttributionToService(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/User/GetNbOfAttributionToService/{id}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(json);
            }
        }

        public async Task<int> GetNbOfAttributionToSite(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"https://localhost:7089/api/User/GetNbOfAttributionToSite/{id}").Result;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(json);
            }
        }
        #endregion

        #region Update (Modification)
        public async Task UpdateUser(int id, User user)
        {
            string json = JsonConvert.SerializeObject(user); /// on transorme notre objet en json

            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7089/api/User/UpdateUser/{id}"))
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
        public async Task DeleteUser(int id)
        {
            var client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7089/api/User/DeleteUser/{id}"))
            {
                using var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (!send.IsSuccessStatusCode)
                    throw new Exception();

                var response = send.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #endregion
    }
}
