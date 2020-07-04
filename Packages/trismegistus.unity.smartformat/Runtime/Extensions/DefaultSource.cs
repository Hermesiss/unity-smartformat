using System;
using Trismegistus.SmartFormat.Core.Extensions;
using UnityEngine;

namespace Trismegistus.SmartFormat.Extensions
{
    [Serializable]
    public class DefaultSource : ISource
    {
        // There is a bug with SerializeReference that causes empty instances to not deserialize. This is a workaround while we wait for the fix (case 1183547)
        [SerializeField, HideInInspector]
        int dummyObject;

        public DefaultSource(SmartFormatter formatter)
        {
            formatter.Parser.AddOperators(","); // This is for alignment.
            formatter.Parser.AddAdditionalSelectorChars("-"); // This is for alignment.
        }

        /// <summary>
        /// Performs the default index-based selector, same as String.Format.
        /// </summary>
        public bool TryEvaluateSelector(ISelectorInfo selectorInfo)
        {
            var current = selectorInfo.CurrentValue;
            var selector = selectorInfo.SelectorText;
            var formatDetails = selectorInfo.FormatDetails;

            int selectorValue;
            if (int.TryParse(selector, out selectorValue))
            {
                // Argument Index:
                // Just like String.Format, the arg index must be in-range,
                // should be the first item, and shouldn't have any operator:
                if (selectorInfo.SelectorIndex == 0
                    && selectorValue < formatDetails.OriginalArgs.Length
                    && selectorInfo.SelectorOperator == "")
                {
                    // This selector is an argument index.
                    selectorInfo.Result = formatDetails.OriginalArgs[selectorValue];
                    return true;
                }

                // Alignment:
                // An alignment item should be preceded by a comma
                if (selectorInfo.SelectorOperator == ",")
                {
                    // This selector is actually an Alignment modifier.
                    selectorInfo.Placeholder.Alignment = selectorValue;
                    selectorInfo.Result = current; // (don't change the current item)
                    return true;
                }
            }

            return false;
        }
    }
}
