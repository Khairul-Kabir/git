using FeedbackCollection.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackCollection.Models.ViewModels
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string CommentName { get; set; }
        public System.Guid CommentUniqueCode { get; set; }
        public System.Guid PostUniqueCode { get; set; }
        public System.DateTime MakeDate { get; set; }
        public string MadeBy { get; set; }

        public List<LikeDislike> LikeDislikeList { get; set; }
    }
}