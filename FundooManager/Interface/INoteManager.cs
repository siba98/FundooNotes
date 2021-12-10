
namespace FundooManager.Interface
{
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INoteManager
    {
        Task<string> AddNote(NoteModel note);
        Task<string> EditNote(NoteModel note);
        Task<string> AddReminder(int NoteId, string Reminder);
        Task<string> DeleteReminder(int NoteId);
        Task<string> EditColour(int NoteId, string Colour);
        Task<string> PinOrUnPinnedNotes(int NoteId);
        Task<string> ArchiveOrUnArchiveNotes(int NoteId);
        Task<string> TrashOrRestoreNotes(int NoteId);
        Task<string> DeleteNoteFromTrash(int NoteId);
        IEnumerable<NoteModel> GetArchive(int UserId);
        IEnumerable<NoteModel> GetNotes(int UserId);
        IEnumerable<NoteModel> GetTrash(int UserId);
        IEnumerable<NoteModel> GetReminders(int UserId);
        Task<string> ImageUpload(int noteId, IFormFile image);
        Task<string> EmptyTrash(int UserId);
    }
}
