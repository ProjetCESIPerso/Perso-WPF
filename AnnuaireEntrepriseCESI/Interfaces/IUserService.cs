using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntrepriseCESI.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Récupère tout les utilisateur de l'entreprise
        /// </summary>
        /// <returns>Retourne une liste d'utilisateur</returns>
        Task<List<UserDTO>> GetAllUsers();

        /// <summary>
        /// Récupère un utilisateur par rapport à son ID
        /// </summary>
        /// <param name="id">ID de l'utilisateur</param>
        /// <returns>Retourne l'utilisateur</returns>
        Task<UserDTO> GetById(int id);

        /// <summary>
        /// Récupère le nombre d'attribution d'un service
        /// </summary>
        /// <param name="id">ID du service</param>
        /// <returns>nombre d'attributions</returns>
        Task<int> GetNbOfAttributionToService(int id);

        /// <summary>
        /// Récupère le nombre d'attribution d'un site
        /// </summary>
        /// <param name="id">ID du site</param>
        /// <returns>nombre d'attributions</returns>
        Task<int> GetNbOfAttributionToSite(int id);


        /// <summary>
        /// Permet d'ajouter un utilisateur 
        /// </summary>
        /// <param name="site">données de l'utilisateur</param>
        /// <returns>L'utilisateur créé</returns>
        Task AddUser(User user);

        /// <summary>
        /// Permet de supprimer un utilisateur grace à l'ID
        /// </summary>
        /// <param name="id">ID de l'utilisateur</param>
        /// <returns>true = validée</returns>
        Task DeleteUser(int id);

        /// <summary>
        /// Permet de mettre à jour un utilisateur
        /// </summary>
        /// <param name="id">ID de l'utilisateur à modifier</param>
        /// <param name="user">Nouvelle valeur</param>
        /// <returns>L'utilisateur modifié</returns>
        Task UpdateUser(int id, User user);
    }
}
