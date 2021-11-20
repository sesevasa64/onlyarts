using System;
using System.ComponentModel.DataAnnotations;
using onlyarts.Interfaces;

namespace onlyarts.Models
{
    public class Subscription : IEntity
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