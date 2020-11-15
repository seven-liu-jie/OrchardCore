using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Moq;
using OrchardCore.Email;
using OrchardCore.Users;
using OrchardCore.Users.Services;

namespace OrchardCore.Tests.OrchardCore.Users
{
    public static class UsersMockHelper
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class, IUser
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserAccountValidator<TUser>(new IdentityErrorDescriber(), new EmailAddressValidator()));
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            return mgr;
        }

        public static Mock<RoleManager<TRole>> MockRoleManager<TRole>() where TRole : class
        {
            var store = new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());

            return new Mock<RoleManager<TRole>>(store, roles, new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), null);
        }
    }
}
