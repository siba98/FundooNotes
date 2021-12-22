// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// class CollaboratorRepository
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ICollaboratorRepository" />
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// object created for UserContext
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CollaboratorRepository(UserContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// method for adding collaborator
        /// </summary>
        /// <param name="collaborator">passing collaborator parameter for CollaboratorModel</param>
        /// <returns>returns the collaborator that added</returns>
        public async Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaboratorDetails)
        {
            try
            {
                var checkEmail = await this.context.Collaborator.Where(x => x.Email == collaboratorDetails.Email).SingleOrDefaultAsync();
                if (checkEmail == null)
                {
                    this.context.Collaborator.Add(collaboratorDetails);
                    await this.context.SaveChangesAsync();
                    return collaboratorDetails;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for deleting label from note
        /// </summary>
        /// <param name="collaboratorId">passing parameter as CollaboratorId</param>
        /// <returns>returns boolean value</returns>
        public async Task<bool> DeleteCollaborator(int collaboratorId)
        {
            try
            {
                var validCollaboratorExist = await this.context.Collaborator.Where(x => x.CollaboratorId == collaboratorId).SingleOrDefaultAsync();
                if (validCollaboratorExist != null)
                {
                    this.context.Collaborator.Remove(validCollaboratorExist);
                    await this.context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the collaborator id's
        /// </summary>
        /// <param name="noteId">passing parameter as noteId</param>
        /// <returns>returns all the collaborators</returns>
        public IEnumerable<CollaboratorModel> GetCollaborator(int noteId)
        {
            try
            {
                IEnumerable<CollaboratorModel> collaboratorList = this.context.Collaborator.Where(x => x.NoteId == noteId).ToList();
                if (collaboratorList.Count() != 0)
                {
                    return collaboratorList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}