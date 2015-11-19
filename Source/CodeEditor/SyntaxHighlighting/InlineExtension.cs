using System.Windows;
using System.Windows.Documents;

namespace CodeEditor.SyntaxHighlighting {

    /// <summary>
    /// An extension of Inline to provide additional methods.
    /// </summary>
    public static class InlineExtension {

        /// <summary>
        /// Get the Paragraph enclosing the Inline.
        /// </summary>
        /// <param name="inline"></param>
        /// <returns></returns>
        public static Paragraph GetParagraph(this Inline inline) {

            var contentElement = (ContentElement)inline.Parent;

            while (contentElement != null) {
                var p = contentElement as Paragraph;

                if (p != null) {
                    return p;
                }
                contentElement = (ContentElement)inline.Parent;
            }

            return null;
        }
    }
}
