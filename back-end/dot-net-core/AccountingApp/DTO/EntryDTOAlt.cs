using System;

namespace DTO
{
    public class EntryDTOAlt
    {
        public Guid Id { get; set; }

        public DateTime DateOfPayment { get; set; }

        public Decimal Sum { get; set; }

        public String Description { get; set; }

        public Boolean? IsExpense { get; set; }

    }
}

