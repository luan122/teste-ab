using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a category entity in the system.
    /// Includes information such as title, creation date, update date, and deletion status.
    /// </summary>
    public class Category : BaseEntity, ICloneable
    {
        /// <summary>
        /// Gets or sets the title of the category.
        /// </summary>
        public string Title { get; set; } = string.Empty!;

        /// <summary>
        /// Gets or sets the date and time when the category was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow!;

        /// <summary>
        /// Gets or sets the date and time when the category was last updated.
        /// Null if the category has not been updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the category is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false!;

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public virtual List<Product> Products { get; set; }
        /// <summary>
        /// Performs validation of the category entity using the CategoryValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:<br />
        /// - IsValid: Indicates whether all validation rules passed<br />
        /// - Errors: Collection of validation errors if any rules failed<br />
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">Title format and length</list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CategoryValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}
