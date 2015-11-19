using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor.SyntaxHighlighting {

    /// <summary>
    /// The ContentType-enum contains all available types of text content.
    /// This is used to apply formatting colors, and allows different colors
    /// for differente types of content.
    /// </summary>
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
