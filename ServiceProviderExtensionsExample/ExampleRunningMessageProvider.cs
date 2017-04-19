namespace ServiceProviderExtensionsExample
{
    internal interface IRunningMessageProvider
    {
        string GetRunningMessage();
    }

    internal class ExampleRunningMessageProvider : IRunningMessageProvider
    {
        public string GetRunningMessage()
        {
            return "Running... Press 'enter' to exit.";
        }
    }
}