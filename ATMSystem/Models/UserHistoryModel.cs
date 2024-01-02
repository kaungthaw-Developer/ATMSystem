using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMSystem.Models
{
    [Table("User_History")]
    public class UserHistoryModel
    {
        [Key]
        public string HistoryID {  get; set; }
        public string UserID {  get; set; }
        public DateTime LastUpdate {  get; set; }
        public decimal CashOutAmount { get; set; }
    }
}
