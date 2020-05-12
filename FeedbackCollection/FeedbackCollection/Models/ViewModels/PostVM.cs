using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackCollection.Models.ViewModels
{
    public class PostVM
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public System.Guid PostUniqueCode { get; set; }
        public System.DateTime MakeDate { get; set; }
        public string MadeBy { get; set; }
        public List<CommentVM> CommentList { get; set; }
    }
}