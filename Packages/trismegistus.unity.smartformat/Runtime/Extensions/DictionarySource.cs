using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Trismegistus.SmartFormat.Core.Extensions;
using UnityEngine;

namespace Trismegistus.SmartFormat.Extensions
{
    [Serializable]
    public class DictionarySource : ISource
    {
        // There is a bug with SerializeReference that causes empty instances to not deserialize. This is a workaround while we wait for the fix (case 1183547)
        [SerializeField, HideInInspector]
        int dummyObject;

        public DictionarySource(SmartFormatter formatter)
        {
            // Add some special info to the parser:
            formatter.Parser.AddAlphanumericSelectors(); // (A-Z + a-z)
            formatter.Parser.AddAdditionalSelectorChars("_");
            formatter.Parser.AddOperators(".");
        }

        public bool TryEvaluateSelector(ISelectorInfo selectorInfo)
        {
            var current = selectorInfo.CurrentValue;
            var selector = selectorInfo.SelectorText;

            // See if current is a IDictionary and contains the selector:
            var rawDict = current as IDictionary;
            if (rawDict != null)
                foreach (DictionaryEntry entry in rawDict)
                {
                    var key = entry.Key as string ?? entry.Key.ToString();

                    if (key.Equals(selector, selectorInfo.FormatDetails.Settings.GetCaseSensitivityComparison()))
                    {
                        selectorInfo.Result = entry.Value;
                        return true;
                    }
                }

            // this check is for dynamics and generic dictionaries
            var dict = current as IDictionary<string, object>;

            if (dict != null)
            {
                var val = dict.FirstOrDefault(x =>
                    x.Key.Equals(selector, selectorInfo.FormatDetails.Settings.GetCaseSensitivityComparison())).Value;
                if (val != null)
                {
                    selectorInfo.Result = val;
                    return true;
                }
            }

            return false;
        }
    }
}
