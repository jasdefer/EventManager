using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

namespace BusinessLayer
{
    public class TestSeed : IDataSeeder
    {
        private readonly RegionDto[] Regions = new RegionDto[]
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

        private readonly VisitorDto[] Visitors = new VisitorDto[]
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
        private readonly IPasswordHasher<VisitorDto> Hasher;

        public TestSeed(RegionManager regionManager, VisitorManager visitorManager, IPasswordHasher<VisitorDto> hasher)
        {
            RegionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            VisitorManager = visitorManager ?? throw new ArgumentNullException(nameof(visitorManager));
            Hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }
        public RegionManager RegionManager { get; }

        public VisitorManager VisitorManager { get; }

        public void SeedData()
        {
            foreach (var region in Regions)
            {
                region.Id = RegionManager.Add(region);
            }

            foreach (var visitor in Visitors)
            {
                visitor.PasswordHash = Hasher.HashPassword(visitor, "password123");
                visitor.Id = VisitorManager.Add(visitor);
            }


            Regions[0].VisitorIds = Visitors.Select(x => x.Id);
            Regions[1].VisitorIds = new List<int>() { Visitors.First().Id, Visitors.Last().Id };

            foreach (var region in Regions)
            {
                RegionManager.Update(region);
            }
        }
    }
}
