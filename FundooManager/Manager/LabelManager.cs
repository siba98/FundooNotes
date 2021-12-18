

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
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

        public async Task<string> RenameLabel(int LabelId, string Label)
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
