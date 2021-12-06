using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelRepository:ILabelRepository
    {
        private readonly UserContext context;

        public LabelRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<string> AddLabel(LabelModel labelModel)
        {
            try
            {
                this.context.Labels.Add(labelModel);
                await this.context.SaveChangesAsync();
                return "Label Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public async Task<string> DeleteLabel(int LabelId)
        {
            try
            {
                var labelExist = await this.context.Labels.Where(x => x.LabelId == LabelId).SingleOrDefaultAsync();
                if (labelExist != null)
                {
                    this.context.Labels.Remove(labelExist);
                    await this.context.SaveChangesAsync();
                    return "Label Deleted Successfully";
                }
                return "Label Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveLabelFromNote(int LabelId)
        {
            try
            {
                var labelExist = await this.context.Labels.Where(x => x.LabelId == LabelId).SingleOrDefaultAsync();
                if (labelExist != null)
                {
                    labelExist.NoteId = null;
                    await this.context.SaveChangesAsync();
                    return "Label Removed From Note Successfully";
                }
                return "Label Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetNotesByLabelName(string Label)
        {
            try
            {
                IEnumerable<LabelModel> LabelExist = this.context.Labels.Where(x => x.Label == Label).ToList();
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

        public async Task<string> RenameLabel(LabelModel labelModel)
        {
            try
            {
                var labelExist = await this.context.Labels.Where(x => x.LabelId == labelModel.LabelId).SingleOrDefaultAsync();
                if (labelExist != null)
                {
                    this.context.Labels.Update(labelExist);
                    await this.context.SaveChangesAsync();
                    return "Label name Successfully Renamed";
                }
                return "Label not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
