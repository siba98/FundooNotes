using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class ResetPasswordModel
    {
        [Required]//this indicate property must have value
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
