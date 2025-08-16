using CustomRueIHint.CustUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRueIHint.API
{
    public static class CustUIManager
    {
        private static readonly Dictionary<int, CustUI.CustUI> AllUIs = new Dictionary<int, CustUI.CustUI>();

        public static CustUI.CustUI Get(int id)
        {
            AllUIs.TryGetValue(id, out var ui);
            return ui;
        }

        public static bool TryGet(int id, out CustUI.CustUI ui)
        {
            return AllUIs.TryGetValue(id, out ui);
        }

        public static void AddContent(int id, ContentType type, string content)
        {
            if (AllUIs.TryGetValue(id, out var ui))
            {
                ui.Contents[type] = content;

                foreach (var hubUIs in PlayerExtensions.ActiveUIs)
                {
                    if (hubUIs.Value.ContainsKey(id))
                    {
                        hubUIs.Key.RemoveUI(ui);
                        hubUIs.Key.AddUI(ui);
                    }
                }
            }
        }
    }
}
