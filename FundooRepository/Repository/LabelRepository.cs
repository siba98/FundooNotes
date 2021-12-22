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
    /// LabelRepository class for Label Api's
    /// </summary>
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// object created for UserContext
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the LabelRepository class
        /// </summary>
        /// <param name="context">taking context as parameter</param>
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
                this.context.Labels.Add(labelModel);
                await this.context.SaveChangesAsync();
                return labelModel;
            }
            catch (ArgumentNullException ex)
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
                IEnumerable<LabelModel> LabelList = this.context.Labels.Where(x => x.NoteId == NoteId).ToList();
                if (LabelList.Count() != 0)
                {
                    return LabelList;
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
        /// <param name="UserId">passing parameter as UserId</param>
        /// <returns>returns all the labels</returns>
        public IEnumerable<LabelModel> GetLabelByUserId(int UserId)
        {
            try
            {
                IEnumerable<LabelModel> LabelList = this.context.Labels.Where(x => x.UserId == UserId).ToList();
                if (LabelList.Count() != 0)
                {
                    return LabelList;
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
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>returns boolean type</returns>
        public async Task<bool> DeleteLabel(int LabelId)
        {
            try
            {
                var validLabel = await this.context.Labels.Where(x => x.LabelId == LabelId).SingleOrDefaultAsync();
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
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <returns>returns object from where the label removed </returns>
        public async Task<LabelModel> RemoveLabelFromNote(int LabelId)
        {
            try
            {
                var validLabel = await this.context.Labels.Where(x => x.LabelId == LabelId).SingleOrDefaultAsync();
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
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>returns all the notes</returns>
        public IEnumerable<LabelModel> GetNotesByLabelName(string Label)
        {
            try
            {
                IEnumerable<LabelModel> LabelExist = this.context.Labels.Where(x => x.Label == Label).ToList();
                //var result = from d in Note
                //             join s in Lebel
                //             on d.NoteId equals s.NoteId into g
                //             select new
                //             {
                //                 DepartmentName = d.DepartmentName,
                //                 Students = g
                //             };
                if (LabelExist.Count() != 0)
                {
                    return LabelExist;
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
        /// <param name="LabelId">passing parameter as LabelId</param>
        /// <param name="Label">passing parameter as Label</param>
        /// <returns>returns the label name that renamed</returns>
        public async Task<LabelModel> RenameLabel(int LabelId, string Label)
        {
            try
            {
                var validLabel = await this.context.Labels.Where(x => x.LabelId == LabelId).SingleOrDefaultAsync();
                if (validLabel != null)
                {
                    validLabel.Label = Label;
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