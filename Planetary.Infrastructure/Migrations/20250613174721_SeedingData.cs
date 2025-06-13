using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planetary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Solar System Planets
                INSERT INTO Planets (Id, Name, StellarSystem, DistanceFromEarth, Radius, Mass, SurfaceTemperature, 
                                    SurfaceGravity, HasAtmosphere, AtmosphericComposition, AtmosphericPressure,
                                    HasWater, WaterCoverage, PlanetType, DiscoveryDate)
                VALUES 
                -- Mercury
                (NEWID(), 'Mercury', 'Solar System', 0.39, 2439.7, 0.055, 440, 0.38, 0, 'Minimal - traces of oxygen, sodium, hydrogen', 0,
                 0, 0, 'Terrestrial', '1631-11-07'),
                
                -- Venus
                (NEWID(), 'Venus', 'Solar System', 0.72, 6051.8, 0.815, 737, 0.91, 1, 'CO2 (96.5%), Nitrogen (3.5%)', 92,
                 0, 0, 'Terrestrial', '1610-09-01'),
                
                -- Earth
                (NEWID(), 'Earth', 'Solar System', 0, 6371, 1, 288, 1, 1, 'Nitrogen (78%), Oxygen (21%), Argon (0.9%)', 1,
                 1, 71, 'Terrestrial', '0001-01-01'),
                
                -- Mars
                (NEWID(), 'Mars', 'Solar System', 0.52, 3389.5, 0.107, 210, 0.38, 1, 'CO2 (95.3%), Nitrogen (2.7%), Argon (1.6%)', 0.006,
                 1, 0.001, 'Terrestrial', '1610-09-01'),
                
                -- Jupiter
                (NEWID(), 'Jupiter', 'Solar System', 5.2, 69911, 317.8, 165, 2.53, 1, 'Hydrogen (89%), Helium (10%), Methane (0.3%)', 0,
                 0, 0, 'Gas Giant', '1610-01-07'),
                
                -- Saturn
                (NEWID(), 'Saturn', 'Solar System', 9.54, 58232, 95.2, 134, 1.07, 1, 'Hydrogen (96.3%), Helium (3.25%)', 0,
                 0, 0, 'Gas Giant', '1610-07-25'),
                
                -- Uranus
                (NEWID(), 'Uranus', 'Solar System', 19.2, 25362, 14.5, 76, 0.89, 1, 'Hydrogen (83%), Helium (15%), Methane (2.3%)', 0,
                 1, 0, 'Ice Giant', '1781-03-13'),
                
                -- Neptune
                (NEWID(), 'Neptune', 'Solar System', 30.06, 24622, 17.1, 72, 1.14, 1, 'Hydrogen (80%), Helium (19%), Methane (1.5%)', 0,
                 1, 0, 'Ice Giant', '1846-09-23'),
                
                -- Exoplanets
                -- Proxima Centauri b
                (NEWID(), 'Proxima Centauri b', 'Alpha Centauri', 4.2, 7254.5, 1.27, 234, 1.1, 1, 'Unknown', 0.5,
                 1, 30, 'Super Earth', '2016-08-24'),
                
                -- TRAPPIST-1e
                (NEWID(), 'TRAPPIST-1e', 'TRAPPIST-1', 39, 5826, 0.77, 251, 0.85, 1, 'Unknown', 0.7,
                 1, 45, 'Terrestrial', '2017-02-22'),
                
                -- Kepler-22b
                (NEWID(), 'Kepler-22b', 'Kepler-22', 620, 15369, 2.4, 295, 1.6, 1, 'Unknown', 1.2,
                 1, 80, 'Super Earth', '2011-12-05'),
                
                -- HD 209458 b (Osiris)
                (NEWID(), 'HD 209458 b', 'HD 209458', 150, 94380, 220, 1100, 9.6, 1, 'Hydrogen, Helium, Water vapor, Sodium', 0,
                 1, 0, 'Hot Jupiter', '1999-11-05'),
                
                -- K2-18b
                (NEWID(), 'K2-18b', 'K2-18', 124, 17173, 8.6, 265, 2.3, 1, 'Hydrogen, Methane, Water vapor', 1.5,
                 1, 60, 'Super Earth', '2015-05-30'),
                
                -- 55 Cancri e (Janssen)
                (NEWID(), 'Janssen', '55 Cancri', 41, 12990, 8.08, 2573, 1.9, 1, 'Carbon dioxide, Oxygen', 90,
                 0, 0, 'Super Earth', '2004-08-30'),
                
                -- Gliese 581g
                (NEWID(), 'Gliese 581g', 'Gliese 581', 20, 8436, 3.1, 278, 1.4, 1, 'Unknown', 1,
                 1, 40, 'Super Earth', '2010-09-29'),
                
                -- Kepler-442b
                (NEWID(), 'Kepler-442b', 'Kepler-442', 1206, 10194, 2.34, 273, 1.3, 1, 'Unknown', 0.9,
                 1, 65, 'Super Earth', '2015-01-06');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove all seeded planets
            migrationBuilder.Sql("DELETE FROM Planets");
        }
    }
}
