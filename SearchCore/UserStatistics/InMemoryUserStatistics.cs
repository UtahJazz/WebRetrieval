using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SearchCore.UserStatistics
{
    [Serializable]
    [ProtoContract]
    public sealed class InMemoryUserStatistics : IUserStatistics
    {
        public int GetTagPreference(string tag)
        {
            return _preference.ContainsKey(tag) ? _preference[tag] : 0;
        }

        public void AppendTag(string tag)
        {
            if (_preference.ContainsKey(tag))
            {
                _preference[tag]++;
            }
            else
            {
                _preference.Add(tag, 1);
            }
        }

        [ProtoMember(1)]
        private readonly IDictionary<string, int> _preference = new Dictionary<string, int>();
    }
}
