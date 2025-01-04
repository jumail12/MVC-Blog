using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_BlogWebApp.AppDbContext;
using MVC_BlogWebApp.Models.BlogDto;
using MVC_BlogWebApp.Models.BlogModel;

namespace MVC_BlogWebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDb appDb;

        public BlogController(AppDb appDb)
        {
            this.appDb = appDb;
        }

        [HttpGet]
        public async Task<IActionResult> ListBlog()
        {
          var blogs= await appDb.Blogs.Select(a=> new Blog
          {
              bId=a.bId,
              Title = a.Title,
              Author = a.Author,
          }).ToListAsync();

            return View(blogs); 
        }

        //add new blog
        [HttpGet]
        public async Task<IActionResult> AddBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogAction(BlogViewDto blog)
        {
           if(!ModelState.IsValid)
            {
                return View(blog);
            }

            var newBlog = new Blog
            {
                Title = blog.Title,
                Author = blog.Author,
                Content = blog.Content,
            };

            await appDb.Blogs.AddAsync(newBlog);
            await appDb.SaveChangesAsync();

            return RedirectToAction(nameof(ListBlog));
        }


        //view by id
        [HttpGet]
        public async Task<IActionResult> ViewById(Guid id)
        {
            var blog=await appDb.Blogs.SingleOrDefaultAsync(a=>a.bId==id);

            var viewB = new BlogViewDto
            {
                bId = blog.bId,
                Title=blog.Title,
                Author=blog.Author,
                Content=blog.Content,
            };

            return View(viewB);
        }

        //editblog

        [HttpGet]
        public async Task<IActionResult> EditBlog(Guid id)
        {
            var blog= await appDb.Blogs.SingleOrDefaultAsync(b=>b.bId==id);
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> EditConf(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View(blog); // Return with validation errors
            }

            var existingBlog = await appDb.Blogs.SingleOrDefaultAsync(b => b.bId == blog.bId);

            if (existingBlog != null)
            {
                existingBlog.Title = blog.Title;
                existingBlog.Content = blog.Content;
                existingBlog.Author = blog.Author;

                await appDb.SaveChangesAsync(); // Save updated entity
                return RedirectToAction(nameof(ListBlog));
            }

            return NotFound(); // If the blog is not found
        }

        //DeleteBlog
        [HttpGet]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog= await appDb.Blogs.SingleOrDefaultAsync(l=>l.bId==id); 

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteBlogConf(Guid id)
        {
            var exist=await appDb.Blogs.SingleOrDefaultAsync(a=>a.bId==id);
            if(exist == null)
            {
                return NotFound();
            }

             appDb.Blogs.Remove(exist);
            await appDb.SaveChangesAsync(); 

            return RedirectToAction(nameof(ListBlog));
        }




    }
}
