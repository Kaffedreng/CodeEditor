using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace CodeEditor.Interfaces {

    public interface ISyntaxProcessor {

        Regex SplitWordsRegex { get; }
        int GetWordTypeId(string word);
        Inline FormatInlineForId(Inline inline, int id);
    }
}
