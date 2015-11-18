using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor.SyntaxHighlighting {

    public enum ContentType {
        PlainText,
        Comments,
        DocumentationMarkup,
        DocumentationMarkupKeywords,
        Strings,
        Characters,
        Numbers,
        Keywords,
        PreprocessorStatements,
        URLs,
        Attributes,
        ProjectClassNames,
        ProjectFunctionAndMethodNames,
        ProjectConstants,
        ProjectTypeNames,
        ProjectInstanceVariablesAndGlobals,
        ProjectPreprocessorMacros,
        OtherClassNames,
        OtherFunctionAndMethodNames,
        OtherConstants,
        OtherTypeNames,
        OtherInstanceVariablesAndGlobals,
        OtherPreprocessorMacros,
    }
}
