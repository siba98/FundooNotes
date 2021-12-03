using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager collaboratorManager;

        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;
        }

        [HttpPost]
        [Route("api/addCollaborator")]
        public IActionResult AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                string message = this.collaboratorManager.AddCollaborator(collaborator);
                if (message.Equals("Collaborator Added Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteCollaborator")]
        public IActionResult DeleteCollaborator(int NoteId)
        {
            try
            {
                string message = this.collaboratorManager.DeleteCollaborator(NoteId);
                if (message.Equals("Collaborator Deleted Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getCollaborator")]
        public IActionResult GetCollaborator(int NoteId)
        {
            try
            {
                IEnumerable<CollaboratorModel> result = this.collaboratorManager.GetCollaborator(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaborators ID Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaboratos ID Not Available", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
