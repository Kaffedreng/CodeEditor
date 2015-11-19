using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;

namespace CodeEditor.SyntaxHighlighting {

    using Interfaces;

    /// <summary>
    /// The BaseSyntaxProcessor handles non-language specific keywords.
    /// All SyntaxProcessor-classes must subclass this class, and apply custom formatting if needed.
    /// </summary>
    public abstract class BaseSyntaxProcessor : ISyntaxProcessor {

        private static Regex splitRegex = new Regex(@"(\s|\(|\)|\+|\-|\%|\*|\[|\]|/)", RegexOptions.Compiled);

        #region Keywords
        private static HashSet<string> keywords = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase) {

            // Structures
            "class",
            "enum",
            "struct",

            // Data Types
            "int",
            "string",
            "bool",

            // Flow Control
            "if",
            "else",
            "break",
            "continue",
            "return",
            "throw",
            "new",

            // Access Modifiers
            "using",
            "namespace",
            "public",
            "protected",
            "private",
            "static",
            "get",
            "set"
        };
        #endregion

        #region Functions
        private static readonly HashSet<string> functions = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase) {

        };
        #endregion

        public BaseSyntaxProcessor() {
            if (this is ISyntaxProcessorCustomization) {
                var customizedThis = (ISyntaxProcessorCustomization)this;

                // TODO: Enumerate all Content Types and add keywords from them

                foreach (var keyword in customizedThis.CustomKeywordsForContentType(ContentType.Keywords)) {
                    keywords.Add(keyword);
                }

                foreach (var function in customizedThis.CustomKeywordsForContentType(ContentType.ProjectFunctionAndMethodNames)) {
                    functions.Add(function);
                }

                // TODO: Do the same for all content types
            }
        }

        public Regex SplitWordsRegex {
            get {
                return splitRegex;
            }
        }

        public ContentType ContentTypeForWord(string word) {

            if (keywords.Contains(word)) {
                return ContentType.Keywords;
            }

            if (functions.Contains(word)) {
                return ContentType.ProjectFunctionAndMethodNames;
            }

            return ContentType.PlainText;
        }

        public Inline FormatInlineWithContentType(Inline inline, ContentType contentType) {
            switch (contentType) {

                case ContentType.Comments:
                case ContentType.DocumentationMarkup:
                case ContentType.DocumentationMarkupKeywords:
                    inline.Foreground = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
                    break;

                default:
                    break;
            }

            return inline;
        }

    }

}