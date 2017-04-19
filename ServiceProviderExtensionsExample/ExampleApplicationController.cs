using System;

namespace ServiceProviderExtensionsExample
{
    internal interface IApplicationController
    {
        void Run();
    }

    internal class ExampleApplicationController : IApplicationController
    {
        private readonly IRunningMessageProvider runningMessageProvider;

        public ExampleApplicationController(IRunningMessageProvider runningMessageProvider)
        {
            this.runningMessageProvider = runningMessageProvider;
        }

        public void Run()
        {
            Console.WriteLine(this.runningMessageProvider.GetRunningMessage());
            Console.ReadLine();
        }
    }
}