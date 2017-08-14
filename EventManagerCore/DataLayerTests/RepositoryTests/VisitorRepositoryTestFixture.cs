using DataLayer.DataModel;
using DataLayer.Repository;

namespace DataLayerTests.RepositoryTests
{
    public abstract class VisitorRepositoryTestFixture<TV> : RepositoryTestFixture<Visitor,int,TV> where TV:IVistorRepository
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
