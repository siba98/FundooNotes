using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string AddNote(NoteModel note)
        {
            try
            {
                this.context.Note.Add(note);
                this.context.SaveChanges();
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
        public string EditNote(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
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

        public string AddReminder(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefault();
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

        public string DeleteReminder(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefault();
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
        public string EditColour(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefault();
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

        public string EditPin(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
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

        public string EditArchive(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
                if (noteExist != null)
                {
                    if (noteExist.Archive == false)
                    {
                        noteExist.Archive = note.Archive;
                        noteExist.Pin = false;
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

        public string EditTrash(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
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

        public string DeleteNoteFromTrash(NoteModel note)
        {
            try
            {
                var noteExist = this.context.Note.Where(x => x.NoteId == note.NoteId).SingleOrDefault();
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
                if (notesExist != null)
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

        public string ImageUpload(int noteId, IFormFile image)
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
                    this.context.SaveChanges();
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
