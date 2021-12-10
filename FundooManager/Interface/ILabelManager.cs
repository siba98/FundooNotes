namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    public interface ILabelManager
    {

        Task<string> AddLabel(LabelModel labelModel);

        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);

        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        
        IEnumerable<LabelModel> GetNotesByLabelName(string Label);
        
        Task<string> RemoveLabelFromNote(int LabelId);
        
        Task<string> DeleteLabel(int LabelId);
        
        Task<string> RenameLabel(int LabelId, string Label);
    }
}
