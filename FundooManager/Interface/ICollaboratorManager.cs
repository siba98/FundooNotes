using System.Collections.Generic;
using System.Threading.Tasks;
using FundooModels;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator);
        Task<bool> DeleteCollaborator(int CollaboratorId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}
