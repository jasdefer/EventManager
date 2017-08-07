using DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.RepositoryTests
{
    public abstract class VisitorRepositoryTestFixture : RepositoryTestFixture<Visitor,int>
    {
        protected override Visitor CreateEntity()
        {
            return new Visitor()
            {
                Email = "RandomVisitor@email.org",
                Username = "RandomVisitor",
                PasswordHash = "SuperSecurePasswordHash123",
            };
        }

        protected override Visitor UpdateEntity(Visitor entity)
        {
            entity.Bio = entity.Bio ?? string.Empty + " more description ";
            return entity;
        }
    }
}
