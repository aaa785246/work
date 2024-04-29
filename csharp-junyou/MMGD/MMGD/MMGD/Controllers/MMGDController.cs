using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MMGD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
    }




    //login
    public record searchLoginState(string user_email, string user_password);
    public record loginState(string user_name, string user_email, bool Authorized);
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
