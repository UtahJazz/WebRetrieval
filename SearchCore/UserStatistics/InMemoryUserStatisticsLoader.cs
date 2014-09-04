using System;
using System.Collections.Generic;

namespace SearchCore.UserStatistics
{
    public sealed class InMemoryUserStatisticsLoader : IUserStatisticsLoader
    {
        public void Save(Guid id, IUserStatistics indexDictionary)
        {
            _savedUserStatisticses.Add(id, indexDictionary);
        }

        public IUserStatistics Load(Guid id)
        {
            return _savedUserStatisticses.ContainsKey(id) ? _savedUserStatisticses[id] : new InMemoryUserStatistics();
        }

        private readonly IDictionary<Guid, IUserStatistics> _savedUserStatisticses =
            new Dictionary<Guid, IUserStatistics>();
    }
}
