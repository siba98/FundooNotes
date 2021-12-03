using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        string AddCollaborator(CollaboratorModel collaborator);
        string DeleteCollaborator(int NoteId);
        IEnumerable<CollaboratorModel> GetCollaborator(int NoteId);
    }
}
