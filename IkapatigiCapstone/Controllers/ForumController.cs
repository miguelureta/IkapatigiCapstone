using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;



namespace IkapatigiCapstone.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _hcontext;
        private readonly IWebHostEnvironment _webhost;
        public ForumController(ApplicationDbContext context, IHttpContextAccessor icontext, IWebHostEnvironment webhost)
        {
            _context = context;
            _hcontext = icontext;
            _webhost = webhost;
        }
        //public void OnGet()
        //{
        //    if(string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
        //    {

        //    }
        //    var targetForum = HttpContext.Session.GetString(ForumIDKey);
        //}
        // GET: ForumController
        public ActionResult Index()
        {
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("forumsmodlogged") || _hcontext.HttpContext.Session.GetString("Session").Equals("adminlogged"))
            {
                var list = _context.Forums.ToList();
                return View(list);
            }
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }
            //return RedirectToAction("aLogin", "Account");
        }

        // GET: ForumController/Details/5
        [Route("Details")]
        public ActionResult Details(int id)
        {   
            //if (id == null)
            //{
            //    return RedirectToAction("Index");
            //}
            var pvm = new PostViewModel();
            pvm.Posts = _context.Posts.Where(p => p.ForumId == id).ToList();
            pvm._Forum = _context.Forums.Where(f => f.ForumId == id).FirstOrDefault();
            
            _hcontext.HttpContext.Session.SetInt32("ForumTarget", id);
            if (pvm == null)
            {
                return RedirectToAction("Index");
            }
            return View("PostView",pvm);
        }

        // GET: ForumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForumController/Create
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Forum _forum/*, User _user*/)
        {
            try
            {
                var forum = new Forum();

                forum.Title = _forum.Title;
                forum.Description = _forum.Description;
                //forum.ImageUrl = _forum.ImageUrl;
                forum.Created = DateTime.Now;

                _context.Forums.Add(forum);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


            }
           
        }

        // GET: ForumController/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var forum = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();
            if (forum == null)
            {
                return RedirectToAction("Index");
            }

            return View(forum);
        }

        // POST: ForumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Forum post)
        {
            try
            {
                var newp = _context.Forums.Where(f => f.ForumId == id).SingleOrDefault();
                newp.Title = post.Title;
                newp.Description = post.Description;
                newp.ImageUrl = post.ImageUrl;
                _context.Forums.Update(newp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


            }
           
            //try
            //{
            //    _context.Forums.Update(newp);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: ForumController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ForumController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Forum");
            }

            var post = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();
            if (post == null)
            {
                return RedirectToAction("Index", "Forum");
            }

            _context.Forums.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Forum");
        }

        public IActionResult CreatePost()
        {
            //var post = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();
            //if (id == null)
            //{
            //    return RedirectToAction("Index");
            //}


            //if (post == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //Post post = new Post();
            //return PartialView("CreatePost", post);
            return View("CreatePost");
        }

        [HttpPost]
        public IActionResult CreatePost(/*CreatePostModel post*/ CreatePostImageModel post)
        {
            
            //Old CreatePost method
            //var inPost = new Post();
            //inPost.Title = post.Title;
            //inPost.Content = post.Content;
            //inPost.Created = DateTime.Now;

            //inPost.ForumId = _hcontext.HttpContext.Session.GetInt32("ForumTarget");

            //New CreatePost method with Image
            
            //if (post.PdImage!=null)
            //{
            //    string filePath = null;
            //    string fileName = null;
            //    string uploadFolder = Path.Combine(_webhost.WebRootPath, "images");
            //    fileName = Guid.NewGuid().ToString() + "_" + post.PdImage.FileName;
            //    filePath = Path.Combine(uploadFolder, fileName);
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await post.PdImage.CopyToAsync(fileStream);
            //        fileStream.CopyTo(fileStream);

            //        fileStream.Close();
            //    }
            //    var postImage = new PostImage()
            //    {
            //        PostID = inPost.PostId,
            //        ImageName = fileName,
            //        UserID = _hcontext.HttpContext.Session.GetInt32("logMemberID")
            //    };
            //    _context.PostImages.Add(postImage);
            //    _context.SaveChanges();
            //}
            
            
            
            
            //inPost.ForumId = _hcontext.HttpContext.Session.GetInt32("ForumTarget");
            
            //var inputPost = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();


            //if(id==null)
            //{
            //    return View("Index");
            //}
            try {
                var inPost = new Post();
                inPost.Title = post.Title;
                inPost.Content = post.Content;
                inPost.Created = DateTime.Now;
                inPost.ForumId = _hcontext.HttpContext.Session.GetInt32("ForumTarget");

                _context.Posts.Add(inPost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");
            }
        }

        public IActionResult ViewReplies(int id)
        {
            var replyModel = new PostReplyViewCreateModel();
            replyModel.postReplies = _context.PostReplies.Where(p => p.PostId == id).ToList();
            replyModel._Post = _context.Posts.Where(i => i.PostId == id).SingleOrDefault();
            _hcontext.HttpContext.Session.SetInt32("PostTarget", id);
            return View(replyModel);
        }

        //public IActionResult CreateReply(PostReplyViewCreateModel rep, int id)
        //{
        //    _hcontext.HttpContext.Session.SetInt32("PostTarget", id);
        //    var prm = new PostReplyViewCreateModel();
        //    prm.PostID = _hcontext.HttpContext.Session.GetInt32("PostTarget");
        //    prm.postReplies = _context.PostReplies.Where(p => p.PostId == id).ToList();

        //    return View(prm);
        //}
        

        //[HttpPost]
        //public ActionResult CreateReply(CreatePostReplyModel crm)
        //{
        //    var reply = new PostReply();
        //    reply.Content = crm.Content;
        //    reply.Created = DateTime.Now;
        //    reply.PostId = _hcontext.HttpContext.Session.GetInt32("PostTarget");

        //    _context.PostReplies.Add(reply);
        //    _context.SaveChanges();
        //    return RedirectToAction("CreateReply");
        //}

        public IActionResult CreateReply(PostReplyViewCreateModel rep)
        {
            var prm = new PostReplyViewCreateModel();
            prm.PostID = _hcontext.HttpContext.Session.GetInt32("PostTarget");
            prm.postReplies = _context.PostReplies.Where(p => p.PostId == _hcontext.HttpContext.Session.GetInt32("PostTarget")).ToList();
            return View("CreateReply");
        }

        [HttpPost]
        public ActionResult CreateReply(CreatePostReplyModel reply/*, int? id*/)
        {
            //var inputPost = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();


            //if(id==null)
            //{
            //    return View("Index");
            //}

            try
            {
                var inReply = new PostReply();
                inReply.Content = reply.Content;
                inReply.Created = DateTime.Now;
                inReply.PostId = _hcontext.HttpContext.Session.GetInt32("PostTarget");

                _context.PostReplies.Add(inReply);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


            }
         
        }



        //THIS IS THE MemberForums

        public ActionResult MemberIndex()
        {
            var list = _context.Forums.ToList();
            return View(list);
        }

        // GET: ForumController/Details/5
        [Route("MemberPostView")]
        public ActionResult MemberPostView(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MemberIndex");
            }
            var pvm = new PostViewModel();
            pvm.Posts = _context.Posts.Where(p => p.ForumId == id).ToList();
            pvm._Forum = _context.Forums.Where(f => f.ForumId == id).FirstOrDefault();
            _hcontext.HttpContext.Session.SetInt32("ForumTarget", (int)id);
            if (pvm == null)
            {
                return RedirectToAction("MemberIndex");
            }
            return View("MemberPostView", pvm);
        }


        public IActionResult MemberCreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberCreatePost(CreatePostModel post/*, int? id*/)
        {
            //var inputPost = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();


            //if(id==null)
            //{
            //    return View("Index");
            //}
            var inPost = new Post();
            inPost.Title = post.Title;
            inPost.Content = post.Content;
            inPost.Created = DateTime.Now;
            inPost.ForumId = _hcontext.HttpContext.Session.GetInt32("ForumTarget");

            _context.Posts.Add(inPost);
            _context.SaveChanges();
            return RedirectToAction("MemberIndex");
        }

        public IActionResult MemberViewReplies(int id)
        {
            var replyModel = new PostReplyViewCreateModel();
            replyModel.postReplies = _context.PostReplies.Where(p => p.PostId == id).ToList();
            replyModel._Post = _context.Posts.Where(i => i.PostId == id).SingleOrDefault();
            _hcontext.HttpContext.Session.SetInt32("PostTarget", id);
            return View("MemberViewReplies",replyModel);
        }

        public IActionResult MemberCreateImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MemberCreateImage(PostReplyViewImageCreateModel img)
        {
            string filePath = null;
            string fileName = null;

            string uploadFolder = Path.Combine(_webhost.WebRootPath, "images");
            fileName = Guid.NewGuid().ToString() + "_" + img.postImage.FileName;
            filePath = Path.Combine(uploadFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await img.postImage.CopyToAsync(fileStream);
                fileStream.CopyTo(fileStream);

                fileStream.Close();
            }
            var newImg = new PostImage
            {
                ImageName = fileName,
                UserID = _hcontext.HttpContext.Session.GetInt32("logMemberID")
            };
            _context.PostImages.Add(newImg);
            _context.SaveChanges();

            return RedirectToAction("MemberIndex");

        }
        //[HttpPost]
        //public ActionResult CreateReply(CreatePostReplyModel crm)
        //{
        //    var reply = new PostReply();
        //    reply.Content = crm.Content;
        //    reply.Created = DateTime.Now;
        //    reply.PostId = _hcontext.HttpContext.Session.GetInt32("PostTarget");

        //    _context.PostReplies.Add(reply);
        //    _context.SaveChanges();
        //    return RedirectToAction("CreateReply");
        //}

        public IActionResult MemberCreateReply(PostReplyViewCreateModel rep)
        {
            var prm = new PostReplyViewCreateModel();
            prm.PostID = _hcontext.HttpContext.Session.GetInt32("PostTarget");
            prm.postReplies = _context.PostReplies.Where(p => p.PostId == _hcontext.HttpContext.Session.GetInt32("PostTarget")).ToList();
            return View("MemberCreateReply");
        }

        [HttpPost]
        public ActionResult MemberCreateReply(CreatePostReplyModel reply/*, int? id*/)
        {
            //var inputPost = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();


            //if(id==null)
            //{
            //    return View("Index");
            //}
            var inReply = new PostReply();
            inReply.Content = reply.Content;
            inReply.Created = DateTime.Now;
            inReply.PostId = _hcontext.HttpContext.Session.GetInt32("PostTarget");

            _context.PostReplies.Add(inReply);
            _context.SaveChanges();
            return RedirectToAction("MemberIndex");
        }

        public ActionResult ViewImage(int id)
        {
            var postImg = _context.PostImages.ToList();

            return View(postImg);
        }
    }
}
