using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using SearchCore.DirectoryProvider;
using SearchCore.Index;
using SearchCore.IndexBuilder;
using SearchCore.Metadata;
using SearchCore.Parsers;
using SearchCore.TextFilter;

namespace ReverseIndexBuilderConsole
{
    public static class Program
    {
        private static void Main()
        {
            var punctuationFilter = new PunctuationTextFilter();

            var lightPunctuationFilter = new LightPunctuationFilter();

            var reverseIndexBuilder = new ReverseIndexBuilder(
                new InMemoryIndex(),
                punctuationFilter,
                lightPunctuationFilter);

            var contentLoader = new StackoverflowContentLoader();

            var metadataFactory = new InMemoryMetadataFactory();

            var documentMetadataBuilder = new DocumentMetadataBuilder(
                punctuationFilter,
                metadataFactory);

            Console.WriteLine("Index building...");

            var forwardIndexIdMatcher = DirectoryProviderFactory.CreateForwardIndexIdMatcher();

            var forwardIndexDirectoryProvider =
                DirectoryProviderFactory.CreateForwardDirectoryProvider();

            var metadataLoader = new InFileMetadataLoader(
                DirectoryProviderFactory.CreateMetadataDirectoryProvider());


            Parallel.ForEach(forwardIndexDirectoryProvider
            .GetFiles()
            .OrderBy(forwardIndexIdMatcher.MatchFile),
            (file, state) =>
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.F1)
                    {
                        state.Break();
                    }
                }  
                Console.WriteLine("Proces file " + file);
                var fileContent = forwardIndexDirectoryProvider.GetFileContent(file);
                var fileName = Path.GetFileName(file);
                var fileData = contentLoader.LoadData(fileContent);
                reverseIndexBuilder.AppendFile(
                    fileData,
                    forwardIndexIdMatcher.MatchFile(fileName));
                var metadata = documentMetadataBuilder.Build(fileData);
                metadataLoader.Save(
                    forwardIndexIdMatcher.MatchFile(fileName),
                    metadata);
            });

            Console.WriteLine("Save index");
            forwardIndexIdMatcher.Save();

            var index = reverseIndexBuilder.GetIndex();

            var indexLoader = new InFileIndexLoader(
                ConfigProvider.GetStringValue("ReverseIndexDirectory"));
            indexLoader.Save(index);

            Console.WriteLine("Press something to exit.");
            Console.ReadKey();
        }
    }
}
