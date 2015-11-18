using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using CodeEditor.SyntaxHighlighting;

namespace CodeEditor.Interfaces {

    public interface ISyntaxProcessorCustomization {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        HashSet<string> CustomKeywordsForContentType(ContentType contentType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns>The color used for formatting, or null if no custom color is defined.</returns>
        Color? CustomFontColorForContentType(ContentType contentType);

    }

    public interface ISyntaxProcessor {

        /// <summary>
        /// A regular expression that defines how to split the words.
        /// </summary>
        Regex SplitWordsRegex { get; }

        /// <summary>
        /// Each word has a specific content type.
        /// All elements are isolated in Inline text, and passed to the FormatInlineWithContentType-method.
        /// This way you can have different colors for different types of keywords.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        ContentType ContentTypeForWord(string word);

        /// <summary>
        /// Applies formatting (color) to the text of the specified content type
        /// </summary>
        /// <param name="inline"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Inline FormatInlineWithContentType(Inline inline, ContentType contentType);

    }
}
