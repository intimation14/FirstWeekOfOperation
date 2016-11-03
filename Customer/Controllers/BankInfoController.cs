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
    public class BankInfoController : Controller
    {
       // private 客戶資料Entities dbCustomer = new 客戶資料Entities();
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository repo1 = RepositoryHelper.Get客戶資料Repository();
        


        // GET: BankInfo
        public ActionResult Index(string search)
        {
            // var data = dbCustomer.客戶銀行資訊.ToList();
            List<客戶銀行資訊> data = null;

            if (!string.IsNullOrEmpty(search))
            {
                //data = data.Where(a => a.銀行名稱.Contains(search)).ToList();
                data = repo.GetSearchData(search).ToList();
            }

            //data= data.Where(c => c.IsDeleted != true).ToList();
            data = repo.All().ToList();

            return View(data);

            //var bankinfo = dbCustomer.客戶銀行資訊.Include(CustData => dbCustomer.客戶資料);
            //if (!string.IsNullOrEmpty(search)) {
            //    bankinfo = bankinfo.Where(b => b.銀行名稱.Contains(search));
            //}
            //bankinfo = bankinfo.Where(c => c.IsDeleted != true);
            
            //return View(bankinfo);


        }

        public ActionResult Create()
        {
            ViewBag.客戶id = new SelectList(repo1.All(), "id", "客戶名稱" );
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 data)
        {

            if (ModelState.IsValid)
            {
                //dbCustomer.Entry(data).State = EntityState.Added;
                //dbCustomer.客戶銀行資訊.Add(data);
                repo.UnitOfWork.Context.Entry(data).State = EntityState.Added;
                repo.Add(data);

                try
                {
                    //dbCustomer.SaveChanges();
                    repo.UnitOfWork.Commit();
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


            }
           // ViewBag.客戶id = new SelectList(dbCustomer.客戶資料,  "id", "客戶名稱" );
            return RedirectToAction("Index");
          
        }

        public ActionResult Edit(int id)
        {
            ViewBag.客戶id = new SelectList(repo1.All(), "id", "客戶名稱");
            // var EditData = dbCustomer.客戶銀行資訊.Find(id);
            var EditData = repo.Find(id);

            return View(EditData);
        }

        [HttpPost] //POST
        public ActionResult Edit(客戶銀行資訊 data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //dbCustomer.Entry(data).State = EntityState.Modified;
                    //dbCustomer.SaveChanges();
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

            //return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            //var delData = dbCustomer.客戶銀行資訊.Find(id);
            var delData = repo.Find(id);

            //dbCustomer.客戶銀行資訊.Remove(delData);
            delData.IsDeleted = true;
            repo.UnitOfWork.Commit();
            //dbCustomer.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            //var delData = dbCustomer.客戶銀行資訊.Find(id);
            var delData = repo.Find(id);
            return View(delData);
        }


    }
}

