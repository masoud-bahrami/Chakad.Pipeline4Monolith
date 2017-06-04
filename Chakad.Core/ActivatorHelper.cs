using System;
using System.Reflection;

namespace Chakad.Core
{
    public static class ActivatorHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static object CreateNewInstance(string assemblyPath, string fullName)
        {
            Guard.AgainstNullAndEmpty(assemblyPath, assemblyPath);
            
            // dynamically load assembly from file Test.dll
            var assembly = Assembly.LoadFile(assemblyPath);

            // get type of class Calculator from just loaded assembly
            var type = assembly.GetType(fullName);

            // create instance of class Calculator
            var instance = Activator.CreateInstance(type);

            return instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyPath"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static T CreateNewInstance<T>(string assemblyPath, string fullName)
        {
            Guard.AgainstNullAndEmpty(assemblyPath, assemblyPath);
            Guard.AgainstNullAndEmpty(fullName, fullName);

            // dynamically load assembly from file Test.dll
            var assembly = Assembly.LoadFile(assemblyPath);

            // get type of class Calculator from just loaded assembly
            var type = assembly.GetType(fullName);

            // create instance of class Calculator
            var instance = Activator.CreateInstance(type);

            return (T)instance;
        }

        public static T CreateNewInstance<T>(Type type)
        {
            Guard.AgainstNull(@"ActivatorHelper.CreateNewInstance<T> type could not be null", type);
            
            // create instance of class Calculator
            var instance = Activator.CreateInstance(type);

            return (T)instance;
        }

        public static object CreateNewInstance(Type type)
        {
            Guard.AgainstNull(@"ActivatorHelper.CreateNewInstance type could not be null", type);

            // create instance of class Calculator
            var instance = Activator.CreateInstance(type);

            return instance;
        }
    }
}
