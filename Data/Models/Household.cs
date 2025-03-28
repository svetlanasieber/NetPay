using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NetPay.Common.EntityValidationConstants.Household;

namespace NetPay.Data.Models
{
    public class Household
    {
        public Household()
        {
            this.Expenses = new HashSet<Expense>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ContactPersonMaxLength, MinimumLength = ContactPersonMinLength)]
        public string ContactPerson { get; set; } = null!;

        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(PhoneNumberLength)]
        [RegularExpression(PhoneNumberValidationRegex)]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Expense> Expenses { get; set; }
    }
} 