// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller class for collaborators API implementation 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// Object created for ICollaboratorManager
        /// </summary>
        private readonly ICollaboratorManager collaboratorManager;

        /// <summary>
        /// Initializes a new instance of the CollaboratorController class
        /// </summary>
        /// <param name="collaboratorManager">parameter collaboratorManager for ICollaboratorManager</param>
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;
        }

        /// <summary>
        /// adding new collaborator
        /// </summary>
        /// <param name="collaborator">passing collaborator parameter for CollaboratorModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("addCollaborator")]
        public async Task<IActionResult> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                string message = await this.collaboratorManager.AddCollaborator(collaborator);
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

        /// <summary>
        /// deleting existing collaborator
        /// </summary>
        /// <param name="CollaboratorId">passing parameter CollaboratorId</param>
        /// <returns>response status from api</returns>
        [HttpDelete]
        [Route("deleteCollaborator")]
        public async Task<IActionResult> DeleteCollaborator(int CollaboratorId)
        {
            try
            {
                string message = await this.collaboratorManager.DeleteCollaborator(CollaboratorId);
                if (message.Equals("Collaborator ID Deleted Successfully"))
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

        /// <summary>
        /// getting all collaborator
        /// </summary>
        /// <param name="NoteId">passing parameter NoteId</param>
        /// <returns>response status from api</returns>
        [HttpGet]
        [Route("getCollaborator")]
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