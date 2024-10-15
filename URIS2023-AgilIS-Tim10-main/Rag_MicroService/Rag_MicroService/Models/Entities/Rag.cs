using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Rag_MicroService.Models.Entities
{
    [Index(nameof(BacklogItemId), IsUnique = true)]
    public class Rag
    {
        [Key]
        public Guid RagId { get; set; }

        public string RagValue { get; set; }

        
        public Guid BacklogItemId { get; set; }

    }
}
