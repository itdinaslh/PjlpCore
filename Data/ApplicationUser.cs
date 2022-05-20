using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PjlpCore.Data;

public class ApplicationUser : IdentityUser {
    public string? FullName { get; set; }
}