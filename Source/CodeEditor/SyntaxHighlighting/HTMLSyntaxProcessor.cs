using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CodeEditor.Interfaces;

namespace CodeEditor.SyntaxHighlighting
{
    class HTMLSyntaxProcessor : ISyntaxProcessorCustomization
    {
        // html syntax
        public HashSet<string> CustomKeywordsForContentType(ContentType contentType)
        {
            //switch ()
            //{
                    
            //}

            return null;
        }

        public Color? CustomFontColorForContentType(ContentType contentType)
        {
            return null;
        }
    }
}
