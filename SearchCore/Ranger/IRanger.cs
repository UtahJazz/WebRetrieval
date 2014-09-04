using System;
using System.Collections.Generic;

namespace SearchCore.Ranger
{
    public interface IRanger
    {
        IEnumerable<int> Rank(
            Guid userId, 
            string userQuery, 
            IEnumerable<int> files);
    }
}
