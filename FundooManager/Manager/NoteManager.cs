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

        public async Task<string> EditColour(int NoteId, string Colour)
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

        public async Task<string> AddReminder(int NoteId, string Reminder)
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

        public async Task<string> DeleteReminder(int NoteId)
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

        public async Task<string> PinOrUnPinnedNotes(int NoteId)
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

        public async Task<string> ArchiveOrUnArchiveNotes(int NoteId)
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

        public async Task<string> TrashNotes(int NoteId)
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


        public async Task<string> DeleteNoteFromTrash(int NoteId)
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

        public async Task<string> RestoreNotesFromTrash(int NoteId)
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
