using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace CodeEditor.Views {

    using CodeEditor.Interfaces;
    using CodeEditor.SyntaxHighlighting;

    public class TextEditorControl : UserControl {

        private bool disableTextChangedEvent;

        // TODO: Changable in the menu
        // TODO: Get SyntaxProcessor from Document Type
        // TODO: Register Syntax automatically
        private ISyntaxProcessor syntaxProcessor;

        public bool DisableAddingWhiteSpacesOnEnter { get; set; }

        public TextEditorControl() {
            Console.WriteLine("TextEditorControl Constructor");

            // TODO: Determine Syntax
            this.syntaxProcessor = new SQLSyntaxProcessor();

            var style = new Style(typeof(Paragraph));
            style.Setters.Add(new Setter(Block.MarginProperty, new Thickness(0)));
            this.Resources.Add(typeof(Paragraph), style);
        }

        //public string ParagraphProcessorType {
        //    get {
        //        return this.syntaxProcessor.GetType().FullName;
        //    }
        //    set {
        //        if (String.IsNullOrEmpty(value)) {
        //            throw new ArgumentException("ParagraphProcessorType cannot be null or empty string");
        //        }

        //        var type = Type.GetType(value);
        //        if (type == null) {
        //            throw new ArgumentException(
        //                String.Format("Type '{0}' passed to ParagraphProcessorType cannot be recognized", value));
        //        }

        //        if (!typeof(ISyntaxProcessor).IsAssignableFrom(type)) {
        //            throw new ArgumentException(
        //                String.Format("Type '{0}' passed to ParagraphProcessorType must implement IParagraphProcessor", type));
        //        }

        //        Console.WriteLine("Using Syntax Processor: {0}", type);
        //        this.syntaxProcessor = (ISyntaxProcessor)Activator.CreateInstance(type);
        //    }
        //}

        /// <summary>
        /// If you want to pass long text please use SetLongText method instead
        /// </summary>
        //public string Text {
        //    get {
        //        var range = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);
        //        return range.Text;
        //    }
        //    set {
        //        if (String.IsNullOrEmpty(value)) {
        //            new TextRange(this.Document.ContentStart, this.Document.ContentEnd) { Text = value };
        //        }
        //        else {
        //            this.SetLongText(value);
        //        }
        //    }
        //}

        private string GetText(RichTextBox textBox) {
            var range = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            return range.Text;
        }

        //private void SetLongText(string text) {
        //    this.disableTextChangedEvent = true;

        //    var lines = text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        //    foreach (var line in lines) {
        //        var p = new Paragraph();
        //        this.Document.Blocks.Add(p);
        //        this.EvaluateParagraph(p, new List<string>(this.syntaxProcessor.SplitWordsRegex.Split(line)));
        //    }

        //    this.disableTextChangedEvent = false;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TextDidChange(object sender, TextChangedEventArgs e) {

            RichTextBox textBox = (RichTextBox)sender;

            if (this.disableTextChangedEvent) {
                return;
            }

            var changedParagraphs = new HashSet<Paragraph>();

            // Prepare a list of paragraphs that need to be changed
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

        private void ReEvaluateParagraphs(RichTextBox textBox, IEnumerable<Paragraph> paragraphs) {

            this.disableTextChangedEvent = true;

            foreach (var p in paragraphs) {
                this.EvaluateParagraph(textBox, p);
            }

            this.disableTextChangedEvent = false;
        }

        /// <summary>
        /// 
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

            Inline cursorNeighboutingElement;
            LinkedList<Inline> inlines = GetListOfInlines(list, out cursorNeighboutingElement);

            this.ReAssignInlinesInParagraph(textBox, paragraph, inlines, cursorNeighboutingElement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cursorNeighbouringElement"></param>
        /// <returns></returns>
        private LinkedList<Inline> GetListOfInlines(List<string> list, out Inline cursorNeighbouringElement) {

            var stringBuilder = new StringBuilder();
            var inlines = new LinkedList<Inline>();
            var wordAroundCaretContentType = ContentType.PlainText;
            cursorNeighbouringElement = null;

            for (var i = 0; i < list.Count; i++) {
                var current = list[i];

                // if this is the caret
                if (current == null) {

                    // if the caret is in the first character - add an empty element to serve as pointer to the caret
                    if (i == 0) {
                        var run = new Run("");
                        inlines.AddLast(run);
                    }

                    // if the caret is not at the very beginning of the row make sure to add a pointer to it
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

                // check if upcomming is the caret pointer (null value) and it is not at the end
                if (i < (list.Count - 2) && list[i + 1] == null) {
                    wordAroundCaretContentType = syntaxProcessor.ContentTypeForWord(current + list[i + 2]);

                    if (wordAroundCaretContentType > 0) {

                        if (stringBuilder.Length > 0) {
                            inlines.AddLast(new Run(stringBuilder.ToString()));
                            stringBuilder = new StringBuilder();
                        }

                        inlines.AddLast(syntaxProcessor.FormatInlineWithContentType(new Run(current), wordAroundCaretContentType));
                        continue;
                    }
                }

                var id = wordAroundCaretContentType > 0 ? wordAroundCaretContentType : syntaxProcessor.ContentTypeForWord(current);

                if (id > 0) {
                    wordAroundCaretContentType = ContentType.PlainText;

                    if (stringBuilder.Length > 0) {
                        inlines.AddLast(new Run(stringBuilder.ToString()));
                        stringBuilder = new StringBuilder();
                    }

                    inlines.AddLast(syntaxProcessor.FormatInlineWithContentType(new Run(current), id));
                }
                else {
                    stringBuilder.Append(current);
                }
            }

            if (stringBuilder.Length > 0) {
                inlines.AddLast(new Run(stringBuilder.ToString()));
            }

            return inlines;
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private List<string> ExtractListOfWords(RichTextBox textBox, Paragraph paragraph) {

            var caretIsWithin = (textBox.CaretPosition != null &&
                                 textBox.CaretPosition.CompareTo(paragraph.ContentStart) >= 0 &&
                                 textBox.CaretPosition.CompareTo(paragraph.ContentEnd) <= 0);

            var list = new List<string>();

            // TODO: If caret is within the paragraph, ...
            if (caretIsWithin) {
                var beforeText = paragraph.GetTextBefore(textBox.CaretPosition);
                var afterText = paragraph.GetTextAfter(textBox.CaretPosition);

                // enter key was pressed
                if (beforeText == "" && afterText == "") {

                    if (this.DisableAddingWhiteSpacesOnEnter) {
                        return null;
                    }

                    // add white space to the new line - same as the previous line
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

                // else

                if (beforeText != "") {
                    list.AddRange(this.syntaxProcessor.SplitWordsRegex.Split(beforeText));
                }

                list.Add(null);

                if (afterText != "") {
                    list.AddRange(this.syntaxProcessor.SplitWordsRegex.Split(afterText));
                }
            }
            else {
                // 
                var text = paragraph.GetText();
                var array = this.syntaxProcessor.SplitWordsRegex.Split(text);
                list.AddRange(array);
            }

            return list;
        }

    }
}
