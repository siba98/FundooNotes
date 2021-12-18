using System.Collections.Generic;
using System.Threading.Tasks;
using FundooModels;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> AddCollaborator(CollaboratorModel collaborator);
        Task<string> DeleteCollaborator(int CollaboratorId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}
