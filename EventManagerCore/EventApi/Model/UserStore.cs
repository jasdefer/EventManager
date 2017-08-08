using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataLayer.DataModel;
using BusinessLayer;
using ValidationRules.Dto;

namespace EventApi.Model
{
    public class UserStore : IUserStore<VisitorDto>, IUserEmailStore<VisitorDto>
    {
        private readonly VisitorManager VisitorManager;
        private readonly IPasswordHasher<VisitorDto> Hasher;

        public UserStore(VisitorManager visitorManager, IPasswordHasher<VisitorDto> hasher)
        {
            VisitorManager = visitorManager ?? throw new ArgumentNullException(nameof(visitorManager));
            Hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public async Task<IdentityResult> CreateAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() => {
                    user.PasswordHash = Hasher.HashPassword(user, user.PasswordHash);
                    });
                VisitorManager.Add(user);
                return IdentityResult.Success;
            }
            catch (Exception)
            {
            }

            return IdentityResult.Failed();
        }

        public Task<IdentityResult> DeleteAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<VisitorDto> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await Task<VisitorDto>.Run(() => VisitorManager.Get().SingleOrDefault(v => v.Email.Normalize().ToUpperInvariant() == normalizedEmail));
        }

        public Task<VisitorDto> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<VisitorDto> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await Task<VisitorDto>.Run(() => VisitorManager.Get().SingleOrDefault(v => v.Username.Normalize().ToUpperInvariant() == normalizedUserName));
        }

        public async Task<string> GetEmailAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            string username = await Task.Run(() => user.Username);

            return username;
        }

        public Task SetEmailAsync(VisitorDto user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(VisitorDto user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(VisitorDto user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(VisitorDto user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(VisitorDto user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
