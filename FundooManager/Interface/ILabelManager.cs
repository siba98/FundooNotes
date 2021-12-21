using System.Collections.Generic;
using System.Threading.Tasks;
using FundooModels;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        Task<LabelModel> AddLabel(LabelModel labelModel);
        Task<bool> DeleteLabel(int LabelId);
        Task<bool> RemoveLabelFromNote(int LabelId);
        Task<LabelModel> RenameLabel(int LabelId, string Label);
        IEnumerable<LabelModel> GetNotesByLabelName(string Label);
    }
}
