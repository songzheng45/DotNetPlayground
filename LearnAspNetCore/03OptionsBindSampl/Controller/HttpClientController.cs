using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Text.Encodings.Web;

// HttpClient 
public class HttpClientController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HtmlEncoder _htmlEncoder;
    public HttpClientController(IHttpClientFactory clientFactory, HtmlEncoder htmlEncoder)
    {
        _clientFactory = clientFactory;
        _htmlEncoder = htmlEncoder;
    }

    public async Task<IActionResult> Index()
    {
        using (var client = _clientFactory.CreateClient("DS.Lottery.PK10"))
        {
            client.BaseAddress = new Uri("http://89ffer.cn");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjpbInQxMjM0NTYiLCJ0MTIzNDU2Il0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3NpZCI6ImsxS0g4RFh4bkpEY0FRSno0dnFnUzN2KzFWT3JyZ0dwREkxNXV0TWNXdjlraEF4MXYwS3gvNHMrckZYWjMzSUMiLCJzdWIiOiJ0MTIzNDU2IiwibmJmIjoxNTM4MDM5Njk1LCJleHAiOjE1MzgwNDAyOTUsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6InRvdWNhbmFwcCJ9.fRebLJAw_U6dc6Er0XNVyu9yZAzHdVAZHIton3Ai9kU");

            string json = "{'numbers':'20180927209','gameNo':'jisuk3','data':[{'gameId':'2a909ea9-9de5-41e5-9758-b351ca4f1b8b','betContent':'4','num':1,'amount':1,'odds1':69.84,'gamePlayId':'e7d2da55-f22d-4d6e-b001-bb82725501ae','playFullName':'和值','singleAmount':1},{'gameId':'2a909ea9-9de5-41e5-9758-b351ca4f1b8b','betContent':'小','num':1,'amount':1,'odds1':1.94,'gamePlayId':'0656b8d9-435f-4bb8-91a5-1c9939d31294','playFullName':'和值','singleAmount':1}],'multiples':1,'nonceStr':'uahqq4lfu6s','timestamp':'1538039762','sign':'e39a65deacc522643a4f1e532ca9b52b'}";
            HttpResponseMessage httpResponse = await client.PostAsync("api/GameBets", new StringContent(json, Encoding.UTF8, "application/json"));
            string html = await httpResponse.Content.ReadAsStringAsync();

            return Content(_htmlEncoder.Encode(html));
        }
    }
}