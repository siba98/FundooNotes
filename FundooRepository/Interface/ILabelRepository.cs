using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        string AddLabelByUserId(LabelModel labelModel);
        //string EditLabel(int userId, string label);
    }
}
