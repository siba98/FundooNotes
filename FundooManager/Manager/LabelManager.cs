using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager:ILabelManager
    {
        private readonly ILabelRepository labelRepository;
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public async Task<string> AddLabel(LabelModel labelModel)
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

        public IEnumerable<LabelModel> GetNotesByLabelName(string Label)
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

        public async Task<string> DeleteLabel(int LabelId)
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

        public async Task<string> RemoveLabelFromNote(int LabelId)
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

        public async Task<string> RenameLabel(LabelModel labelModel)
        {
            try
            {
                return await this.labelRepository.RenameLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
