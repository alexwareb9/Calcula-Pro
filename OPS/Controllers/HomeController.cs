﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
//using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using QuoteTree;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace OnlinePriceSystem.Controllers
{
    public class HomeController : Controller
    {
		private IWebHostEnvironment _hostEnvironment;

		public HomeController(IWebHostEnvironment environment) {
			_hostEnvironment = environment;
		}
		
        public ActionResult Index(int id)
        {
            /*ops_inhouseEntities dc = new ops_inhouseEntities();
			string user = HttpContext.Session.GetString("username");
            if(user == null) return RedirectToAction("Index","Account");
			
			var users = from usr in dc.user_accounts where usr.user == user select usr;
			var store = users.First ().store_id;
			var qry = from prod in dc.products
			          where prod.store_id == store
			          select new ProductUtil {
									id = prod.id,
									name = prod.name,
									created = prod.created.ToString(),
									created_by = prod.created_by,
									modified = prod.modified.ToString(),
									modified_by = prod.modified_by,
									active = prod.active,
									size = prod.size
								};
			ViewBag.store_name = GetStoreName ((Guid)store);
            List<ProductUtil> returnList = qry.ToList();
            PagedList<ProductUtil> pagedlist = new PagedList<ProductUtil>(returnList, 8);
            pagedlist.CurrentPage = id;
            pagedlist.OffSet = 1;*/
            return View();
        }

        
        /*public FileResult Download()
        {
            //get the store name
            ops_inhouseEntities dc = new ops_inhouseEntities();
            //string user = System.Web.HttpContext.Current.User.Identity.Name;
			string user = HttpContext.Session.GetString("username");
            var users = from usr in dc.user_accounts where usr.user == user select usr;
            var store_id = users.First().store_id;
            var store = from str in dc.stores where str.id == store_id select str;
            string store_name = store.First().name;

			string path = _hostEnvironment.WebRootPath;
            if (!Directory.Exists(path + "/App_Data/Downloads/" + store_name + "/MyProducts/"))
                Directory.CreateDirectory(path + "/App_Data/Downloads/"+ store_name + "/MyProducts/");
            else
            {
                //empty the downloads folder
                System.IO.DirectoryInfo myProducts = new DirectoryInfo(path + "/App_Data/Downloads/" + store_name + "/MyProducts");
                foreach (FileInfo file in myProducts.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in myProducts.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            NameValueCollection keys = new NameValueCollection();
			keys = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.ToString());
            foreach (var key in keys.AllKeys)
            {
                if (key != "_")
                {
                    //string value = keys[key.ToString()];
					string value = path + "/Products/" + store_name + "/" + key;
                    try
                    {
                        //fz.CreateZip(key, value, true, "");
                        CopyFolder(new DirectoryInfo(value), new DirectoryInfo(path + "/App_Data/Downloads/" + store_name + "/MyProducts/" + key));
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                }               
            }
            FastZip fz = new FastZip();
            fz.CreateZip(path + "/App_Data/Downloads/" + store_name + "/MyProducts.zip", path + "/App_Data/Downloads/" + store_name + "/MyProducts", true, "");

            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "/App_Data/Downloads/" + store_name + "/" + "MyProducts.zip");
            string fileName = "MyProducts.zip";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }*/

		// This action handles the form POST and the upload
		[HttpPost]
		
		/*public ActionResult Upload()
		{
			int id = 0;
			var file = Request.Form.Files[0];
			// Verify that the user selected a file
			if (file != null && file.Length > 0) 
			{
				//get the store name
                ops_inhouseEntities dc = new ops_inhouseEntities();
				string user = HttpContext.Session.GetString("username");
				var users = from usr in dc.user_accounts where usr.user == user select usr;
				var store_id = users.First ().store_id;
				var store = from str in dc.stores where str.id == store_id select str;
				string store_name = store.First ().name;
				// extract only the fielname
				//var fileName = Path.GetFileName(file.FileName);
				// store the file inside ~/App_Data/uploads folder
				string pathRoot = _hostEnvironment.WebRootPath;
				if (!Directory.Exists(pathRoot + "/App_Data/Uploads/" + store_name))
					Directory.CreateDirectory(pathRoot + "/App_Data/Uploads/" + store_name);
				var path = pathRoot + "/App_Data/Uploads/" + store_name + "/" + file.FileName;
				//file.SaveAs(path);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					file.CopyTo(stream);
				}

				Extract(path);
                ExtractToProducts(path);
				QTree tree = new QTree (pathRoot + "/Products/" + store_name + "/" + file.FileName.Split('.')[0], true);
				//Save to database
				var qry = from prod in dc.products where (prod.name == tree.Root.Name && prod.store_id == store_id) select prod;
				if (qry.Count() > 0)
				{
					var item = qry.Single();
					item.name = tree.Root.Name;
					MemoryStream stream = tree.Serialize ();
					item.product = stream.ToArray();
					item.modified_by = user;
					item.modified = DateTime.Now;
					item.size = (int)stream.Length;
                    string xml = tree.SerializeToString();
                    item.product_xml = xml;
					dc.SaveChanges();
				}
				else
				{
					product1 product = new product1();
					MemoryStream stream = tree.Serialize ();
					product.product = stream.ToArray();
					product.name = tree.Root.Name;
					product.created_by = user;
					product.modified_by = user;
					product.created = DateTime.Now;
					product.modified = DateTime.Now;
					product.store_id = store_id;
                    product.active = false;
					product.size = (int)stream.Length;
                    string xml = tree.SerializeToString();
                    product.product_xml = xml;
					dc.products.Add(product);
					dc.SaveChanges();
				}
				id = qry.First ().id;

				var toJson = JsonConvert.SerializeObject(tree, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.All,
					PreserveReferencesHandling = PreserveReferencesHandling.All,
					Formatting = Formatting.Indented
				});    
				HttpContext.Session.SetString("tree", toJson);

				HttpContext.Session.SetString("test", "true");
				return RedirectToAction("Edit","TreeView", new { product = id });        
			}

			return RedirectToAction("Index","MyProducts");        
		}*/

        
		/*private void Extract(string path){
			//string startPath = @"c:\example\start";
			//string zipPath = @"c:\example\result.zip";
			//get the store name
            ops_inhouseEntities dc = new ops_inhouseEntities();
			string user = HttpContext.Session.GetString("username");
			var users = from usr in dc.user_accounts where usr.user == user select usr;
			var store_id = users.First ().store_id;
			var store = from str in dc.stores where str.id == store_id select str;
			string store_name = store.First ().name;

			string pathRoot = _hostEnvironment.WebRootPath;
			string extractPath = pathRoot + "/App_Data/Uploads/Products/" + store_name;

			FastZip fz = new FastZip();
			//fz.ExtractZip(path, extractPath, null);
			System.IO.Compression.ZipFile.ExtractToDirectory(path, extractPath);
		}*/

        
		/*private void ExtractToProducts(string path){
			//string startPath = @"c:\example\start";
			//string zipPath = @"c:\example\result.zip";
			//get the store name
            ops_inhouseEntities dc = new ops_inhouseEntities();
			string user = HttpContext.Session.GetString("username");
			var users = from usr in dc.user_accounts where usr.user == user select usr;
			var store_id = users.First ().store_id;
			var store = from str in dc.stores where str.id == store_id select str;
			string store_name = store.First ().name;

			string pathRoot = _hostEnvironment.WebRootPath;		
			string extractPath = pathRoot + "/Products/" + store_name;

			FastZip fz = new FastZip();
			//fz.ExtractZip(path, extractPath, null);
			System.IO.Compression.ZipFile.ExtractToDirectory(path, extractPath);
		}*/

        
		/*public string GetStoreName(Guid id)
		{
            ops_inhouseEntities dc = new ops_inhouseEntities();
			var store = from str in dc.stores where str.id == id select str;
			string store_name = store.First ().name;
			return store_name;
		}*/

        //very cool method I found online
        public static void CopyFolder(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFolder(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }
    }

	public class ProductUtil
	{
		public int id;
		public string name;
		public string created;
		public string created_by;
		public string modified;
		public string modified_by;
		public bool? active;
		public int? size;

		public ProductUtil()
		{
		}
	}
}
