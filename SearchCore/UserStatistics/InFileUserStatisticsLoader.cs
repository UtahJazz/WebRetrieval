using System;
using System.IO;
using ProtoBuf;

namespace SearchCore.UserStatistics
{
    public sealed class InFileUserStatisticsLoader : IUserStatisticsLoader
    {
        public InFileUserStatisticsLoader(string statisticsPath)
        {
            _statisticsPath = statisticsPath;
        }

        public void Save(Guid id, IUserStatistics indexDictionary)
        {
            var userStatisticsPath = GetPathToStatistics(id);
            if (File.Exists(userStatisticsPath))
            {
                File.Delete(userStatisticsPath);
            }

            using (var file = File.Create(userStatisticsPath))
            {
                Serializer.Serialize(file, indexDictionary);
            }
        }

        public IUserStatistics Load(Guid id)
        {
            var fileMetadataPath = GetPathToStatistics(id);
            if (!File.Exists(fileMetadataPath))
            {
                return new InMemoryUserStatistics();
            }

            using (var file = File.Open(fileMetadataPath, FileMode.Open))
            {
                return Serializer.Deserialize<InMemoryUserStatistics>(file);
            }
        }

        private string GetPathToStatistics(Guid id)
        {
            return Path.Combine(_statisticsPath, id.ToString());
        }

        private readonly string _statisticsPath;
    }
}
