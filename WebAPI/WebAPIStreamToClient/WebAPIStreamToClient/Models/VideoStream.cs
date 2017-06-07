using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;

namespace WebAPIStreamToClient.Models
{
    public class VideoStream
    {
        private readonly string _filename;

        public long FileSize { get; set; }

        public VideoStream(string filename, string ext)
        {
            _filename = HostingEnvironment.MapPath($"~/downloads/{filename}.{ext}");
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];
                using (var video = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    FileSize = video.Length;

                    var bytesRead = buffer.Length;

                    while (video.CanRead)
                    {
                        bytesRead = video.Read(buffer, 0, buffer.Length);
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            }
            catch (HttpException e)
            {
                return;
            }
            catch (IOException e)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}