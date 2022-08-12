using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Entry
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public DateTime DeletedAt { get; set; }

        [Required(ErrorMessage = "DateOfPayment is required.")]
        [DataType(DataType.Date,ErrorMessage = "DateOfPayment has to follow the format 'date'.")]
        public DateTime DateOfPayment { get; set; }

        [Required(ErrorMessage = "Sum is required.")]
        [Range(0,10000000,ErrorMessage = "Sum has to be between 0 and 10 000 000 (ten million).")]
        public Decimal Sum { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(1,ErrorMessage = "Too short.")]
        [MaxLength(1000,ErrorMessage = "Too long.")]
        public String Description { get; set; }

        [Required(ErrorMessage = "IsExpense is required.")]
        public Boolean IsExpense { get; set; }

        public Guid AppUserId { get; set; }

        public AppUser? AppUser { get; set; }

    }
}

