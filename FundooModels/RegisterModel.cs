// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// RegisterModel class for user registration
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the value as integer
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the value for user's firstname as string type 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the value for user's lastname as string type 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the value for user's email as string type 
        /// </summary>
        [Required]
        //[RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the value for user's password as string type 
        /// </summary>
        [Required]
        //[RegularExpression("^(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password is not valid. Password Should be 8 Character contain 1 Uppercase, 1 Special character, 1 Number")]
        public string Password { get; set; }
    }
}
