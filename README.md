Her er vores CodeEditor-projekt.

Projektet er et tekstredigeringsprogram med muligvis for tilføjelse af Plugins gennem Plugin Manager-menuen. Der er også implementeret syntax highlighting.

Der har været en udfordring med at få bindings til at virke med en RichTextBox. Det er tilsyneladende ikke muligt - og uden den kan vi ikke lave Syntax Highlighting.

Derfor har vi lavet en branch hvor der er en RichTextBox, men som bruger Events istedet for Bindings. Til gengæld har vi fungerende Syntax Highlighting i et hvis omfang, med mulighed for, let at tilføje flere sprog.

#master
Denne branch indeholder et program med en almindelig TextBox, som så bruger bindings. Der er ikke syntax highlighting.

#syntax-highlighting
Denne branch indeholder et program med en RichTextBox, som bruger en Event Handler istedet for bindings. Til gengæld er der fungerende Syntax highlighting.
På denne branch er der desværre problemer med menupunkterne, som ikke dukker frem.

Prøv for eksempel disse to eksempler:

### C-Sharp
	using System;

	  namespace CodeEditor.Views {

      using CodeEditor.Interfaces;
      using CodeEditor.SyntaxHighlighting;

      public class TextEditorControl : UserControl, IUserControl {

	    // A boolean property
        private bool booleanProperty;
        
    	}
	}
	
###SQL
	SELECT Book.title AS Title,
       count(*) AS Authors
 	FROM  Book
 	JOIN  Book_author
   	ON  Book.isbn = Book_author.isbn
 	GROUP BY Book.title;