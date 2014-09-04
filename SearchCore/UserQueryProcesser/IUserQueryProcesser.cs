using System.Collections.Generic;

namespace SearchCore.UserQueryProcesser
{
    public interface IUserQueryProcesser
    {
        IEnumerable<int> GetMatchedFiles(string userQuery);
    }
}
