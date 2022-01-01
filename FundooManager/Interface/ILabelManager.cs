// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// interface ILabelManager for label api's
    /// </summary>
    public interface ILabelManager
    {
        /// <summary>
        /// method for getting all the labels by note id
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns all the labels</returns>
        IEnumerable<LabelModel> GetLabelByNoteId(int NoteId);

        /// <summary>
        /// method for getting all the labels by user id
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all the labels</returns>
        IEnumerable<LabelModel> GetLabelByUserId(int UserId);

        /// <summary>
        /// method for adding label
        /// </summary>
        /// <param name="labelModel">passing labelModel parameter for LabelModel</param>
        /// <returns>Returns the label that added</returns>
        Task<LabelModel> AddLabel(LabelModel labelModel);

        /// <summary>
        /// method for deleting the label
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>returns boolean value</returns>
        Task<bool> DeleteLabel(int LabelId);

        /// <summary>
        /// method for removing label from note
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>returns from where the label removed </returns>
        Task<LabelModel> RemoveLabelFromNote(int LabelId);

        /// <summary>
        /// method for rename label according to label id
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>returns the label name that renamed</returns>
        Task<LabelModel> RenameLabel(int LabelId, string Label);

        /// <summary>
        /// method for getting all the notes by label name
        /// </summary>
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>returns all the notes</returns>
        IEnumerable<NoteModel> GetNotesByLabelName(string Label);

    }
}
