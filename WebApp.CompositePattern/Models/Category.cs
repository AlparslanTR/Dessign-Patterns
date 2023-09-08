namespace WebApp.CompositePattern.Models
{
    public class Category
    {
       // Id        Name        UserId      ReferenceId
       // 1         Kitaplar      1              0    
       // 2    Roman Kitaplar     1              1    
       // Alt kategori olmayanlar 0 olarak tutulacak. Alt kategoriye girenler ise +1 şeklinde gidecektir.

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<Book>Books { get; set; }
        public int ReferenceId { get; set; }
    }
}
