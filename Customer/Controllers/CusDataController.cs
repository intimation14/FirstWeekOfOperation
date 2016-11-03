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
    public class CusDataController : Controller
    {
        //private 客戶資料Entities dbCustomer  = new 客戶資料Entities();
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();


        // GET: CusData
        public ActionResult Index(string search, string 客戶分類)
        {
            //var client = dbCustomer.客戶資料.ToList();
           
            List<客戶資料> client = null;
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(客戶分類))
            {
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(客戶分類))
                {
                    client = repo.GetSearchData("3",search, 客戶分類).ToList();
                }
                else if (!string.IsNullOrEmpty(search))
                {
                    //client = client.Where(p => p.客戶名稱.Contains(search)).ToList();
                    client = repo.GetSearchData("1",search, 客戶分類).ToList();

                }
                if (!string.IsNullOrEmpty(客戶分類))
                {
                    client = repo.GetSearchData("2", search, 客戶分類).ToList();
                }
            }
            else
            {
                //client = client.Where(q => q.IsDeleted != true).ToList();
                client = repo.getNoExistIsDeleted().ToList();

            }
            var options = (from p in repo.All() select p.客戶分類).Distinct().OrderBy(p => p).ToList();
            ViewBag.客戶分類 = new SelectList(options);

            return View(client);
        }

     

        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(客戶資料 data)
        {
            //dbCustomer.客戶資料.Add(data);

            //dbCustomer.SaveChanges();
            repo.Add(data);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");

            //return View();
        }

     
        public ActionResult Edit(int id)
        {

             //var EditData = dbCustomer.客戶資料.Find(id);
            var EditData = repo.Find(id);
            return View(EditData);
        }

        [HttpPost] //POST
        public ActionResult Edit(客戶資料 data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.UnitOfWork.Context.Entry(data).State = EntityState.Modified;
                    repo.UnitOfWork.Commit();
                    // dbCustomer.Entry(data).State = EntityState.Modified;
                    //dbCustomer.SaveChanges();

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

            return RedirectToAction("Index");

            //return View();
        }

        [HttpGet] //?ID=5
        public ActionResult Delete(int id)
        {
            //var delData = dbCustomer.客戶資料.Find(id);
            var delData = repo.Find(id);

           
            //dbCustomer.客戶銀行資訊.RemoveRange(delData.客戶銀行資訊);
            //dbCustomer.客戶聯絡人.RemoveRange(delData.客戶聯絡人);
            //dbCustomer.客戶資料.Remove(delData);

            delData.IsDeleted = true;

            repo.UnitOfWork.Commit();
            //dbCustomer.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
           // var delData = dbCustomer.客戶資料.Find(id);
           var delData = repo.Find(id);
            return View(delData);
        }

        public ActionResult CustomerCount()
        {
             客戶資料Entities dbCustomer = new 客戶資料Entities();
            var data = dbCustomer.vw_CustomerCount;
            
            return View(data);
        }

    }
}