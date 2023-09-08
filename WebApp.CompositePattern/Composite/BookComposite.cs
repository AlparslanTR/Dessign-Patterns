﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace WebApp.CompositePattern.Composite
{
    public class BookComposite : IComponent
    {
        public List<IComponent> _components;
        public BookComposite(int ıd, string name)
        {
            Id = ıd;
            Name = name;
            _components = new List<IComponent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        


        public void Add(IComponent component)
        {
            _components.Add(component);
        }

        public void Remove(IComponent component)
        {
            _components.Remove(component);
        }


        public int Count()
        {
            return _components.Sum(x=>x.Count());
        }

        public string Display()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($@"<div class='text-info my-1'>
                         <a href='#' class='menu'>
                         {Name}
                         ({Count()})  
                         </a>
                         </div>");

            if (!_components.Any()) return sb.ToString();

            sb.Append("<ul class='list-group-flush ml-3'>");
            foreach (var item in _components)
            {
                sb.Append(item.Display());
            }
            sb.Append("</ul>");
                            
            return sb.ToString();                      
        }

        public List<SelectListItem> GetSelectListItems(string line)
        {
            var list = new List<SelectListItem> {
                new($"{line}{Name}",Id.ToString()),
            };

            if (_components.Any(x=>x is BookComposite ))
            {
                line += " - ";
            }

            _components.ForEach(x =>
            {
                if (x is BookComposite bookComposite)
                {
                    list.AddRange(bookComposite.GetSelectListItems(line));
                }
            });

            return list;
        }
    }
}
