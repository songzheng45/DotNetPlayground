using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebAPIStreamToClient.Models;

namespace WebAPIStreamToClient.Controllers
{
    public class VideoController : ApiController
    {
        public HttpResponseMessage Get(string filename, string ext)
        {
            VideoStream videoStream = new VideoStream(filename, ext);

            var response = Request.CreateResponse();
            Action<Stream, HttpContent, TransportContext> writeAction = videoStream.WriteToStream;
            response.Content = new PushStreamContent(writeAction, new MediaTypeHeaderValue($"video/{ext}"));
            response.Content.Headers.ContentLength = videoStream.FileSize;

            return response;
        }
    }
}