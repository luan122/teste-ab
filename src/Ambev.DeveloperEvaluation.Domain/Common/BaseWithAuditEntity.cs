﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public class BaseWithAuditEntity : BaseEntity, IBaseWithSoftDelete, IBaseWithCreatedAtEntity, IBaseWithUpdatedAtEntity
    {
        public bool IsDeleted { get; set; } = false!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow!;
        public DateTime? UpdatedAt { get; set; }
    }
}
