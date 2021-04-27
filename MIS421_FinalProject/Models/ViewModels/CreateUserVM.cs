﻿using MIS421_FinalProject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MIS421_FinalProject
{
    public class CreateUserVM
    {
        public ApplicationUser AppUser { get; set; }

        public List<string> AllRoles { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
