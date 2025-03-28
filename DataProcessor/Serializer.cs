using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NetPay.Data;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ExportDtos;

namespace NetPay.DataProcessor
{
    public class Serializer
    {
        public static string ExportHouseholdsWhichHaveExpensesToPay(NetPayContext context)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(HouseholdExportDto[]), new XmlRootAttribute("Households"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            var households = context.Households
                .Where(h => h.Expenses.Any(e => e.PaymentStatus != PaymentStatus.Paid))
                .Select(h => new
                {
                    h.ContactPerson,
                    h.Email,
                    h.PhoneNumber,
                    UnpaidExpences = h.Expenses
                    .Where(e => e.PaymentStatus != PaymentStatus.Paid)
                        .Select(e => new
                        {
                            e.ExpenseName,
                            e.Amount,
                            e.DueDate,
                            e.Service.ServiceName
                        })
                        .OrderBy(e => e.DueDate)
                        .ToList()
                })
                .OrderBy(h => h.ContactPerson)
                .ToArray();

            var householdsFormatted = households.Select(h => new HouseholdExportDto
            {
                ContactPerson = h.ContactPerson,
                Email = h.Email,
                PhoneNumber = h.PhoneNumber,
                UnpaidExpenses = h.UnpaidExpences.Select(e => new ExpenseExportDto
                {
                    ExpenseName = e.ExpenseName,
                    Amount = e.Amount.ToString("F2"),
                    PaymentDate = e.DueDate.ToString("yyyy-MM-dd"),
                    ServiceName = e.ServiceName
                })
                .OrderBy(e => e.PaymentDate)
                .ThenBy(e => e.Amount)
                .ToList()
            })
            .OrderBy(h => h.ContactPerson)
            .ToArray();

            serializer.Serialize(writer, householdsFormatted, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string ExportAllServicesWithSuppliers(NetPayContext context)
        {
            var servicesWithSuppliers = context.Services
                .Select(s => new
                {
                    ServiceName = s.ServiceName,
                    Suppliers = s.SuppliersServices
                    .Select(ss => new
                    {
                        SupplierName = ss.Supplier.SupplierName
                    })
                    .OrderBy(s => s.SupplierName)
                    .ToList()
                })
                .OrderBy(s => s.ServiceName)
                .ToList();

            return JsonConvert.SerializeObject(servicesWithSuppliers, Formatting.Indented);
        }

        public static string ExportServicesWithSuppliers(NetPayContext context)
        {
            return ExportAllServicesWithSuppliers(context);
        }
    }
}
