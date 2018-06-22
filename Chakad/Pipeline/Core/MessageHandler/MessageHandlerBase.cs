using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Core.Extensions;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Resource;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public abstract class MessageHandlerBase<T, TOut> :
        IHandleMessage
        where T : class, IMessageInterface
        where TOut : class
        //, IChakadResult
    {
        public ICommandPipeline Pipeline { get; set; }

        public virtual string Handle(T message)
        {
            var errorMessage = "";
            GuardAgainstMessage.CanCheckInToPipeline(message, ref errorMessage);

            //TODO some processing goes here
            var res = new StringBuilder();
            GuardAgainstMessagePolicy(ref res, message);

            if (!string.IsNullOrEmpty(res.ToString()))
                res.Append(errorMessage);
            else
                res.Append(errorMessage);

            return res.ToString();
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

            foreach (var propertyInfo in messageType.GetProperties().ToList())
            {
                var customAttributes = propertyInfo.GetCustomAttributes(true);

                foreach (var customAttribute in customAttributes)
                {
                    var type = customAttribute.GetType();

                    if (type == typeof(ChakadRequiredAttribute))
                    {
                        var value = propertyInfo.GetValue(message, null);
                        if (!Guard.InformMeIfNull(value)) continue;

                        var fieldName = ((ChakadRequiredAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        res.Append(ResourceHelper.GetResourceValue("TheFieldCanNotBeEmpty", null, fieldName))
                            .Append(Environment.NewLine);
                    }
                    else if (type == typeof(ChakadRequiredAndNotDefaultNumberAttribute))
                    {
                        var value = propertyInfo.GetValue(message, null);
                        if (Guard.AgainstDefaultNumber(value))continue;

                            var fieldName = ((ChakadRequiredAndNotDefaultNumberAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        res.Append(ResourceHelper.GetResourceValue("TheFieldCanNotBeEmpty", null, fieldName))
                            .Append(Environment.NewLine);
                    }
                    else if (type == typeof(ChakadMaxLengthAttribute))
                    {
                        var maximumValue = ((ChakadMaxLengthAttribute)customAttribute).Length;

                        var value = propertyInfo.GetValue(message, null);
                        var fieldName = ((ChakadMaxLengthAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        try
                        {
                            if (!Guard.AgainstGreatherThan(maximumValue, long.Parse(value.ToString())))
                            {
                                res.Append(ResourceHelper.GetResourceValue("MaxLengthIs", null, fieldName, maximumValue.ToString()))
                                    .Append(Environment.NewLine);
                            }
                        }
                        catch (Exception ex)
                        {
                            res.Append(ResourceHelper.GetResourceValue("MaxLengthIs", null, fieldName, maximumValue.ToString()))
                                    .Append(Environment.NewLine);
                            Console.WriteLine(ex.GetaAllMessages());
                        }
                    }
                    else if (type == typeof(ChakadMinLengthAttribute))
                    {
                        var minimumValue = ((ChakadMinLengthAttribute)customAttribute).Length;

                        var value = propertyInfo.GetValue(message, null);

                        var fieldName = ((ChakadMinLengthAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        try
                        {
                            if (!Guard.AgainstGreatherThan(minimumValue, long.Parse(value.ToString())))
                            {
                                res.Append(ResourceHelper.GetResourceValue("MinLengthIs", null, fieldName, minimumValue.ToString()))
                                    .Append(Environment.NewLine);
                            }
                        }
                        catch (Exception ex)
                        {
                            res.Append(ResourceHelper.GetResourceValue("MinLengthIs", null, fieldName, minimumValue.ToString()))
                                    .Append(Environment.NewLine);
                            Console.WriteLine(ex.GetaAllMessages());
                        }
                    }
                    else if (type == typeof(RangeAttributeAttribute))
                    {
                        var minimumValue = ((RangeAttributeAttribute)customAttribute).Minimum;
                        var maximumValue = ((RangeAttributeAttribute)customAttribute).Maximum;

                        var fieldName = ((RangeAttributeAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        var value = propertyInfo.GetValue(message, null);
                        try
                        {
                            if (
                                !Guard.AgainstBetween(long.Parse(value.ToString()), long.Parse(minimumValue.ToString()),
                                    long.Parse(maximumValue.ToString())))
                            {
                                res.Append(ResourceHelper.GetResourceValue("RangeIsBetween", null, fieldName,
                                    minimumValue.ToString(), maximumValue.ToString()))
                                    .Append(Environment.NewLine);
                            }
                        }
                        catch (Exception ex)
                        {
                            res.Append(ResourceHelper.GetResourceValue("RangeIsBetween", null, fieldName,
                                    minimumValue.ToString(), maximumValue.ToString()))
                                    .Append(Environment.NewLine);
                            Console.WriteLine(ex.GetaAllMessages());
                        }
                    }
                    else if (type == typeof(ChakadGuidRequiredAttribute))
                    {
                        var fieldName = ((ChakadGuidRequiredAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        try
                        {
                            var value = propertyInfo.GetValue(message, null);

                            if (!Guard.AgainstNullGuid(Guid.Parse(value.ToString())))
                                res.Append(ResourceHelper.GetResourceValue("TheFieldCanNotBeEmpty", null, fieldName))
                                    .Append(Environment.NewLine);
                        }
                        catch (Exception ex)
                        {
                            res.Append(ResourceHelper.GetResourceValue("TheFieldCanNotBeEmpty", null, fieldName))
                                    .Append(Environment.NewLine);
                            Console.WriteLine(ex.GetaAllMessages());
                        }
                    }
                    else if (type == typeof(ChakadStringLengthAttribute))
                    {
                        var fieldName = ((ChakadStringLengthAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        var maximumLength = ((ChakadStringLengthAttribute)customAttribute).MaximumLength;

                        try
                        {
                            var value = propertyInfo.GetValue(message, null);

                            if (!Guard.AgainstStringMaxLength(value.ToString(), maximumLength))
                                res.Append(ResourceHelper.GetResourceValue("MaxLengthIs", null, fieldName, maximumLength.ToString()));
                        }
                        catch (Exception ex)
                        {
                            res.Append(ResourceHelper.GetResourceValue("MaxLengthIs", null, fieldName, maximumLength.ToString()));
                            Console.WriteLine(ex.GetaAllMessages());
                        }
                    }
                    else if (type == typeof(ChakadStringRequiredAttribute))
                    {
                        var fieldName = ((ChakadStringRequiredAttribute)customAttribute).FieldName;

                        if (string.IsNullOrWhiteSpace(fieldName))
                            fieldName = propertyInfo.Name;

                        try
                        {
                            var value = propertyInfo.GetValue(message, null);

                            if (!Guard.AgainstString(value.ToString()))
                                res.Append(ResourceHelper.GetResourceValue("TheFieldCanNotBeEmpty", null, fieldName));
                        }
                        catch (Exception ex)
                        {
                            res.Append(ResourceHelper.GetResourceValue("TheFieldCanNotBeEmpty", null, fieldName));
                            Console.WriteLine(ex.GetaAllMessages());
                        }
                    }
                }
            }
        }
    }
}