using Bz.ClassFinder.Interfaces;
using System;

namespace Bz.ClassFinder.Attributes
{
    public class BzDescriptionAttribute : Attribute, IBzDescription
    {
        public BzDescriptionAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
    }
}
