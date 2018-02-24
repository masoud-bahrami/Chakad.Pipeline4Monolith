using System;
using Chakad.Core;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.Options;

namespace Chakad.Samples.PhoneBook.Commands
{
    public class CreateContact : ChakadRequest<CreateContactResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }

    public class CreateContactWithChakadMessagProperty : ChakadRequest<CreateContactResult>
    {
        public CreateContactWithChakadMessagProperty(string firstName,
            string lastName,
            string address)
        {
            FirstName = ChakadMessageProperty<string>.Build(new ChakadMessagePropertyConfig<string>(
                ChakadFieldType.String, firstName, "نام", true));

            LastName = ChakadMessageProperty<string>.Build(new ChakadMessagePropertyConfig<string>(
                ChakadFieldType.String,  lastName,"نام خانوادگی", true)
            {
                ValiationPolicy = o =>
                {
                    if (!Guard.AgainstString(o))
                        return new ValidationResultViewModel(false, "فیلد نام خانوادگی نمی تواند خالی باشد");

                    if (o.ToString().Length < 4)
                        return new ValidationResultViewModel(false, "طول نام خانوادگی باید بیشتر از چهار حرف باشد");

                    return new ValidationResultViewModel(true, "");
                }
            });


            Address =address;

            Address6 = 9;
        }
        public ChakadMessageProperty<string> FirstName { get; set; }
        public ChakadMessageProperty<string> LastName { get; set; }
        [ChakadRequired("آدرس")]
        public string Address { get; set; }
        [ChakadMaxLengthAttribute(4,"آدرس1")]
        public string Address1 { get; set; }
        [ChakadStringLength(5,"آدرس2")]
        public string Address2 { get; set; }
        [ChakadStringRequiredAttribute("آدرس3")]
        public string Address3 { get; set; }
        [ChakadMinLengthAttribute(10,"آدرس4")]
        public string Address4 { get; set; }
        [ChakadGuidRequiredAttribute("آدرس5")]
        public Guid Address5 { get; set; }
        [RangeAttributeAttribute(10,30,"آدرس6")]
        public int Address6 { get; set; }
        [ChakadRequiredAndNotDefaultNumberAttribute("آدرس 7")]
        public decimal Address7 { get; set; }
        [ChakadRequiredAndNotDefaultNumberAttribute("آدرس 8")]
        public int Address8 { get; set; }
    }
    public class CreateContactResult : ChakadResult
    {
        public int Id { get; set; }
    }
}
