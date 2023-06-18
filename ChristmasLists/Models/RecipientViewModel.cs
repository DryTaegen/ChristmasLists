using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristmasLists.Models
{
    public class RecipientViewModel
    {
        public List<Lists>? Lists { get; set; }
        public SelectList? Recipient { get; set; }
        public string? RecipientName { get; set; }
        public string? SearchString { get; set; }
    }
}
