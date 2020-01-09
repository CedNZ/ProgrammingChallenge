using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celo.Model
{
    public class User
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        public string Name => $"{Title}. {FirstName} {LastName}";

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("ProfilePicturePath")]
        public string ProfilePicturePath { get; set; }

        [Required]
        [DisplayName("ThumbnailPath")]
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
