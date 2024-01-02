namespace ATMSystem.Models
{
    public class CashOutModel
    {
        public string UserID { get; set; }
        public int CardNumber { get; set; }
        public int PinCode { get; set; }
        public decimal MoneyAmount { get; set; }
        public bool NeedResult { get; set; }
    }
}
