namespace Oniria.Core.Dtos.Organization.Reponse
{
    public class DashboardResponse
    {
        public string OrgnanizationId { get; set; }

        public int TotalPatients { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalDreams { get; set; }
        public int DreamsToday { get; set; }
        public int DreamsThisWeek { get; set; }
        public int DreamsThisMonth { get; set; }

        public List<PatientDreamCountResponse> TopPatientsByDreams { get; set; }
        public Dictionary<string, int> PatientsByAgeGroup { get; set; }
        public Dictionary<string, int> PatientsByGender { get; set; }

        public List<RecentPatientResponse> RecentPatients { get; set; }
        public List<RecentDreamResponse> RecentDreams { get; set; }
        public Dictionary<DateTime, int> DreamsByDate { get; set; } = new();


        public int PatientsWithoutDreamsIn30Days { get; set; }
    }

    public class PatientDreamCountResponse
    {
        public string FullName { get; set; }
        public int DreamCount { get; set; }
    }

    public class RecentPatientResponse
    {
        public string FullName { get; set; }
        public DateTime RegisteredDate { get; set; }
    }

    public class RecentDreamResponse
    {
        public string PatientName { get; set; }
        public DateTime DreamDate { get; set; }
        public string ShortDescription { get; set; }
    }

    public class RecentEmployeeResponse
    {
        public string FullName { get; set; }
        public DateTime HiredDate { get; set; }
    }
}

