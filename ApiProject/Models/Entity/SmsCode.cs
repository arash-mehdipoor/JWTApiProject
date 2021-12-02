using System;

namespace ApiProject.Models.Entity
{
    public class SmsCode
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
        public int RequestCount { get; set; }
        public DateTime ExpierDate { get; set; }
    }
}
