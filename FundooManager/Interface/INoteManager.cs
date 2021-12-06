using FundooModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
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
        IEnumerable<NoteModel> GetNotes(int UserId);
        IEnumerable<NoteModel> GetTrash(int UserId);
        IEnumerable<NoteModel> GetReminders(int UserId);
        Task<string> ImageUpload(int noteId, IFormFile image);
        Task<bool> RestoreNoteFromTrash(int NoteId);
    }
}
