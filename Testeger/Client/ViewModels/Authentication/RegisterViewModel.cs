﻿using System.ComponentModel.DataAnnotations;

namespace Testeger.Client.ViewModels.Authentication;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters.", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).+$",
        ErrorMessage = "Password must have at least one uppercase letter, one digit, and one special character.")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email must be valid.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    public string? PhoneNumber { get; set; }
}