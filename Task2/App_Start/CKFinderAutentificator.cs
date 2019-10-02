using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CKSource.CKFinder.Connector.Core;
using CKSource.CKFinder.Connector.Core.Authentication;

namespace Task2.App_Start
{
    public class CKFinderAuthenticator : IAuthenticator
    {
        public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
        {
            var user = new User(true, new List<string>());
            return Task.FromResult((IUser)user);
        }
    }
}