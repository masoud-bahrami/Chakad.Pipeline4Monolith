using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Chakad.Core
{
    public static class TypeHelper
    {
        public static List<Type> GetTypes(string path, params Type[] inheritedfrom)
        {
            var types = new List<Type>();
            var files =
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(
                    s =>
                        s.ToLower().Contains("chakad.") &&
                        s.ToLower().EndsWith(".dll")
                    );
            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    if (assembly == null) continue;
                    var collection =
                        assembly.GetTypes().Where(type => !type.IsGenericType &&
                                                          !type.IsAbstract &&
                                                          !type.Assembly.IsDynamic);
                    if (inheritedfrom != null)
                        collection = collection.Where(
                            type => inheritedfrom.Any(type.IsImplementInterface));

                    types.AddRange(collection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return types;
        }
        public static List<Type> GetTypes(Assembly assembly, params Type[] inheritedfrom)
        {
            var types = new List<Type>();
            try
            {
                if (assembly == null) return types;
                var collection =
                    assembly.GetTypes().Where(type => !type.IsGenericType &&
                                                      !type.IsAbstract &&
                                                      !type.Assembly.IsDynamic);
                if (inheritedfrom != null)
                    collection = collection.Where(
                        type => inheritedfrom.Any(type.IsImplementInterface));

                types.AddRange(collection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return types;
        }
        public static List<Type> GetTypes(List<Assembly> assemblies, params Type[] inheritedfrom)
        {
            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    if (assembly == null) return types;
                    var collection =
                        assembly.GetTypes().Where(type => !type.IsGenericType &&
                                                          !type.IsAbstract &&
                                                          !type.Assembly.IsDynamic);
                    if (inheritedfrom != null)
                        collection = collection.Where(
                            type => inheritedfrom.Any(type.IsImplementInterface));

                    types.AddRange(collection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return types;
        }
    }
}