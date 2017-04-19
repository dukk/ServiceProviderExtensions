using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceProviderExtensionsExample
{
    internal static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider, bool throwIfMissing = false)
            where T : class
        {
            if (null == serviceProvider)
                throw new ArgumentNullException(nameof(serviceProvider));

            var service = serviceProvider.GetService(typeof(T)) as T;

            if (throwIfMissing && null == service)
                throw new Exception($"Requested service, {typeof(T).FullName}, is missing from the {nameof(serviceProvider)}.");

            return service;
        }

        public static T ConstructInstance<T>(this IServiceProvider serviceProvider, ConstructorInfo constructorToUse = null)
            where T : class
        {
            if (null == serviceProvider)
                throw new ArgumentNullException(nameof(serviceProvider));

            var constructor = constructorToUse ?? getLargestConstructor<T>();
            var parameters = constructor.GetParameters();

            if (!parameters.Any())
                return constructor.Invoke(null) as T;

            var parameterValues = new List<object>();

            foreach (var parameter in parameters)
                parameterValues.Add(serviceProvider.GetService(parameter.ParameterType));

            return constructor.Invoke(parameterValues.ToArray()) as T;
        }

        private static ConstructorInfo getLargestConstructor<T>()
        {
            return (from constructor in typeof(T).GetConstructors()
                    orderby constructor.GetParameters().Length descending
                    select constructor).First();
        }
    }
}