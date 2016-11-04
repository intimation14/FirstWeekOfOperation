using Customer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class ContactController : Controller
    {
        //private 客戶資料Entities dbCustomer = new Models.客戶資料Entities();
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository repo1 = RepositoryHelper.Get客戶資料Repository();

        // GET: Contact
        public ActionResult Index(string search,string 職稱 )
        {
            //var data = dbCustomer.客戶聯絡人.ToList();
            List<客戶聯絡人> data = null;

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(職稱))
            {
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(職稱))
                {

                    data = repo.GetSearchData("1", search, 職稱).ToList();
                }
                else if (!string.IsNullOrEmpty(search))
                {

                    data = repo.GetSearchData("2", search, 職稱).ToList();

                }
                else if (!string.IsNullOrEmpty(職稱))
                {

                    data = repo.GetSearchData("3", search, 職稱).ToList();
                }

                //data = data.Where(q => q.姓名.Contains(search)).ToList();
            }
            //data = data.Where(c => c.IsDeleted != true).ToList();
            data = repo.All().ToList();

            var options = (from p in repo.All() select p.職稱).Distinct().OrderBy(p => p).ToList();
            ViewBag.職稱 = new SelectList(options);

            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo1.All(), "id", "客戶名稱");
            //ViewBag.客戶Id = new SelectList(dbCustomer.客戶資料, "id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 data)
        {
            ViewBag.客戶id = new SelectList(repo1.All(), "id", "客戶名稱");
           // ViewBag.客戶Id = new SelectList(dbCustomer.客戶資料, "id", "客戶名稱");
            if (ModelState.IsValid)
            {
                //dbCustomer.Entry(data).State = EntityState.Added;
                repo.UnitOfWork.Context.Entry(data).State = EntityState.Added;
                try
                {
                    //dbCustomer.SaveChanges();
                    repo.UnitOfWork.Commit();
                }
                catch (DbEntityValidationException ex) //Entity Framework 發生驗證例外時的處裡方法
                {
                    throw ex;
                    //foreach (var entityErrors in ex.InnerException)
                    //{
                    //    foreach (var item in entityErrors.ValidationErrors)
                    //    {
                    //        //
                    //        throw new DbEntityValidationException(item.PropertyName + "發生錯誤" + item.PropertyName);
                    //    }

                    //}

                }
               
                return RedirectToAction("Index");
            }
            else {
                return View();
            }
          
            
        }

        public ActionResult Edit(int id)
        {
            ViewBag.客戶id = new SelectList(repo1.All(), "id", "客戶名稱");
            var EditData = repo.Find(id);
            //var EditData = dbCustomer.客戶聯絡人.Find(id);
            return View(EditData);
        }


        [HttpPost]
        public ActionResult Edit(客戶聯絡人 data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // dbCustomer.Entry(data).State = EntityState.Modified;
                    // dbCustomer.SaveChanges();
                    repo.UnitOfWork.Context.Entry(data).State = EntityState.Modified;
                    repo.UnitOfWork.Commit();
                }

                //return View();
            }
            catch (DbEntityValidationException ex) //Entity Framework 發生驗證例外時的處裡方法
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var item in entityErrors.ValidationErrors)
                    {
                        //
                        throw new DbEntityValidationException(item.PropertyName + "發生錯誤" + item.PropertyName);
                    }

                }

            }
            ViewBag.客戶id = new SelectList(repo1.All(), "id", "客戶名稱");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id )
        {
            //var delData = dbCustomer.客戶聯絡人.Find(id);
            var delData = repo.Find(id);

            // dbCustomer.客戶聯絡人.Remove(delData);
            delData.IsDeleted = true;

            //dbCustomer.SaveChanges();
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
           
        }

        public ActionResult Details(int id)
        {
            //var delData = dbCustomer.客戶聯絡人.Find(id);
            var delData = repo.Find(id);
            return View(delData);
        }

    }
}