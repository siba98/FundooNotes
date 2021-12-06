using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        string AddLabelByUserId(LabelModel labelModel);
        string AddLabelByNoteId(LabelModel labelModel);
        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        string DeleteLabel(int UserId, string Label);
        string RemoveLabelFromNote(int UserId, int NoteId, string Label);
    }
}
