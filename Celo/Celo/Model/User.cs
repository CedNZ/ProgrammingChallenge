using System;

namespace Celo.Model
{
    public class User
    {
        public int Id { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Name => $"{Title}. {FirstName} {LastName}";

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicturePath { get; set; }

        public string ThumbnailPath { get; set; }
    }
}
