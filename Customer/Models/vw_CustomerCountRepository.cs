using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class vw_CustomerCountRepository : EFRepository<vw_CustomerCount>, Ivw_CustomerCountRepository
	{

	}

	public  interface Ivw_CustomerCountRepository : IRepository<vw_CustomerCount>
	{

	}
}