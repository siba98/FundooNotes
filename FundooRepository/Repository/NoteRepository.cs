using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public NoteRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

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
        /// Will Update Title and Description of Note
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
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

        public async Task<string> AddReminder(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Reminder = note.Reminder;
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

        public async Task<string> DeleteReminder(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
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
        public async Task<string> EditColour(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Colour = note.Colour;
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

        public async Task<string> EditPin(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Pin == false)
                    {
                        noteExist.Pin = note.Pin;
                        this.context.Note.Update(noteExist);
                        this.context.SaveChanges();
                        return "Note Pinned Successfully";
                    }
                    if (noteExist.Pin == true)
                    {
                        noteExist.Pin = note.Pin;
                        this.context.Note.Update(noteExist);
                        this.context.SaveChanges();
                        return "Note UnPinned Successfully";
                    }
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditArchive(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Archive == false)
                    {
                        noteExist.Archive = note.Archive;
                        noteExist.Pin = note.Pin;
                        this.context.Note.Update(noteExist);
                        this.context.SaveChanges();
                        return "Note Archived and Unpinned Successfully";
                    }
                    if (noteExist.Archive == true)
                    {
                        noteExist.Archive = note.Archive;
                        this.context.Note.Update(noteExist);
                        this.context.SaveChanges();
                        return "Note UnArchive Successfully";
                    }
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditTrash(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash == false)
                    {
                        noteExist.Trash = note.Trash;
                        this.context.Note.Update(noteExist);
                        this.context.SaveChanges();
                        return "Note Trashed Successfully";
                    }
                    if (noteExist.Trash == true)
                    {
                        noteExist.Trash = note.Trash;
                        this.context.Note.Update(noteExist);
                        this.context.SaveChanges();
                        return "Note Not Trashed";
                    }
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteNoteFromTrash(NoteModel note)
        {
            try
            {
                var noteExist = await this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefaultAsync();
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
