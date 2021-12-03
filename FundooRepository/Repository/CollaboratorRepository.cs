using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext context;

        public CollaboratorRepository(UserContext context)
        {
            this.context = context;
        }

        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                this.context.Collaborator.Add(collaborator);
                this.context.SaveChanges();
                return "Collaborator Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var collaboratorExist = this.context.Collaborator.Where(x => x.CollaboratorId == collaborator.CollaboratorId).SingleOrDefault();
                if (collaboratorExist != null)
                {
                    this.context.Collaborator.Remove(collaboratorExist);
                    this.context.SaveChanges();
                    return "Collaborator Deleted Successfully";
                }
                return "Collaborator Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
