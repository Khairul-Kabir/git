using FeedbackCollection.Models.CustomModel;
using FeedbackCollection.Models.DB;
using FeedbackCollection.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackCollection.Models.Rpository
{
    public interface IFeedBack
    {
        JsonModel CreatePost(Post post);
        JsonModel PostList(PaginationVM pagination);
        JsonModel GetPostList();
        JsonModel CommentOnPost(Comment comment);
        JsonModel LikePost(LikeDislike likeDislike);
        JsonModel DislikePost(LikeDislike likeDislike);
    }
}
