
namespace FundooRepository.Interface
{
    using FundooModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ILabelRepository
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
