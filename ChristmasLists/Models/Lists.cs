using System;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace ChristmasLists.Models
{
    public class Lists
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Recipient { get; set; }
        [Required]

        [DataType(DataType.Date)]
        public DateTime year { get; set; }
        [Required]
        public string? gift { get; set; }
        [Required]
        public string? giver { get; set; }


    }
}
