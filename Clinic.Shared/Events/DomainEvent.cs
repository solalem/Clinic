namespace Clinic.Shared.Events
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
