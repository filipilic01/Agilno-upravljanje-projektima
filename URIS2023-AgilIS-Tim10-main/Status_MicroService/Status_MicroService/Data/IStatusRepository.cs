using Status_MicroService.Entities;

namespace Status_MicroService.Data
{
    public interface IStatusRepository
    {
        List<Status> GetStatus();
        public Status GetStatusId(Guid Id);
        public void RemoveStatus(Guid Id);
        public StatusConfirmation AddStatus(Status status);
        public Status UpdateStatus(Status status);
        public bool SaveChanges();

        public Status GetStatusByBacklogItemId(Guid id);

    }
}
