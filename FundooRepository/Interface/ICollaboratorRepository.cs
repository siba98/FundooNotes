// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using FundooModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// interface ICollaboratorRepository
    /// </summary>
    public interface ICollaboratorRepository
    {
        /// <summary>
        /// method for adding collaborator
        /// </summary>
        /// <param name="collaborator">passing collaborator parameter for CollaboratorModel</param>
        /// <returns>returns the collaborator that added</returns>
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// method for deleteing label from note
        /// </summary>
        /// <param name="CollaboratorId">passing parameter as CollaboratorId</param>
        /// <returns>returns boolean value</returns>
        Task<bool> DeleteCollaborator(int CollaboratorId);

        /// <summary>
        /// method for getting all the collaborator id's
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns all the collaborators</returns>
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}