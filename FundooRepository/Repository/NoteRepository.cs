// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// class NoteRepository
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.INoteRepository" />
    public class NoteRepository : INoteRepository
    {
        /// <summary>
        /// object created for UserContext
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// object created for IConfiguration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration">The configuration.</param>
        public NoteRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// method for adding new note
        /// </summary>
        /// <param name="noteDetails">passing noteDetails parameter for NoteModel</param>
        /// <returns>returns the note that added</returns>
        public async Task<NoteModel> AddNote(NoteModel noteDetails)
        {
            try
            {
                if (noteDetails.Title != null || noteDetails.Description != null || noteDetails.Reminder != null || noteDetails.Image != null)
                {
                    this.context.Note.Add(noteDetails);
                    await this.context.SaveChangesAsync();
                    return noteDetails;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for edit the title and description of a note
        /// </summary>
        /// <param name="noteDetails">passing note parameter for NoteModel</param>
        /// <returns>returns updated note details</returns>
        public async Task<NoteModel> EditNote(NoteModel noteDetails)
        {
            try
            {
                var validNote = await this.context.Note.Where(x => x.NoteId == noteDetails.NoteId).FirstOrDefaultAsync();
                if (validNote != null && validNote.Trash != true )
                {
                    validNote.Title = noteDetails.Title;
                    validNote.Description = noteDetails.Description;
                    this.context.Note.Update(validNote);
                    await this.context.SaveChangesAsync();
                    return validNote;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for adding reminder for a note
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <param name="reminder">passing parameter as Reminder</param>
        /// <returns>returns updated note that reminder added</returns>
        public async Task<NoteModel> AddReminder(int noteId, string reminder)
        {
            try
            {
                var validNote = await this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefaultAsync();
                if (validNote != null)
                {
                    validNote.Reminder = reminder;
                    this.context.Note.Update(validNote);
                    await this.context.SaveChangesAsync();
                    return validNote;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for delete reminder from a note
        /// </summary>
        /// <param name="noteId">passing parameter as noteId</param>
        /// <returns>returns updated note that reminder deleted</returns>
        public async Task<NoteModel> DeleteReminder(int noteId)
        {
            try
            {
                var validNote = await this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefaultAsync();
                if (validNote != null)
                {
                    validNote.Reminder = null;
                    await this.context.SaveChangesAsync();
                    return validNote;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for edit colour for a note
        /// </summary>
        /// <param name="noteId">passing parameter as noteId</param>
        /// <param name="colour">passing parameter as colour</param>
        /// <returns>returns colour updated details that changed in a note</returns>
        public async Task<NoteModel> EditColour(int noteId, string colour)
        {
            try
            {
                var validNote = await this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefaultAsync();
                if (validNote != null)
                {
                    validNote.Colour = colour;
                    this.context.Note.Update(validNote);
                    await this.context.SaveChangesAsync();
                    return validNote;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note pin or unpin
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <returns>returns the note that it is pin or unpin</returns>
        public async Task<string> PinOrUnPinnedNotes(int noteId)
        {
            try
            {
                string message;
                var validNote = await this.context.Note.Where(x => x.NoteId == noteId && x.Trash == false).SingleOrDefaultAsync();
                if (validNote != null)
                {
                    if (validNote.Pin == false)
                    {
                        validNote.Pin = true;
                        if (validNote.Archive == true)
                        {
                            validNote.Archive = false;
                            message = "Note UnArchived and Pinned Successfully";
                        }
                        else
                        {
                            message = "Note Pinned Successfully";
                        }
                    }
                    else
                    {
                        validNote.Pin = false;
                        message = "Note UnPinned Successfully";
                    }

                    this.context.Note.Update(validNote);
                    await this.context.SaveChangesAsync();
                }
                else
                {
                    message = "Note Not Exist";
                }

                return message;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note Archive Or UnArchive
        /// </summary>
        /// <param name="noteId">passing parameter as noteId</param>
        /// <returns>returns the note that it is archive or unarchive</returns>
        public async Task<string> ArchiveOrUnArchiveNotes(int noteId)
        {
            try
            {
                string message;
                var validNote = await this.context.Note.Where(x => x.NoteId == noteId && x.Trash != true).SingleOrDefaultAsync();
                if (validNote != null)
                {
                    if (validNote.Archive == false)
                    {
                        validNote.Archive = true;
                        if (validNote.Pin == true)
                        {
                            validNote.Pin = false;
                            message = "Note Archived and Unpinned Successfully";
                        }
                        else
                        {
                            message = "Note Archived Successfully";
                        }
                    }
                    else
                    {
                        validNote.Archive = false;
                        message = "Note Unarchived Successfully";
                    }

                    this.context.Note.Update(validNote);
                    await this.context.SaveChangesAsync();
                }
                else
                {
                    message = "Note Not Exist";
                }

                return message;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note Trash
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <returns>returns note that trashed</returns>
        public async Task<NoteModel> TrashNotes(int noteId)
        {
            try
            {
                var availNote = await this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefaultAsync();
                if ((availNote != null && availNote.Pin == true) || (availNote != null && availNote.Archive == true))
                {
                    availNote.Trash = true;
                    availNote.Pin = false;
                    availNote.Archive = false;
                    this.context.Note.Update(availNote);
                    await this.context.SaveChangesAsync();
                    return availNote;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for restore notes from trash
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <returns>returns notes that restored from trash</returns>
        public async Task<NoteModel> RestoreNotesFromTrash(int noteId)
        {
            try
            {
                var availNote = await this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefaultAsync();
                if (availNote != null)
                {
                    availNote.Trash = false;
                    availNote.Pin = false;
                    this.context.Note.Update(availNote);
                    await this.context.SaveChangesAsync();
                    return availNote;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for delete note from trash
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <returns>returns boolean value</returns>
        public async Task<bool> DeleteNoteFromTrash(int noteId)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash == true)
                    {
                        this.context.Note.Remove(noteExist);
                        this.context.SaveChanges();
                        return true;
                    }
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the archived notes
        /// </summary>
        /// <param name="userId">passing parameter as UserId</param>
        /// <returns>returns all archived notes</returns>
        public IEnumerable<NoteModel> GetArchive(int userId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == userId && x.Archive == true).ToList();
                if (notesExist.Count() != 0)
                {
                    return notesExist;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the notes
        /// </summary>
        /// <param name="userId">passing parameter as UserId</param>
        /// <returns>returns all notes</returns>
        public IEnumerable<NoteModel> GetNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == userId && x.Archive == false && x.Trash == false).ToList();
                
                if (notesExist.Count() != 0)
                {
                    return notesExist;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the trash notes
        /// </summary>
        /// <param name="userId">passing parameter as UserId</param>
        /// <returns>returns all Trash notes</returns>
        public IEnumerable<NoteModel> GetTrash(int userId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == userId && x.Trash == true).ToList();
                if (notesExist.Count() != 0)
                {
                    return notesExist;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the reminder notes
        /// </summary>
        /// <param name="userId">passing parameter as UserId</param>
        /// <returns>returns all Reminders notes</returns>
        public IEnumerable<NoteModel> GetReminders(int userId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == userId && x.Trash == false && x.Reminder != null).ToList();
                if (notesExist.Count() != 0)
                {
                    return notesExist;
                }

                return null;
            }
            catch (ArgumentNullException ex)
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
                Account account = new Account(this.configuration.GetValue<string>("CloudinaryAccount:CloudName"), this.configuration.GetValue<string>("CloudinaryAccount:Apikey"), this.configuration.GetValue<string>("CloudinaryAccount:Apisecret"));
                var cloudinary = new Cloudinary(account);
                var uploadparams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadparams);
                string imagePath = uploadResult.Url.ToString();
                var findNote = this.context.Note.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (findNote != null)
                {
                    findNote.Image = imagePath;
                    this.context.Note.Update(findNote);
                    await this.context.SaveChangesAsync();
                    return findNote;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}