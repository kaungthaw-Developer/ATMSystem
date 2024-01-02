using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMSystem.Models
{
    [Table("User_Register")]
    public class UserRegisterModel
    {
        [Key]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber {  get; set; }
        public string Email { get; set; }
        public int PinCode {  get; set; }
        public decimal TotalAmount {  get; set; }
        public string Password { get; set; }
        public long CardNumber {  get; set; }
        public bool Active {  get; set; }
    }
}
