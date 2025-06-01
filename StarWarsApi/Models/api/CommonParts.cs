public class HeartMath
    {
        public string link { get; set; }
        public string details { get; set; }
    }

    public class PartnerDiscounts
    {
        public SaberMasters saberMasters { get; set; }
        public HeartMath heartMath { get; set; }
    }

    public class SaberMasters
    {
        public string link { get; set; }
        public string details { get; set; }
    }

    public class Social
    {
        public string discord { get; set; }
        public string reddit { get; set; }
        public string github { get; set; }
    }

    public class Support
    {
        public string contact { get; set; }
        public string donate { get; set; }
        public PartnerDiscounts partnerDiscounts { get; set; }
    }
