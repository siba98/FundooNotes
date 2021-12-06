using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    //[Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager noteManager;

        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        [HttpPost]
        [Route("api/addNote")]
        public async Task<IActionResult> AddNote([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.AddNote(note);
                if (message.Equals("Note Added Successfully"))
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
        [Route("api/editNote")]
        public async Task<IActionResult> EditNote([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.EditNote(note);
                if (message.Equals("Note Updated Successfully"))
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
        [Route("api/addReminder")]
        public async Task<IActionResult> AddReminder([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.AddReminder(note);
                if (message.Equals("Reminder Added Successfully"))
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
        [Route("api/deleteReminder")]
        public async Task<IActionResult> DeleteReminder([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.DeleteReminder(note);
                if (message.Equals("Reminder Removed Successfully"))
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
        [Route("api/editColour")]
        public async Task<IActionResult> EditColour([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.EditColour(note);
                if (message.Equals("Colour Updated Successfully"))
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
        [Route("api/EditPin")]
        public async Task<IActionResult> EditPin([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.EditPin(note);
                if (message.Equals("Note Pinned Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note UnPinned Successfully"))
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
        [Route("api/editArchive")]
        public async Task<IActionResult> EditArchive([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.noteManager.EditArchive(note);
                if (message.Equals("Note Archived and Unpinned Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note UnArchived Successfully"))
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
        [Route("api/editTrash")]
        public async Task<IActionResult> EditTrash(NoteModel note)
        {
            try
            {
                string message = await this.noteManager.EditTrash(note);
                if (message.Equals("Note Trashed Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note Not Trashed"))
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
        [Route("api/deleteNoteFromTrash")]
        public async Task<IActionResult> DeleteNoteFromTrash(NoteModel note)
        {
            try
            {
                string message = await this.noteManager.DeleteNoteFromTrash(note);
                if (message.Equals("Note Deleted Successfully"))
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
        [Route("api/restoreNoteFromTrash")]
        public async Task<IActionResult> RestoreNoteFromTrash(int NoteId)
        {
            try
            {
                bool message = await this.noteManager.RestoreNoteFromTrash(NoteId);
                if (message.Equals("Note Restored Successfully"))
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
        [Route("api/imageUpload")]
        public async Task<IActionResult> ImageUpload(int noteId, IFormFile image)
        {
            try
            {
                string message = await this.noteManager.ImageUpload(noteId, image);
                if (message.Equals("Image Uploaded Successfully"))
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
        [Route("api/getArchive")]
        public IActionResult GetArchive(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetArchive(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Archive Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Archived Notes Not Available", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getNotes")]
        public IActionResult GetNotes(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetNotes(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Notes Retrived Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes Not Available To Be Retrieve", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getTrash")]
        public IActionResult GetTrash(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetTrash(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes retrieved from the trash successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Trash is empty", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getReminders")]
        public IActionResult GetReminders(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetReminders(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Reminder For Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reminder Not Created For Any Note", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
