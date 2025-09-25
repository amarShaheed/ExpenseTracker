﻿namespace ExpenseTracker.Models
{
    // Models/RegisterViewModel.cs
    using System.ComponentModel.DataAnnotations;

   
        public class RegisterViewModel
        {
            [Required]
            public string FullName { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
