using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> AddCollaborator(CollaboratorModel collaborator);
        Task<string> DeleteCollaborator(int NoteId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}
