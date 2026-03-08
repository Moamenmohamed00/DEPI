namespace LinkVaultApi.Models
{
    public class Categoty:BaseEntity
    {
        public string Name { get; set; }
        public string Description {  get; set; }

        public ICollection<BookMark> bookMarks { get; set; }=new List<BookMark>();// LinkVaultApi.Middlewares.GlobalExceptionMiddleware[0]//Object reference not set to an instance of an object.
        public ICollection<Note> notes { get; set; } = new List<Note>();
    }
}
