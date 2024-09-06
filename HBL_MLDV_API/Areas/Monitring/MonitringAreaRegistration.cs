using System.Web.Mvc;

namespace HBL_MLDV_API.Areas.Monitring
{
    public class MonitringAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Monitring";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Monitring_default",
                "Monitring/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}