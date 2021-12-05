using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        [HttpPost]
        [Route("api/addLabelByUserId")]
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

        //[HttpPost]
        //[Route("api/editLabel")]
        //public IActionResult EditLabel(int UserId, string Label)
        //{
        //    try
        //    {
        //        string message = this.labelManager.EditLabel(UserId, Label);
        //        if (message.Equals("Label Edited Successfully"))
        //        {
        //            return this.Ok(new { Status = true, Message = message });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { Status = false, Message = message });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new { Status = false, ex.Message });
        //    }
        //}

    }
}
