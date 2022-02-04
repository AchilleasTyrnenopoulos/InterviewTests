using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public class PagedList<T>
    {
        public List<T> DataList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
