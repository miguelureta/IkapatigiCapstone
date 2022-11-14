﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
namespace IkapatigiCapstone.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _hcontext;

        public ForumController(ApplicationDbContext context, IHttpContextAccessor icontext)
        {
            _context = context;
            _hcontext = icontext;
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
            var list = _context.Forums.ToList();
            return View(list);
        }

        // GET: ForumController/Details/5
        [Route("Details")]
        public ActionResult Details(int id)
        {   
            if (id == null)
            {
                return RedirectToAction("Index");
            }
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
            var forum = new Forum();
            
            forum.Title = _forum.Title;
            forum.Description = _forum.Description;
            forum.ImageUrl = _forum.ImageUrl;
            forum.Created = DateTime.Now;
            
            _context.Forums.Add(forum);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
            
        }

        // GET: ForumController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
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
            var newp = _context.Forums.Where(f => f.ForumId == id).SingleOrDefault();
            newp.Title = post.Title;
            newp.Description = post.Description;
            newp.ImageUrl = post.ImageUrl;
            _context.Forums.Update(newp);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
        public ActionResult CreatePost(CreatePostModel post/*, int? id*/)
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
            return RedirectToAction("Index");
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
            var inReply = new PostReply();
            inReply.Content = reply.Content;
            inReply.Created = DateTime.Now;
            inReply.PostId = _hcontext.HttpContext.Session.GetInt32("PostTarget");

            _context.PostReplies.Add(inReply);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }












        //THIS IS THE MemberForums

        public ActionResult MemberIndex()
        {
            var list = _context.Forums.ToList();
            return View(list);
        }

        // GET: ForumController/Details/5
        [Route("MemberPostView")]
        public ActionResult MemberPostView(int id)
        {
            if (id == null)
            {
                return RedirectToAction("MemberIndex");
            }
            var pvm = new PostViewModel();
            pvm.Posts = _context.Posts.Where(p => p.ForumId == id).ToList();
            pvm._Forum = _context.Forums.Where(f => f.ForumId == id).FirstOrDefault();
            _hcontext.HttpContext.Session.SetInt32("ForumTarget", id);
            if (pvm == null)
            {
                return RedirectToAction("MemberIndex");
            }
            return View("MemberPostView", pvm);
        }


       
        public IActionResult MemberCreatePost()
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

    }
}
