using System;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Samples.PhoneBook.Commands
{
    public class UpdateContact : ChakadRequest<UpdateContactResult>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }

    public class UpdateContactResult : ChakadResult
    {
        public int Id { get; set; }
    }
}
