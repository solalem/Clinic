using System;
using System.Text;
using System.Collections.Generic;

namespace Clinic.SharedKernel.Domain.Abstractions.Events
{
    /// <summary>
    /// Domain Event base class
    /// </summary>
    public class DomainEvent
    {
        public DateTime Time { get; private set; }
        public bool PreCommit { get; set; } = true;

        public DomainEvent()
        {
            Time = DateTime.Now;
        }
    }
}
