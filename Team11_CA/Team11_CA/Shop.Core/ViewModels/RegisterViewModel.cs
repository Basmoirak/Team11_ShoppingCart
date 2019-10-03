using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[_A-z0-9]*((-|\s)*[_A-z0-9])*$", ErrorMessage = "Invalid Username")]
        [StringLength(40, ErrorMessage = "{0} must be at least {2}", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "{0} must be at least {2}", MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z]{3,30}\s*)+", ErrorMessage = "Invalid Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z]{3,30}", ErrorMessage = "Invalid Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Address should be more than 10 characters.", MinimumLength = 10)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]+", ErrorMessage = "Invalid Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [RegularExpression(@"\d+ ?\w{0,9} ?\d+", ErrorMessage = "Invalid Phone Number")]
        public int PhoneNumber { get; set; }
    }
}