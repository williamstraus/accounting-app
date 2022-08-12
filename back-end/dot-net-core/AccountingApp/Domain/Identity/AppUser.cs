using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public override Guid Id { get; set; }

        public String FullName { get; set; }

        public ICollection<Entry> Entries { get; set; }
    }
}

