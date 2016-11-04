using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Customer.Models.ViewModels
{
    public class BatchUpdateViewModel
    {
        //[Required]
        public int Id { get; set; }

        public int 客戶Id { get; set; }

        //[StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        //[Required]
        public string 職稱 { get; set; }

        //[StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        //[RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "「手機」的電話格式必須為0911-xxxxxx")]
        public string 手機 { get; set; }

        //[StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public string Email { get; set; }

        public string 姓名 { get; set; }
    }
}