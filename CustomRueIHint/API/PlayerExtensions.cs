using CustomRueIHint.Hints;
using RueI.Displays;
using RueI.Displays.Scheduling;
using RueI.Elements;
using RueI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomRueIHint.CustUI;

namespace CustomRueIHint.API
{
    public static class PlayerExtensions
    {
        public static readonly Dictionary<ReferenceHub, Dictionary<int, TimedElemRef<SetElement>>> _activeHints = new Dictionary<ReferenceHub, Dictionary<int, TimedElemRef<SetElement>>>();
        public static readonly Dictionary<ReferenceHub, Dictionary<int, TimedElemRef<SetElement>>> ActiveUIs = new Dictionary<ReferenceHub, Dictionary<int, TimedElemRef<SetElement>>>();
        public static void AddUI(this ReferenceHub hub, CustUI.CustUI ui)
        {
            var displayCore = DisplayCore.Get(hub);
            var element = new SetElement(ui.VerticalPosition, ui.BuildUIString());

            if (!ActiveUIs.TryGetValue(hub, out var uis))
            {
                uis = new Dictionary<int, TimedElemRef<SetElement>>();
                ActiveUIs.Add(hub, uis);
            }

            var elemRef = new TimedElemRef<SetElement>();
            uis.Add(ui.ID, elemRef);

            displayCore.AddTemp(
                element,
                TimeSpan.FromSeconds(ui.ShowTime),
                elemRef
            );
        }
        public static void RemoveUI(this ReferenceHub hub, CustUI.CustUI ui)
        {
            if (ActiveUIs.TryGetValue(hub, out var uis) && uis.TryGetValue(ui.ID, out var elemRef))
            {
                DisplayCore.Get(hub).RemoveReference(elemRef);
                uis.Remove(ui.ID);
            }
        }
        public static void AddRueIHint(this ReferenceHub hub, Hint hint)
        {
            var displayCore = DisplayCore.Get(hub);
            var parsedData = hint.GetParsedData(hub);

            var elemRef = new TimedElemRef<SetElement>();
            var element = new SetElement(hint.VerticalPosition, parsedData.Content);

            if (!_activeHints.TryGetValue(hub, out var hints))
            {
                hints = new Dictionary<int, TimedElemRef<SetElement>>();
                _activeHints[hub] = hints;
            }

            hints[hint.ID] = elemRef;

            displayCore.AddTemp(
                element,
                TimeSpan.FromSeconds(hint.ShowTime),
                elemRef
            );
        }

        public static void RemoveHint(this ReferenceHub hub, Hint hint)
        {
            if (_activeHints.TryGetValue(hub, out var hints) && hints.TryGetValue(hint.ID, out var elemRef))
            {
                DisplayCore.Get(hub).RemoveReference(elemRef);
                hints.Remove(hint.ID);
            }
        }

        public static bool IsShow(this ReferenceHub hub, int id)
        {
            return _activeHints.TryGetValue(hub, out var hints) && hints.ContainsKey(id);
        }

    }
}
