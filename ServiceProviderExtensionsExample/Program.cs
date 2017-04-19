using System;
using System.ComponentModel.Design;

namespace ServiceProviderExtensionsExample
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            using (var serviceContainer = new ServiceContainer())
            {
                serviceContainer.AddService(typeof(IRunningMessageProvider), (container, type) => container.ConstructInstance<ExampleRunningMessageProvider>());
                serviceContainer.AddService(typeof(IApplicationController), (container, type) => container.ConstructInstance<ExampleApplicationController>());

                var applicationController = serviceContainer.GetService<IApplicationController>();

                applicationController.Run();
            }
        }
    }
}