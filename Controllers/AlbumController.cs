using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Albums.Models;
using System.Net.Http;

namespace Albums.Controllers
{
    public class AlbumController : Controller
    {
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ILogger<AlbumController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            List<Album> albums = new List<Album>() { };
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTaskAlbum = await client.GetAsync("albums");
                var responseTaskPhoto =  await client.GetAsync("photos");
                var responseTaskComments = await client.GetAsync("comments");
                if(responseTaskAlbum.IsSuccessStatusCode && responseTaskComments.IsSuccessStatusCode && responseTaskPhoto.IsSuccessStatusCode)
                {
                    var readTaskAlbum = await responseTaskAlbum.Content.ReadAsStringAsync();
                    var readTaskPhoto = await responseTaskPhoto.Content.ReadAsStringAsync();
                    var readTaskComment = await responseTaskComments.Content.ReadAsStringAsync();
                    albums = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Album>>(readTaskAlbum);
                    List<Photo> photos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Photo>>(readTaskPhoto);
                    List<Comment> comments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comment>>(readTaskComment);
                    photos.ForEach(photo => photo.Comments = comments.Where(comment => photo.Id == comment.PostId).ToList());
                    albums.ForEach(album => album.Photos = photos.Where(photo => photo.AlbumId == album.Id).ToList());
                }
            }
            return View(albums);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
