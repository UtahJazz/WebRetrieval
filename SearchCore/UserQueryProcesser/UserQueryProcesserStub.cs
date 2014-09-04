using System.Collections.Generic;

namespace SearchCore.UserQueryProcesser
{
    public sealed class UserQueryProcesserStub : IUserQueryProcesser
    {
        public IEnumerable<int> GetMatchedFiles(string userQuery)
        {
            return new[]
            {
                1, 2, 3, 4
            };
        }
    }
}
