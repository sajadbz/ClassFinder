using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bz.ClassFinder.Attributes;
using Bz.ClassFinder.Interfaces;
using Bz.ClassFinder.Models;

namespace Bz.ClassFinder
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Use (BzDescriptionAttribute) for your class, method, controller or action.
        /// </summary>
        /// <typeparam name="TBaseClass">Type of your assembly that your search for class or method, exp: StartUp class </typeparam>
        /// <returns>List of BzClassInfo</returns>
        public static IList<BzClassInfo> GetClassAndMethods<TBaseClass>() where TBaseClass : new()
        {
            return GetClassAndMethods<TBaseClass, BzDescriptionAttribute>().ToList();
        }
        
        public static IEnumerable<BzClassInfo> GetClassAndMethods(string dllFullName)
        {
            Assembly assembly = Assembly.LoadFrom(dllFullName);
            return GetTypesWithAttribute(assembly, typeof(BzDescriptionAttribute))
                .Select(c => new BzClassInfo()
                {
                    FullName = c.FullName,
                    Title = c.GetCustomAttribute<BzDescriptionAttribute>()?.Title,
                    Methods = GetMethodsWithAttribute(c.GetMethods(), typeof(BzDescriptionAttribute))
                        .Select(a => new BzMethodInfo()
                        {
                            FullName = $"{c.FullName}.{a.Name}",
                            Title = a.GetCustomAttribute<BzDescriptionAttribute>()?.Title
                        }).ToList()
                });
        }

        private static IEnumerable<BzClassInfo> GetClassAndMethods<TAttribute>(string dllFullName) where TAttribute : Attribute, IBzDescription
        {
            Assembly assembly = Assembly.LoadFrom(dllFullName);
            return GetTypesWithAttribute(assembly, typeof(TAttribute))
                .Select(c => new BzClassInfo()
                {
                    FullName = c.FullName,
                    Title = c.GetCustomAttribute<TAttribute>()?.Title,
                    Methods = GetMethodsWithAttribute(c.GetMethods(), typeof(BzDescriptionAttribute))
                        .Select(a => new BzMethodInfo()
                        {
                            FullName = $"{c.FullName}.{a.Name}",
                            Title = a.GetCustomAttribute<TAttribute>()?.Title
                        }).ToList()
                });
        }

        private static IEnumerable<BzClassInfo> GetClassAndMethods<TBaseClass, TAttribute>() where TBaseClass : new() where TAttribute : Attribute, IBzDescription
        {
            return GetTypesWithAttribute(Assembly.GetAssembly(new TBaseClass().GetType()), typeof(TAttribute))
                .Select(c => new BzClassInfo()
                {
                    FullName = c.FullName,
                    Title = c.GetCustomAttribute<TAttribute>()?.Title,
                    Methods = GetMethodsWithAttribute(c.GetMethods(), typeof(BzDescriptionAttribute))
                    .Select(a => new BzMethodInfo()
                    {
                        FullName = $"{c.FullName}.{a.Name}",
                        Title = a.GetCustomAttribute<TAttribute>()?.Title
                    }).ToList()
                });
        }

        private static IEnumerable<Type> GetTypesWithAttribute(Assembly assembly, Type attribute)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attribute, true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        private static IEnumerable<MethodInfo> GetMethodsWithAttribute(MethodInfo[] methods, Type attribute)
        {
            return methods
                .Where(m => m.GetCustomAttributes(attribute, false).Length > 0)
                .ToArray();
        }
    }
}
