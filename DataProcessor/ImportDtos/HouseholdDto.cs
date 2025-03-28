using NetPay.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace NetPay.DataProcessor.ImportDtos
{
    [XmlType("Household")]
    public class HouseholdDto
    {
        [XmlAttribute("phone")]
        [Required]
        [StringLength(ValidationConstants.PhoneNumberLength, MinimumLength = ValidationConstants.PhoneNumberLength)]
        [RegularExpression(ValidationConstants.PhoneNumberPattern)]
        public string PhoneNumber { get; set; } = null!;

        [XmlElement("ContactPerson")]
        [Required]
        [StringLength(ValidationConstants.HouseholdContactPersonMaxLength, 
            MinimumLength = ValidationConstants.HouseholdContactPersonMinLength)]
        public string ContactPerson { get; set; } = null!;

        [XmlElement("Email")]
        [StringLength(ValidationConstants.EmailMaxLength, MinimumLength = ValidationConstants.EmailMinLength)]
        public string? Email { get; set; }
    }
} 