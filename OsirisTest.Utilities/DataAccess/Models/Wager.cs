using System;
using OsirisTest.Utilities.DataAccess.Models.Base;

namespace OsirisTest.Utilities.DataAccess.Models
{
    public class Wager : BaseModel
    {
        
        public Guid WagerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime WagerDateTime { get; set; }
    }
}
