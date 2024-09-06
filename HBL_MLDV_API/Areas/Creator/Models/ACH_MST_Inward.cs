using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class ACH_MST_Inward
    {
        [Key]
        public int ach_mst_Id { get; set; }
        public string msg_id { get; set; }

        public string Acct_No { get; set; }
        public DateTime Time_Created { get; set; }

        public string No_of_Transaction { get; set; }

        public string Total_Value { get; set; }

        public DateTime Settlement_Date { get; set; }

        public string Set_Method { get; set; }

        public string Prty { get; set; }

        public string BIC { get; set; }
        public DateTime CRETD_ON { get; set; }
        public int CRETD_BY { get; set; }
        public long DOC_INSTC { get; set; }
        public int IS_Deleted { get; set; }
        public string ACH_Stream { get; set; }
        [Ignore]
        [Skip]
        public string Is_editable { get; set; }
        [Ignore]
        [Skip]
        public string STS_CDE { get; set; }
        [Ignore]
        [Skip]
        public List<ACH_DTL_Inward> dtl { get; set; }
        [Skip]
        [Ignore]
        public List<TITLFTCHNG_RSPNS> Title_Fetching_RSPNS { get; set; }
        //hold work
        [Skip]
        [Ignore]
        public Hold_RSPNS Hold_RSPNS { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        public string CRDT_CNCY { get; set; }
        public string DBT_CNCY { get; set; }
        public string CrTrxCode { get; set; }
        public string DrTrxCode { get; set; }
        public string C36_EXCH_RTS { get; set; }
        public string CRDT_AMT { get; set; }
        public string RESERVE_ACC { get; set; }
    }
    public class ACH_DTL_Inward
    {
        [Ignore]
        public int Id { get; set; }
        public int ach_mst_Id { get; set; }
        public string amt { get; set; }
        public int S_No { get; set; }
        public string Inst_ID { get; set; }
        public string End_To_End_ID { get; set; }
        public string Text_ID { get; set; }
        public string CD { get; set; }
        public string Prty { get; set; }
        public string Currency { get; set; }
        public string ChrBr { get; set; }
        public string IBAN_dbi { get; set; }
        public string Name_dbi { get; set; }
        public string BIC_dbi { get; set; }
        public string IBAN_cdi { get; set; }
        public string Name_cdi { get; set; }
        public string BIC_cdi { get; set; }
        public string Detail { get; set; }

        [Skip]
        [Ignore]
        public TITLFTCHNG_RSPNS Title_Fetching_RSPNS { get; set; }
    }

}