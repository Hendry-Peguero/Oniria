namespace Oniria.ViewModels.Organization
{
    public class PatientTrackingViewModel
    {
        public string FullName { get; set; }
        public DateTime BornDate { get; set; }
        public int Age => DateTime.Now.Year - BornDate.Year;
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }


        public int TotalDreams { get; set; }
        public List<PatientDreamViewModel> Dreams { get; set; }
        public List<DreamDateCountViewModel> DreamsByDateList { get; set; } = new List<DreamDateCountViewModel>();

        public bool HasDreamsInLast30Days { get; set; }
        public DateTime? LastDreamDate { get; set; }
    }

    public class DreamDateCountViewModel
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public class PatientDreamViewModel
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}
