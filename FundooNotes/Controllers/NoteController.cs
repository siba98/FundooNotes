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
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("addNote")]
        public async Task<IActionResult> AddNote([FromBody] NoteModel note)
        {
            try
            {
                var result = await this.noteManager.AddNote(note);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Successfully Added", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Adding Unsuccessful" });
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
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("editNote")]
        public async Task<IActionResult> EditNote([FromBody] NoteModel note)
        {
            try
            {
                var result = await this.noteManager.EditNote(note);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Edited Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Edit Unsuccessful" });
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Reminder">passing parameter as Reminder</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("addReminder")]
        public async Task<IActionResult> AddReminder(int NoteId, string Reminder)
        {
            try
            {
                var result = await this.noteManager.AddReminder(NoteId, Reminder);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Reminder Successfully Added", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reminder Adding Unsuccessful" });
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>response status from api</returns>
        [HttpDelete]
        [Route("deleteReminder")]
        public async Task<IActionResult> DeleteReminder(int NoteId)
        {
            try
            {
                var result = await this.noteManager.DeleteReminder(NoteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Reminder Deleted Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to Delete Reminder " });
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Colour">passing parameter as Colour</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("editColour")]
        public async Task<IActionResult> EditColour(int NoteId, string Colour)
        {
            try
            {
                var result = await this.noteManager.EditColour(NoteId, Colour);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Colour Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Colour Not Updated" });
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
        /// <param name="NoteId">passing parameter as NoteId</param>
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
        /// <param name="NoteId">passing parameter as NoteId</param>
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>response status from api</returns>
        [HttpDelete]
        [Route("trashNotes")]
        public async Task<IActionResult> TrashNotes(int NoteId)
        {
            try
            {
                var result = await this.noteManager.TrashNotes(NoteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Trashed Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Trash Note" });
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>response status from api</returns>
        [HttpDelete]
        [Route("deleteNoteFromTrash")]
        public async Task<IActionResult> DeleteNoteFromTrash(int NoteId)
        {
            try
            {
                var result = await this.noteManager.DeleteNoteFromTrash(NoteId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Note Deleted Forever from Trash Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to Delete Note from Trash" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// method for restore notes from trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("restoreNotesFromTrash")]
        public async Task<IActionResult> RestoreNotesFromTrash(int NoteId)
        {
            try
            {
                var result = await this.noteManager.RestoreNotesFromTrash(NoteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Restored From Trash Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to Restore Note Trash" });
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
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <param name="image">passing parameter as image</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("imageUpload")]
        public async Task<IActionResult> ImageUpload(int noteId, IFormFile image)
        {
            try
            {
                var result = await this.noteManager.ImageUpload(noteId, image);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Successfully Image Uploaded", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to Upload Image" });
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
        /// <param name="UserId">passing parameter as UserId</param>
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
                    return this.Ok(new { Status = true, Message = "Archive Notes Retrieved Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Archived Notes Not Available"});
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
        /// <param name="UserId">passing parameter as UserId</param>
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
                    return this.Ok(new { Status = true, Message = "All Notes Retrived Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes Not Available To Be Retrieve" });
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
        /// <param name="UserId">passing parameter as UserId</param>
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
                    return this.Ok(new { Status = true, Message = "Notes retrieved from the trash successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Trash is empty" });
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
        /// <param name="UserId">passing parameter as UserId</param>
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
                    return this.Ok(new { Status = true, Message = "Reminder For Notes Retrieved Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reminder Not Created For Any Note" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}