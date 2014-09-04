namespace SearchCore.Parsers.BlockTextBuilder
{
    public interface IBlockTextBuilder
    {
        void AppendTextBlock(string text);

        string Build();

        void Clear();

        string[] SplitByBlocks(string text);
    }
}
