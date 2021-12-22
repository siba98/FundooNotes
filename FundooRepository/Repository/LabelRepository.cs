// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// class LabelRepository
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ILabelRepository" />
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// object created for UserContext
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LabelRepository(UserContext context)
        {
            this.context = context;
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
                var validLabel = await this.context.Labels.Where(x => x.Label == labelModel.Label).SingleOrDefaultAsync();
                if (validLabel == null)
                {
                    this.context.Labels.Add(labelModel);
                    await this.context.SaveChangesAsync();
                    return labelModel;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the labels by note id
        /// </summary>
        /// <param name="noteId">passing parameter as NoteId</param>
        /// <returns>returns all the labels</returns>
        public IEnumerable<LabelModel> GetLabelByNoteId(int noteId)
        {
            try
            {
                IEnumerable<LabelModel> labelList = this.context.Labels.Where(x => x.NoteId == noteId).ToList();
                if (labelList.Count() != 0)
                {
                    return labelList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the labels by user id
        /// </summary>
        /// <param name="userId">passing parameter as UserId</param>
        /// <returns>returns all the labels</returns>
        public IEnumerable<LabelModel> GetLabelByUserId(int userId)
        {
            try
            {
                IEnumerable<LabelModel> labelList = this.context.Labels.Where(x => x.UserId == userId).ToList();
                if (labelList.Count() != 0)
                {
                    return labelList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for deleting the label
        /// </summary>
        /// <param name="labelId">passing parameter as LabelId</param>
        /// <returns>returns boolean type</returns>
        public async Task<bool> DeleteLabel(int labelId)
        {
            try
            {
                var validLabel = await this.context.Labels.Where(x => x.LabelId == labelId).SingleOrDefaultAsync();
                if (validLabel != null)
                {
                    this.context.Labels.Remove(validLabel);
                    await this.context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for removing label from note
        /// </summary>
        /// <param name="labelId">passing parameter as LabelId</param>
        /// <returns>returns object from where the label removed </returns>
        public async Task<LabelModel> RemoveLabelFromNote(int labelId)
        {
            try
            {
                var validLabel = await this.context.Labels.Where(x => x.LabelId == labelId).SingleOrDefaultAsync();
                if (validLabel != null)
                {
                    validLabel.NoteId = null;
                    await this.context.SaveChangesAsync();
                    return validLabel;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting all the notes by label name
        /// </summary>
        /// <param name="label">passing parameter as Label</param>
        /// <returns>returns all the notes</returns>
        public IEnumerable<LabelModel> GetNotesByLabelName(string label)
        {
            try
            {
                IEnumerable<LabelModel> labelExist = this.context.Labels.Where(x => x.Label == label).ToList();
                if (labelExist.Count() != 0)
                {
                    return labelExist;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for rename label according to label id
        /// </summary>
        /// <param name="labelId">passing parameter as LabelId</param>
        /// <param name="label">passing parameter as Label</param>
        /// <returns>returns the label name that renamed</returns>
        public async Task<LabelModel> RenameLabel(int labelId, string label)
        {
            try
            {
                var validLabel = await this.context.Labels.Where(x => x.LabelId == labelId).SingleOrDefaultAsync();
                if (validLabel != null)
                {
                    validLabel.Label = label;
                    this.context.Labels.Update(validLabel);
                    await this.context.SaveChangesAsync();
                    return validLabel;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}