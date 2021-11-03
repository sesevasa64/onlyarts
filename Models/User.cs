using System;
using System.ComponentModel.DataAnnotations;

namespace onlyarts.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname {get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegisDate { get; set;}
        public uint Money {get; set;}
    }
}
