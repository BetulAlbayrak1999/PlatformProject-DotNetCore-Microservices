using PlatformAPI.Models;

namespace PlatformAPI.Data.Repositories
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();

        Platform GetPlatformById(int id);

        void CreatePlatform(Platform plat);
    }
}
