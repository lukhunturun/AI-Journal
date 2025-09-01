using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JournalAPI.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}