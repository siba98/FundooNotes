namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;
    public interface ILabelManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelModel"></param>
        /// <returns></returns>
        Task<string> AddLabel(LabelModel labelModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);
        
        IEnumerable<LabelModel> GetNotesByLabelName(string Label);
        
        Task<string> RemoveLabelFromNote(int LabelId);
        
        Task<string> DeleteLabel(int LabelId);
        
        Task<string> RenameLabel(int LabelId, string Label);
    }
}
