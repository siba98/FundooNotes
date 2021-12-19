// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
   

    /// <summary>
    /// NoteController class for Notes API implementation
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        /// <summary>
        /// Object created for INoteManager
        /// </summary>
        private readonly INoteManager noteManager;

        /// <summary>
        /// Initializes a new instance of the NoteController class
        /// </summary>
        /// <param name="noteManager">parameter noteManager for INoteManager</param>
        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        /// <summary>
        /// api for adding note
        /// </summary>
        /// <param name="note"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for edit a note
        /// </summary>
        /// <param name="note"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for add reminder
        /// </summary>
        /// <param name="NoteId"></param>
        /// <param name="Reminder"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for delete reminder 
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for edit colour
        /// </summary>
        /// <param name="NoteId"></param>
        /// <param name="Colour"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for pin or unpin note
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("pinOrUnPinnedNotes")]
        public async Task<IActionResult> PinOrUnPinnedNotes(int NoteId)
        {
            try
            {
                string message = await this.noteManager.PinOrUnPinnedNotes(NoteId);
                if (message.Equals("Note Not Exist"))
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// api for archive or unarchive note
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("archiveOrUnArchiveNotes")]
        public async Task<IActionResult> ArchiveOrUnArchiveNotes(int NoteId)
        {
            try
            {
                string message = await this.noteManager.ArchiveOrUnArchiveNotes(NoteId);
                if (message.Equals("Note Not Exist"))
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// api for trash or restore note
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("trashNotes")]
        public async Task<IActionResult> TrashNotes(int NoteId)
        {
            try
            {
                string message = await this.noteManager.TrashNotes(NoteId);
                if (message.Equals("Note Not Exist"))
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// api for delete note from trash
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for upload image in note
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="image"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for get archive notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for get notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>response status from api</returns>
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


        /// <summary>
        /// api for get trash notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// api for get reminder notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>response status from api</returns>
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
    }
}