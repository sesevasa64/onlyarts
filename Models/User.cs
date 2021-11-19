using System;
using System.ComponentModel.DataAnnotations;

namespace onlyarts.Models
{
      public class User
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public  int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname {get; set; }
        public string Email {get; set; }
        public string LinkToAvatar {get; set; }     //ссылка на аву
        public string Info {get; set; } 
        public DateTime RegisDate { get; set; }
        public decimal Money { get; set; }
    }
}