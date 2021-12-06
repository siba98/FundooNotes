using FundooModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        Task<string> AddNote(NoteModel note);
        Task<string> EditNote(NoteModel note);
        Task<string> AddReminder(NoteModel note);
        Task<string> DeleteReminder(NoteModel note);
        Task<string> EditColour(NoteModel note);
        Task<string> EditPin(NoteModel note);
        Task<string> EditArchive(NoteModel note);
        Task<string> EditTrash(NoteModel note);
        Task<string> DeleteNoteFromTrash(NoteModel note);
        IEnumerable<NoteModel> GetArchive(int UserId);
        Task<string> ImageUpload(int noteId, IFormFile image);
        IEnumerable<NoteModel> GetNotes(int userId);
        IEnumerable<NoteModel> GetTrash(int userId);
        IEnumerable<NoteModel> GetReminders(int userId);
    }
}
