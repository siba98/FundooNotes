// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// NoteController class for Notes API implementation
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager noteManager;

        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        [HttpPost]
        [Route("addNote")]
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
        [Route("editNote")]
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
        [Route("addReminder")]
        public async Task<IActionResult> AddReminder(int NoteId, string Reminder)
        {
            try
            {
                string message = await this.noteManager.AddReminder(NoteId, Reminder);
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
        [Route("deleteReminder")]
        public async Task<IActionResult> DeleteReminder(int NoteId)
        {
            try
            {
                string message = await this.noteManager.DeleteReminder(NoteId);
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
        [Route("editColour")]
        public async Task<IActionResult> EditColour(int NoteId, string Colour)
        {
            try
            {
                string message = await this.noteManager.EditColour(NoteId, Colour);
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
        [Route("pinOrUnPinnedNotes")]
        public async Task<IActionResult> PinOrUnPinnedNotes(int NoteId)
        {
            try
            {
                string message = await this.noteManager.PinOrUnPinnedNotes(NoteId);
                if (message.Equals("Note Pinned Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note UnPinned Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note UnArchived and Pinned Successfully"))
                {
                    return this.Ok(new { status = true, Message = message });
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
        [Route("archiveOrUnArchiveNotes")]
        public async Task<IActionResult> ArchiveOrUnArchiveNotes(int NoteId)
        {
            try
            {
                string message = await this.noteManager.ArchiveOrUnArchiveNotes(NoteId);
                if (message.Equals("Note Archived and Unpinned Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note UnArchived Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note Archived Successfully"))
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
        [Route("trashOrRestoreNotes")]
        public async Task<IActionResult> TrashOrRestoreNotes(int NoteId)
        {
            try
            {
                string message = await this.noteManager.TrashOrRestoreNotes(NoteId);
                if (message.Equals("Note Trashed Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note Restored From Trash Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                if (message.Equals("Note Unpinned and Trashed Successfully"))
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
        [Route("deleteNoteFromTrash")]
        public async Task<IActionResult> DeleteNoteFromTrash(int NoteId)
        {
            try
            {
                string message = await this.noteManager.DeleteNoteFromTrash(NoteId);
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

        [HttpPut]
        [Route("imageUpload")]
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
        [Route("getArchive")]
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
        [Route("getNotes")]
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
        [Route("getTrash")]
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
        [Route("getReminders")]
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
    
        [HttpDelete]
        [Route("EmptyTrash")]
        public async Task<IActionResult> EmptyTrash(int UserId)
        {
            try
            {
                string message = await this.noteManager.EmptyTrash(UserId);
                if (message.Equals("Trash is Successfully Empty"))
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
