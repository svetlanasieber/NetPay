using System.Xml.Serialization;
using System.Collections.Generic;

namespace NetPay.DataProcessor.ExportDtos
{
    [XmlType("Household")]
    public class HouseholdExportDto
    {
        [XmlElement("ContactPerson")]
        public string ContactPerson { get; set; } = null!;

        [XmlElement("Email")]
        public string? Email { get; set; }

        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; } = null!;

        [XmlArray("Expenses")]
        [XmlArrayItem("Expense")]
        public virtual List<ExpenseExportDto> UnpaidExpenses { get; set; } = new List<ExpenseExportDto>();
    }
} 