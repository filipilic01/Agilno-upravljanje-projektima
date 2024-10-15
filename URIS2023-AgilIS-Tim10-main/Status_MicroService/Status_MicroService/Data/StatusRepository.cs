using AutoMapper;
using Status_MicroService.Entities;

namespace Status_MicroService.Data
{
    public class StatusRepository : IStatusRepository
    {
        private readonly StatusDBContext context;
        private readonly IMapper mapper;

        public StatusRepository(StatusDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public StatusConfirmation AddStatus(Status status)
        {
            var createdStatus = context.Status.Add(status);
            return mapper.Map<StatusConfirmation>(createdStatus.Entity);
        }

        public List<Status> GetStatus()
        {
            List<Status> status = context.Status.ToList();
            return status;
        }

        public Status GetStatusId(Guid Id)
        {
            return context.Status.FirstOrDefault(a => a.IdStatusa == Id);
        }

        public void RemoveStatus(Guid Id)
        {
            var status = GetStatusId(Id);
            context.Remove(status);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Status UpdateStatus(Status status)
        {
            try
            {
                var existingStatus = context.Status.FirstOrDefault(e => e.IdStatusa == status.IdStatusa);

                if (existingStatus != null)
                {

                    existingStatus.VrednostStatusa = status.VrednostStatusa;

                    context.SaveChanges(); // Save changes to the database

                    return existingStatus;
                }
                else
                {
                    throw new KeyNotFoundException($"Status with ID {status.IdStatusa} not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("Error updating status", ex);
            }
        }

        public Status GetStatusByBacklogItemId(Guid id)
        {
            return context.Status.FirstOrDefault(e => e.BacklogItemId == id);
        }

    }
}
