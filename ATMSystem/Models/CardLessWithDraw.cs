using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMSystem.Models
{

    public class CardLessWithDraw
    {
        public int id {  get; set; }
        public string ?phoneNumber {  get; set; }
        public int temPinCode {  get; set; }
        public string password {  get; set; }
        public double moneyAmount {  get; set; }
        
    }
}
