using System;
using System.Collections.Generic;
using System.Linq;

namespace Planetary.Domain.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            UserType = UserType.Viewer; // Default role
        }

        public User(
            string username,
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            UserType userType)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public UserType UserType { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string Organization { get; private set; } = string.Empty;
        public string JobTitle { get; private set; } = string.Empty;
        public string ResearchInterests { get; private set; } = string.Empty;
        public DateTime CreatedDate { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public string PreferredPlanetarySystem { get; private set; } = string.Empty;
        // For backward compatibility
        public string Role => UserType.ToString();
        
        // Navigation property for planets owned by this user
        public ICollection<Planet> Planets { get; private set; } = new List<Planet>();

        public void SetLastLogin()
        {
            LastLoginDate = DateTime.UtcNow;
        }

        public void UpdateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            Username = username;
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            Email = email;
        }

        public void UpdatePasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty", nameof(passwordHash));

            PasswordHash = passwordHash;
        }

        public void UpdateUserType(UserType userType)
        {
            UserType = userType;
        }

        public void SetActiveStatus(bool isActive)
        {
            IsActive = isActive;
        }

        public void UpdateProfile(
            string firstName,
            string lastName,
            string organization,
            string jobTitle,
            string researchInterests,
            string preferredPlanetarySystem)
        {
            FirstName = firstName;
            LastName = lastName;
            Organization = organization;
            JobTitle = jobTitle;
            ResearchInterests = researchInterests;
            PreferredPlanetarySystem = preferredPlanetarySystem;
        }

        public bool IsOwnerOfPlanet(Guid planetId)
        {
            return UserType == UserType.SuperAdmin || 
                   Planets.Any(p => p.Id == planetId);
        }
    }
}