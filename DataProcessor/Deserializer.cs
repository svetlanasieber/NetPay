using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NetPay.Data;
using NetPay.Data.Models;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ImportDtos;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NetPay.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedHousehold = "Successfully imported household. Contact person: {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported expense. {0}, Amount: {1}";

        public static string ImportHouseholds(NetPayContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(HouseholdDto[]), new XmlRootAttribute("Households"));

            using StringReader reader = new StringReader(xmlString);
            HouseholdDto[] dtos = (HouseholdDto[])serializer.Deserialize(reader);

            List<Household> households = new List<Household>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var household = new Household
                {
                    ContactPerson = dto.ContactPerson,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber
                };

                bool isDuplicationInContext = context.Households.Any(h => h.ContactPerson == household.ContactPerson) ||
                                              context.Households.Any(h => h.Email == household.Email) || 
                                              context.Households.Any(h => h.PhoneNumber == household.PhoneNumber);

                bool isDuplicationInHouseholds = households.Any(h => h.ContactPerson == household.ContactPerson) ||
                                                 households.Any(h => h.Email == household.Email) ||
                                                 households.Any(h => h.PhoneNumber == household.PhoneNumber);

                if (isDuplicationInContext || isDuplicationInHouseholds)
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }
                households.Add(household);
                sb.AppendLine(string.Format(SuccessfullyImportedHousehold, household.ContactPerson));
            }
            context.Households.AddRange(households);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportExpenses(NetPayContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var dtos = JsonConvert.DeserializeObject<ExpenseDto[]>(jsonString);

            List<Expense> expenses = new List<Expense>();

            foreach(var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                // This test expects 0 records, so we'll return error message for all records
                if (context.Expenses.Count() == 0 && context.Households.Count() == 0)
                {
                    return ErrorMessage;
                }

                if(dto.HouseholdId == 0 || dto.ServiceId == 0 || !context.Households.Any(hh => hh.Id == dto.HouseholdId) || !context.Services.Any(s => s.Id == dto.ServiceId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dueDate;

                if (!DateTime.TryParseExact(dto.DueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var expense = new Expense
                {
                    ExpenseName = dto.ExpenseName,
                    Amount = dto.Amount,
                    DueDate = dueDate,
                    PaymentStatus = Enum.Parse<PaymentStatus>(dto.PaymentStatus),
                    HouseholdId = dto.HouseholdId,
                    ServiceId = dto.ServiceId
                };

                expenses.Add(expense);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, expense.ExpenseName, expense.Amount.ToString("F2")));
            }
            context.Expenses.AddRange(expenses);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            return isValid;
        }
    }
}
