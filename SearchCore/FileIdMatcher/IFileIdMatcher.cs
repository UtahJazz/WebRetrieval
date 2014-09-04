namespace SearchCore.FileIdMatcher
{
    public interface IFileIdMatcher
    {
        int MatchFile(string fileName);

        string GetFileById(int fileId);

        string GetMatchedExtention();

        void Load();

        void Save();
    }
}
