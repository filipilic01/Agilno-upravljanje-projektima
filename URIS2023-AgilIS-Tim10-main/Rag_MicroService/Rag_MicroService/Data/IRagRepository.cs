using Rag_MicroService.Models;
using Rag_MicroService.Models.Entities;

namespace Rag_MicroService.Data
{
    public interface IRagRepository
    {
        List<Rag> GetRags();

        Rag GetRagById(Guid id);

        RagConfirmation CreateRag(Rag rag);

        void UpdateRag(Rag rag);

        void DeleteRag(Guid id);

        Rag GetRagByBacklogItemId(Guid id);


    }
}
