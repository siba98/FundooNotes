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

        public string DeleteCollaborator(int NoteId)
        {
            try
            {
                var collaboratorExist = this.context.Collaborator.Where(x => x.NoteId == NoteId).SingleOrDefault();
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
