using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MMGD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using BCrypt;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Principal;
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

        //雜湊
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


        //註冊
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
        [HttpPost("modeifyPwd")]
        public async Task<ObjectResult> modeifyPwd(modeifyPwd data)
        {
            try
            {
                var email = await (from userData in _context.userData
                                   where data.user_email == userData.email
                                   select userData).FirstOrDefaultAsync();
                email.pwd = HashPassword(data.user_pwd);
                
                _context.userData.Update(email);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "密碼更新成功");
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //登入驗證
        [HttpPost("loginCheck")]
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
                    string.IsNullOrWhiteSpace(data.article_number.ToString()) || string.IsNullOrWhiteSpace(data.msg_content))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "輸入內容不可為空");
                }
                //目前的留言樓數
                var dataFloor = await (from message in _context.message_floor
                                       where message.article_number == data.article_number
                                       orderby Convert.ToInt32(message.msg_floor) descending
                                       select message.msg_floor
                                ).FirstOrDefaultAsync();
                var likeCount = await (from i in _context.interaction_article
                                       where data.article_number == i.article_number
                                       select i).FirstOrDefaultAsync();

                var param_message = new message_floor
                {
                    username = data.msg_userName,
                    msg_floor = Convert.ToString(Convert.ToInt32(dataFloor) + 1),
                    article_number = data.article_number,
                    msg_content = data.msg_content
                };




                var param_notice = new notice
                {
                    email = data.poster_email,
                    message_username = data.msg_userName,
                    article_number = data.article_number,
                    msg_floor = Convert.ToString(Convert.ToInt32(dataFloor) + 1),
                    message_content = data.msg_content,
                    viewed = "N"
                };

                likeCount.message_couter = Convert.ToString(Convert.ToInt32(dataFloor) + 1);

                _context.interaction_article.UpdateRange(likeCount);
                _context.message_floor.AddAsync(param_message);
                _context.notice.AddAsync(param_notice);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, likeCount);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //搜尋文章
        [HttpPost("articlesearch")]
        public async Task<ObjectResult> articleSearch(articleName data)
        {
            var tmp = await (from article in _context.article
                             join interaction in _context.interaction_article on article.article_number equals interaction.article_number
                             where string.IsNullOrWhiteSpace(data.article_name) || article.title.Contains(data.article_name)
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
        //點擊增加愛心
        [HttpPost("likelycount")]
        public async Task<ObjectResult> likelycount(likelyCount data)
        {
            try
            {   
                //把該文章的愛心數抓出來
                var number = await (from i in _context.interaction_article
                                    where data.articlenumber == i.article_number
                                    select i).ToListAsync();

                int count = int.Parse(number[0].like_couter);
                //該使用者喜歡的文章 資料表
               
                var likeData = await (from like in _context.like_article
                                  where data.articlenumber == like.article_number
                                  select like).FirstOrDefaultAsync();

                if (data.count_like == true)
                {
                    if (likeData == null)
                    {
                        var like_article = new like_article
                        {
                            email = data.user_email,
                            article_number = data.articlenumber
                        };
                    count++;
                    _context.like_article.Add(like_article);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status409Conflict, "該用戶已經按過讚");
                    }
                }
                else
                {
                count--;
                _context.like_article.RemoveRange(likeData);
                }

                number[0].like_couter = count.ToString();
                _context.interaction_article.UpdateRange(number);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, number);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //已經喜歡的要亮起
        [HttpPost("Islike")]
        public async Task<ObjectResult> isLike(isLike data)
        {
            try
            {
                //把該文章的愛心數抓出來
                var isLike = await (from like in _context.like_article
                                    where data.articlenumber == like.article_number&&
                                    data.user_email == like.email
                                    select like).FirstOrDefaultAsync();
                if (isLike != null)
                {
                return StatusCode(StatusCodes.Status200OK, true);
                }else
                {
                    return StatusCode(StatusCodes.Status200OK, false);
                }




            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Iscollect")]
        public async Task<ObjectResult> Iscollect(collectArticle data)
        {
            try
            {
                var collectArticle = await (from collect in _context.collect_article
                                            where data.articlenumber == collect.article_number &&
                                            data.user_email == collect.email
                                            select collect).FirstOrDefaultAsync();
                if (collectArticle != null)
                {
                    return StatusCode(StatusCodes.Status200OK, true);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, false);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("collectarticle")]
        public async Task<ObjectResult> collectarticle(collectArticle data)
        {
            try
            {
                string notice = "";
                var collectArticle = await (from collect in _context.collect_article
                                    where data.articlenumber == collect.article_number &&
                                    data.user_email == collect.email
                                    select collect).FirstOrDefaultAsync();
                if (collectArticle == null)
                {
                    var collect = new collect_article
                    {
                        article_number = data.articlenumber,
                        email = data.user_email
                    };
                    _context.collect_article.Add(collect);
                    notice = "成功新增";
                }else
                {
                    _context.collect_article.Remove(collectArticle);
                    notice = "成功刪除";
                }

                    await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, notice);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //忘記密碼功能先搜尋信箱是否註冊過
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
        //新增文章
        [HttpPost("newarticle")]
        public async Task<ObjectResult> newarticle(newArticle data)
        {
            try
            {
                var articleNumber = await (from article in _context.article
                                           select article.article_number)
                                           .OrderByDescending(x=>x)
                                           .FirstOrDefaultAsync();
                if (articleNumber == null)
                {
                    articleNumber = 0;
                }
                var tmp = new article
                {
                    article_number = articleNumber + 1,
                    title = data.title,
                    article_content = data.article_content,
                    email = data.user_email,
                    username = data.user_name
                };

                var interaction = new interaction_article
                {
                    article_number = articleNumber + 1,
                    like_couter = "0",
                    message_couter = "0"
                };
                var message_floor = new message_floor
                {
                    username = "0",
                    msg_floor = "0",
                    article_number = articleNumber + 1 ,
                    msg_content= "0"
                    
                };

                _context.article.AddAsync(tmp);
                _context.interaction_article.AddAsync(interaction);
                _context.message_floor.AddAsync(message_floor);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, tmp); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
        //根據文章號碼去取得文章內容
        [HttpPost("searcharticlenumber")]
        public async Task<ObjectResult> searcharticlenumber(articleNumber data )
        {
            try
            {
                var number = await (from article in _context.article
                                    join interaction in _context.interaction_article on article.article_number equals interaction.article_number
                                    where data.articlenumber == article.article_number
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


                return StatusCode(StatusCodes.Status200OK, number);
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }

        //抓留言
        [HttpPost("articlemessage")]
        public async Task<ObjectResult> articlemessage(messageFloor data)
        {
            try
            {
                var floor = await (from message in _context.message_floor
                                   where data.articlenumber == message.article_number &&
                                   message.msg_floor != "0"
                                   select message).ToListAsync();


                return StatusCode(StatusCodes.Status200OK, floor);
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
        //刪除留言
        [HttpPost("deletemessage")]
        public async Task<ObjectResult> deletemessage(deleteMessageFloor data)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                //訊息往後的樓數降低(messageFloor表)
                var floorLower = await (from message in _context.message_floor
                                   where data.articlenumber == message.article_number &&
                                   data.msg_floor < Convert.ToInt32(message.msg_floor)  
                                   select message).ToListAsync();
                //要刪除的訊息(messageFloor表)
                var deleteFloor = await (from message in _context.message_floor
                                        where data.articlenumber == message.article_number &&
                                        data.msg_floor == Convert.ToInt32(message.msg_floor)
                                        select message).FirstOrDefaultAsync();
                //要刪除的訊息(notice表)
                var deleteNotice = await (from notice in _context.notice
                                          where data.articlenumber == notice.article_number &&
                                          data.msg_floor == Convert.ToInt32(notice.msg_floor)
                                          select notice).FirstOrDefaultAsync();
                //訊息樓數降低(interaction_article表)
                var interaction = await (from inter in _context.interaction_article
                                         where data.articlenumber == inter.article_number
                                         select inter).FirstOrDefaultAsync();

                var noticeLower = await (from notice in _context.notice
                                    where data.articlenumber == notice.article_number &&
                                    data.msg_floor < Convert.ToInt32(notice.msg_floor)
                                    select notice).ToListAsync();

                _context.message_floor.Remove(deleteFloor);
                _context.notice.Remove(deleteNotice);

                foreach (var item in floorLower)
                {
                    var newFloor = new message_floor
                    {
                        username = item.username,
                        article_number = item.article_number,
                        msg_floor = (Convert.ToInt32(item.msg_floor) - 1).ToString(),
                        msg_content = item.msg_content
                    };

                    _context.message_floor.Remove(item);
                    _context.message_floor.Add(newFloor);
                }

                foreach (var item in noticeLower)
                {
                    var newNotice = new notice
                    {
                        email = item.email,
                        message_username = item.message_username,
                        article_number = item.article_number,
                        message_content = item.message_content,
                        viewed = item.viewed,
                        msg_floor = (Convert.ToInt32(item.msg_floor) - 1).ToString()
                    };
                    _context.notice.Remove(item);
                    _context.notice.Add(newNotice);

                }

                interaction.message_couter = (Convert.ToInt32(interaction.message_couter) - 1).ToString();

                _context.interaction_article.Update(interaction);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return StatusCode(StatusCodes.Status200OK, "成功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //修改留言
        [HttpPost("modeifymessage")]
        public async Task<ObjectResult> modeifymessage(modeifymessage data)
        {
            try
            {
                var modeifymsg = await (from msg in _context.message_floor
                                        join no in _context.notice on new { msg.article_number, msg.msg_floor } equals new { no.article_number, no.msg_floor }
                                        where data.articlenumber == msg.article_number &&
                                        data.msg_floor == Convert.ToInt32(msg.msg_floor)
                                        select new { msg, no }).FirstOrDefaultAsync();

                modeifymsg.msg.msg_content = data.msg_content;
                modeifymsg.no.message_content = data.msg_content;

                _context.message_floor.UpdateRange(modeifymsg.msg);
                _context.notice.UpdateRange(modeifymsg.no);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, modeifymsg);
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
        //個人文章
        [HttpPost("personalArticle")]
        public async Task<ObjectResult> personalArticle(personalArticle data)
        {
            try
            {
                //個人貼文
                if(data.content == "1")
                {
                    var personal = await (from article in _context.article
                                          join interaction in _context.interaction_article on article.article_number equals interaction.article_number
                                          where data.user_email == article.email
                                          select new
                                          {
                                              article.article_number,
                                              article.title,
                                              article.article_content,
                                              interaction.like_couter,
                                              interaction.message_couter
                                          }).ToListAsync();
                    return StatusCode(StatusCodes.Status200OK, personal);
                }
                else if(data.content == "2")
                {
                    //var collect = await (from article in _context.article
                    //                     join collectArticle in _context.collect_article on article.email equals collectArticle.email
                    //                     join interaction in _context.interaction_article on article.article_number equals interaction.article_number
                    //                     where data.user_email == collectArticle.email
                    //                     select new
                    //                     {
                    //                         article.article_number,
                    //                         article.title,
                    //                         article.article_content,
                    //                         interaction.like_couter,
                    //                         interaction.message_couter
                    //                     }).ToListAsync();

                    var collect = await (from collectArticle in _context.collect_article
                                         join article in _context.article on collectArticle.article_number equals article.article_number
                                         join interaction in _context.interaction_article on collectArticle.article_number equals interaction.article_number
                                         where data.user_email == collectArticle.email
                                         select new
                                         {
                                             collectArticle.article_number,
                                             article.title,
                                             article.article_content,
                                             interaction.like_couter,
                                             interaction.message_couter
                                         }).ToListAsync();

                    return StatusCode(StatusCodes.Status200OK, collect);
                }



                return StatusCode(StatusCodes.Status200OK, "無傳入content");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "錯誤");
            }
        }
    }


    //register 
    public record registerState(string userName, string email, string password);
    //modeifyPwd
    public record modeifyPwd(string user_email ,string user_pwd);
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
    public record addMessage(string poster_email, string msg_userName, int article_number, string msg_content);

    //article
    public record articleName(string article_name);
    public class allArticle {
        public string username { get; set; }
        public string email { get; set; }
        public int article_number { get; set; }
        public string article_title { get; set; }
        public string article_content { get; set; }
        public string like_count { get; set; }
        public string message_count { get; set; }
    }
    //existed
    public record existedEmail(string user_email);
    //newarticle
    public record newArticle(string title,string article_content,string user_email,string user_name);
    //searcharticlenumber
    public record articleNumber(int articlenumber);
    //likelycount
    public record likelyCount(string user_email,int articlenumber,bool count_like);
    //isLike
    public record isLike(string user_email, int articlenumber);
    public record messageFloor(int articlenumber);
    
    public record deleteMessageFloor(int articlenumber,int msg_floor);

    public record modeifymessage(int articlenumber, int msg_floor,string msg_content);

    public record collectArticle(int articlenumber,string user_email);

    public record personalArticle(string content, string user_email);
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
