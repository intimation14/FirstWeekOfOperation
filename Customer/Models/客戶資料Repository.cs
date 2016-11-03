using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
       

        public IQueryable<客戶資料> getNoExistIsDeleted()
        {
            return base.All().Where(q => q.IsDeleted == false);
        }

        public IQueryable<客戶資料> GetSearchData(string type ,string search, string sType)
        {
            IQueryable<客戶資料> orders=null;
            if (type == "1")
            {
                orders=  this.getNoExistIsDeleted().Where(p => p.客戶名稱.Contains(search));
            }
            else if (type == "2")
            {
                orders = this.getNoExistIsDeleted().Where(q => q.客戶分類.Contains(sType));
            }
            else if (type == "3")
            {
                orders = this.getNoExistIsDeleted().Where(p => p.客戶名稱.Contains(search)).Where(q => q.客戶分類.Contains(sType));
            }

            return orders;

        }

        public 客戶資料 Find(int id)
        {
            return this.getNoExistIsDeleted().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶資料> getDdl客戶分類(string Type)
        {
            return this.getNoExistIsDeleted().Where(p => p.客戶分類.Contains(Type));
           
            
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}