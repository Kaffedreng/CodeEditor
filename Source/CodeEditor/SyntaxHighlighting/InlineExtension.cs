using System.Windows;
using System.Windows.Documents;

namespace CodeEditor.SyntaxHighlighting {

    public static class InlineExtension {

        public static Paragraph GetParagraph(this Inline inline) {

            var contentElement = ((ContentElement)inline.Parent);

            while (contentElement != null) {
                var p = contentElement as Paragraph;

                if (p != null) {
                    return p;
                }
                contentElement = ((ContentElement)inline.Parent);
            }

            return null;
        }
    }
}
