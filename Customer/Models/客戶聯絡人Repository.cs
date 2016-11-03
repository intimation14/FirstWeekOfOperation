using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{

      
        public IQueryable<客戶聯絡人> getNoExistIsDeleted()
        {
            return base.All().Where(q => q.IsDeleted == false);
        }

        public IQueryable<客戶聯絡人> GetSearchData(string sType,string search,string sTitle)
        {
            IQueryable<客戶聯絡人> DATA = null;
            if (sType == "1")
            {
                DATA =this.getNoExistIsDeleted().Where(p => p.姓名.Contains(search)).Where(q=>q.職稱.Contains(sTitle));
            }
            else if (sType == "2")
            {
                DATA = this.getNoExistIsDeleted().Where(p => p.姓名.Contains(search));
            }
            else if (sType == "3")
            {
                DATA = this.getNoExistIsDeleted().Where(p => p.職稱.Contains(sTitle));
            }

            return DATA;
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.getNoExistIsDeleted().FirstOrDefault(p => p.Id == id);
        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}