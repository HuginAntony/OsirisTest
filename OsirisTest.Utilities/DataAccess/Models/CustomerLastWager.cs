using System;
using OsirisTest.Utilities.DataAccess.Models.Base;

namespace OsirisTest.Utilities.DataAccess.Models
{
    public class CustomerLastWager: BaseModel
    {
        public decimal LastWagerAmount { get; set; }

        public DateTime LastWagerDateTime { get; set; }
    }
}
