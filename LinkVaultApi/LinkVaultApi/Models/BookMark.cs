using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.Models
{
    public class BookMark:BaseEntity
    {
        public string Title { get; set; }
       // [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$",
        //ErrorMessage = "Please enter a valid URL")]
        //[Url(ErrorMessage = "Please enter a valid URL")]//fluent is need function to check so annotation is easeir
        public string URL { get; set; }
        public bool IsFavorite { get; set; }//=false;
        public bool IsArchived { get; set; }//=false;
        
        //nav prop
        public Categoty categoty { get; set; }
        public int CategoryId {  get; set; }
        public ICollection<BookMarkNotes> Notes { get; set; }
    }
}
