using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class LabelManager:ILabelManager
    {
        private readonly ILabelRepository labelRepository;
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public string AddLabelByUserId(LabelModel labelModel)
        {
            try
            {
                return this.labelRepository.AddLabelByUserId(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string AddLabelByNoteId(LabelModel labelModel)
        {
            try
            {
                return this.labelRepository.AddLabelByNoteId(labelModel);
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

        public string DeleteLabel(int UserId, string Label)
        {
            try
            {
                return this.labelRepository.DeleteLabel(UserId, Label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RemoveLabelFromNote(int UserId, int NoteId, string Label)
        {
            try
            {
                return this.labelRepository.RemoveLabelFromNote(UserId, NoteId, Label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
