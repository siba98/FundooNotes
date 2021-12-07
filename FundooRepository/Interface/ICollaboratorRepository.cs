using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ICollaboratorRepository
    {
        Task<string> AddCollaborator(CollaboratorModel collaborator);
        Task<string> DeleteCollaborator(int NoteId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}
