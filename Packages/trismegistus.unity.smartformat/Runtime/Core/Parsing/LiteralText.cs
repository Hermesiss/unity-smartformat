using System;
using Trismegistus.SmartFormat.Core.Settings;

namespace Trismegistus.SmartFormat.Core.Parsing
{
    /// <summary>
    /// Represents the literal text that is found
    /// in a parsed format string.
    /// </summary>
    public class LiteralText : FormatItem
    {
        public LiteralText(SmartSettings smartSettings, Format parent, int startIndex) : base(smartSettings, parent,
                                                                                              startIndex)
        {
        }

        public LiteralText(SmartSettings smartSettings, Format parent) : base(smartSettings, parent, parent.startIndex)
        {
        }

        public override string ToString()
        {
            return SmartSettings.ConvertCharacterStringLiterals
                ? ConvertCharacterLiteralsToUnicode()
                : baseString.Substring(startIndex, endIndex - startIndex);
        }

        private string ConvertCharacterLiteralsToUnicode()
        {
            var source = baseString.Substring(startIndex, endIndex - startIndex);

            // No character literal escaping - nothing to do
            if (source[0] != Parser.m_CharLiteralEscapeChar)
                return source;

            // The string length should be 2: espace character \ and literal character
            if (source.Length < 2) throw new ArgumentException($"Missing escape sequence in literal: \"{source}\"");

            char c;
            switch (source[1])
            {
                case '\'':
                    c = '\'';
                    break;
                case '\"':
                    c = '\"';
                    break;
                case '\\':
                    c = '\\';
                    break;
                case '0':
                    c = '\0';
                    break;
                case 'a':
                    c = '\a';
                    break;
                case 'b':
                    c = '\b';
                    break;
                case 'f':
                    c = '\f';
                    break;
                case 'n':
                    c = '\n';
                    break;
                case 'r':
                    c = '\r';
                    break;
                case 't':
                    c = '\t';
                    break;
                case 'v':
                    c = '\v';
                    break;
                default:
                    throw new ArgumentException($"Unrecognized escape sequence in literal: \"{source}\"");
            }

            return c.ToString();
        }
    }
}
