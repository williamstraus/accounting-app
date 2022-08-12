using System;

namespace DTO
{
    public class EntryDTO
    {
        public Guid Id { get; set; }

        public String DateOfPayment { get; set; }

        public Decimal Sum { get; set; }

        public String Description { get; set; }

        public Boolean? IsExpense { get; set; }

    }
}

