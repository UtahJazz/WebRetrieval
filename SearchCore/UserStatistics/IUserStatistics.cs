namespace SearchCore.UserStatistics
{
    public interface IUserStatistics
    {
        int GetTagPreference(string tag);

        void AppendTag(string tag);
    }
}
