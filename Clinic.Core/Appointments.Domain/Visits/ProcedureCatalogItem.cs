namespace Clinic.Core.Appointments.Domain.Visits
{
    public class ProcedureCatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
