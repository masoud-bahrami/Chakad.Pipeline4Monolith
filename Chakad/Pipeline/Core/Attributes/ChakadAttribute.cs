using System;
using System.Reflection;

namespace Chakad.Pipeline.Core.Attributes
{
    public class ChakadRequiredAttribute : Attribute
    {
        public string FieldName { get; set; }
        public ChakadRequiredAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
    public class ChakadRequiredAndNotDefaultNumberAttribute : Attribute
    {
        Assembly _assembly;
        string _resourceFileName;
        string _resourceKey;
        public string FieldName { get; set; }
        public ChakadRequiredAndNotDefaultNumberAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
        public ChakadRequiredAndNotDefaultNumberAttribute(Assembly assembly, string resourceFileName, string resourceKey)
        {
            _assembly = assembly;
            _resourceFileName = resourceFileName;
            _resourceKey = resourceKey;
        }
    }
    public class ChakadMaxLengthAttribute : Attribute
    {
        public readonly string FieldName;
        public int Length { get; set; }
        public ChakadMaxLengthAttribute(int length, string fieldName)
        {
            FieldName = fieldName;
            Length = length;
        }
    }
    public class ChakadStringLengthAttribute : Attribute
    {
        public readonly string FieldName;
        public int MaximumLength { get; set; }
        public ChakadStringLengthAttribute(int maximumLength, string fieldName)
        {
            FieldName = fieldName;
            MaximumLength = maximumLength;
        }
    }
    public class ChakadStringRequiredAttribute : Attribute
    {
        public readonly string FieldName;
        public ChakadStringRequiredAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
    public class ChakadMinLengthAttribute : Attribute
    {
        public readonly string FieldName;
        public int Length { get; set; }
        public ChakadMinLengthAttribute(int length, string fieldName)
        {
            FieldName = fieldName;
            Length = length;
        }
    }
    public class ChakadGuidRequiredAttribute : Attribute
    {
        public readonly string FieldName;
        public ChakadGuidRequiredAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
    public class RangeAttributeAttribute : Attribute
    {
        public readonly string FieldName;
        public int Maximum { get; set; }
        public int Minimum { get; set; }
        public RangeAttributeAttribute(int minimum, int maximum, string fieldName)
        {
            FieldName = fieldName;
            Maximum = maximum;
            Minimum = minimum;
        }
    }
}
