using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackCollection.Models.CustomModel
{
    public class JsonModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}