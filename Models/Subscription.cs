using System;
using System.ComponentModel.DataAnnotations;

namespace onlyarts.Models
{
    public class Subscription
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public int Id { get; set; }
        public DateTime EndSubDate { get; set; }
        
        public User SubUser { get; set; }
        public User Author { get; set; }
        public SubType SubType { get; set; }
    }
}