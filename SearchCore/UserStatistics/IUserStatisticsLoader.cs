using System;

namespace SearchCore.UserStatistics
{
    public interface IUserStatisticsLoader
    {
        void Save(Guid id, IUserStatistics indexDictionary);

        IUserStatistics Load(Guid id);
    }
}
