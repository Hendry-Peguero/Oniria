namespace Oniria.Core.Dtos.Organization.Reponse
{
    public class PatientTrackingResponse
    {
        public string FullName { get; set; }
        public DateTime BornDate { get; set; }
        public int Age => DateTime.Now.Year - BornDate.Year;
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }


        public int TotalDreams { get; set; }
        public List<PatientDreamResponse> Dreams { get; set; }
        public List<DreamDateCountResponse> DreamsByDateList { get; set; } = new List<DreamDateCountResponse>();

        public bool HasDreamsInLast30Days { get; set; }
        public DateTime? LastDreamDate { get; set; }
    }

    public class DreamDateCountResponse
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public class PatientDreamResponse
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}
