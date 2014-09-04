using System;
using ProtoBuf;

namespace SearchCore.Metadata
{
    [Serializable]
    [ProtoContract]
    public sealed class WordPosition
    {
        public WordPosition()
        {
        }

        public WordPosition(
            int paragraph, 
            int inParagraphPosition)
        {
            InParagraphPosition = inParagraphPosition;
            Paragraph = paragraph;
        }

        [ProtoMember(1)]
        public int Paragraph { get; private set; }

        [ProtoMember(2)]
        public int InParagraphPosition { get; private set; }
       
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var castedObject = obj as WordPosition;
            return Paragraph == castedObject.Paragraph && InParagraphPosition == castedObject.InParagraphPosition;
        }

        public override int GetHashCode()
        {
            return Paragraph | InParagraphPosition;
        }
    }
}
