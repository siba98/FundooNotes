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
        [Route("addLabelByUserId")]
        public IActionResult AddLabelByUserId([FromBody] LabelModel labelModel)
        {
            try
            {
                string message = this.labelManager.AddLabelByUserId(labelModel);
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

        [HttpPost]
        [Route("addLabelByNoteId")]
        public IActionResult AddLabelByNoteId([FromBody] LabelModel labelModel)
        {
            try
            {
                string message = this.labelManager.AddLabelByNoteId(labelModel);
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

        [HttpPost]
        [Route("deleteLabel")]
        public IActionResult DeleteLabel(int UserId, string Label)
        {
            try
            {
                string message = this.labelManager.DeleteLabel(UserId, Label);
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

        [HttpPost]
        [Route("removeLabelFromNote")]
        public IActionResult RemoveLabelFromNote(int UserId, int NoteId, string Label)
        {
            try
            {
                string message = this.labelManager.RemoveLabelFromNote(UserId, NoteId, Label);
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

    }
}
