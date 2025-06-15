using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planetary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Planets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@"
                -- Clear any existing users first to avoid duplicates
                DELETE FROM Users;

                -- SuperAdmin (username: admin, password: admin)
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'admin', 'admin@planetary.com', 
                 -- BCrypt hash of 'admin'
                 '$2a$11$yw9PHmZh9FZ69qZ86NPZJ.Z164Z9q4jvhVcaytAYlwYt.E9rDz7HC', 
                 'Admin', 'User', 
                 0, -- SuperAdmin UserType = 0
                 1, -- IsActive = true
                 'Planetary Administration', 'System Administrator', 'All Planetary Systems',
                 GETDATE(), 'All Systems', '', '');

                -- PlanetAdmin users (username: planet1-3, password: planet1-3)
                -- PlanetAdmin 1
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'planet1', 'planet1@planetary.com', 
                 -- BCrypt hash of 'planet1'
                 '$2a$11$J9Wsu9wkv/ZsCPcgYxRWwec4vUUIk2BYzWwREuathIFPMTMPd5YEK', 
                 'Planet', 'Admin1', 
                 1, -- PlanetAdmin UserType = 1
                 1, -- IsActive = true
                 'Planet Research Org', 'Planet Researcher', 'Terrestrial Planets',
                 GETDATE(), 'Solar System', '', 'Earth,Mars');

                -- PlanetAdmin 2
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'planet2', 'planet2@planetary.com', 
                 -- BCrypt hash of 'planet2'
                 '$2a$11$ylkfU1MbYU6iUXiYsg4hC.wKepDmmKQBsx9K9/TCbW0nFq0gPE1Lu', 
                 'Planet', 'Admin2', 
                 1, -- PlanetAdmin UserType = 1
                 1, -- IsActive = true
                 'Gas Giants Research', 'Senior Researcher', 'Gas Giants',
                 GETDATE(), 'Outer Solar System', '', 'Jupiter,Saturn');

                -- PlanetAdmin 3
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'planet3', 'planet3@planetary.com', 
                 -- BCrypt hash of 'planet3'
                 '$2a$11$eMuRZmFN0Rjf8deXlodWuuL1ZEfYQlo0PYeQQEY9yHgxbfWyG8y6S', 
                 'Planet', 'Admin3', 
                 1, -- PlanetAdmin UserType = 1
                 1, -- IsActive = true
                 'Exoplanet Detection Team', 'Exoplanet Analyst', 'Exoplanets',
                 GETDATE(), 'Proxima Centauri', '', '');

                -- 10 Viewer users (username: viewer1-10, password: viewer1-10)
                -- Viewer 1
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer1', 'viewer1@planetary.com', 
                 -- BCrypt hash of 'viewer1'
                 '$2a$11$YKI1c2dUoyWrflKgFO9TkO.l5egdHMTCt1F0LOEyJJ38GMlSxLUgO', 
                 'Viewer', 'One', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Public Science', 'Astronomy Enthusiast', 'Solar System',
                 GETDATE(), 'Solar System', '', 'Jupiter');

                -- Viewer 2
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer2', 'viewer2@planetary.com', 
                 -- BCrypt hash of 'viewer2'
                 '$2a$11$sGzNZul/Idrp9XePeJ9GDuWM.QXJKFJf4tp2G3Q6THlWMtF9aR5DS', 
                 'Viewer', 'Two', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'University Research', 'Student', 'Mars Exploration',
                 GETDATE(), 'Solar System', '', 'Mars');

                -- Viewer 3
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer3', 'viewer3@planetary.com', 
                 -- BCrypt hash of 'viewer3'
                 '$2a$11$kXeql5bgSA0r0Q0MXAWWj.OIJGlpx.J8NjbhFtWYzCdZkkQ.NJvIC', 
                 'Viewer', 'Three', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Astronomy Club', 'Member', 'Moon Phases',
                 GETDATE(), 'Earth-Moon System', '', 'Earth');

                -- Viewer 4
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer4', 'viewer4@planetary.com', 
                 -- BCrypt hash of 'viewer4'
                 '$2a$11$q4DQiRUribPNQ1ixsnXJv.kXyQREdUNJhvwRLk4WCqABNzTqkOiI2', 
                 'Viewer', 'Four', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Public Observatory', 'Guide', 'Saturn Rings',
                 GETDATE(), 'Saturn System', '', 'Saturn');

                -- Viewer 5
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer5', 'viewer5@planetary.com', 
                 -- BCrypt hash of 'viewer5'
                 '$2a$11$JSEd0pd37/9E9RdaPxUHFusiwDlswJxw7Mt1UIU3Dfx9csi3JPnRi', 
                 'Viewer', 'Five', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Space Foundation', 'Educator', 'All Planets',
                 GETDATE(), 'Solar System', '', 'Earth,Mars,Jupiter');

                -- Viewer 6
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer6', 'viewer6@planetary.com', 
                 -- BCrypt hash of 'viewer6'
                 '$2a$11$6sfgTNyghI91Z8G9iAGOWup/aZJflGPGZC5dtyNFEZrbZYwsj8nZ2', 
                 'Viewer', 'Six', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Astronomical Society', 'Member', 'Outer Planets',
                 GETDATE(), 'Outer Solar System', '', 'Uranus,Neptune');

                -- Viewer 7
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer7', 'viewer7@planetary.com', 
                 -- BCrypt hash of 'viewer7'
                 '$2a$11$WiB9my8Ic.RKIfXr3NhvROnfObSDCp1ma1QiIEfIhCAe98hR2RBOm', 
                 'Viewer', 'Seven', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Planetary Research', 'Assistant', 'Inner Planets',
                 GETDATE(), 'Inner Solar System', '', 'Mercury,Venus');

                -- Viewer 8
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer8', 'viewer8@planetary.com', 
                 -- BCrypt hash of 'viewer8'
                 '$2a$11$6YXt5E0jJEudADpcLnmaOeXoEKV6H.VfBHJaKQl1DXVUr3QGkjR2S', 
                 'Viewer', 'Eight', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Science Museum', 'Docent', 'Solar System',
                 GETDATE(), 'Solar System', '', 'Earth,Mars');

                -- Viewer 9
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer9', 'viewer9@planetary.com', 
                 -- BCrypt hash of 'viewer9'
                 '$2a$11$X5eK84RDvznInlIpkwJc8OYrKPmVK6adGtj89JcwNJCpfoaGJjruG', 
                 'Viewer', 'Nine', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Space Agency', 'Analyst', 'Dwarf Planets',
                 GETDATE(), 'Kuiper Belt', '', 'Pluto');

                -- Viewer 10
                INSERT INTO Users (Id, Username, Email, PasswordHash, FirstName, LastName, 
                                  UserType, IsActive, Organization, JobTitle, ResearchInterests, 
                                  CreatedDate, PreferredPlanetarySystem, OwnedPlanetIds, FavoritePlanets)
                VALUES 
                (NEWID(), 'viewer10', 'viewer10@planetary.com', 
                 -- BCrypt hash of 'viewer10'
                 '$2a$11$ibbE0zBPR0cvCF8fnu5n6e3tVhR7gQAiUvynL/lxj56mz7FGQ9H/a', 
                 'Viewer', 'Ten', 
                 2, -- Viewer UserType = 2
                 1, -- IsActive = true
                 'Amateur Astronomy', 'Hobbyist', 'All Planets',
                 GETDATE(), 'Solar System', '', 'Jupiter,Saturn,Earth,Mars');
            ");

            migrationBuilder.Sql(@"
                -- Find the first PlanetAdmin user (UserType = 1)
                DECLARE @FirstPlanetAdminId UNIQUEIDENTIFIER;
                SELECT TOP 1 @FirstPlanetAdminId = Id 
                FROM Users 
                WHERE UserType = 1 -- PlanetAdmin
                ORDER BY CreatedDate;

                -- If we found a PlanetAdmin user, assign all planets to them
                IF @FirstPlanetAdminId IS NOT NULL
                BEGIN
                    -- Update all planets to be owned by this PlanetAdmin
                    UPDATE Planets SET UserId = @FirstPlanetAdminId;
                    
                    -- Also update the OwnedPlanetIds for this PlanetAdmin
                    -- First, gather all planet IDs
                    DECLARE @AllPlanetIds NVARCHAR(MAX) = '';
                    SELECT @AllPlanetIds = @AllPlanetIds + CONVERT(NVARCHAR(36), Id) + ',' 
                    FROM Planets;

                    -- Remove trailing comma if any
                    IF LEN(@AllPlanetIds) > 0
                        SET @AllPlanetIds = LEFT(@AllPlanetIds, LEN(@AllPlanetIds) - 1);

                    -- Update the PlanetAdmin's OwnedPlanetIds
                    UPDATE Users 
                    SET OwnedPlanetIds = @AllPlanetIds
                    WHERE Id = @FirstPlanetAdminId;
                END
                ELSE
                BEGIN
                    -- Fallback to first SuperAdmin if no PlanetAdmin is found
                    DECLARE @SuperAdminId UNIQUEIDENTIFIER;
                    SELECT TOP 1 @SuperAdminId = Id 
                    FROM Users 
                    WHERE UserType = 0 -- SuperAdmin
                    ORDER BY CreatedDate;
                    
                    -- If SuperAdmin exists, assign planets to them
                    IF @SuperAdminId IS NOT NULL
                    BEGIN
                        UPDATE Planets SET UserId = @SuperAdminId;
                    END
                    ELSE
                    BEGIN
                        -- If neither PlanetAdmin nor SuperAdmin exists, assign to first user
                        DECLARE @FirstUserId UNIQUEIDENTIFIER;
                        SELECT TOP 1 @FirstUserId = Id 
                        FROM Users 
                        ORDER BY CreatedDate;
                        
                        IF @FirstUserId IS NOT NULL
                        BEGIN
                            UPDATE Planets SET UserId = @FirstUserId;
                        END
                    END
                END
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_UserId",
                table: "Planets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Users_UserId",
                table: "Planets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Users_UserId",
                table: "Planets");

            migrationBuilder.DropIndex(
                name: "IX_Planets_UserId",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Planets");
        }
    }
}
