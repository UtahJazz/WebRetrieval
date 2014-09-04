using System;
using System.Collections.Generic;

namespace SearchCore.Ranger
{
    public class RangerStub : IRanger
    {
        public IEnumerable<int> Rank(Guid userId, string userQuery, IEnumerable<int> files)
        {
            return files;
        }
    }
}
