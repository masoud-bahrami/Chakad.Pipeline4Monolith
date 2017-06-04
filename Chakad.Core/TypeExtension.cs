#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

#endregion

namespace Chakad.Core
{
    //TODO
    public static class TypeExtension
    {
        public static bool CheckInheritedFromGenericType(this Type type, Type genericType)
        {
            if (type == null)
                return false;
            if (type.Name == genericType.Name)
                return true;
            return type.GetInterfaces().Any(t => t.CheckInheritedFromGenericType(genericType)) ||
                   type.BaseType.CheckInheritedFromGenericType(genericType);
        }


        public static string GetQualifiedName(this Type type)
        {
            return String.Format("{0}, {1}", type.FullName, (type.Assembly.ManifestModule).Name.Replace(".dll", ""));
        }

        public static bool IsPrimitveType(this Type type)
        {
            if (type.IsPrimitive || type == typeof(string) || type == typeof(string) || type == typeof(DateTime) || type == typeof(TimeSpan) ||
                type == typeof(Guid) || type == typeof(decimal))
            {
                return true;
            }
            if (typeof(Nullable<>).Name == type.Name)
                return type.GetGenericArguments()[0].IsPrimitveType();

            return false;
        }

        public static string GetAssemblyFileName(this Type type)
        {
            return (type.Assembly.ManifestModule).Name;
        }


        public static bool IsDateTimeType(this Type type)
        {
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return true;
            return false;
        }

        public static bool IsStringType(this Type type)
        {
            if (type == typeof(string))
                return true;
            return false;
        }

        public static bool IsBooleanType(this Type type)
        {
            if (type == typeof(bool) || type == typeof(bool?))
                return true;
            return false;
        }

        public static bool IsNumericType(this Type type)
        {
            if (type == typeof(int) || type == typeof(int?) ||
                type == typeof(Int64) || type == typeof(Int64?) ||
                type == typeof(double) || type == typeof(double?) ||
                type == typeof(decimal) || type == typeof(decimal?) ||
                type == typeof(float) || type == typeof(float?) ||
                type == typeof(long) || type == typeof(long?)
                )
                return true;
            return false;
        }

        public static PropertyInfo GetPropertyInfo(this Type type, string member, bool ignoreList = false)
        {

            if (type == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(member))
            {
                return null;
            }

            var columns = member.Split(new[] { '.' });


            var propertyInfo = type.GetProperty(columns[0]);
            if (propertyInfo == null)
                return null;
            var info = propertyInfo;
            for (var count = 1; count < columns.Length; ++count)
            {
                var propertyType = info.PropertyType;
                if (ignoreList && propertyType.IsImplementInterface(typeof(IList)))
                {
                    var arguments = propertyType.GetGenericArguments();
                    if (arguments.Length > 0)
                    {
                        propertyType = arguments[0];
                    }
                }
                propertyInfo = GetPropertyInfo(propertyType, (columns[count]));
                if (propertyInfo == null)
                    return null;
                info = propertyInfo;
            }
            return info;
        }


        public static PropertyInfo GetSearchablePropertyInformation(this Type type, string member)
        {
            return GetSearchablePropertyInformation(type, member, false);
        }

        public static PropertyInfo GetSearchablePropertyInformation(this Type type, string member, bool ignoreList)
        {
            if (String.IsNullOrEmpty(member))
                return null;
            if (type != null)
            {
                var columns = member.Split(new[] { '.' });

                var propertyInformation = type.GetProperty(columns[0]);
                if (propertyInformation == null)
                    return null;
                var info = propertyInformation;
                for (var count = 1; count < columns.Length; ++count)
                {
                    //info = info.PropertyType.GetProperty(columns[count]);
                    var propertyType = info.PropertyType;
                    if (ignoreList && propertyType.IsImplementInterface(typeof(IList)))
                    {
                        var arguments = propertyType.GetGenericArguments();
                        if (arguments.Length > 0)
                        {
                            type = arguments[0];
                        }
                    }
                    var getPropertyInformation = type.GetProperty(columns[count]);
                    if (getPropertyInformation == null)
                        return null;
                    info = getPropertyInformation;
                }
                return info;
            }
            return null;
        }

        public static bool HasAttribute(this Type type, Type attributeType)
        {
            return type.GetCustomAttributes(attributeType, true).Length > 0;
        }

        public static bool HasAttribute(this Type type, Type attributeType, bool inherit)
        {
            return type.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        public static bool IsImplementInterface(this Type type, Type interfaceType)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Any(type1 => type1 == interfaceType);
        }

        public static bool IsInheritFrom(this Type type, Type parentType)
        {
            if (type == null)
                return false;

            if (type.AssemblyQualifiedName == parentType.AssemblyQualifiedName)
                return true;

            if (type.AssemblyQualifiedName == typeof(Object).AssemblyQualifiedName)
                return false;

            var parent = type.BaseType;
            return IsInheritFrom(parent, parentType);
        }

        public static bool IsCustomProperty(this Type type, string dataMember, Type attributeType)
        {
            if (type == null)
                return false;

            var propertyInfo = GetPropertyInfo(type, dataMember);
            if (propertyInfo == null)
                return false;
            var attributes = propertyInfo.GetCustomAttributes(attributeType, true);

            if (attributes.Length > 0)
                return true;
            return false;
        }

