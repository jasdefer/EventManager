using DataTransfer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BusinessLayer;
using Microsoft.Extensions.Logging;

namespace EventApi.Services.Identity
{
    public class UserStore : IUserStore<VisitorDto>, IUserEmailStore<VisitorDto>
    {
        private readonly VisitorManager VisitorManager;
        private readonly IPasswordHasher<VisitorDto> Hasher;
        private readonly ILogger<UserStore> Logger;

        public UserStore(VisitorManager visitorManager, IPasswordHasher<VisitorDto> hasher, ILogger<UserStore> logger)
        {
            VisitorManager = visitorManager ?? throw new ArgumentNullException(nameof(visitorManager));
            Hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            Logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() => VisitorManager.Add(user));
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                Logger.LogWarning(4, e, "Cannot create visitor");
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> DeleteAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() => VisitorManager.Delete(user.Id));
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                Logger.LogWarning(4, e, "Cannot delete visitor");
                return IdentityResult.Failed();
            }
        }

        public void Dispose()
        {
            
        }

        public async Task<VisitorDto> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            VisitorDto visitor = null;
            try
            {
                visitor = await Task.Run(() => VisitorManager.Get().SingleOrDefault(v => v.Email.ToUpperInvariant()==normalizedEmail));
            }
            catch (Exception e)
            {
                Logger.LogWarning(4, e, "Cannot search for email.");
            }

            return visitor;
        }

        public async Task<VisitorDto> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            VisitorDto visitor = null;
            try
            {
                visitor = await Task.Run(() => VisitorManager.Get(int.Parse(userId)));
            }
            catch (Exception e)
            {
                Logger.LogWarning(4, e, "Cannot search for email.");
            }

            return visitor;
        }

        public async Task<VisitorDto> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            VisitorDto visitor = null;
            try
            {
                visitor = await Task.Run(() => VisitorManager.Get().SingleOrDefault(v => v.Username.ToUpperInvariant() == normalizedUserName));
            }
            catch (Exception e)
            {
                Logger.LogWarning(4, e, "Cannot search for email.");
            }

            return visitor;
        }

        public async Task<string> GetEmailAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNormalizedEmailAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Email.ToUpperInvariant());
        }

        public async Task<string> GetNormalizedUserNameAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Username.ToUpperInvariant());
        }

        public async Task<string> GetUserIdAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Username);
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
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(VisitorDto user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(VisitorDto user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(VisitorDto user, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() => VisitorManager.Update(user));
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                Logger.LogWarning(4, e, "Cannot update user");
            }

            return IdentityResult.Failed();
        }
    }
}
