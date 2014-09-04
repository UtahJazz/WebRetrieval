using System;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class RangerParameter
    {
        public RangerParameter(int fileId)
        {
            FileId = fileId;
        }

        public int FileId { get; private set; }

        public double WordNearest { get; set; }

        public double TfIdf { get; set; }

        public double TagBased { get; set; }

        public double TitleBased { get; set; }

        public double VoteBased { get; set; }

        public double UserPreferenceBased { get; set; }

        public double FullRank { get; set; }

        public double GetRank(Rank rank)
        {
            switch (rank)
            {
                case Rank.WordNearest:
                    return WordNearest;
                case Rank.TfIdf:
                    return TfIdf;
                case Rank.TagBased:
                    return TagBased;
                case Rank.VoteBased:
                    return VoteBased;
                case Rank.UserPreferenceBased:
                    return UserPreferenceBased;
                case Rank.TitleBased:
                    return TitleBased;
                default:
                    throw new ArgumentOutOfRangeException("rank");
            }
        }

        public void SetRank(Rank rank, double value)
        {
            switch (rank)
            {
                case Rank.WordNearest:
                    WordNearest = value;
                    break;
                case Rank.TfIdf:
                    TfIdf = value;
                    break;
                case Rank.TagBased:
                    TagBased = value;
                    break;
                case Rank.VoteBased:
                    VoteBased = value;
                    break;
                case Rank.UserPreferenceBased:
                    UserPreferenceBased = value;
                    break;
                case Rank.TitleBased:
                    TitleBased = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("rank");
            }
        }
    }
}
