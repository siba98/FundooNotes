using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class LabelRepository:ILabelRepository
    {
        private readonly UserContext context;

        public LabelRepository(UserContext context)
        {
            this.context = context;
        }

        public string AddLabelByUserId(LabelModel labelModel)
        {
            try
            {
                var validUser = this.context.Labels.Where(x => x.UserId == labelModel.UserId).FirstOrDefault();
                if (validUser != null)
                {
                    var validLabel = this.context.Labels.Where(x => x.Label != labelModel.Label && x.NoteId == null).SingleOrDefault();
                    if (validLabel != null)
                    {
                        this.context.Labels.Add(labelModel);
                        this.context.SaveChanges();
                        return "Label Added Successfully";
                    }
                    return "Label With This Same Name Already Exists";
                }
                return "UserId Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string AddLabelByNoteId(LabelModel labelModel)
        {
            try
            {
                var validUser = this.context.Labels.Where(x => x.UserId == labelModel.UserId).FirstOrDefault();
                if (validUser != null)
                {
                    var validNote = this.context.Labels.Where(x => x.NoteId == labelModel.NoteId).SingleOrDefault();
                    if (validNote != null)
                    {
                        var validLabel = this.context.Labels.Where(x => x.Label == labelModel.Label).SingleOrDefault();
                        if (validLabel != null)
                        {
                            this.context.Labels.Add(labelModel);
                            this.context.SaveChanges();
                            return "Label Added Successfully To The Note";
                        }
                        else if(validLabel == null)
                        {
                            this.context.Labels.Add(labelModel);
                            this.context.SaveChanges();
                            return "Label Created and Added To The Note";
                        }
                        return "Label Already Exists in the Note";
                    }
                    return "Note Not Exist";
                }
                return "User Not Exist";
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

        public string DeleteLabel(int UserId, string Label)
        {
            try
            {
                var userExist = this.context.Labels.Where(x => x.UserId == UserId).SingleOrDefault();
                if (userExist != null)
                {
                    var labelExist = this.context.Labels.Where(x => x.Label == Label).SingleOrDefault();
                    if (labelExist != null)
                    {
                        labelExist.Label = null;
                        this.context.SaveChanges();
                        return "Label Deleted Successfully";
                    }
                    return "Label Not Exist";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RemoveLabelFromNote(int UserId, int NoteId, string Label)
        {
            try
            {
                var noteExist = this.context.Labels.Where(x => x.NoteId == NoteId).SingleOrDefault();
                if (noteExist != null)
                {
                    var labelExist = this.context.Labels.Where(x => x.Label == Label).SingleOrDefault();
                    if (labelExist != null)
                    {
                        labelExist.Label = null;
                        this.context.SaveChanges();
                        return "Label Removed From Note Successfully";
                    }
                    return "Label Not Exist in The Note";
                }
                return "Note Not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
