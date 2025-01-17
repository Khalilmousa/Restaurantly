﻿namespace Restarauntly.Web.ViewModels.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseCategoryViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
