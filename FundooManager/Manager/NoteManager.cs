// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// NoteManager class for Note Api's
    /// </summary>
    /// <seealso cref="FundooManager.Interface.INoteManager" />
    public class NoteManager : INoteManager
    {
        /// <summary>
        /// object created for INoteRepository
        /// </summary>
        private readonly INoteRepository noteRepository;

        /// <summary>
        /// Initializes a new instance of the NoteManager class
        /// </summary>
        /// <param name="noteRepository">taking noteRepository as parameter</param>
        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        /// <summary>
        /// method for adding new note
        /// </summary>
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>returns the note that added</returns>
        public async Task<NoteModel> AddNote(NoteModel note)
        {
            try
            {
                return await this.noteRepository.AddNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for edit the title and description of a note
        /// </summary>
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>returns updated note details</returns>
        public async Task<NoteModel> EditNote(NoteModel note)
        {
            try
            {
                return await this.noteRepository.EditNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for adding reminder for a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Reminder">passing parameter as Reminder</param>
        /// <returns>returns updated note that reminder added</returns>
        public async Task<NoteModel> AddReminder(int NoteId, string Reminder)
        {
            try
            {
                return await this.noteRepository.AddReminder(NoteId, Reminder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for delete reminder from a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns updated note that reminder deleted</returns>
        public async Task<NoteModel> DeleteReminder(int NoteId)
        {
            try
            {
                return await this.noteRepository.DeleteReminder(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for edit colour for a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Colour">passing parameter as Colour</param>
        /// <returns>returns colour updated details that changed in a note</returns>
        public async Task<NoteModel> EditColour(int NoteId, string Colour)
        {
            try
            {
                return await this.noteRepository.EditColour(NoteId, Colour);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note pin or unpin
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns the note that it is pin or unpin</returns>
        public async Task<ResponseModel<NoteModel>> PinOrUnPinnedNotes(int NoteId)
        {
            try
            {
                return await this.noteRepository.PinOrUnPinnedNotes(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note Archive Or UnArchive
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns the note that it is archive or unarchive</returns>
        public async Task<ResponseModel<NoteModel>> ArchiveOrUnArchiveNotes(int NoteId)
        {
            try
            {
                return await this.noteRepository.ArchiveOrUnArchiveNotes(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note Trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns note that trashed</returns>
        public async Task<ResponseModel<NoteModel>> TrashNotes(int NoteId)
        {
            try
            {
                return await this.noteRepository.TrashNotes(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for delete note from trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns boolean value</returns>
        public async Task<bool> DeleteNoteFromTrash(int NoteId)
        {
            try
            {
                return await this.noteRepository.DeleteNoteFromTrash(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for restore notes from trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns notes that restored from trash</returns>
        public async Task<NoteModel> RestoreNotesFromTrash(int NoteId)
        {
            try
            {
                return await this.noteRepository.RestoreNotesFromTrash(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the archived notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all archived notes</returns>
        public IEnumerable<NoteModel> GetArchive(int UserId)
        {
            try
            {
                return this.noteRepository.GetArchive(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all notes</returns>
        public IEnumerable<NoteModel> GetNotes(int UserId)
        {
            try
            {
                return this.noteRepository.GetNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the trash notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all Trash notes</returns>
        public IEnumerable<NoteModel> GetTrash(int UserId)
        {
            try
            {
                return this.noteRepository.GetTrash(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the reminder notes
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all Reminders notes</returns>
        public IEnumerable<NoteModel> GetReminders(int UserId)
        {
            try
            {
                return this.noteRepository.GetReminders(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for uploading images
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <param name="image">passing parameter as image</param>
        /// <returns>returns image uploaded details to the note</returns>
        public async Task<NoteModel> ImageUpload(int noteId, IFormFile image)
        {
            try
            {
                return await this.noteRepository.ImageUpload(noteId, image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}