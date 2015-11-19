﻿using System;
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

            // Flow Control
            "if",
            "else",
            "break",
            "continue",
            "return",
            "throw",
            "new",

            // Access Modifiers
            "public",
            "protected",
            "private",
            "static"
        };
        #endregion

        #region Functions
        private static readonly HashSet<string> functions = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase) {

        };
        #endregion

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