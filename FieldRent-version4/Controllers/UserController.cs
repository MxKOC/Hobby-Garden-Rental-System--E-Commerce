using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FieldRent.Data.Abstract;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Entity;
using FieldRent.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FieldRent.Controllers
{

    public class UserController : Controller
    {

        private IRequestRepository _requestRepository;
        private IMapRepository _mapRepository;
        private IUserRepository _userRepository;
        private IFieldRepository _fieldRepository;
        private BlogContext _context;
        public UserController(BlogContext context, IUserRepository userRepository, IMapRepository mapRepository, IFieldRepository fieldRepository, IRequestRepository requestRepository)
        {
            _mapRepository = mapRepository;
            _fieldRepository = fieldRepository;
            _userRepository = userRepository;
            _requestRepository = requestRepository;
            _context = context;
        }


        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public IActionResult FieldList()
        {
            var fieldList = _fieldRepository.Fields.ToList();

            return View(fieldList);
        }


        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var maps = _mapRepository.Maps;


            maps = maps.Where(i => i.UserId == userId).Include(x => x.Requests).Include(x => x.User);

            var purchaseHistories = _context.PurchaseHistories.Where(i => i.UserId == userId).Include(x => x.ShoppingCart).Include(x => x.Map);



            var viewModel = new UserIndexViewModel
            {
                maps = await maps.ToListAsync(),
                purchaseHistories = await purchaseHistories.ToListAsync()
            };

            return View(viewModel);





            return View(await maps.ToListAsync());
        }




        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            var map = await _mapRepository.Maps.Include(x => x.User).Include(x => x.Requests).FirstOrDefaultAsync(x => x.MapId == id);
            return View(map);
        }









        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public async Task<IActionResult> Rent_Map(int? userid, int? fieldid)
        {
            if (userid == null)
            {
                return NotFound();
            }


            var times = Enum.GetValues(typeof(Duration)).Cast<Duration>();
            ViewBag.TimeList = new SelectList(times);

            var user = await _userRepository.Users.Select(x => new MapEditViewModel
            {
                UserId = x.UserId,
                MapCheckIds = x.Maps,
            }).FirstOrDefaultAsync(p => p.UserId == userid);
            ViewBag.xxx = await _mapRepository.Maps.Where(x => x.UserId != userid && x.MapIsActive == true && x.FieldId == fieldid).ToListAsync(); //.Where(x=>x.UserId!=id) tüm liste için sil
            ViewBag.yyy = await _requestRepository.Requests.ToListAsync();




            return View(user);

        }




        [HttpPost]
        [Authorize(Policy = "NonAdmin")]
        public IActionResult Rent_Map(MapEditViewModel model, int[] MapIds, int[] ReqIds)
        {


            var entity = _userRepository.Users.Include(i => i.Maps).FirstOrDefault(m => m.UserId == model.UserId);

            if (entity == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");




            var ShoppingCartIds = new List<int>();

            var shoppingCartsProperties = _context.Model.FindEntityType(typeof(ShoppingCart)).GetProperties();
            var columnNames = shoppingCartsProperties.Select(property => property.Name).ToList();

            var Enum_Req = ReqIds.Select(id => _requestRepository.Requests.FirstOrDefault(i => i.RequestId == id)).ToList();

            for (int i = 0; i < MapIds.Length; i++)
            {

                var shoppingCart = new ShoppingCart();


                foreach (var item in Enum_Req)
                {
                    if (columnNames.Contains(item.RequestName))
                    {

                        shoppingCart.Price += item.RequestPrice;
                        shoppingCart.GetType().GetProperty(item.RequestName).SetValue(shoppingCart, true);


                    }
                }


                _context.ShoppingCarts.Add(shoppingCart);
                _context.SaveChanges();
                ShoppingCartIds.Add(shoppingCart.Id);
            }






            var time = model.Time;
            var newtime = DateTime.Now;


            if (time == Duration.Daily)
            {
                newtime = DateTime.Now.AddDays(1);
            }
            else if (time == Duration.Weekly)
            {
                newtime = DateTime.Now.AddDays(7);
            }
            else if (time == Duration.Mountly)
            {
                newtime = DateTime.Now.AddMonths(1);
            }
            else if (time == Duration.Yearly)
            {
                newtime = DateTime.Now.AddYears(1);
            }




            var Enum_Maps = MapIds.Select(id => _mapRepository.Maps.FirstOrDefault(i => i.MapId == id)).ToList();
            foreach (var item in Enum_Maps)
            {



                //(Herbiri)Map in time MapStart MapStop ekle
                //(Herbiri)MapIsActive = false





            }

            //Gelen mapi veya map leri user a ekle














            var viewModel = new PaymentDataViewModel
            {
                ShoppingCartIds = ShoppingCartIds,
                UserId = userId,
                MapIds = MapIds,
                Time = time
            };

            return RedirectToAction("Payment", "User", viewModel);
        }


        //Payment

        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public IActionResult Payment(PaymentDataViewModel viewModel)
        {


            PaymentDetail paymentDetail = new PaymentDetail
            {
                ShoppingCartIds = viewModel.ShoppingCartIds,
                UserId = viewModel.UserId,
                MapIds = viewModel.MapIds,
                Time = viewModel.Time
            };



            return View(paymentDetail);
        }

        [HttpPost]
        [Authorize(Policy = "NonAdmin")]
        public IActionResult Payment(PaymentDetail paymentDetail)
        {

            var shoppingCartIds = paymentDetail.ShoppingCartIds;
            var userId = paymentDetail.UserId;
            var MapIds = paymentDetail.MapIds;
            var Time = paymentDetail.Time;

            int Kat=0;
            if (Time == Duration.Daily)
            {
                Kat = 1;
            }
            else if (Time == Duration.Weekly)
            {
                Kat = 7;
            }
            else if (Time == Duration.Mountly)
            {
                Kat = 30;
            }
            else if (Time == Duration.Yearly)
            {
                Kat = 360;
            }

            var shopCarts = _context.ShoppingCarts.Where(e => shoppingCartIds.Contains(e.Id)).ToList();
            var entities = _mapRepository.Maps.Where(e => MapIds.Contains(e.MapId)).ToList();



            if (paymentDetail.CardNumber == "1" && paymentDetail.Cvc == 1
               && paymentDetail.LastMonth == 1 && paymentDetail.LastYear == 1)
            {
                int i = 0;
                foreach (var entity in entities)
                {

                    var purchase = new PurchaseHistory
                    {
                        Price = shopCarts[i].Price + entity.MapPrice*Kat,
                        PurchaseDate = DateTime.Now,
                        ShoppingCart = shopCarts[i],
                        UserId = userId,
                        MapId = entity.MapId,
                        Time = Time
                    };

                    // Purchase i PurchaseHistory tablosuna ekle.
                    _context.PurchaseHistories.Add(purchase);
                    _context.SaveChanges();


                    _mapRepository.EditMap2(

                                    new Map
                                    {
                                        MapId = entity.MapId,
                                        MapIsActive = false
                                    }
                                );


                    i++;
                }


                return RedirectToAction("Index", "User");
            }
            else
            {
                return BadRequest();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////Map kısmı 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////Map kısmı 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////Map kısmı 









        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public async Task<IActionResult> Change_Map(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var map = await _mapRepository.Maps.FirstOrDefaultAsync(p => p.MapId == id);
            ViewBag.mapslist = await _mapRepository.Maps.Where(x => x.UserId != id && x.MapIsActive == true).ToListAsync(); //.Where(x=>x.UserId!=id) tüm liste için sil
            ViewBag.maplist2 = await _mapRepository.Maps.Where(x => x.MapIsActive == true).ToListAsync();
            return View();
        }




        [HttpPost]
        [Authorize(Policy = "NonAdmin")]
        public IActionResult Change_Map(int selectId, int Id)
        {
            var newmap = _mapRepository.Maps.Include(x => x.User).Include(x => x.Requests).FirstOrDefault(p => p.MapId == selectId);
            var oldmap = _mapRepository.Maps.Include(x => x.User).Include(x => x.Requests).FirstOrDefault(p => p.MapId == Id);





            //yeni map e UserId Requests MapIsActive = false MapStart = DateTime.Now, MapStop = oldmap.MapStop, ve time gidiyor



            // Eski mapde isactive true  start stop time null oluyor



            return RedirectToAction("Index");
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////







        /*
                // Kullanıcı mapi seçince maple(field) mapin detayları gelecek.
                [HttpPost]
                [Route("MapDetails")]
                public IActionResult MapDetails(int fieldId)
                {
                    fieldId = 2;
                    var isGot = _context.Fields.Any(x => x.FieldId == fieldId);
                    if (fieldId != 0 && (!isGot))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        var map = _context.Fields.Where(x => x.FieldId == fieldId).First();
                        // Burda fieldId ' e atılan sorguda map listesi boş döndü. O yüzden maps için sorgu eklendi.
                        map.Maps = _context.Maps.Where(x => x.FieldId == fieldId).ToList();
                        return Ok(map);
                    }
                }

        */




        // Kullanıcı profil sayfasına girdiği zaman
        [HttpGet]
        [Authorize(Policy = "NonAdmin")]
        public async Task<IActionResult> UserDetails()
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");


            // User bilgileri gerekiyor.
            var user = await _userRepository.Users.Include(x => x.Maps).FirstOrDefaultAsync(x => x.UserId == userId);
            //var loginUser = _context.Users.Where(x => x.UserId == userId);
            return View(user);
        }


        // Erhan, 











    }
}