// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// LabelManager class for Label Api's
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ILabelManager" />
    public class LabelManager:ILabelManager
    {
        /// <summary>
        /// object created for ILabelRepository
        /// </summary>
        private readonly ILabelRepository labelRepository;

        /// <summary>
        /// Initializes a new instance of the LabelManager class
        /// </summary>
        /// <param name="labelRepository">taking labelRepository as parameter</param>
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        /// <summary>
        /// method for adding label
        /// </summary>
        /// <param name="labelModel">passing labelModel parameter for LabelModel</param>
        /// <returns>Returns the label that added</returns>
        public async Task<LabelModel> AddLabel(LabelModel labelModel)
        {
            try
            {
                return await this.labelRepository.AddLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the labels by note id
        /// </summary>
        /// <param name="NoteId">passing parameter as NoteId</param>
        /// <returns>returns all the labels</returns>
        public IEnumerable<LabelModel> GetLabelByNoteId(int NoteId)
        {
            try
            {
                return this.labelRepository.GetLabelByNoteId(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the labels by user id
        /// </summary>
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all the labels</returns>
        public IEnumerable<LabelModel> GetLabelByUserId(int UserId)
        {
            try
            {
                return this.labelRepository.GetLabelByUserId(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the notes by label name
        /// </summary>
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>returns all the notes</returns>
        public IEnumerable<NoteModel> GetNotesByLabelName(string Label)
        {
            try
            {
                return this.labelRepository.GetNotesByLabelName(Label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for deleting the label
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>returns boolean value</returns>
        public async Task<bool> DeleteLabel(int LabelId)
        {
            try
            {
                return await this.labelRepository.DeleteLabel(LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for removing label from note
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>returns from where the label removed </returns>
        public async Task<LabelModel> RemoveLabelFromNote(int LabelId)
        {
            try
            {
                return await this.labelRepository.RemoveLabelFromNote(LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for rename label according to label id
        /// </summary>
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>returns the label name that renamed</returns>
        public async Task<LabelModel> RenameLabel(int LabelId, string Label)
        {
            try
            {
                return await this.labelRepository.RenameLabel(LabelId, Label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
