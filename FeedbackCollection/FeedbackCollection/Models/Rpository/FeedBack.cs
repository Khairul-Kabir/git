using FeedbackCollection.Models.CustomModel;
using FeedbackCollection.Models.DB;
using FeedbackCollection.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace FeedbackCollection.Models.Rpository
{
    public class FeedBack : IFeedBack
    {
        FeedBackEntities DbContext = new FeedBackEntities();
        public JsonModel CommentOnPost(Comment comment)
        {
            JsonModel jsonModel = new JsonModel();

            try
            {
                comment.MakeDate = System.DateTime.Now;
                DbContext.Comments.Add(comment);
                var result = DbContext.SaveChanges();
                if (result > 0)
                {
                    jsonModel.Status = true;
                    jsonModel.Message = "Successful";
                    jsonModel.Data = comment;
                }
                else
                {
                    jsonModel.Status = false;
                    jsonModel.Message = "Failed";
                    jsonModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }

        public JsonModel CreatePost(Post post)
        {
            JsonModel jsonModel = new JsonModel();

            try
            {
                post.MakeDate = System.DateTime.Now;
                DbContext.Posts.Add(post);
                var result = DbContext.SaveChanges();
                if (result > 0)
                {
                    jsonModel.Status = true;
                    jsonModel.Message = "Successful";
                    jsonModel.Data = post;
                }
                else
                {
                    jsonModel.Status = false;
                    jsonModel.Message = "Failed";
                    jsonModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }

        public JsonModel DislikePost(LikeDislike likeDislike)
        {
            JsonModel jsonModel = new JsonModel();

            try
            {
                var result = 0;
                var data = DbContext.LikeDislikes.Where(x => x.CommentUniqueCode == likeDislike.CommentUniqueCode).FirstOrDefault();
                if (data != null)
                {
                    data.CmntDislike = data.CmntDislike + 1; ;
                    DbContext.Entry(likeDislike).State = EntityState.Modified;
                    result= DbContext.SaveChanges();
                }
                else
                {
                    likeDislike.CmntDislike = 1;
                    DbContext.LikeDislikes.Add(likeDislike);
                    result= DbContext.SaveChanges();
                }
                
                if (result>0)
                {
                    jsonModel.Status = true;
                    jsonModel.Message = "Successful";
                    jsonModel.Data = likeDislike;
                }
                else
                {
                    jsonModel.Status = false;
                    jsonModel.Message = "Failed";
                    jsonModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }

        public JsonModel LikePost(LikeDislike likeDislike)
        {
            JsonModel jsonModel = new JsonModel();

            try
            {
                var result = 0;
                var data = DbContext.LikeDislikes.Where(x => x.CommentUniqueCode == likeDislike.CommentUniqueCode).FirstOrDefault();
                if (data != null)
                {
                    data.CmntDislike = data.CmntLike + 1; ;
                    DbContext.Entry(likeDislike).State = EntityState.Modified;
                    result = DbContext.SaveChanges();
                }
                else
                {
                    likeDislike.CmntLike = 1;
                    DbContext.LikeDislikes.Add(likeDislike);
                    result = DbContext.SaveChanges();
                }

                if (result > 0)
                {
                    jsonModel.Status = true;
                    jsonModel.Message = "Successful";
                    jsonModel.Data = likeDislike;
                }
                else
                {
                    jsonModel.Status = false;
                    jsonModel.Message = "Failed";
                    jsonModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }

        public JsonModel PostList(PaginationVM pagination)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                //from post in DbContext.Posts.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageIndex).ToList()
                var postList = (from post in DbContext.Posts.ToList()
                                select new PostVM
                                {
                                    Id = post.Id,
                                    PostName = post.PostName,
                                    PostUniqueCode = post.PostUniqueCode,
                                    MadeBy = post.MadeBy,
                                    MakeDate = post.MakeDate,
                                    CommentList = (from comment in DbContext.Comments
                                                   where (post.PostUniqueCode == comment.PostUniqueCode)
                                                   join like in DbContext.LikeDislikes on comment.CommentUniqueCode equals like.CommentUniqueCode
                                                   select new CommentVM
                                                   {
                                                       Id = comment.Id,
                                                       CommentName = comment.CommentName,
                                                       MadeBy = comment.MadeBy,
                                                       MakeDate = comment.MakeDate,
                                                       LikeDislikeList = DbContext.LikeDislikes.Where(x => x.CommentUniqueCode == comment.CommentUniqueCode).ToList()
                                                   }).ToList()
                                }).ToList();
                if (postList.Count > 0)
                {
                    jsonModel.Status = true;
                    jsonModel.Message ="Successful";
                    jsonModel.Data = postList;
                }
                else
                {
                    jsonModel.Status = false;
                    jsonModel.Message = "No Data Found";
                    jsonModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }

        public JsonModel GetPostList()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                //from post in DbContext.Posts.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageIndex).ToList()
                var postList = (from post in DbContext.Posts.ToList()
                                select new PostVM
                                {
                                    Id = post.Id,
                                    PostName = post.PostName,
                                    PostUniqueCode = post.PostUniqueCode,
                                    MadeBy = post.MadeBy,
                                    MakeDate = post.MakeDate,
                                    CommentList = (from comment in DbContext.Comments
                                                   where (post.PostUniqueCode == comment.PostUniqueCode)
                                                   join like in DbContext.LikeDislikes on comment.CommentUniqueCode equals like.CommentUniqueCode
                                                   select new CommentVM
                                                   {
                                                       Id = comment.Id,
                                                       CommentName = comment.CommentName,
                                                       MadeBy = comment.MadeBy,
                                                       MakeDate = comment.MakeDate,
                                                       LikeDislikeList = DbContext.LikeDislikes.Where(x => x.CommentUniqueCode == comment.CommentUniqueCode).ToList()
                                                   }).ToList()
                                }).ToList();
                if (postList.Count > 0)
                {
                    jsonModel.Status = true;
                    jsonModel.Message = "Successful";
                    jsonModel.Data = postList;
                }
                else
                {
                    jsonModel.Status = false;
                    jsonModel.Message = "No Data Found";
                    jsonModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }
    }
}