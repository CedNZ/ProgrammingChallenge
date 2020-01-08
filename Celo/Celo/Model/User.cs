using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celo.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Name => $"{Title}. {FirstName} {LastName}";

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicturePath { get; set; }

        public string ThumbnailPath { get; set; }

        public string Gender { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
