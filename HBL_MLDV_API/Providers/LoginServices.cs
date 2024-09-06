using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using HBL_MLDV_API.Models.Security;
using HBL_MLDV_API.Providers;

namespace BusinessApi.Providers
{
    public class LoginServices : IDisposable
    {
        EncryptDecrypt crypt = new EncryptDecrypt();
       
        private async Task<UserAuthRepository> GenerateSession(vu_users model,string token) {
            UserAccountModel obj = new UserAccountModel();
            DataTable dt = new DataTable();
            List<UserAccountDetails> detail = new List<UserAccountDetails>();
            string query = "SELECT * from vu_user_urls  where id=" + model.user_sk + "";
            using (DbContextHelper _ctx  = new DbContextHelper())
            {
                dt = _ctx.SelectDataTable(query);
            }
            obj.UserId = model.user_sk;
            obj.UserFullName = model.username;
            obj.UserFullName_Dec = crypt.Decrypt(model.username);
            obj.Password = model.password;
            obj.Password_Dec = crypt.Decrypt(model.password);
            obj.Token = token;
            foreach (DataRow dr in dt.Rows)
            {
                UserAccountDetails ud = new UserAccountDetails();
                ud.UserId = Convert.ToInt32(dr["id"]);
                ud.CanEdit = Convert.ToInt32(dr["can_edit"]);
                ud.CanView = Convert.ToInt32(dr["can_view"]);
                ud.CanAdd = Convert.ToInt32(dr["can_add"]);
                ud.CanDel = Convert.ToInt32(dr["can_del"]);
                ud.AcivityId = Convert.ToInt32(dr["activityid"]);
                ud.AcivityName = dr["activitydisc"].ToString();
                ud.AcivityTitle = dr["activitytitle"].ToString();
                ud.ActivityUrl = dr["activityurl"].ToString();
                ud.ActivityParentId = Convert.ToInt32(dr["activityparentid"]);
                ud.ActivityIcon = dr["activiticon"].ToString();
                detail.Add(ud);
            }
            return new UserAuthRepository { UserAccountObj = obj, UserAccountDetailObj = detail };
          }

       

          public void Dispose()
          {

          }
    }
}
