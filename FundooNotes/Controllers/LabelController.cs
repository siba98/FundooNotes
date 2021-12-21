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
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;

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
                    return this.BadRequest(new ResponseModel<LabelModel> { Status = false, Message = "Unable to Add Label", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<LabelModel> { Status = false, Message = ex.Message });
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
                    return this.Ok(new ResponseModel<bool> { Status = true, Message = "Label Deleted Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<bool> { Status = false, Message = "Label Delete Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<bool> { Status = false, Message = ex.Message });
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
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Labels Present in Notes Retrieved Successfully", Data = (LabelModel)result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<LabelModel> { Status = false, Message = "Labels Not Available", Data = (LabelModel)result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<LabelModel> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// An api for get all the labels by user id
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>response status from api</returns>
        [HttpGet]
        [Route("getLabelByUserId")]
        public IActionResult GetLabelByUserId(int UserId)
        {
            try
            {
                IEnumerable<LabelModel> result = this.labelManager.GetLabelByUserId(UserId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Labels Retrieved Successfully", Data = (LabelModel)result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<LabelModel> { Status = false, Message = "Labels Not Available", Data = (LabelModel)result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<LabelModel> { Status = false, Message = ex.Message });
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
                IEnumerable<LabelModel> result = this.labelManager.GetNotesByLabelName(labelName);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Notes Retrieved Successfully", Data = (LabelModel)result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<LabelModel> { Status = false, Message = "Notes Not Available", Data = (LabelModel)result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<LabelModel> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// an api for remove label from note
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("RemoveLabelFromNote")]
        public async Task<IActionResult> RemoveLabelFromNote(int LabelId)
        {
            try
            {
                bool result = await this.labelManager.RemoveLabelFromNote(LabelId);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<bool> { Status = true, Message = "Label Successfull Removed from Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<bool> { Status = false, Message = "Label Removed from Note UnSuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<bool> { Status = false, Message = ex.Message });
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
        public async Task<IActionResult> RenameLabel(int LabelId, string Label)
        {
            try
            {
                var result = await this.labelManager.RenameLabel(LabelId, Label);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label Renamed Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<LabelModel> { Status = false, Message = "Label Reanme Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<LabelModel> { Status = false, Message = ex.Message });
            }
        }
    }
}
