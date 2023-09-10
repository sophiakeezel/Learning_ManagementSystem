using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Library.LearningManagement.Models
{
    public class Module
    {

        private static int lastId = 0;
        private int id = 0;

        public int Id
        {
            get; private set;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContentItem> Content { get; set; }

        public Module()
        {
            Content = new List<ContentItem>();
            Name = string.Empty;
            Description = string.Empty;
            Id = ++lastId;
        }

        public override string ToString()
        {
            return $"{Name}: {Description}\n" + 
                $"{string.Join("\n\t", Content.Select(c => c.ToString()).ToArray())}";
        }
    }
}