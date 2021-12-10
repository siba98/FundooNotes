namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    public interface ILabelManager
    {

        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        Task<string> AddLabel(LabelModel labelModel);
        Task<string> DeleteLabel(int LabelId);
        Task<string> RemoveLabelFromNote(int LabelId);
        Task<string> RenameLabel(int LabelId, string Label);
        IEnumerable<LabelModel> GetNotesByLabelName(string Label);
    }
}
