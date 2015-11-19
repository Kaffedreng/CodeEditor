using System.Windows.Documents;

namespace CodeEditor.SyntaxHighlighting {

    /// <summary>
    /// An extension of Paragraph to provide additional methods.
    /// </summary>
    public static class ParagraphExtension {

        /// <summary>
        /// Get the text in the specified paragraph.
        /// </summary>
        /// <param name="inline"></param>
        /// <returns></returns>
        public static string GetText(this Paragraph paragraph) {
            var range = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
            return range.Text;
        }

        /// <summary>
        /// Get the text before the specified paragraph.
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public static string GetTextBefore(this Paragraph paragraph, TextPointer pointer) {
            var range = new TextRange(paragraph.ContentStart, pointer);
            return range.Text;
        }

        /// <summary>
        /// Get the text after the specified paragraph.
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public static string GetTextAfter(this Paragraph paragraph, TextPointer pointer) {
            var range = new TextRange(pointer, paragraph.ContentEnd);
            return range.Text;
        }
    }
}
