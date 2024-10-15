﻿using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.DTO.BacklogItemDTOs
{
    public class BacklogItemConfirmationDto
    {
        public string BacklogItemName { get; set; }

        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
