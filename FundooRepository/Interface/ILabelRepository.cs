using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        string AddLabel(LabelModel labelModel);
    }
}
