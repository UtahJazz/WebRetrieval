using Common;
using SearchCore.FileIdMatcher;

namespace SearchCore.DirectoryProvider
{
    public static class DirectoryProviderFactory
    {
        public static IFileIdMatcher CreateMetadataFileIdMatcher()
        {
            return new ByNameFileIdMatcher("");
        }

        public static IFileIdMatcher CreateForwardIndexIdMatcher()
        {
            return new ByNameFileIdMatcher(".htm");
        }

        public static IDirectoryProvider CreateMetadataDirectoryProvider()
        {
            return new DirectoryProvider(
                    ConfigProvider.GetStringValue("MetadataIndexDirectory"),
                    CreateMetadataFileIdMatcher());
        }

        public static IDirectoryProvider CreateForwardDirectoryProvider()
        {
            return new DirectoryProvider(
                ConfigProvider.GetStringValue("ForwardIndexDirectory"),
                CreateForwardIndexIdMatcher());
        }
    }
}
