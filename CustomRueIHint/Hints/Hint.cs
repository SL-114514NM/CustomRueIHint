using RueI.Elements.Enums;
using RueI.Parsing;
using RueI.Parsing.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRueIHint.Hints
{
    public class Hint
    {
        private static int _nextId = 1;
        private float _position = -1;
        private VerticalPosition? _positionType;

        public Hint()
        {
            this.ID = _nextId++;
        }

        public float ShowTime { get; set; } = 5f;
        public Func<ReferenceHub, string> AutoText { get; set; }
        public string Text { get; set; }
        public int ID { get; }

        public float VerticalPosition
        {
            get => _positionType != null ? (float)_positionType.Value : _position;
            set
            {
                _position = value;
                _positionType = null;
            }
        }

        public VerticalPosition VerticalPositionType
        {
            get => _positionType ?? CustomRueIHint.VerticalPosition.Middle;
            set
            {
                _positionType = value;
                _position = -1;
            }
        }

        internal ParsedData GetParsedData(ReferenceHub hub)
        {
            string content = string.IsNullOrEmpty(Text) ? AutoText(hub) : Text;
            return Parser.DefaultParser.Parse(content, ElementOptions.Default);
        }
    }
}
