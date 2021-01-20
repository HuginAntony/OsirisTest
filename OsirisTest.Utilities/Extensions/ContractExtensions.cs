using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Utilities.Extensions
{
    public static class ContractExtensions
    {
        public static bool IsValidWager(this Wager wager)
        {
            return wager.Amount > 7.0m;
        }
    }
}
