using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using CodeEditor.SyntaxHighlighting;

namespace CodeEditor.Interfaces {

    public interface ISyntaxProcessorCustomization {

        /// <summary>
        /// Use this method to specify custom keywords for the implemented language.
        /// Using a switch on the content type, you can return a HashSet of keywords
        /// that should be added for that particular content type.
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns>A HashSet of strings, or null if no custom keywords should be added for the specified contentType.</returns>
        HashSet<string> CustomKeywordsForContentType(ContentType contentType);

        /// <summary>
        /// This method is used to override the default colors
        /// and assign custom colors for specific content types.
        /// If you want to override the default color, 
        /// return a new color for the content type you wish to customize.
        /// Return null for a content type to keep the default settings.
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
