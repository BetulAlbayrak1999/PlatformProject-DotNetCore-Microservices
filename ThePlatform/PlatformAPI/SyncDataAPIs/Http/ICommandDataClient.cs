using PlatformAPI.Dtos;

namespace PlatformAPI.SyncDataAPIs.Http
{
    public interface ICommandDataClient
    {
        public Task SendPlatformToCommand(PlatformReadDto platform);
    }
}
