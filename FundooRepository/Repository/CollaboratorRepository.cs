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
    /// CollaboratorRepository class for Collaborator Api's
    /// </summary>
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// object created for UserContext
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the CollaboratorRepository class
        /// </summary>
        /// <param name="context">taking context as parameter</param>
        public CollaboratorRepository(UserContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// method for adding collaborator
        /// </summary>
        /// <param name="collaborator">passing collaborator parameter for CollaboratorModel</param>
        /// <returns>returns the collaborator that added</returns>
        public async Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                this.context.Collaborator.Add(collaborator);
                await this.context.SaveChangesAsync();
                return collaborator;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for deleteing label from note
        /// </summary>
        /// <param name="CollaboratorId">passing parameter as CollaboratorId</param>
        /// <returns>returns boolean value</returns>
        public async Task<bool> DeleteCollaborator(int CollaboratorId)
        {
            try
            {
                var validCollaboratorExist = await this.context.Collaborator.Where(x => x.CollaboratorId == CollaboratorId).SingleOrDefaultAsync();
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns all the collaborators</returns>
        public IEnumerable<CollaboratorModel> GetCollaborator(int NoteId)
        {
            try
            {
                IEnumerable<CollaboratorModel> CollaboratorList = this.context.Collaborator.Where(x => x.NoteId == NoteId).ToList();
                if (CollaboratorList.Count() != 0)
                {
                    return CollaboratorList;
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