﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Domain.Common
{
    public class Auditable
    {
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
