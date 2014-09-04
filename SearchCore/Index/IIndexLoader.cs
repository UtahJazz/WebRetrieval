namespace SearchCore.Index
{
    public interface IIndexLoader
    {
        void Save(IIndex indexDictionary);

        IIndex Load();
    }
}
