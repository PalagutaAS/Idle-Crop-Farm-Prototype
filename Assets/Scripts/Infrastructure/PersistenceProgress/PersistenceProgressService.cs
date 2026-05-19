using SavesData;

namespace Infrastructure.PersistenceProgress
{
    public class PersistenceProgressService : IPersistenceProgressService
    {
        public GameProgress Progress { get; set; }
    }

    public interface IPersistenceProgressService
    {
        public GameProgress Progress { get; set; }
    }
}