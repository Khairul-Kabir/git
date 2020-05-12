using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackCollection.Models.ViewModels
{
    public class PaginationVM
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string searchItem { get; set; }
        public string Status { get; set; }
    }
}