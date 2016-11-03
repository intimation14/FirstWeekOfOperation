using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{

        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(q => q.IsDeleted == false);
        }

        public IQueryable<客戶聯絡人> GetSearchData(string search)
        {
            return this.All().Where(p => p.姓名.Contains(search));
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}