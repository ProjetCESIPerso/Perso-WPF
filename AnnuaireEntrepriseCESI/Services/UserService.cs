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
using System.Xml.Linq;

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

                using var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

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
                        Service = item.Service,
                        Site = item.Site
                 
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
            User userUpdate = new User()
            {
                Id = id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                MobilePhone = user.MobilePhone,
                ServiceId = user.ServiceId,
                SiteId = user.SiteId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(userUpdate), Encoding.UTF8, "application/json");

            using(HttpClient client =  new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:7089/api/User/UpdateUser/{id}");
                HttpResponseMessage result = await client.PutAsync(client.BaseAddress, content);
                if (!result.IsSuccessStatusCode)
                    throw new Exception();

                var response = result.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #region Delete (Suppression)
        public async Task DeleteUser(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7089/api/User/DeleteUser/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var result = response.EnsureSuccessStatusCode();
            }
        }
        #endregion

        #endregion
    }
}
