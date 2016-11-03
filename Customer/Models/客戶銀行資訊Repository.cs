using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(q => q.IsDeleted == false);
        }

        public IQueryable<客戶銀行資訊> GetSearchData(string search)
        {
            return this.All().Where(p => p.銀行名稱.Contains(search));
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public 客戶銀行資訊 GetEditList(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}