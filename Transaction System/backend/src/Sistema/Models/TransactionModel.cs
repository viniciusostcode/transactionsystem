using Sistema.Models.Enums;

namespace Sistema.Models
{
    public class TransactionModel
    {
        public int? Id { get; set; }
        public string Price { get; set; }
        public string Situation { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
        public string? Coin { get; set; }
        public string? Profit { get; set; }
        public int? UserId { get; set; }
        public virtual UserModel? User { get; set; }

    }
}
