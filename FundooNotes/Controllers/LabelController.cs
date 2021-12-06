using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

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

        [HttpPut]
        [Route("removeLabelFromNote")]
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

        [HttpPut]
        [Route("renameLabel")]
        public async Task<IActionResult> RenameLabel([FromBody] LabelModel labelModel)
        {
            try
            {
                string message = await this.labelManager.RenameLabel(labelModel);
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
