using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ICollaboratorRepository
    {
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator);
        Task<bool> DeleteCollaborator(int CollaboratorId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}