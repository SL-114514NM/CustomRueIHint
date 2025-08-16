using CustomRueIHint.Hints;
using RueI.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRueIHint.API
{
    public static class RueIHintManager
    {
        private static readonly Dictionary<int, Hint> _allHints = new Dictionary<int, Hint>();

        public static Hint Get(int id) => _allHints.TryGetValue(id, out var hint) ? hint : null;

        public static bool TryGet(int id, out Hint hint) => _allHints.TryGetValue(id, out hint);

        public static void HideAfter(Hint hint, float seconds)
        {
            if (hint == null || !_allHints.ContainsKey(hint.ID)) return;

            foreach (var kvp in PlayerExtensions._activeHints)
            {
                if (kvp.Value.ContainsKey(hint.ID))
                {
                    var displayCore = DisplayCore.Get(kvp.Key);
                    displayCore.Scheduler.ReplaceJob(
                        TimeSpan.FromSeconds(seconds),
                        () => kvp.Key.RemoveHint(hint),
                        kvp.Value[hint.ID].JobToken
                    );
                }
            }
        }
    }
}
