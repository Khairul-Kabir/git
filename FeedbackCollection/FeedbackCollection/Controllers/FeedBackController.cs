using FeedbackCollection.CustomPolicies;
using FeedbackCollection.Models.CustomModel;
using FeedbackCollection.Models.Rpository;
using FeedbackCollection.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FeedbackCollection.Controllers
{
    [EnableCors(origins: "https://localhost:44308", headers: "*", methods: "post", exposedHeaders: "*")]
    //[CustomCorsPolicy]
    public class FeedBackController : ApiController
    {
        private IFeedBack _IFeedBack;

        public FeedBackController(IFeedBack feedBack)
        {
            _IFeedBack = feedBack;
        }

        //[HttpGet]
        public JsonModel GetPostList()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                jsonModel = _IFeedBack.GetPostList();
            }
            catch (Exception ex)
            {
                jsonModel.Status = false;
                jsonModel.Message = ex.Message;
                jsonModel.Data = null;
            }
            return jsonModel;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonModel PostList([FromBody]JsonModel json)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                PaginationVM pagination = JsonConvert.DeserializeObject<PaginationVM>(json.Data.ToString());
                jsonModel = _IFeedBack.PostList(pagination);
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
