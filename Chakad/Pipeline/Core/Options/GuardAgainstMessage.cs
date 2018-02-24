using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Chakad.Core;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Options
{
    internal static class GuardAgainstMessage
    {
        public static bool CanCheckInToPipeline<T>(T viewModel, ref string errorMessage)
       where T : class, IMessageInterface
        {
            var propertyInfos = viewModel.GetType().GetProperties()
                .Where(info => info.PropertyType.GUID == typeof(ChakadMessageProperty<>).GUID)
                .ToList();

            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(viewModel);

                var valiationPolicy = propertyInfo.PropertyType.GetProperties()
                    .FirstOrDefault(x => x.Name.ToLower() == "valiationpolicy");

                var value = propertyInfo.PropertyType.GetProperties()
                    .FirstOrDefault(x => x.Name.ToLower() == "value");

                var componentValue = value.GetValue(propertyValue);

                var policyFunction = (Func<object, ValidationResultViewModel>)valiationPolicy.GetValue(propertyValue);

                if (policyFunction != null)
                {
                    var validationResultViewModel = policyFunction(componentValue);

                    if (validationResultViewModel.IsValid) continue;

                    var content = validationResultViewModel.Content;
                    if (string.IsNullOrWhiteSpace(content))
                        content = GetGeneralErrorMessage(propertyInfo, propertyValue);

                    errorMessage += $"{Environment.NewLine},{content}";
                }
                else
                {
                    var isRequiredField = propertyInfo.PropertyType.GetProperties()
                    .FirstOrDefault(x => x.Name.ToLower() == "isrequired");

                    if (isRequiredField == null) continue;

                    var isRequired = (bool)isRequiredField.GetValue(propertyValue);
                    if (!isRequired) continue;

                    var fieldType = propertyInfo.PropertyType.GetProperties()
                    .FirstOrDefault(x => x.Name.ToLower() == "fieldtype");

                    if (fieldType == null) continue;

                    var type = (ChakadFieldType)fieldType.GetValue(propertyValue);
                    var isValid = Guard.Against(type, componentValue);
                    if (isValid) continue;

                    var labelTitle = GetGeneralErrorMessage(propertyInfo, propertyValue);
                    errorMessage += $"{Environment.NewLine}{labelTitle}";
                }
            }

            return string.IsNullOrWhiteSpace(errorMessage);
        }

        private static string GetGeneralErrorMessage(PropertyInfo propertyInfo, object propertyValue)
        {
            var label = propertyInfo.PropertyType.GetProperties().FirstOrDefault(x => x.Name.ToLower() == "title");
            var labelTitle = (string)label.GetValue(propertyValue);
            return Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("fa-ir")
                ? $"فیلد {labelTitle} نمی تواند خالی باشد" : $"The {labelTitle} can not be empy!";
        }
    }

}
