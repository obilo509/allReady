﻿using System.ComponentModel.DataAnnotations;

namespace AllReady.Areas.Admin.ViewModels.Event
{
    public class MessageEventVolunteersViewModel
    {
        public int EventId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
