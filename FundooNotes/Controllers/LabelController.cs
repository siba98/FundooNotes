// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
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
    /// LabelController class for Labels API implementation
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// Object created for ILabelManager
        /// </summary>
        private readonly ILabelManager labelManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelManager">The label manager.</param>
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        /// <summary>
        /// Api for adding label
        /// </summary>
        /// <param name="labelModel">passing labelModel parameter for LabelModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("addLabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelModel labelModel)
        {
            try
            {
                var result = await this.labelManager.AddLabel(labelModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label Already Exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// an api for delete labels
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>response status from api</returns>
        [HttpDelete]
        [Route("deleteLabel")]
        public async Task<IActionResult> DeleteLabel(int LabelId)
        {
            try
            {
                bool result = await this.labelManager.DeleteLabel(LabelId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label Delete Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// An Api for get all the labels by note id
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>response status from api</returns>
        [HttpGet]
        [Route("getLabelByNoteId")]
        public IActionResult GetLabelByNoteId(int NoteId)
        {
            try
            {
                IEnumerable<LabelModel> result = this.labelManager.GetLabelByNoteId(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Labels Present in Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Labels Not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// An api for get all the labels by user id
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>response status from api</returns>
        [HttpGet]
        [Route("getLabelByUserId")]
        public IActionResult GetLabelByUserId(int userId)
        {
            try
            {
                IEnumerable<LabelModel> result = this.labelManager.GetLabelByUserId(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Labels Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Labels Not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// an api for getting all the notes by label name
        /// </summary>
        /// <param name="labelName">passing parameter as labelName</param>
        /// <returns>response status from api</returns>
        [HttpGet]
        [Route("getNotesByLabelName")]
        public IActionResult GetNotesByLabelName(string labelName)
        {
            try
            {
                IEnumerable<NoteModel> result = this.labelManager.GetNotesByLabelName(labelName);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes Not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// an api for remove label from note
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("removeLabelFromNote")]
        public async Task<IActionResult> RemoveLabelFromNote(int LabelId)
        {
            try
            {
                var result = await this.labelManager.RemoveLabelFromNote(LabelId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label Successfull Removed from Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label Removed from Note UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// an api for rename a label name
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("renameLabel")]
        public async Task<IActionResult> RenameLabel(int labelId, string label)
        {
            try
            {
                var result = await this.labelManager.RenameLabel(labelId, label);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label Renamed Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label Reanme Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
