// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INoteManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using FundooModels;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// interface INoteManager for note api's
    /// </summary>
    public interface INoteManager
    {
        /// <summary>
        /// method for adding new note
        /// </summary>
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>returns the note that added</returns>
        Task<NoteModel> AddNote(NoteModel note);

        /// <summary>
        /// method for edit the title and description of a note
        /// </summary>
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>returns updated note details</returns>
        Task<NoteModel> EditNote(NoteModel note);

        /// <summary>
        /// method for adding reminder for a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Reminder">passing parameter as Reminder</param>
        /// <returns>returns updated note that reminder added</returns>
        Task<NoteModel> AddReminder(int NoteId, string Reminder);

        /// <summary>
        /// method for delete reminder from a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns updated note that reminder deleted</returns>
        Task<NoteModel> DeleteReminder(int NoteId);

        /// <summary>
        /// method for edit colour for a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Colour">passing parameter as Colour</param>
        /// <returns>returns colour updated details that changed in a note</returns>
        Task<NoteModel> EditColour(int NoteId, string Colour);

        /// <summary>
        /// method for make a note pin or unpin
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns the note that it is pin or unpin</returns>
        Task<ResponseModel<NoteModel>> PinOrUnPinnedNotes(int NoteId);

        /// <summary>
        /// method for make a note Archive Or UnArchive
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns the note that it is archive or unarchive</returns>
        Task<ResponseModel<NoteModel>> ArchiveOrUnArchiveNotes(int NoteId);

        /// <summary>
        /// method for make a note Trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns note that trashed</returns>
        Task<NoteModel> TrashNotes(int NoteId);

        /// <summary>
        /// method for delete note from trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns boolean value</returns>
        Task<bool> DeleteNoteFromTrash(int NoteId);

        /// <summary>
        /// method for getting all the archived notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all archived notes</returns>
        IEnumerable<NoteModel> GetArchive(int UserId);

        /// <summary>
        /// method for uploading images
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <param name="image">passing parameter as image</param>
        /// <returns>returns image uploaded details to the note</returns>
        Task<NoteModel> ImageUpload(int noteId, IFormFile image);

        /// <summary>
        /// method for getting all the notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all notes</returns>
        IEnumerable<NoteModel> GetNotes(int userId);

        /// <summary>
        /// method for getting all the trash notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all Trash notes</returns>
        IEnumerable<NoteModel> GetTrash(int userId);

        /// <summary>
        /// method for getting all the reminder notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all Reminders notes</returns>
        IEnumerable<NoteModel> GetReminders(int userId);

        /// <summary>
        /// method for restore notes from trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns notes that restored from trash</returns>
        Task<NoteModel> RestoreNotesFromTrash(int noteId);
    }
}
