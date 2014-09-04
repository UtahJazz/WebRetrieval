using System.Collections.Generic;

namespace SearchCore.Microtest
{
    public sealed class MicrotestData
    {
        public MicrotestData(IDictionary<string, string> microtestData)
        {
            _microtestData = microtestData;
        }

        public string GetUrl()
        {
            return _microtestData["url"];
        }

        private readonly IDictionary<string, string> _microtestData;
    }
}
