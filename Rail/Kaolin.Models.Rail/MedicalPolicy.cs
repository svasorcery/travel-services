namespace Kaolin.Models.Rail
{
    public class MedicalPolicy
    {
        public int StatusId { get; set; }
        public int Number { get; set; }
        public int AreaId { get; set; }
        public decimal Cost { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}
