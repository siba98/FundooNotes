using FundooModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        string AddNote(NoteModel note);
        string EditNote(NoteModel note); 
        string AddReminder(NoteModel note);
        string DeleteReminder(NoteModel note);
        string EditColour(NoteModel note);
        string EditPin(NoteModel note);
        string EditArchive(NoteModel note);
        string EditTrash(NoteModel note);
        string DeleteNoteFromTrash(NoteModel note);
        IEnumerable<NoteModel> GetArchive(int UserId);
        IEnumerable<NoteModel> GetNotes(int UserId);
        string ImageUpload(int noteId, IFormFile image);
    }
}
