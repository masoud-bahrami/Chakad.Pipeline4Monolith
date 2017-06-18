﻿using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Commands;
using Chakad.Samples.PhoneBook.Model;

namespace Chakad.Samples.PhoneBook.CommandHandlers
{
    public class UpdateContactHander : IWantToHandleThisRequest<UpdateContact, UpdateContactResult>
    {
        public IContactRepository ContactRepository => ServiceLocator<IContactRepository>.Resolve();
        
        public override async Task<UpdateContactResult> Execute(UpdateContact message)
        {
            
            var contact = new Contact
            {
                FirstName = message.FirstName,
                LastName = message.LastName,
                Address = message.Address,
                Id =message.Id
            };

            ContactRepository.Update(contact);

            return new UpdateContactResult
            {
                Id = contact.Id
            };
        }
    }
}