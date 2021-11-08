using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            
            logger.Info("Program started");
            string choice,choice2, choice3;
            var db= new BloggingContext();;

             try
            {
                do
            {
                Console.WriteLine("1) Display Blogs.");
                Console.WriteLine("2) Add Blog.");
                Console.WriteLine("3) Create Post.");
                Console.WriteLine("4) Display Posts.");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                var query = db.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                }
                else if(choice=="2")
                {
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                 db = new BloggingContext();
                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);
                }
                else if(choice=="3")
                {
                Console.WriteLine("Write the blog you are posting to.");
                choice2 = Console.ReadLine();

                var query = db.Blogs.OrderBy(b => b.Name);

                foreach (var item in query)
                    {
                        //Console.WriteLine(item.Name);
                        if(item.Name == choice2){
                            Console.Write("Enter new Post Title: ");
                            var title = Console.ReadLine();
                            Console.Write("Enter new Post Content: ");
                            var content = Console.ReadLine();

                            var post = new Post {BlogId = item.BlogId, Title = title, Content=content };

                            db = new BloggingContext();
                            db.AddPost(post);
                            logger.Info("Post added - {title}", title);
                        }
                    }

                }
                else if(choice=="4")
                {
                Console.WriteLine("Write the blog you want.");
                choice3 = Console.ReadLine();

                var query = db.Blogs.OrderBy(b => b.Name);


                foreach (var item in query)
                    {
                        if(item.Name == choice3){

                            var query2 = db.Posts.OrderBy(b => b.Title);

                            foreach (var item2 in query2)
                                {
                                Console.WriteLine(item.Name+"/"+item2.Title+"/"+item2.Content);
                                }
                        }
                    }
                }
                
                } while (choice == "1" || choice == "2" || choice =="3" || choice=="4");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
