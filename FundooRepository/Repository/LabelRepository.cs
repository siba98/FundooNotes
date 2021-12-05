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
                var validLabel = this.context.Labels.Where(x => x.UserId == labelModel.UserId && x.Label != labelModel.Label && x.NoteId == null).FirstOrDefault();
                if (validLabel != null)
                {
                    this.context.Labels.Add(labelModel);
                    this.context.SaveChanges();
                    return "Label Added Successfully";
                }
                return "Label With This Same Name Already Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public string EditLabel(int UserId, string Label)
        //{
        //    try
        //    {
        //        var userExist = this.context.Labels.Where(x => x.UserId == UserId).SingleOrDefault();
        //        if (userExist != null)
        //        {
        //            var labelExist = this.context.Labels.Where(x => x.Label == Label).FirstOrDefault();
        //            if (labelExist != null)
        //            {
        //                labelExist.Label = Label;
        //                this.context.Labels.Update(labelExist);
        //                this.context.SaveChanges();
        //                return "Label Edited Successfully";
        //            }
        //            return "Label Not Exist";
        //        }
        //        return "User Not Exist";
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
