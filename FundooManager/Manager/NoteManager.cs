using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository noteRepository;
        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public string AddNote(NoteModel note)
        {
            try
            {
                return this.noteRepository.AddNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EditNote(NoteModel note)
        {
            try
            {
                return this.noteRepository.EditNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string AddReminder(NoteModel note)
        {
            try
            {
                return this.noteRepository.AddReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteReminder(NoteModel note)
        {
            try
            {
                return this.noteRepository.DeleteReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EditColour(NoteModel note)
        {
            try
            {
                return this.noteRepository.EditColour(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EditPin(NoteModel note)
        {
            try
            {
                return this.noteRepository.EditPin(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EditArchive(NoteModel note)
        {
            try
            {
                return this.noteRepository.EditArchive(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EditTrash(NoteModel note)
        {
            try
            {
                return this.noteRepository.EditTrash(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteNoteFromTrash(NoteModel note)
        {
            try
            {
                return this.noteRepository.DeleteNoteFromTrash(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NoteModel> GetArchive(int UserId)
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

        public string ImageUpload(int noteId, IFormFile image)
        {
            try
            {
                return this.noteRepository.ImageUpload(noteId, image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
