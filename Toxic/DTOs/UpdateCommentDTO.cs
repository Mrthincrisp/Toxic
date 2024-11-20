﻿using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class UpdateCommentDTO
    {
        [Required(ErrorMessage = "Content is Required.")]
        public string? Content { get; set; }

    }
}
