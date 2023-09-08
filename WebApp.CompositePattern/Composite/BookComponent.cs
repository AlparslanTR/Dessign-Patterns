
namespace WebApp.CompositePattern.Composite
{
    public class BookComponent : IComponent
    {
        public BookComponent(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }

        public int Id { get ; set ; }
        public string Name { get; set ; }

        public int Count()
        {
            return 1;
        }

        public string Display()
        {
            return $"<li class='list-group-item'>{Name}</li>";
        }
    }
}
