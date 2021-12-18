// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// CollaboratorManager class for Collaborator Api's
    /// </summary>
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// object created for ICollaboratorRepository
        /// </summary>
        private readonly ICollaboratorRepository collaboratorRepository;

        /// <summary>
        /// Initializes a new instance of the CollaboratorManager class
        /// </summary>
        /// <param name="collaboratorRepository">taking collaboratorRepository as parameter</param>
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }

        /// <summary>
        /// method for adding collaborator
        /// </summary>
        /// <param name="collaborator">passing collaborator parameter for CollaboratorModel</param>
        /// <returns>returns string type</returns>
        public async Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                return await this.collaboratorRepository.AddCollaborator(collaborator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for deleteing label from note
        /// </summary>
        /// <param name="CollaboratorId">passing parameter as CollaboratorId</param>
        /// <returns>returns string type</returns>
        public async Task<string> DeleteCollaborator(int CollaboratorId)
        {
            try
            {
                return await this.collaboratorRepository.DeleteCollaborator(CollaboratorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the collaborator id's
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns all the collaborators</returns>
        public IEnumerable<CollaboratorModel> GetCollaborator(int NoteId)
        {
            try
            {
                return this.collaboratorRepository.GetCollaborator(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}