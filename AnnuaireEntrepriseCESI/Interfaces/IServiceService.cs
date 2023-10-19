using AnnuaireEntrepriseCESI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntrepriseCESI.Interfaces
{
    public interface IServiceService
    {
        /// <summary>
        /// Récupère tout les services de l'entreprise
        /// </summary>
        /// <returns>Retourne une liste de service</returns>
        Task<List<Service>> GetAllService();

        /// <summary>
        /// Récupère un service par rapport à son ID
        /// </summary>
        /// <param name="id">ID du service</param>
        /// <returns>Retourne le service</returns>
        Task<Service> GetById(int id);

        /// <summary>
        /// Récupère un service par rapport à son nom
        /// </summary>
        /// <param name="name">Nom du service</param>
        /// <returns>Retourn le service</returns>
        Task<Service> GetByName(string name);

        /// <summary>
        /// Permet d'ajouter un service 
        /// </summary>
        /// <param name="site">données du service</param>
        /// <returns>Le service créé</returns>
        Task AddService(Service service);

        /// <summary>
        /// Permet de supprimer un service grace au nom
        /// </summary>
        /// <param name="name">Nom du service</param>
        /// <returns>true = validée</returns>
        Task DeleteService(string name);

        /// <summary>
        /// Permet de mettre à jour un service
        /// </summary>
        /// <param name="town">Nom du service à modifié</param>
        /// <param name="site">Nouvelle valeur</param>
        /// <returns>Le site modifié</returns>
        Task UpdateService(string name, Service service);
    }
}
