// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ResetPasswordModel class for contain properties for user's reset password
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the email as string type
        /// </summary>
        [Required] //// this indicate property must have value
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the new password as string type
        /// </summary>
        [Required]
        ////[RegularExpression("^(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password is not valid. Password Should be 8 Character contain 1 Uppercase, 1 Special character, 1 Number")]
        public string NewPassword { get; set; }
    }
}
