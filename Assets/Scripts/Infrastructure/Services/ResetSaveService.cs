using Infrastructure.PersistenceProgress;

namespace Infrastructure.Services
{
    public class ResetSaveService : IResetSaveService
    {
        private readonly IPersistenceProgressService _persistenceProgressService;

        public ResetSaveService(IPersistenceProgressService persistenceProgressService)
        {
            _persistenceProgressService = persistenceProgressService;
        }

        public void ResetSave()
        {
            Reset();
        }

        private void Reset()
        {
            _persistenceProgressService.Progress = null;
            _persistenceProgressService.SaveCloudYG();
        }
    }

    public interface IResetSaveService
    {
        void ResetSave();
    }
}