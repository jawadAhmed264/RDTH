namespace RDTH.Models.PackageViewModel
{
    public class PackageDetailViewModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public int NoOfChannels { get; set; }
        public int NewsChannel { get; set; }
        public int EntertainmentChannel { get; set; }
        public int SportsChannel { get; set; }
        public int DocumentariesChannel { get; set; }
        public int Charges { get; set; }
        public string ImageUrl { get; set; }
    }
}
