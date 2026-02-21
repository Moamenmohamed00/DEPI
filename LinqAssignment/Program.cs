using LibraryManagementSystem;
using LINQ_DATA;

var Availablebooks= from Book in LibraryData.Books
                    where(Book.IsAvailable)
                    select Book;
//ConsoleTableExtensions.ToConsoleTable(Availablebooks);//static method
Availablebooks.ToConsoleTable();//extention method

var Booktitle = from Book in LibraryData.Books
                select new { Book.Title };
Booktitle.ToConsoleTable();

var bookGenre=from Book in LibraryData.Books
             // where(Book.Genre=="programmin")
             where(Book.Genre=="Programming")
              select Book;
bookGenre.ToConsoleTable();

var booktitle = from Book in LibraryData.Books
                orderby Book.Title ascending
                select Book;
booktitle.ToConsoleTable();

var expencivebooks=from Book in LibraryData.Books
                   where(Book.Price>=30)
                   select new {Book.Title,Book.Genre,Book.Price};
expencivebooks.ToConsoleTable();

var uniquebooks = LibraryData.Books.DistinctBy(book => book.Genre);
uniquebooks.ToConsoleTable();

var countGenre = LibraryData.Books.GroupBy(b => b.Genre).Select(g => new { genre = g.Key, count = g.Count() });
countGenre.ToConsoleTable();

var recentbooks = LibraryData.Books.Where(b => b.PublishedYear > 2010).Select(b => new { b.Title, b.IsAvailable, b.PublishedYear });
recentbooks.ToConsoleTable();

int pagenum = 1;
int pagesize = 5;
var first5books = LibraryData.Books.Skip((pagenum - 1) * pagesize).Take(pagesize).ToList();
first5books.ToConsoleTable();

var b =LibraryData.Books.Where(b => b.Price > 50).ToList();
var checkexpen = LibraryData.Books.Any(b => b.Price > 50) ? "there are expencive books" : "no all bokks below 50$";
b.ToConsoleTable();
Console.WriteLine(checkexpen);

var booksAuthors = LibraryData.Books.Join(LibraryData.Authors, b => b.AuthorId, a => a.Id, (book, author) => new { book.Id,bookName= book.Title,AuthorName= author.Name, book.AuthorId });
booksAuthors.ToConsoleTable();

var avergeGenre = LibraryData.Books.GroupBy(b => b.Genre).Select(g => new { genre = g.Key, Averge = g.Average(b=>b.Price)});
avergeGenre.ToConsoleTable();

var exepenset = LibraryData.Books.MaxBy(b => b.Price);
Console.WriteLine($"{exepenset.Title},{exepenset.Genre},{exepenset.Price}");
var groupbydecade = LibraryData.Books.GroupBy(b => (b.PublishedYear / 10) * 10).Select(b => new { publish = b.Key, bookname = b.Select(t => t.Title) });
groupbydecade.ToConsoleTable();

var activeloan = LibraryData.Books.Join(LibraryData.Loans, b => b.Id, l => l.BookId, (book, loan) => new { book.Title, loan.LoanDate, loan.ReturnDate, loan.DueDate }).Where(l => l.ReturnDate is null);
activeloan.ToConsoleTable();

var borrowbook = LibraryData.Loans.Join(LibraryData.Books, l => l.BookId, b => b.Id, (loan, book) => new {bookid=book.Id,title=book.Title,loan.LoanDate }).GroupBy(l => new {l.bookid,l.title }).Where(g => g.Count() > 1).Select(g => new {
BookId = g.Key.bookid,Title = g.Key.title,BorrowCount = g.Count()});
borrowbook.ToConsoleTable();

var overdueBookNotReturned = LibraryData.Books.Join(LibraryData.Loans, book => book.Id, loan => loan.BookId, (b, l) => new { Tittle = b.Title, Genre = b.Genre, Avalbality = b.IsAvailable, Return = l.ReturnDate, Due = l.DueDate }).Where(l => l.Return is null);//if (l,b) will be the other way
overdueBookNotReturned.ToConsoleTable();
/*var overdueBooks = LibraryData.Loans
    .Where(l => l.ReturnDate == null && l.DueDate < DateTime.Now)
    .Join(LibraryData.Books,
          loan => loan.BookId,
          book => book.Id,
          (loan, book) => new
          {
              Title = book.Title,
              Genre = book.Genre,
              DueDate = loan.DueDate,
              IsAvailable = book.IsAvailable
          });
overdueBooks.ToConsoleTable();*/
var AuthorBokkCount = LibraryData.Books.Join(LibraryData.Authors, b => b.AuthorId, a => a.Id, (b, a) => new { b.Title, a.Id, a.Name }).GroupBy(a => a.Name).Select(g => new { g.Key, Authorcount = g.Count() }).OrderByDescending(g=>g.Authorcount);
AuthorBokkCount.ToConsoleTable();

var priceRange = LibraryData.Books.GroupBy(b => b.Price < 20 ? "Cheap" : b.Price <= 40 ? "Medium" : "Epenciseve").Select(g => new {range= g.Key,count= g.Count() });
priceRange.ToConsoleTable();

var loanStats = LibraryData.Loans
    .GroupBy(l => l.MemberId)
    .Select(g => new
    {
        MemberId = g.Key,
        TotalLoans = g.Count(),
        ActiveLoans = g.Count(l => l.ReturnDate == null),
        AverageDaysBorrowed = g
            .Where(l => l.ReturnDate != null)
            .Average(l => (l.ReturnDate.Value - l.LoanDate).TotalDays)
    });
loanStats.ToConsoleTable();
