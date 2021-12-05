using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
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

        public string AddLabel(LabelModel labelModel)
        {
            try
            {
                this.context.Labels.Add(labelModel);
                this.context.SaveChanges();
                return "Label Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
