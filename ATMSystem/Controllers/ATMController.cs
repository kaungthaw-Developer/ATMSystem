using ATMSystem.EFDbContext;
using ATMSystem.Models;
using Microsoft.AspNetCore.Mvc;
using IdGen;
using Microsoft.EntityFrameworkCore;

namespace ATMSystem.Controllers
{
    public class ATMController : Controller
    {
        private readonly AppDbContext _appDbContext;
       
        private IdGenerator _idGenerator = new IdGenerator(0);

        public ATMController(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }

        // Property with a private setter
        public IdGenerator IdGenerator
        {
            get { return _idGenerator; }
            private set { _idGenerator = value; }
        }


        public IActionResult home() 
        { 
            return View();
        }
        [ActionName("signIN")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signIn)
        {
            bool logIn = await _appDbContext.Registers.AsNoTracking().AnyAsync(x => x.CardNumber == signIn.CardNumber && x.Password == signIn.Password && x.Active == true);

            if (logIn)
            {
                var userInform = await _appDbContext.Registers.AsNoTracking().FirstOrDefaultAsync(x => x.CardNumber == signIn.CardNumber);
                if (userInform != null)
                {

                    return View("UserAcount", userInform);
                }
                return View();
            }
            else
            {
                return View();
            }

        }
        public IActionResult UserAcount()
        {
            return View();
        }
        public IActionResult create()
        {
            return View();
        }
        [ActionName("Save")]
        [HttpPost]
        public async Task<IActionResult> Registratioin(UserRegisterModel userRegister)
        {
            var number=await _appDbContext.Registers.AsNoTracking().FirstOrDefaultAsync(x=>x.PhoneNumber==userRegister.PhoneNumber);
            if (number!=null)
            {
                return Redirect("create");
            }
            _idGenerator = new IdGenerator(0);
            userRegister.UserID = _idGenerator.CreateId().ToString();
            userRegister.Active = true;
            userRegister.TotalAmount = 0;
            userRegister.CardNumber =Convert.ToInt64( _idGenerator.CreateId().ToString());
            await _appDbContext.Registers.AddAsync(userRegister);
            var result=await _appDbContext.SaveChangesAsync();

            return RedirectToAction("home");
        }


        [HttpPost]
        public async Task<IActionResult> Histroy(string userID)
        {
           
            bool logIn = await _appDbContext.History.AsNoTracking().AnyAsync(x => x.UserID == userID );
            if (logIn)
            {
                List<UserHistoryModel> userHistories = await _appDbContext.History.AsNoTracking().Where(x => x.UserID == userID).OrderByDescending(x => x.LastUpdate).ToListAsync();
               var amount=await _appDbContext.Registers.AsNoTracking().Where(x=>x.UserID== userID).Select(x => new {x.TotalAmount}).FirstOrDefaultAsync();
                var history = new
                {
                    UserHistories = userHistories,
                    Amount = amount
                };
                return Json(history);
            }
            return View();
        }
        public async Task<IActionResult> Nextpage(string userID)
        {
            bool logIn = await _appDbContext.Registers.AsNoTracking().AnyAsync(x => x.UserID == userID && x.Active == true);
            if (logIn)
            {
                var userInform = await _appDbContext.Registers.AsNoTracking().FirstOrDefaultAsync(x => x.UserID == userID && x.Active == true);

                var viewModel = new UserRegisterModel
                {
                    UserID = userInform.UserID,
                };
                return Json("cashOut");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CashOut()
        
        {
           
            return View();
        }
        public IActionResult CardLess()
        {
            return View();
        }
        [ActionName("reqeust")]
        [HttpPost]
        public async Task<IActionResult> CashOut(CashOutModel cashOut)
        {
            try
            {
                var withDraw = await _appDbContext.Registers.FirstOrDefaultAsync(x =>x.UserID==cashOut.UserID && x.CardNumber == cashOut.CardNumber && x.PinCode == cashOut.PinCode);
                if (withDraw != null)
                {
                    withDraw.TotalAmount -= cashOut.MoneyAmount;
                    UserHistoryModel history = new UserHistoryModel()
                    {
                        HistoryID = _idGenerator.CreateId().ToString(),
                        UserID = cashOut.UserID,
                        CashOutAmount = cashOut.MoneyAmount,
                        LastUpdate = DateTime.Now
                    };

                    await _appDbContext.History.AddAsync(history);


                }
                var result = await _appDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
         
            return View();
        }
       

    }
}
