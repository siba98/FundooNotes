using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        Task<string> AddLabel(LabelModel labelModel);
        Task<string> DeleteLabel(int LabelId);
        Task<string> RemoveLabelFromNote(int LabelId);
        Task<string> RenameLabel(LabelModel labelModel);
        IEnumerable<LabelModel> GetNotesByLabelName(string Label);
    }
}
