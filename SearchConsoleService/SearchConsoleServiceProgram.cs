using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Common;
using SearchCore.DirectoryProvider;
using SearchCore.Index;
using SearchCore.Metadata;
using SearchCore.Ranger;
using SearchCore.Ranger.ParametersReducer;
using SearchCore.Ranger.RangerFilter;
using SearchCore.Searcher;
using SearchCore.SnippetBuilder;
using SearchCore.TextFilter;
using SearchCore.UserQueryProcesser;
using SearchCore.UserStatistics;
using SearchCore.WCF;

namespace SearchConsoleService
{
// ReSharper disable ClassNeverInstantiated.Global
    public sealed class SearchConsoleServiceProgram
// ReSharper restore ClassNeverInstantiated.Global
    {
        private static IIndex LoadIndex()
        {
            return 
                new InFileIndexLoader(
                    ConfigProvider.GetStringValue("ReverseIndexDirectory"))
                    .Load();
        }

        private static ISearcher CreateSearcher()
        {
            var index = LoadIndex();

            // var metadataPoolLoader = new MetadataPoolLoader(ConfigProvider.GetStringValue("ReverseIndexDirectory")); 
            
            Console.WriteLine("Load metadata pool.");

            var userStatisticsLoader =
                new InFileUserStatisticsLoader(
                    ConfigProvider.GetStringValue("UserStatisticsDirectory"));

            var metadataLoader = DirectoryProviderFactory.CreateMetadataDirectoryProvider();
           
            var metadataPool = new InMemoryMetadataPool();

            metadataPool.Load(
               metadataLoader,
               DirectoryProviderFactory.CreateMetadataFileIdMatcher(),
               new InFileMetadataLoader(metadataLoader));

            var wordFilter = new PunctuationTextFilter();
            var tagFilter = new LightPunctuationFilter();

            var nearestRanger = new RangerManager(
                new ParameterCalculatorsAggregator(
                    new IRankParameterCalculator[]
                    {
                        new WordNearestParameterCalculator(
                            metadataPool,
                            wordFilter),
                        new TfIdfParameterCalculator(
                            index,
                            wordFilter),
                        new TitleBasedRankCalculator(
                            metadataPool,
                            wordFilter), 
                        new VoteBasedRankCalculator(
                            metadataPool),
                        new TagBasedRankCalculator(
                            metadataPool,
                            tagFilter),
                        new UserPreferenceCalculator(
                            userStatisticsLoader,
                            metadataPool) 
                    }),
                new FileBasedWeightsReducer(
                    ConfigProvider.GetStringValue("RangeWeightFile")));

            return new DocumentSearcher(
                new BinaryQueryProcesser(
                    index,
                    wordFilter, 
                    tagFilter),
                nearestRanger, 
                new SnippetBuilderStub(),
                DirectoryProviderFactory.CreateForwardIndexIdMatcher(),
                DirectoryProviderFactory.CreateForwardDirectoryProvider(),
                metadataPool,
                userStatisticsLoader);
        }

        private static void Main()
        {
            var service = new SearchService(CreateSearcher());
            using (var host = new ServiceHost(service))
            {

                host.AddServiceEndpoint(
                    typeof (ISearchService),
                    new BasicHttpBinding(),
                    "http://localhost:1565/SearchService");
                
                var debugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();

                if (debugBehavior == null)
                {
                    host.Description.Behaviors.Add(new ServiceDebugBehavior
                    {
                        IncludeExceptionDetailInFaults = true
                    });
                }
                else
                {
                    debugBehavior.IncludeExceptionDetailInFaults = true;
                }

                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    HttpGetUrl = new Uri("http://localhost:1565/SearchService"),
                    MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                };

                host.Description.Behaviors.Add(smb);

                host.Open();

                Console.WriteLine("Press something for exit.");
                Console.ReadKey();
            }
        }
    }
}
