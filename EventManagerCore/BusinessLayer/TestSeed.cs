using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using DataTransfer;

namespace BusinessLayer
{
    public class TestSeed : IDataSeeder
    {
        private readonly RegionDto[] _regions = new RegionDto[]
            {
                new RegionDto()
                {
                    Name = "Reg One",
                    Description ="A beautiful first region.",
                    Polygon = "Test polygon string",
                    TimeStamp = new DateTime(2010,01,01),
                    Value = 5,
                },
                new RegionDto()
                {
                    Name = "Reg Two",
                    Description ="Another beautiful region.",
                    Polygon = "Test polygon string",
                    TimeStamp = new DateTime(2010,02,01),
                    Value = 1,
                },
                new RegionDto()
                {
                    Name = "Reg Three",
                    Description ="A not so beautiful region.",
                    Polygon = "Test polygon string",
                    TimeStamp = new DateTime(2010,03,01),
                    Value = 7,
                },
            };

        private readonly VisitorDto[] _visitors = new VisitorDto[]
        {
            new VisitorDto()
            {
                Username = "Harald",
                Email= "Harald@email.org",
                Bio = "I am an awesome person."
            },
            new VisitorDto()
            {
                Username = "Peter",
                Email= "Peter@email.org",
                Bio = "I am another awesome person."
            },
            new VisitorDto()
            {
                Username = "Jochen",
                Email= "Jochen@email.org",
                Bio = "I am a person."
            }
        };
        private readonly IPasswordHasher<VisitorDto> _hasher;

        public TestSeed(RegionManager regionManager, VisitorManager visitorManager, IPasswordHasher<VisitorDto> hasher)
        {
            RegionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            VisitorManager = visitorManager ?? throw new ArgumentNullException(nameof(visitorManager));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }
        public RegionManager RegionManager { get; }

        public VisitorManager VisitorManager { get; }

        public void SeedData()
        {
            foreach (var region in _regions)
            {
                region.Id = RegionManager.Add(region);
            }

            foreach (var visitor in _visitors)
            {
                visitor.PasswordHash = _hasher.HashPassword(visitor, "password123");
                visitor.Id = VisitorManager.Add(visitor);
            }


            _regions[0].VisitorIds = _visitors.Select(x => x.Id);
            _regions[1].VisitorIds = new List<int>() { _visitors.First().Id, _visitors.Last().Id };

            foreach (var region in _regions)
            {
                RegionManager.Update(region);
            }
        }
    }
}
