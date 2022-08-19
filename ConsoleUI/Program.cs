using Entities;
using infrastructure;

var libraryRepository = new LibraryRepository();

UI();

#region UI Functions
void PresentOptions()
{
    Console.WriteLine("1: List all books");
    Console.WriteLine("2: Create a new book");
    Console.WriteLine("3: Remove a book");
    Console.WriteLine("4: List all authors");
    Console.WriteLine("5: Create a new author");
    Console.WriteLine("6: Delete an author and their books");
    Console.WriteLine("7: Rebuild DB (useful in case of schema changes)");
    Console.WriteLine("8: Insert a new Library");
    Console.WriteLine("9: List all libraries");
    Console.WriteLine("Exit: Close the program");
}

void PickOption()
{
    var input = Console.ReadLine();
    if (input.Equals("1"))
    {
        ListAllBooks();
    } else if (input.Equals("2"))
    {
        InsertBook();
    } else if (input.Equals("3"))
    {
        RemoveBook();
    } else if (input.Equals("4"))
    {
        ListAllAuthors();
    } else if (input.Equals("5"))
    {
        InsertAuthor();
    } else if (input.Equals("6"))
    {
        DeleteAuthorAndTheirBooks();
    } else if (input.Equals("7"))
    {
        RebuildDB();
    }
    else if(input.Equals("8"))
    {
        addLibrary();
    }
    else if (input.Equals("9"))
    {
        ListAllAuthors();
    }
    else if (input.Equals("Exit"))
    {
        exitProgram();
    }
}

void UI()
{
    PresentOptions();
    Console.WriteLine();//Whitespace
    PickOption();
    Console.WriteLine();//Whitespace
    UI();
}

#endregion


#region Options

void ListAllBooks()
{
    foreach (Book book in libraryRepository.SelectAllBooks())
    {
        Console.WriteLine("Book number "+book.Id+": "+book.Title);
    }
}

void InsertBook()
{
    Console.WriteLine("Book title: ");
    var title = Console.ReadLine();
    var book = new Book();
    book.Title = title;
    Console.WriteLine("ID of book author?");
    ListAllAuthors();
    int authorId = Int32.Parse(Console.ReadLine());
    book.AuthorId = authorId;
    Console.WriteLine("ID of Library?");
    ListAllLibraries();
    int libraryId = Int32.Parse(Console.ReadLine());
    book.LibraryId = libraryId;
    libraryRepository.InsertBook(book);
    Console.WriteLine("Insertion complete");
}

void addLibrary()
{
    Console.WriteLine("Name of library?");
    var name = Console.ReadLine();
    var library = new Library()
    {
        Name = name
    };
    libraryRepository.AddLibrary(library);
    Console.WriteLine("Insertion complete");
}

void exitProgram()
{
    Console.WriteLine("Program has ended");
    Environment.Exit(0);
}


void RemoveBook()
{
    Console.WriteLine("Write book ID to remove:");
    ListAllBooks();
    int id = Int32.Parse(Console.ReadLine());
    libraryRepository.DeleteBook(id);
    Console.WriteLine("Book removed");
}

void ListAllAuthors()
{
    foreach (Author author in libraryRepository.GetAuthors())
    {
        Console.WriteLine("Author number: "+author.Id+": "+author.Name);
    }
}

void ListAllLibraries()
{
    foreach (Library library in libraryRepository.GetLibraries())
    {
        Console.WriteLine("Library ID" + library.Id + ": " + library.Name);
    }
}



void InsertAuthor()
{
    Console.WriteLine("Name of author?");
    var name = Console.ReadLine();
    var author = new Author()
    {
        Name = name
    };
    libraryRepository.InsertAuthor(author);
    Console.WriteLine("Insertion complete");
}

void DeleteAuthorAndTheirBooks()
{
    Console.WriteLine("ID of author to delete?");
    ListAllAuthors();
    int id = Int32.Parse(Console.ReadLine());
    libraryRepository.DeleteAuthor(id);
    Console.WriteLine("Deletion complete");
}

void RebuildDB()
{
    libraryRepository.Migrate();
    Console.WriteLine("DB rebuilt");
}

#endregion