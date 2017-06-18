﻿using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Model;
using Chakad.Samples.PhoneBook.Queries;

namespace Chakad.Samples.PhoneBook.QueryHandlers
{
    public class GetContactQueryHandler : IWantToHandleThisQuery<GetContactQuery, GetContactQueryResult>
    {
        public IContactRepository ContactRepository => ServiceLocator<IContactRepository>.Resolve();

        public override async Task<GetContactQueryResult> Execute(GetContactQuery message)
        {
            var contact = ContactRepository.Get(message.Id);

            return new GetContactQueryResult
            {
                Entity = new ContactQueryResult
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = contact.Address
                }
            };
        }
    }
}