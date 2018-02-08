using Chakad.Core;
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
        }
        public ChakadMessageProperty<string> FirstName { get; set; }
        public ChakadMessageProperty<string> LastName { get; set; }
        public string Address { get; set; }
    }
    public class CreateContactResult : ChakadResult
    {
        public int Id { get; set; }
    }
}
