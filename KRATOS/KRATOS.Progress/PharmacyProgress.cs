namespace KRATOS.Progress
{
    public class PharmacyProgress
    {
        public int? TotalProgress { get; set; }
        public string Message { get; set; }
        public LogCategory LogCategory { get; set; }
        public PharmacyProgress()
        {
            TotalProgress = -1;
        }
    }
}
