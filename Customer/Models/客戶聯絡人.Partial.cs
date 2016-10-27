namespace Customer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
      

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
          
            var db = new Models.客戶資料Entities();
            var ClientData = db.客戶聯絡人.SqlQuery("select * from 客戶聯絡人 where 客戶id= @客戶id", new SqlParameter("@客戶id", this.客戶Id));
            foreach (var item in ClientData)
            {
                if (this.客戶Id == item.客戶Id && this.Email == item.Email)
                {
                    yield return new ValidationResult("Email與其他聯絡人重複", new string[] { "Email" });
                    yield break;
                }
              
            }
        }

      
    }
        

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "「手機」的電話格式必須為0911-xxxxxx")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
