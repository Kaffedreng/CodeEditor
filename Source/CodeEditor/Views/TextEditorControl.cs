using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CodeEditor.Views {

    using Interfaces;
    using SyntaxHighlighting;

    /// <summary>
    /// The TextEditorControl class is a subclass of UserControl.
    /// It is used to provide support for syntax highlighting in a RichTextBox.
    /// </summary>
    public class TextEditorControl : UserControl {

        private bool disableTextChangedEvent;

        // TODO: Changable in the menu
        // TODO: Get SyntaxProcessor from Document Type
        // TODO: Register Syntax automatically
        private ISyntaxProcessor syntaxProcessor;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TextEditorControl() {

            // TODO: Determine Syntax from file extension/contents
            this.syntaxProcessor = new SQLSyntaxProcessor();
        }

        /// <summary>
        /// Event Handler manually triggered when the text in the RichTextBox.
        /// This method assembles a list of all the paragraphs that has changed since last change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TextDidChange(object sender, TextChangedEventArgs e) {

            RichTextBox textBox = (RichTextBox)sender;

            if (this.disableTextChangedEvent) {
                return;
            }

            // Prepare a list of paragraphs that need to be changed
            var changedParagraphs = new HashSet<Paragraph>();
            foreach (var change in e.Changes) {

                if (change.Offset < (change.Offset + change.AddedLength)) {
                    var start = textBox.Document.ContentStart.GetPositionAtOffset(change.Offset);
                    var end = textBox.Document.ContentStart.GetPositionAtOffset(change.Offset + change.AddedLength);

                    if (start == null || end == null) {
                        continue;
                    }

                    var p = start.Paragraph;
                    if (p == null) {
                        var nearest = start.GetAdjacentElement(LogicalDirection.Forward) as Paragraph;
                        if (nearest != null) p = nearest;
                    }

                    while (p != null) {
                        if (p.ContentStart.CompareTo(end) >= 0) {
                            break;
                        }
                        changedParagraphs.Add(p);
                        p = p.NextBlock as Paragraph;
                    }
                }
            }

            this.ReEvaluateParagraphs(textBox, changedParagraphs);
        }

        /// <summary>
        /// Helper function to get the contents of the RichTextBox as a string.
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        /// // TODO: Change to computed property
        private string GetText(RichTextBox textBox) {
            var range = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            return range.Text;
        }

        private void ReEvaluateParagraphs(RichTextBox textBox, IEnumerable<Paragraph> paragraphs) {

            this.disableTextChangedEvent = true;

            foreach (var p in paragraphs) {
                this.EvaluateParagraph(textBox, p);
            }

            this.disableTextChangedEvent = false;
        }

        /// <summary>
        /// Evaluate the specified paragraph, by getting list of Inlines,
        /// and format them as needed.
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="paragraph"></param>
        /// <param name="list"></param>
        private void EvaluateParagraph(RichTextBox textBox, Paragraph paragraph, List<string> list = null) {

            if (list == null) {
                list = this.ExtractListOfWords(textBox, paragraph);
            }

            if (list == null) {
                return;
            }

            Inline cursorNeighbouringElement;
            LinkedList<Inline> inlines = GetListOfInlines(list, out cursorNeighbouringElement);

            this.ReAssignInlinesInParagraph(textBox, paragraph, inlines, cursorNeighbouringElement);
        }

        /// <summary>
        /// Returns a list of formatted Inline-objects in a list of words.
        /// </summary>
        /// <param name="words"></param>
        /// <param name="cursorNeighbouringElement"></param>
        /// <returns></returns>
        private LinkedList<Inline> GetListOfInlines(List<string> words, out Inline cursorNeighbouringElement) {

            var stringBuilder = new StringBuilder();
            var inlines = new LinkedList<Inline>();
            var wordAroundCaretContentType = ContentType.PlainText;
            cursorNeighbouringElement = null;

            for (var i = 0; i < words.Count; i++) {
                var word = words[i];

                // If this is the caret
                if (word == null) {

                    // If the caret is the first character - add an empty element to serve as pointer to the caret
                    if (i == 0) {
                        var run = new Run("");
                        inlines.AddLast(run);
                    }
                    // If the caret is not at the very beginning of the row make sure to add a pointer to it
                    else {
                        if (stringBuilder.Length > 0) {
                            var run = new Run(stringBuilder.ToString());
                            inlines.AddLast(run);
                            stringBuilder = new StringBuilder();
                        }

                    }
                    cursorNeighbouringElement = inlines.Last.Value;

                    continue;
                }

                // Check if upcoming is the caret pointer (null value) and it is not at the end
                if (i < (words.Count - 2) && words[i + 1] == null) {
                    wordAroundCaretContentType = syntaxProcessor.ContentTypeForWord(word + words[i + 2]);

                    if (wordAroundCaretContentType > 0) {

                        if (stringBuilder.Length > 0) {
                            inlines.AddLast(new Run(stringBuilder.ToString()));
                            stringBuilder = new StringBuilder();
                        }

                        inlines.AddLast(syntaxProcessor.FormatInlineWithContentType(new Run(word), wordAroundCaretContentType));
                        continue;
                    }
                }

                // Get content type of word
                var contentType = syntaxProcessor.ContentTypeForWord(word);

                if (contentType != ContentType.PlainText) {
                    // Add color to the word
                    if (stringBuilder.Length > 0) {
                        inlines.AddLast(new Run(stringBuilder.ToString()));
                        stringBuilder = new StringBuilder();
                    }

                    inlines.AddLast(syntaxProcessor.FormatInlineWithContentType(new Run(word), contentType));
                }
                else {
                    stringBuilder.Append(word);
                }
            }

            if (stringBuilder.Length > 0) {
                inlines.AddLast(new Run(stringBuilder.ToString()));
            }

            return inlines;
        }

        /// <summary>
        /// Reassign the specified Inline-objects in the list, in the paragraph they belong.
        /// This method will clear the affected paragraph and rewrite the contents with formatting.
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="paragraph"></param>
        /// <param name="inlines"></param>
        /// <param name="cursorNeighboutingElement"></param>
        private void ReAssignInlinesInParagraph(RichTextBox textBox, Paragraph paragraph, LinkedList<Inline> inlines, TextElement cursorNeighboutingElement) {

            textBox.BeginChange();
            paragraph.Inlines.Clear();
            paragraph.Inlines.AddRange(inlines);

            if (cursorNeighboutingElement != null) {
                textBox.CaretPosition = cursorNeighboutingElement.ContentEnd;
            }

            textBox.EndChange();
        }

        /// <summary>
        /// Extract a list of words in a paragraph.
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private List<string> ExtractListOfWords(RichTextBox textBox, Paragraph paragraph) {

            // Check if caret position is within the word
            var caretIsWithin = (textBox.CaretPosition != null &&
                                 textBox.CaretPosition.CompareTo(paragraph.ContentStart) >= 0 &&
                                 textBox.CaretPosition.CompareTo(paragraph.ContentEnd) <= 0);

            var list = new List<string>();

            // If caret is within the paragraph, we need to break it up into Inline objects and format the line
            if (caretIsWithin) {

                var beforeText = paragraph.GetTextBefore(textBox.CaretPosition);
                var afterText = paragraph.GetTextAfter(textBox.CaretPosition);

                // If there's no text on either side of the caret, we're on an empty line
                if (beforeText == "" && afterText == "") {

                    // Add white space to the new line - same as the previous line
                    var prev = paragraph.PreviousBlock as Paragraph;
                    if (prev != null) {
                        var previousLine = prev.GetText();
                        var spaces = new StringBuilder();

                        foreach (var ch in previousLine) {
                            if (ch == ' ' || ch == '\t') {
                                spaces.Append(ch);
                            }
                            else {
                                break;
                            }
                        }

                        if (spaces.Length > 0) {
                            beforeText = spaces.ToString();
                        }
                        else {
                            return null;
                        }
                    }
                    else {
                        return null;
                    }
                }

                // If theres text before or after the caret, split the words and add them to the list
                if (beforeText != "") {
                    list.AddRange(this.syntaxProcessor.SplitWordsRegex.Split(beforeText));
                }

                // Add null representing the caret
                list.Add(null);

                if (afterText != "") {
                    list.AddRange(this.syntaxProcessor.SplitWordsRegex.Split(afterText));
                }
            }
            else {
                // If caret is not in the paragraph, just add it as-is.
                // It would already be formatted.
                var text = paragraph.GetText();
                var array = this.syntaxProcessor.SplitWordsRegex.Split(text);
                list.AddRange(array);
            }

            return list;
        }

    }
}
