using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MMGD.Models;
using Microsoft.EntityFrameworkCore;

namespace mmgd.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class mmgdController : ControllerBase
    {
        private readonly interviewContext _context;

        public mmgdController(interviewContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            try
            {
                var result = await _context.userData.ToListAsync();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("loginCheck")]
        //登入驗證
        public async Task<ObjectResult> LoginCheck(searchLoginState data)
        {
            try
            {
                var checkTmp = await (from user in _context.userData
                                      where
                                      user.email == data.user_email &&
                                      user.pwd == data.user_password
                                      select user).FirstOrDefaultAsync();



                if (checkTmp != null)
                {
                    var login = new loginState
                    (
                        checkTmp.username,
                        checkTmp.email,
                        true
                    );

                    //var login2 = new loginState
                    //(
                    //    login.user_name,
                    //    "test@com",
                    //    login.Authorized
                    //);


                    //var login3 = new loginState2()
                    //{
                    //    //user_name = checkTmp.username,
                    //    Authorized = true,
                    //    user_email = checkTmp.email,
                    //};
                    //login3.user_email = "test@com";

                    return StatusCode(StatusCodes.Status200OK, login);
                }
                else
                {
                    var login = new loginState
                    (
                        "",
                        "",
                        false
                    );
                    return StatusCode(StatusCodes.Status401Unauthorized, login);
                }

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //通知
        [HttpPost("notice")]
        public async Task<ObjectResult> membernotice(noticeState data)
        {
            try
            {
                var tmp = await (from message in _context.message_floor
                                 join article in _context.article on message.article_number equals article.article_number
                                 join userData in _context.userData on message.username equals userData.username
                                 where article.article_number == message.article_number &&
                                 data.user_email == article.email
                                 orderby Convert.ToInt32(message.msg_floor)
                                 select message.msg_content).ToListAsync();
                return StatusCode(StatusCodes.Status200OK, tmp);
            }                
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
    }
    //login
    public record searchLoginState(string user_email, string user_password);
    public record loginState(string user_name, string user_email, bool Authorized);
    //notice
    public record noticeState(string user_email);

    //預設值
    //loginState("test@com", false);
    //loginState( "test@com", false, "tests");


    //public class loginState2
    //{
    //    public string user_name { get; init; } = string.Empty;
    //    public string user_email { get; set; } = string.Empty;
    //    public bool Authorized { get; init; } = false;
    //};

}
