using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        Task<NoteModel> AddNote(NoteModel note);
        Task<NoteModel> EditNote(NoteModel note);
        Task<NoteModel> AddReminder(int NoteId, string Reminder);
        Task<NoteModel> DeleteReminder(int NoteId);
        Task<NoteModel> EditColour(int NoteId, string Colour);
        Task<string> PinOrUnPinnedNotes(int NoteId);
        Task<string> ArchiveOrUnArchiveNotes(int NoteId);
        Task<NoteModel> TrashNotes(int NoteId);
        Task<NoteModel> DeleteNoteFromTrash(int NoteId);
        IEnumerable<NoteModel> GetArchive(int UserId);
        Task<NoteModel> ImageUpload(int noteId, IFormFile image);
        IEnumerable<NoteModel> GetNotes(int userId);
        IEnumerable<NoteModel> GetTrash(int userId);
        IEnumerable<NoteModel> GetReminders(int userId);
        Task<NoteModel> RestoreNotesFromTrash(int noteId);
    }
}