        public static IEnumerable<Type> GetAllServices(this Type type)
        {
            if (type == null)
            {
                return new List<Type>();
            }

            var result = new List<Type>(type.GetInterfaces())
                             {
                                 type
                             };

            foreach (var interfaceType in type.GetInterfaces())
            {
                result.AddRange(GetAllServices(interfaceType));
            }

            return result.Distinct();
        }


        public static object ConvertTo(Type type, string text)
        {
            if (type == typeof(string))
                return text;

            if (string.IsNullOrEmpty(text))
                return null;

            var args = type.GetGenericArguments();
            if (args.Length == 1 && args[0].IsValueType)
            {
                if (args[0].GetGenericArguments().Any())
                    return ConvertTo(args[0], text);

                var nullableType = typeof(Nullable<>).MakeGenericType(args);
                if (type == nullableType)
                {
                    return text.ToLower() == "null" ? null : ConvertTo(args[0], text);
                }
            }


            if (type == typeof(Boolean))
            {
                if (text.ToLower() == "on")
                    return true;
                if (text.ToLower() == "off")
                    return false;
                return XmlConvert.ToBoolean(text.ToLower());
            }

            if (type == typeof(Byte))
                return XmlConvert.ToByte(text);

            if (type == typeof(Char))
                return XmlConvert.ToChar(text);

            if (type == typeof(DateTime))
            {

                var match = Regex.Match(text, @"(?<=/Date\().+?(?=\))");
                //                /Date(1245398693390)/
                if (match.Success)
                {
                    var s = (match.Groups[0].Value);
                    var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    return dateTime.AddMilliseconds(long.Parse(s));
                }
                DateTime date;
                if (DateTime.TryParse(text, out date))
                    return date;

                return XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
            }

            if (type == typeof(DateTimeOffset))
                return XmlConvert.ToDateTimeOffset(text);

            if (type == typeof(decimal))
                return XmlConvert.ToDecimal(text);

            if (type == typeof(double))
                return XmlConvert.ToDouble(text);

            if (type == typeof(Guid))
                return string.IsNullOrEmpty(text) ? Guid.Empty : XmlConvert.ToGuid(text);

            if (type == typeof(Int16))
                return XmlConvert.ToInt16(text);

            if (type == typeof(Int32))
                return XmlConvert.ToInt32(text);

            if (type == typeof(Int64))
                return XmlConvert.ToInt64(text);

            if (type == typeof(sbyte))
                return XmlConvert.ToSByte(text);

            if (type == typeof(Single))
                return XmlConvert.ToSingle(text);

            if (type == typeof(TimeSpan))
                return XmlConvert.ToTimeSpan(text);

            if (type == typeof(UInt16))
                return XmlConvert.ToUInt16(text);

            if (type == typeof(UInt32))
                return XmlConvert.ToUInt32(text);

            if (type == typeof(UInt64))
                return XmlConvert.ToUInt64(text);

            if (type.IsEnum)
                return Enum.Parse(type, text);

            if (type == typeof(byte[]))
                return Convert.FromBase64String(text);

            if (type == typeof(Uri))
                return new Uri(text);

            return null;
        }

        public static bool IsInFonixNameSpace(this Type type)
        {
            return type.Namespace != null && type.Namespace.ToLower().Contains("fonix");
        }

        /// <summary>
        /// Extract value of specified property named by field Title in object
        /// </summary>
        /// <param name="type">Type of Target object, </param>
        /// <param name="_object">object that you want extract value from it</param>
        /// <param name="fieldTitle">specified property which I try to extract Its value</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static object ExtractValueOfProperty(this Type type, object _object, string fieldTitle, BindingFlags flags = BindingFlags.Public)
        {
            if (type == null)
                throw new ArgumentNullException(@"Type Extension => type could not be null");

            //var fields = type.GetFields(flags); // Obtain all fields
            var properties = type.GetProperties(); // Obtain all fields

            foreach (var field in from field in properties let name = field.Name where name == fieldTitle select field)
            {
                return field.GetValue(_object); // Get value
            }

            return string.Empty;
        }

        /// <summary>
        /// By using reflection, in specific _object, set all founded properties values
        /// </summary>
        /// <param name="type">type of object, well be using by Reflection</param>
        /// <param name="_object">Specific object, which you want set Its Properties</param>
        /// <param name="fieldsValues">A Key-Pair collection, Containts field name and its value</param>
        /// <param name="onlyFirstMatchedProperty"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static object SetPropertiesValues(this Type type, ref object _object, Dictionary<string, object> fieldsValues, bool onlyFirstMatchedProperty = true, BindingFlags flags = BindingFlags.Public)
        {
            if (type == null)
                throw new NullReferenceException(@" Type Extension => type could not be null");

            //var properties = type.GetProperties(flags); // Obtain all fields
            var properties = type.GetProperties(); // Obtain all fields

            foreach (var fieldsValue in fieldsValues)
            {
                var value = fieldsValue;
                foreach (var field in from field in properties let name = field.Name where name == value.Key select field)
                {
                    if (onlyFirstMatchedProperty)
                    {

                        field.SetValue(_object, value.Value);
                        continue;
                    }
                    field.GetValue(_object); // Get value
                }
            }

            return string.Empty;
        }
    }
}