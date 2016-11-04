using Customer.Models;
using Customer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class BCController : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        private 客戶資料Entities dbCustomer = new Models.客戶資料Entities();

        // GET: BC
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult ContactList()
        {
            var data = repo.getNoExistIsDeleted();
            return View(data);
        }

        public ActionResult ContactResult()
        {
            return View();
        }


        public ActionResult BatchUpdate(BatchUpdateViewModel[] items)
        {

            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var Contact = repo.Find(item.Id);
                    
                    Contact.職稱 = item.職稱;
                    Contact.手機 = item.手機;
                    Contact.電話 = item.電話;
                }
                //dbCustomer.SaveChanges();
                try 
                {
                    repo.UnitOfWork.Commit();
                }
                catch (DbEntityValidationException ex )
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


            return RedirectToAction("ContactList");
        }

     
    }
}