using System;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Samples.PhoneBook.Commands
{
    public class DeleteContact : ChakadRequest<ChakadResult>
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
