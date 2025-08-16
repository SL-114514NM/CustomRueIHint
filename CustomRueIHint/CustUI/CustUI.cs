using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRueIHint.CustUI
{
    public class CustUI
    {
        private static int _nextId = 1;
        private float _verticalPos = -1;
        private VerticalPosition? _verticalPosType;

        public CustUI()
        {
            this.ID = _nextId++;
        }

        public float Length { get; set; } = 10f;
        public float Width { get; set; } = 5f;
        public string Color { get; set; } = "#FFFFFFFF";
        public float ShowTime { get; set; } = 5f;
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Middle;
        public int ID { get; }
        public Dictionary<ContentType, string> Contents { get; } = new Dictionary<ContentType, string>();

        public float VerticalPosition
        {
            get => _verticalPosType != null ? (float)_verticalPosType.Value : _verticalPos;
            set
            {
                _verticalPos = value;
                _verticalPosType = null;
            }
        }

        public VerticalPosition VerticalPositionType
        {
            get => _verticalPosType ?? CustomRueIHint.VerticalPosition.Middle;
            set
            {
                _verticalPosType = value;
                _verticalPos = -1;
            }
        }

        internal string BuildUIString()
        {
            var contentLength = Math.Max(0, Length - 0.5f);
            var contentWidth = Math.Max(0, Width - 0.5f);

            var sb = new StringBuilder()
                .Append($"<size={Length}cm><line-height={Width}cm>")
                .Append($"<color={Color}>")
                .Append(GetAlignmentTag())
                .Append("<quad />");

            foreach (var content in Contents)
            {
                sb.Append(BuildContentString(content.Key, content.Value, contentLength, contentWidth));
            }

            return sb.ToString();
        }

        private string GetAlignmentTag()
        {
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left: return "<align=left>";
                case HorizontalAlignment.Right: return "<align=right>";
                default: return "<align=center>";
            }
        }

        private string BuildContentString(ContentType type, string content, float length, float width)
        {
            switch (type)
            {
                case ContentType.Video:
                    return $"<video src=\"{content}\" width={length}cm height={width}cm>";
                case ContentType.Image:
                    return $"<img src=\"{content}\" width={length}cm height={width}cm>";
                default:
                    return $"<size={length}cm><line-height={width}cm>{content}</line-height></size>";
            }
        }
    }
}
