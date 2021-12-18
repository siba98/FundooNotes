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
        /// 
        /// </summary>
        /// <param name="labelModel"></param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("addLabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelModel labelModel)
        {
            try
            {
                string message = await this.labelManager.AddLabel(labelModel);
                if (message.Equals("Label Added Successfully"))
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
        /// 
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns>response status from api</returns>
        [HttpDelete]
        [Route("deleteLabel")]
        public async Task<IActionResult> DeleteLabel(int LabelId)
        {
            try
            {
                string message = await this.labelManager.DeleteLabel(LabelId);
                if (message.Equals("Label Deleted Successfully"))
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
        /// 
        /// </summary>
        /// <param name="NoteId"></param>
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
                    return this.BadRequest(new { Status = false, Message = "Labels Not Available", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
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
                    return this.Ok(new { Status = true, Message = "Labels Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Labels Not Available", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelName"></param>
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
                    return this.Ok(new { Status = true, Message = "Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes Not Available", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("RemoveLabelFromNote")]
        public async Task<IActionResult> RemoveLabelFromNote(int LabelId)
        {
            try
            {
                string message = await this.labelManager.RemoveLabelFromNote(LabelId);
                if (message.Equals("Label Removed From Note Successfully"))
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
        /// 
        /// </summary>
        /// <param name="LabelId"></param>
        /// <param name="Label"></param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("renameLabel")]
        public async Task<IActionResult> RenameLabel(int LabelId, string Label)
        {
            try
            {
                string message = await this.labelManager.RenameLabel(LabelId, Label);
                if (message.Equals("Label name Successfully Renamed"))
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
    }
}
