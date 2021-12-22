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
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// Object created for ICollaboratorManager
        /// </summary>
        private readonly ICollaboratorManager collaboratorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="collaboratorManager">The collaborator manager.</param>
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
                var result = await this.collaboratorManager.AddCollaborator(collaborator);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CollaboratorModel> { Status = true, Message = "Collaborator Successfully Added", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaborator Already Exist" });
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
                var result = await this.collaboratorManager.DeleteCollaborator(CollaboratorId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Collaborator Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Delete Collaborator Unsuccessful" });
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
                    return this.BadRequest(new { Status = false, Message = "Collaboratos ID Not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}