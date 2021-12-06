using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository noteRepository;
        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public async Task<string> AddNote(NoteModel note)
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

        public async Task<string> EditNote(NoteModel note)
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

        public async Task<string> AddReminder(NoteModel note)
        {
            try
            {
                return await this.noteRepository.AddReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteReminder(NoteModel note)
        {
            try
            {
                return await this.noteRepository.DeleteReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditColour(NoteModel note)
        {
            try
            {
                return await this.noteRepository.EditColour(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditPin(NoteModel note)
        {
            try
            {
                return await this.noteRepository.EditPin(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditArchive(NoteModel note)
        {
            try
            {
                return await this.noteRepository.EditArchive(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditTrash(NoteModel note)
        {
            try
            {
                return await this.noteRepository.EditTrash(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteNoteFromTrash(NoteModel note)
        {
            try
            {
                return await this.noteRepository.DeleteNoteFromTrash(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public async Task<string> ImageUpload(int noteId, IFormFile image)
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
