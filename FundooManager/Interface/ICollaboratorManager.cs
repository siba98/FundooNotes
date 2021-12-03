using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        string AddCollaborator(CollaboratorModel collaborator);
        string DeleteCollaborator(CollaboratorModel collaborator);
    }
}
