namespace CommandsAPI.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
