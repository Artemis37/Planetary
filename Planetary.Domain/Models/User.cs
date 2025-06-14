using System;
using System.Collections.Generic;

namespace Planetary.Domain.Models
{
    public class User
    {
        private readonly List<string> _favoritePlanets = new();

        public User()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }

        public User(
            string username,
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            string role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty; 
        public bool IsActive { get; private set; } = true;
        public string Organization { get; private set; } = string.Empty;
        public string JobTitle { get; private set; } = string.Empty;
        public string ResearchInterests { get; private set; } = string.Empty;
        public DateTime CreatedDate { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public string PreferredPlanetarySystem { get; private set; } = string.Empty; 

        public IReadOnlyCollection<string> FavoritePlanets => _favoritePlanets.AsReadOnly();

        public void AddFavoritePlanet(string planetName)
        {
            if (string.IsNullOrWhiteSpace(planetName))
                throw new ArgumentException("Planet name cannot be empty", nameof(planetName));

            if (!_favoritePlanets.Contains(planetName))
                _favoritePlanets.Add(planetName);
        }

        public void RemoveFavoritePlanet(string planetName)
        {
            _favoritePlanets.Remove(planetName);
        }

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

        public void UpdateRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            Role = role;
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
    }
}