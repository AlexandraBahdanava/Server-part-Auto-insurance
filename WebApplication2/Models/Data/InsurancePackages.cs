using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Data
{
    public class InsurancePackages
    {
        [Key]
        public int Id { get; set; }

        public int price { get; set; }

        public DateTime duration { get; set; }

        public string carType { get; set; }

        public string description { get; set; }

        // Предполагаем, что BankId - это внешний ключ
        [ForeignKey("BankId")]
        public int BankId { get; set; }

        public Bank Bank { get; set; }
    }
}
