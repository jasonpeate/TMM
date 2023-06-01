namespace TMM.Logic
{
    public class CompleteCustomerModel
    {
        public string Title { get; set; }

        public string Forename { get; set; }

        public string SureName { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNo { get; set; }

        public CompleteAddressModel[] Addresses { get; set; }
    }

    public class CompleteAddressModel
    {
        public string AddressLine1 { get; set; }

        public string Town { get; set; }

        public string? County { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }
        public bool MainAddress { get; set; }
    }
}
