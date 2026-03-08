namespace LinkVaultApi.Models
{
    public class Note:BaseEntity
    {
        public string Title {  get; set; }
        public string Content { get; set; }
        public bool IsPinned { get; set; }//=false;
        public Categoty categoty { get; set; }
        public int CategoryId {  get; set; }
    }
}
