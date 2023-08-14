using DocnetCorePractice.Data.Entity;
using DocnetCorePractice.Enum;
using DocnetCorePractice.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Xml.Linq;
using ILogger = Serilog.ILogger;

namespace DocnetCorePractice.Controllers
{
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly ILogger _logger;

        public ActionController()
        {
            _logger = Log.Logger;
        }

        private static List<UserEntity> users = new List<UserEntity>()
        {
            new UserEntity()
            {
                Id = Guid.NewGuid().ToString("N"),
                FirstName = "huy",
                LastName = "nguyen",
                Sex = Enum.Sex.Male,
                Address = "Ho chi Minh",
                Balance = 100000,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0123456789",
                Roles = Enum.Roles.Basic,
                TotalProduct = 0
            }
        };

        private static List<CaffeEntity> caffes = new List<CaffeEntity>()
        {
            new CaffeEntity()
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Ca phe sua",
                Price = 20000,
                Discount = 10,
                Type = Enum.ProductType.A,
                IsActive = true
            }
        };

        [HttpGet]
        [Route("api/[controller]/get-all")]
        public IActionResult getAll()
        {
            return Ok(caffes);
        }


        // 1. Viết API insert thêm caffe mới vào menu với input là CaffeModel, kiểm tra điều kiện:
        //      - Name và Id không tồn tại trong CaffeEntity (nếu không thỏa return code 400)
        //      - Price hoặc discount >= 0 (nếu không thỏa return code 400)
        //   Nếu có điều kiện nào vi phạm thì không insert và return failed.

        [HttpPost]
        [Route("api/[controller]/get-insert-caffe")]
        public IActionResult getinsert([FromBody] CaffeModel newCaffe)
        {
            try
            {

            var caffe = new CaffeEntity()
            {
                Id = newCaffe.Id,
                Name = newCaffe.Name,
                Price = newCaffe.Price,
                Discount = newCaffe.Discount,
                IsActive = true,
            };

            if (caffes.Any(_ => _.Id == caffe.Id && _.Name == caffe.Name
            || newCaffe.Price < 0 || newCaffe.Discount < 0))
            {
                return BadRequest(400);
            }
            caffes.Add(caffe);
            return Ok(caffes);
            }
            catch (Exception ex) {return BadRequest(500); }
        }

        // 2. Viết API get all caffe có IsActive = true theo CaffeModel. nếu không có caffe nào thì return code 204

        //[HttpGet]
        //[Route("api/[controller]/get-all-caffe")]
        //public IActionResult getallCaffee([FromBody] CaffeModel newCaffe)
        //{
        //    var caffe = new CaffeEntity()
        //    {
        //        IsActive = true,
        //    };

        //    if (caffes.Any(_ => _.IsActive == caffe.IsActive == true))
        //    {
        //        return Ok(caffes);
        //    }
        //    return BadRequest(204);
        //}

        // 3. Viết API get detail caffe có input là Id với điều kiện isActive bằng true. Nếu không có user nào thì return code 204

        [HttpGet]
        [Route("api/[controller]/get-detail-caffe/{id}")]
        public IActionResult getdetail([FromBody] string Id)
        {
            try
            {

                var cafe = caffes.Where(p => p.Id == Id).FirstOrDefault();
                if (cafe != null && caffes.Any(p => p.IsActive == true))
                {
                    return Ok(caffes);
                }
                return BadRequest(204);           
            }
            catch (Exception ex) {return BadRequest(500); }
        }

        // 4. Viết API update caffe với input là Id, price và discount. kiểm tra điều kiện:
        //      - Id tồn tại trong CaffeEntity (nếu không thỏa return code 404)
        //      - Price hoặc discount >= 0 (nếu không thỏa return code 400)
        //   Nếu có điều kiện nào vi phạm thì không insert và return failed.

        [HttpPut]
        [Route("api/[controller]/get-update-caffe")]
        public IActionResult updatecafe([FromBody] CaffeModel newCaffe)
        {
            try
            {

            var caffe = new CaffeEntity()
            {
                Id = newCaffe.Id,
                Name = newCaffe.Name,
                Price = newCaffe.Price,
                Discount = newCaffe.Discount,
            };
            if (caffes.Any(_ => _.Id != caffe.Id))
            {
                return BadRequest(404);
            }
            if (newCaffe.Price < 0 || newCaffe.Discount < 0 )
            {
                return BadRequest(400);
            }
            var cafe = caffes.Where(_ => _.Id == newCaffe.Id).FirstOrDefault();
                cafe.Id = newCaffe.Id;
                cafe.Price = newCaffe.Price;
                cafe.Discount = newCaffe.Discount;
                return Ok(caffes);            
            }
            catch (Exception ex) { return BadRequest(500); }
        }

        // 5. Viết API Delete caffe với input là Id. Caffe sẽ được delete nếu thỏa điều kiện sau:
        //      - Id tồn tại trong CaffeEntity (nếu không thỏa return code 400)

        [HttpDelete]
        [Route("api/[controller]/get-delete-caffe")]
        public IActionResult getdelete([FromBody] string Id)
        {
            try
            {

            var cafe = caffes.Where(_ => _.Id ==Id).FirstOrDefault();
            if (cafe != null)
            {
                caffes.Remove(cafe);
                return Ok(caffes);
            }
            return BadRequest(400);
            }
            catch (Exception ex) {return BadRequest(500); }
        }
        // 6.Viết API insert thêm user mới với input là UserModel, kiểm tra điều kiện:
        //      - PhoneNumber và Id không tồn tại trong UserEntity (nếu không thỏa return code 400)
        //      - ngày sinh không được nhập quá Datatime.Now (nếu không thỏa return code 400)
        //      - PhoneNumber phải đúng 10 ký tự (nếu không thỏa return code 400)
        //      - Balance hoặc TotalProduct >= 0 (nếu không thỏa return code 400)
        //  Nếu có điều kiện nào vi phạm thì không insert và return failed.

        [HttpPost]
        [Route("api/[controller]/get-insert-user")]
        public IActionResult insertUser( UserModel user)
        {
            try
            {

            var use = new UserEntity()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Balance = user.Balance,
                TotalProduct = user.TotalProduct,
                Roles = Enum.Roles.Vip1,

            };
            if(users.Any(p => p.Id == user.Id) && users.Any(p => p.PhoneNumber == user.PhoneNumber)
               && users.Any(p => p.DateOfBirth > DateTime.Now) && user.PhoneNumber.Length != 10 
               && ( user.Balance < 0 || user.TotalProduct < 0))
            {
                return BadRequest(400);
            }
            users.Add(use);
            return Ok(users);
            }
            catch (Exception ex) {return BadRequest(500); }
        }

        // 7.Viết API get all user data trả về được parse theo UserModel. nếu không có user nào thì return code 204
        [HttpGet]
        [Route("api/[controller]/get-all-user")]
        public IActionResult getalluser()
        {
            try
            {
                var user = from p in users
                select new UserModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                };
                if (users == null)
                {
                    return BadRequest(204);
                }
                return Ok(users);
            }
            catch (Exception ex) { return BadRequest(500); }
        }
        // 8.Với input là ngày sinh(có kiều dữ liệu DateTime) và role(có kiểu dữ liệu Enum), Viết API get users với điều kiện:
        //      - là thành viên vip(có thể là vip1 hoặc vip2) và sinh trong tháng 6
        //  Nếu không có user nào thì return code 204
        //[HttpGet]
        //[Route("api/[controller]/get-DateOfBirth-role")]
        //public IActionResult getDateOfBirth()
        //{
        //    if (users.Any(p => p.DateOfBirth.Month == 6) && users.Any(p => p.Roles == Enum.Roles.Vip1)
        //            || users.Any(p => p.Roles == Enum.Roles.Vip2))
        //        {
        //            return Ok(users);
        //        }
        //    return BadRequest(204);
        //}
        // 9.Viết API update user với input là UserModel, kiểm tra điều kiện:
        //      - Id phải tồn tại trong UserEntity (nếu không thỏa return code 404)
        //      - ngày sinh không được nhập quá Datatime.Now (nếu không thỏa return code 400)
        //      - PhoneNumber phải đúng 10 ký tự (nếu không thỏa return code 400)
        //      - Balance hoặc TotalProduct >= 0 (nếu không thỏa return code 400)
        //  Nếu có điều kiện nào vi phạm thì không update và return code 400 cho client.

        [HttpDelete]
        [Route("api/[Controller]/get-update-user")]
        public IActionResult updateuser(UserModel usermodel)
        {
            try
            {
                var user = new UserEntity()
                {
                    Id = usermodel.Id,
                    FirstName = usermodel.FirstName,
                    LastName = usermodel.LastName,
                    Sex = usermodel.Sex,
                    Address = usermodel.Address,
                    Balance = usermodel.Balance,
                    DateOfBirth = usermodel.DateOfBirth,
                    PhoneNumber = usermodel.PhoneNumber,
                    TotalProduct = usermodel.TotalProduct,
                    Roles = Enum.Roles.Vip1
                };
                if (users.Any(p => p.Id == user.Id) && users.Any(p => p.PhoneNumber == user.PhoneNumber)
                   && users.Any(p => p.DateOfBirth > DateTime.Now) && user.PhoneNumber.Length != 10
                   && (user.Balance < 0 || user.TotalProduct < 0))
                    {
                        return BadRequest(400);
                    }
                var u = users.Where(p => p.Id == usermodel.Id).FirstOrDefault();
                    u.Id = usermodel.Id;
                    u.FirstName = usermodel.FirstName;
                    u.LastName = usermodel.LastName;
                    u.Sex = usermodel.Sex;
                    u.Address = usermodel.Address;
                    u.Balance = usermodel.Balance;
                    u.DateOfBirth = usermodel.DateOfBirth;
                    u.PhoneNumber = usermodel.PhoneNumber;
                    u.TotalProduct = usermodel.TotalProduct;
                    u.Roles = Enum.Roles.Vip1;
                    return Ok(users);
            }
            catch (Exception ex) { return BadRequest(500); }
        }
        // 10. Viết API Delete user với input là Id. User sẽ được delete nếu thỏa các điều kiện sau:
        //      - Id tồn tại trong UserEntity (nếu không thỏa return code 400)
        //      - Balance của user bằng 0 (nếu không thỏa return code 400)

        [HttpDelete]
        [Route("api/[Controller]/get-delete-user/{id}")]
        public IActionResult deteleuser(string id)
        { try
            {
                var user = users.Where(_ => _.Id == id).FirstOrDefault();
                if (id != null && user.Balance == 0)
                {
                    users.Remove(user);
                    return Ok(users);
                }
                return BadRequest(400);
            }
            catch (Exception ex) { return BadRequest(500); }
        }
        // (Lưu ý: các API phải được đặt trong try/catch, nếu API lỗi sẽ return về code 500)
    }
}
