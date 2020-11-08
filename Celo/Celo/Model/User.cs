using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Celo.Model
{
    public class User
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        public string Name => $"{Title}. {FirstName} {LastName}";

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("DoB")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Date of Birth")]
        public string Birthday => DateOfBirth.Date.ToShortDateString();

        [Required]
        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Profile Picture")]
        public string ProfilePicturePath { get; set; }

        [Required]
        [DisplayName("Thumbnail")]
        public string ThumbnailPath { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
