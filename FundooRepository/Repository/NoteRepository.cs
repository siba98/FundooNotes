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
    /// NoteRepository class for Note Api's
    /// </summary>
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
        /// Initializes a new instance of the NoteManager class
        /// </summary>
        /// <param name="context">taking context as parameter</param>
        /// <param name="configuration">taking configuration as parameter</param>
        public NoteRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// method for adding new note
        /// </summary>
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>returns string type</returns>
        public async Task<string> AddNote(NoteModel note)
        {
            try
            {
                this.context.Note.Add(note);
                await this.context.SaveChangesAsync();
                return "Note Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for edit the title and description of a note
        /// </summary>
        /// <param name="note">passing note parameter for NoteModel</param>
        /// <returns>returns string type</returns>
        public async Task<string> EditNote(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Title = note.Title;
                    noteExist.Description = note.Description;
                    this.context.Note.Update(noteExist);
                    this.context.SaveChanges();
                    return "Note Updated Successfully";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for adding reminder for a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Reminder">passing parameter as Reminder</param>
        /// <returns></returns>
        public async Task<string> AddReminder(int NoteId, string Reminder)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Reminder = Reminder;
                    this.context.Note.Update(noteExist);
                    this.context.SaveChanges();
                    return "Reminder Added Successfully";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for delete reminder from a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns string type</returns>
        public async Task<string> DeleteReminder(int NoteId)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Reminder = null;
                    this.context.SaveChanges();
                    return "Reminder Removed Successfully";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for edit colour for a note
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <param name="Colour">passing parameter as Colour</param>
        /// <returns>returns string type</returns>
        public async Task<string> EditColour(int NoteId, string Colour)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Colour = Colour;
                    this.context.Note.Update(noteExist);
                    this.context.SaveChanges();
                    return "Colour Updated Successfully";
                }
                return "Colour Not Updated";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note pin or unpin
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns string type</returns>
        public async Task<string> PinOrUnPinnedNotes(int NoteId)
        {
            try
            {
                string message;
                var noteExist = await this.context.Note.Where(x => x.NoteId == NoteId && x.Trash == false).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Pin == false)
                    {
                        noteExist.Pin = true;
                        if (noteExist.Archive == true)
                        {
                            noteExist.Archive = false;
                            noteExist.Pin = true;
                            message = "Note UnArchived and Pinned Successfully";
                        }
                        message = "Note Pinned Successfully";
                    }
                    else
                    {
                        noteExist.Pin = false;
                        message = "Note UnPinned Successfully";
                    }
                    this.context.Note.Update(noteExist);
                    await this.context.SaveChangesAsync();
                }
                message = "Note Not Exist";
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns string type</returns>
        public async Task<string> ArchiveOrUnArchiveNotes(int NoteId)
        {
            try
            {
                string message;
                var noteExist = await this.context.Note.Where(x => x.NoteId == NoteId && x.Trash != true).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Archive == false)
                    {
                        noteExist.Archive = true;
                        if (noteExist.Pin == true)
                        {
                            noteExist.Pin = false;
                            message = "Note Archived and Unpinned Successfully";
                        }
                        message = "Note Archived Successfully";
                    }
                    else
                    {
                        noteExist.Archive = false;
                        message = "Note Unarchived Successfully";
                    }
                    this.context.Note.Update(noteExist);
                    await this.context.SaveChangesAsync();
                }
                message = "Note Not Exist";
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
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns string type</returns>
        public async Task<string> TrashNotes(int NoteId)
        {
            try
            {
                var availNote = await this.context.Note.Where(x => x.NoteId == NoteId).SingleOrDefaultAsync();
                if (availNote != null)
                {
                    availNote.Trash = true;
                    if (availNote.Pin == true)
                    {
                        availNote.Trash = true;
                        availNote.Pin = false;
                        this.context.Note.Update(availNote);
                        await this.context.SaveChangesAsync();
                        return "Note Unpinned and Trashed Successfully";
                    }
                    return "Note Trashed Successfully";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for make a note restore from Trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns string type</returns>
        public async Task<string> RestoreNotesFromTrash(int NoteId)
        {
            try
            {
                var availNote = await this.context.Note.Where(x => x.NoteId == NoteId).SingleOrDefaultAsync();
                if (availNote != null)
                {
                    availNote.Trash = false;
                    availNote.Pin = false;
                    this.context.Note.Update(availNote);
                    await this.context.SaveChangesAsync();
                    return "Note Restored from Trashed Successfully";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for delete note from trash
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns string type</returns>
        public async Task<string> DeleteNoteFromTrash(int NoteId)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash == true)
                    {
                        this.context.Note.Remove(noteExist);
                        this.context.SaveChanges();
                        return "Note Deleted Successfully";
                    }
                }
                return "Note Not Exist in Trash";
            }
            catch (ArgumentNullException ex)
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
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == UserId && x.Archive == true).ToList();
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
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all notes</returns>
        public IEnumerable<NoteModel> GetNotes(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == UserId && x.Archive == false && x.Trash == false).ToList();
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
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all Trash notes</returns>
        public IEnumerable<NoteModel> GetTrash(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == UserId && x.Trash == true).ToList();
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
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all Reminders notes</returns>
        public IEnumerable<NoteModel> GetReminders(int UserId)
        {
            try
            {
                IEnumerable<NoteModel> notesExist = this.context.Note.Where(x => x.UserId == UserId && x.Trash == false && x.Reminder != null).ToList();
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
        /// <returns></returns>
        public async Task<string> ImageUpload(int noteId, IFormFile image)
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
                    return "Image Uploaded Successfully";
                }
                return "noteID not Exist";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}