namespace Oniria.ViewModels.Organization
{
    public class DashboardViewModel
    {
        public string OrgnanizationId { get; set; }

        public int TotalPatients { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalDreams { get; set; }
        public int DreamsToday { get; set; }
        public int DreamsThisWeek { get; set; }
        public int DreamsThisMonth { get; set; }

        public List<PatientDreamCountViewModel> TopPatientsByDreams { get; set; }
        public Dictionary<string, int> PatientsByAgeGroup { get; set; }
        public Dictionary<string, int> PatientsByGender { get; set; }

        public List<RecentPatientViewModel> RecentPatients { get; set; }
        public List<RecentDreamViewModel> RecentDreams { get; set; }
        public Dictionary<DateTime, int> DreamsByDate { get; set; }

        public int PatientsWithoutDreamsIn30Days { get; set; }
    }

    public class PatientDreamCountViewModel
    {
        public string FullName { get; set; }
        public int DreamCount { get; set; }
    }

    public class RecentPatientViewModel
    {
        public string FullName { get; set; }
        public DateTime RegisteredDate { get; set; }
    }

    public class RecentDreamViewModel
    {
        public string PatientName { get; set; }
        public DateTime DreamDate { get; set; }
        public string ShortDescription { get; set; }
    }

    public class RecentEmployeeViewModel
    {
        public string FullName { get; set; }
        public DateTime HiredDate { get; set; }
    }
}
