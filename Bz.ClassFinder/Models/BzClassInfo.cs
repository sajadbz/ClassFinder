using System;
using System.Collections.Generic;
using System.Text;

namespace Bz.ClassFinder.Models
{

    public class BzClassInfo
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public IEnumerable<BzMethodInfo> Methods { get; set; }
    }
}
