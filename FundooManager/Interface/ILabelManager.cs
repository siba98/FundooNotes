using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        Task<string> AddLabel(LabelModel labelModel);
        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        IEnumerable<LabelModel> GetNotesByLabelName(string Label);
        Task<string> RemoveLabelFromNote(int LabelId);
        Task<string> DeleteLabel(int LabelId);
        Task<string> RenameLabel(LabelModel labelModel);

    }
}
