using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MMGD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using BCrypt;
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

        [HttpGet("test-non-database")]
        public async Task<ActionResult> Test2()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, "測試");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

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

        private string HashPassword(string password)
        {
            // 生成随机的salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            // 使用salt哈希密码
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            // 使用哈希后的密码和明文密码进行比较
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        [HttpPost("register")]
        public async Task<ObjectResult> register(registerState data)
        {
            try
            {
                //信箱驗證
                var accoutCheck = await (from user in _context.userData
                                        where user.email == data.email ||
                                        user.username == data.userName
                                        select user).ToListAsync();
                if (accoutCheck.Any())
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "信箱或名稱重複");
                }
                else
                {
                    var reg = new userData
                    {
                        email = data.email,
                        username = data.userName,
                        pwd = HashPassword(data.password)
                    };
                    _context.userData.AddAsync(reg);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, reg);
                }


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

                //取出資料庫加鹽密碼
                var saltPwd = await (from user in _context.userData
                                      where
                                      user.email == data.user_email
                                      select user.pwd).FirstOrDefaultAsync();

                


                if (saltPwd == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "找不到該筆電子郵件");
                }
                else
                {  
                    
                    var verify = VerifyPassword(data.user_password, saltPwd);
                    if (verify == true)
                    {
                        var userName = await (from user in _context.userData
                                              where user.email == data.user_email
                                              select user.username).FirstOrDefaultAsync();

                        var state = new loginState
                        {
                            user_name = userName,
                            verify = verify
                        };

                    return StatusCode(StatusCodes.Status200OK, state);
                    }

                  
                    return StatusCode(StatusCodes.Status401Unauthorized, verify);
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
                var tmp = await (from notice in _context.notice
                                 where data.user_email == notice.email
                                 orderby Convert.ToInt32(notice.msg_floor)
                                 select new { notice.message_username, notice.message_content, notice.article_number, notice.viewed }).ToListAsync();
                return StatusCode(StatusCodes.Status200OK, tmp);
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
        //關閉通知
        //關閉通知後把現有留言的viewed改成Y
        [HttpPost("closenotice")]
        public async Task<ObjectResult> viewedState(noticeState data)
        {
            try
            {
                var tmp = await (from notice in _context.notice
                                 where data.user_email == notice.email 
                                 select notice)
                                 .ToListAsync();

                foreach (var item in tmp)
                {
                    item.viewed = "Y";
                }
                _context.notice.UpdateRange(tmp);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, tmp);
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }


        //新增留言並同步增加到通知表
        [HttpPost("addmessage")]
        public async Task<ObjectResult> addMessage(addMessage data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.msg_userName) ||
                    string.IsNullOrWhiteSpace(data.article_number) || string.IsNullOrWhiteSpace(data.msg_content))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "輸入內容不可為空");
                }
                //目前的留言樓數
                var dataFloor = await (from message in _context.message_floor
                                       where message.article_number == data.article_number
                                       orderby Convert.ToInt32(message.msg_floor) descending
                                       select message.msg_floor
                                ).FirstOrDefaultAsync();
                //發文者
                //var poster = await (from userData in _context.userData
                //                    join article in _context.article on userData.username equals article.username
                //                    where 
                //                    select userData.email).FirstOrDefaultAsync();



                var param_message = new message_floor
                {
                    username = data.msg_userName,
                    msg_floor = Convert.ToString(Convert.ToInt32(dataFloor) + 1),
                    article_number = data.article_number,
                    msg_content = data.msg_content
                };




                var param_notice = new notice
                {
                    email = data.poster,
                    message_username = data.msg_userName,
                    article_number = data.article_number,
                    msg_floor = Convert.ToString(Convert.ToInt32(dataFloor) + 1),
                    message_content = data.msg_content,
                    viewed = "N"
                };

                _context.message_floor.AddAsync(param_message);
                _context.notice.AddAsync(param_notice);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "OK");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
        [HttpPost("articlesearch")]
        public async Task<ObjectResult>articleSearch(articleName data)
        {
            var tmp =await (from article in _context.article
                            join interaction in _context.interaction_article on article.article_number equals interaction.article_number
                            where string.IsNullOrWhiteSpace(data.article_name) ||article.title.Contains(data.article_name)
                            select new allArticle()
                            {
                                username = article.username,
                                email = article.email,
                                article_number = article.article_number,
                                article_title = article.title,
                                article_content = article.article_content,
                                like_count = interaction.like_couter,
                                message_count = interaction.message_couter
                            }).ToListAsync();
            return Ok(tmp);
        }
        [HttpPost("existed")]
        public async Task<ObjectResult> existedEmail(existedEmail data)
        {
            try
            {
                var tmp = await (from userData in _context.userData
                                 where data.user_email == userData.email
                                 select userData.email).ToListAsync();
                if(tmp.Any())
                {
                return StatusCode(StatusCodes.Status200OK, tmp);
                }
                return StatusCode(StatusCodes.Status200OK, "該信箱沒有註冊");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }

    }


    //register 
    public record registerState(string userName, string email, string password);
    //login
    public record searchLoginState(string user_email, string user_password);
    public class loginState
    {
        public string user_name { get; set; }
        public bool verify { get; set; }
    }
    //notice
    public record noticeState(string user_email);
    

    //addMessage
    public record addMessage(string poster, string msg_userName, string article_number, string msg_content);

    //article
    public record articleName(string article_name);
    public class allArticle {
        public string username { get; set; }
        public string email { get; set; }
        public string article_number { get; set; }
        public string article_title { get; set; }
        public string article_content { get; set; }
        public string like_count { get; set; }
        public string message_count { get; set; }
    }
    //existed
    public record existedEmail(string user_email);

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
