using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    [DisplayName("First Name")]
    
    public string? FirstName { get; set; }
    [DisplayName("Last Name")]
    public string? LastName { get; set; }
    [DisplayName("Street Address")]
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    [DisplayName("Postal Code")]
    public string? PostalCode { get; set; }

    [NotMapped]
    public string FullName {  get { return FirstName + " " + LastName; } }
}

