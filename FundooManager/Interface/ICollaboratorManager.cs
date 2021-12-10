using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> AddCollaborator(CollaboratorModel collaborator);
        Task<string> DeleteCollaborator(int CollaboratorId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}
