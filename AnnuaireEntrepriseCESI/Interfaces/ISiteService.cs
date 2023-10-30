using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntrepriseCESI.Interfaces
{
    public interface ISiteService
    {
        /// <summary>
        /// Récupère tout les sites de l'entreprise
        /// </summary>
        /// <returns>Retourne une liste de site</returns>
        Task<List<SiteDTO>> GetAllSite();

        /// <summary>
        /// Récupère un site par rapport à son ID
        /// </summary>
        /// <param name="id">ID du site</param>
        /// <returns>Retourne le site</returns>
        Task<SiteDTO> GetById(int id);

        /// <summary>
        /// Récupère un site par rapport à son nom
        /// </summary>
        /// <param name="name">Nom du site</param>
        /// <returns>Retourn le site</returns>
        Task<SiteDTO> GetByName(string name);

        /// <summary>
        /// Permet d'ajouter un site 
        /// </summary>
        /// <param name="site">données du site</param>
        /// <returns>Le site créé</returns>
        Task AddSite(Site site);

        /// <summary>
        /// Permet de supprimer un site grace au nom
        /// </summary>
        /// <param name="name">Nom du site</param>
        /// <returns>true = validée</returns>
        Task DeleteSite(string name);

        /// <summary>
        /// Permet de mettre à jour un site
        /// </summary>
        /// <param name="town">Nom du site à modifié</param>
        /// <param name="site">Nouvelle valeur</param>
        /// <returns>Le site modifié</returns>
        Task UpdateSite(string town, Site site);


    }
}
