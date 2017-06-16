using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Samples.PhoneBook.Commands
{
    public class CreateContact : ChakadRequest<CreateContactResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }

    public class CreateContactResult : ChakadResult
    {
        public int Id { get; set; }
    }
}
