using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Providers.Creator
{
    public class PaymentLoadgmenrServices :IDisposable
    {
        public Payment_Lodgment_RSPNS GETTrasByID(Payment_Lodgment_RQST pl)
        {
            try
            {
                //  using (Connection db = new Connection())
                // {
                //   SqlConnection con = db.getDatabaseConnection();
                Payment_Lodgment_RSPNS aprv = new Payment_Lodgment_RSPNS();
                // Get user aprv dt
                aprv.Payment_Lodgment_RefNo = "Asjahdhshf04735";
                aprv.Reponse_Code = "Valid";
                aprv.Reponse_Message="fgfg";
                aprv.Reponse_Status = "ff";
                return aprv;
                //}

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Dispose()
        {

        }
    }
}