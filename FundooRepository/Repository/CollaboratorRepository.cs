using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext context;

        public CollaboratorRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                this.context.Collaborator.Add(collaborator);
                await this.context.SaveChangesAsync();
                return "Collaborator Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteCollaborator(int CollaboratorId)
        {
            try
            {
                var collaboratorExist = await this.context.Collaborator.Where(x => x.CollaboratorId == CollaboratorId).SingleOrDefaultAsync();
                if (collaboratorExist != null)
                {
                    this.context.Collaborator.Remove(collaboratorExist);
                    await this.context.SaveChangesAsync();
                    return "Collaborator Deleted Successfully";
                }
                return "Collaborator Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
