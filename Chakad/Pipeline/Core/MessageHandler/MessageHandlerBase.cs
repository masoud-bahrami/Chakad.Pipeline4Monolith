using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.Options;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public abstract class MessageHandlerBase<T, TOut> :
        IHandleMessage
        where T : class, IMessageInterface
        where TOut : class, IChakadResult
    {
        public IPipeline Pipeline { get; set; }

        public virtual string Handle(T message)
        {
            var errorMessage = "";
            GuardAgainstMessage.CanCheckInToPipeline(message, ref errorMessage);

            //TODO some processing goes here
            //var res = new StringBuilder();
            //GuardAgainstMessagePolicy(ref res, message);
            //return res.ToString();
            return errorMessage;
        }

        public abstract Task<TOut> Execute(T message);

        public abstract Task<bool> CheckAccessPolicy(T message);

        protected async Task<TOut> InternalHandle(T message)
        {
            //TODO Orhestration message... for example

            var result = Handle(message);

            if (!string.IsNullOrEmpty(result))
                throw new ChakadMessageViolationPolicyException(result);

            var hasAccess = await CheckAccessPolicy(message);
            if (!hasAccess)
                throw new ChakadNotAccessPolicyException("You dont have access to do this operations!");

            var execute = await Execute(message);
            return execute;
        }

        private void GuardAgainstMessagePolicy(ref StringBuilder res, T message)
        {
            var messageType = message.GetType();

            var shouldCkeckPolicy = messageType.GetCustomAttributes().ToList()
                .Any(attribute => attribute.GetType() == typeof(IHaveNotAnyPolicyAttribute));

            if (shouldCkeckPolicy)
                return;


            foreach (PropertyInfo propertyInfo in messageType.GetProperties().ToList())
            {
                var customAttributes = propertyInfo.GetCustomAttributes(true);

                foreach (var customAttribute in customAttributes)
                {
                    var type = customAttribute.GetType();

                    if (type == typeof(RequiredAttribute))
                    {
                        var value = propertyInfo.GetValue(message, null);
                        if (Guard.InformMeIfNull(value))
                            res.Append($"{propertyInfo.Name} could not be null {Environment.NewLine}");
                    }
                    else if (type == typeof(MaxLengthAttribute))
                    {
                        var maximumValue = ((MaxLengthAttribute)customAttribute).Length;

                        var value = propertyInfo.GetValue(message, null);
                        try
                        {
                            if (!Guard.AgainstGreatherThan(maximumValue, long.Parse(value.ToString())))
                                res.Append($"{propertyInfo.Name} could not greather than {maximumValue} {Environment.NewLine}");
                        }
                        catch (Exception ex)
                        {
                            res.Append($"{propertyInfo.Name} should be number, error message is {ex.Message}{Environment.NewLine}");
                        }
                    }
                    else if (type == typeof(MinLengthAttribute))
                    {
                        var minimumValue = ((MinLengthAttribute)customAttribute).Length;

                        var value = propertyInfo.GetValue(message, null);

                        try
                        {
                            if (!Guard.AgainstGreatherThan(minimumValue, long.Parse(value.ToString())))
                                res.Append($"{propertyInfo.Name} could not shorter than {minimumValue} {Environment.NewLine}");
                        }
                        catch (Exception ex)
                        {
                            res.Append($"{propertyInfo.Name} should be number, error message is {ex.Message}{Environment.NewLine}");
                        }
                    }
                    else if (type == typeof(RangeAttribute))
                    {
                        var minimumValue = ((RangeAttribute)customAttribute).Minimum;
                        var maximumValue = ((RangeAttribute)customAttribute).Maximum;

                        var value = propertyInfo.GetValue(message, null);
                        try
                        {
                            if (!Guard.AgainstBetween(long.Parse(value.ToString()), long.Parse(minimumValue.ToString()), long.Parse(maximumValue.ToString())))
                                res.Append($"{propertyInfo.Name} should be between {minimumValue} and {maximumValue} {Environment.NewLine}");
                        }
                        catch (Exception ex)
                        {
                            res.Append($"{propertyInfo.Name} should be between {minimumValue} and {maximumValue} , error message is {ex.Message}{Environment.NewLine}");
                        }
                    }
                    else if (type == typeof(GuidRequiredAttribute))
                    {
                        try
                        {
                            var value = propertyInfo.GetValue(message, null);

                            if (!Guard.AgainstNullGuid(Guid.Parse(value.ToString())))
                                res.Append($"{propertyInfo.Name} could not null or Guid.Empty {Environment.NewLine}");
                        }
                        catch (Exception ex)
                        {
                            res.Append($"{propertyInfo.Name} should be Guid, error message is {ex.Message}{Environment.NewLine}");
                        }
                    }
                    else if (type == typeof(StringLengthAttribute))
                    {
                        try
                        {
                            var maximumLength = ((StringLengthAttribute)customAttribute).MaximumLength;
                            var value = propertyInfo.GetValue(message, null);

                            if (!Guard.AgainstStringMaxLength(value.ToString(), maximumLength))
                                res.Append($"{propertyInfo.Name} lenght should be shorther than {maximumLength} {Environment.NewLine}");
                        }
                        catch (Exception ex)
                        {
                            res.Append($"{propertyInfo.Name} should be String, error message is {ex.Message}{Environment.NewLine}");
                        }
                    }
                }
            }
        }
    }
}