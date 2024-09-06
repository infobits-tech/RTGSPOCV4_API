using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class VU_TXN_DTL_INPUT
    {
        //[Ignore]
        //[Skip]
        //public VU_TXN_DTL_INPUT()
        //{
        //    transactions = new List<VU_TXN_DTL_INPUT>();
        //}
        //[Ignore]
        //[Skip]
        //public string StateRefrence { get; set; }
        //[Ignore]
        //[Skip]
        //public int key { get; set; }
        //public List<VU_TXN_DTL_INPUT> transactions { get; set; }
        //[Key]
        public int? Doc_SK { get; set; }
        public int? snder_ref_seq { get; set; }
        public string TXN_MST_ID { get; set; }

        public string MSG_TYP { get; set; }
        public string TXN_DTE_TME { get; set; }
        public string MSG_STATUS { get; set; }
        public string REMARKS { get; set; }
        public string C20_SNDR_REF { get; set; }
        public string C23_BNK_OPR_CDE { get; set; }
        public string C51_SNDNG_INSTN { get; set; }
        public string C50_ORDNG_CUST_AC { get; set; }
        public string C50_ORDNG_CUST_NAME { get; set; }
        public string C50_ORDNG_CUST_ADD1 { get; set; }
        public string C50_ORDNG_CUST_ADD2 { get; set; }
        public string C50_ORDNG_CUST_ADD3 { get; set; }
        public string C52A_ORDNG_INSTN_AC { get; set; }
        public string C52A_ORDNG_INSTN_BIC { get; set; }
        public string C26T_TXN_TYP_CDE { get; set; }
        public string CrTrxCode { get; set; }
        public string DrTrxCode { get; set; }
        public string RESERVE_ACC { get; set; }
        public string C77B_REG_RPTNG { get; set; }
        [Ignore]
        [Skip]
        public string C77B_REG_RPTNG_1 { get; set; }
        [Ignore]
        [Skip]
        public string C77B_REG_RPTNG_2 { get; set; }
        [Ignore]
        [Skip]
        public string C77B_REG_RPTNG_3 { get; set; }
        public string C71A_CHGS_DTLS { get; set; }
        public string C36_EXCH_RTS { get; set; }
        public string C21_RLT_TXN_REF { get; set; }
        public string C32B_TXN_AMT { get; set; }
        public string C57A_ACCT_INSTN_AC { get; set; }
        public string C57A_ACCT_INSTN_BIC { get; set; }
        public string C59_BNFCY_CUST_AC { get; set; }

        public string C59_BNFCY_CUST_NAME { get; set; }

        public string C59_BNFCY_CUST_ADD1 { get; set; }

        public string C59_BNFCY_CUST_ADD2 { get; set; }

        public string C59_BNFCY_CUST_ADD3 { get; set; }
        public string C70_REMIT_INFO { get; set; }
        [Ignore]
        [Skip]
        public string C70_REMIT_INFO_1 { get; set; }
        [Ignore]
        [Skip]
        public string C70_REMIT_INFO_2 { get; set; }
        [Ignore]
        [Skip]
        public string C70_REMIT_INFO_3 { get; set; }
        public string C33B_CNCY_AMOUNT { get; set; }
        public string C71F_SNDR_CHGS { get; set; }
        public string C71G_RCVR_CHGS { get; set; }
        public string C32A_VDTE_CNCY_AMT { get; set; }
        public string VAL_DTE { get; set; }
        public string CNCY_CDE { get; set; }
        public string AMNT { get; set; }
        public string C53_SNDR_CPND_AC { get; set; }
        public string C53_SNDR_CPND_BIC { get; set; }
        public string C54_RCVR_CPND_AC { get; set; }
        public string C54_RCVR_CPND_BIC { get; set; }
        public string C72_SNDR_RCVR_INFO { get; set; }
        [Ignore]
        [Skip]
        public string C72_SNDR_RCVR_INFO_txt_area { get; set; }
        public string C23E_INSTRUC_CDE { get; set; }
        public string C55_3RD_REIMBRS_INSTN { get; set; }
        public string C56A_INTER_INSTN { get; set; }
        public string C58_BNFCY_INSTN_AC { get; set; }
        public string C58_BNFCY_INSTN_BIC { get; set; }
        public string C79_NARRATIVE { get; set; }
        public string C12_MSG_RQSTD { get; set; }
        public string C25_ACCT_ID { get; set; }
        public string C34F_FLR_LMT_INDIC { get; set; }
        public string TEMPLATE_NAME { get; set; }
        public string Bank_Periority { get; set; }
        public int Created_By { get; set; }
        [Ignore]
        [Skip]
        public string Is_editable { get; set; }
        public long DOC_INSTC { get; set; }
        public int Br_Sk { get; set; }

        [Ignore]
        [Skip]
        public string Status_desc { get; set; }

        public string CRDT_CNCY { get; set; }
        public string DBT_CNCY { get; set; }
        public string CRDT_AMT { get; set; }
        public string DBT_ACC { get; set; }
        public string DBT_AMT { get; set; }

        public int DOC_Typ_Sk { get; set; }
        [Ignore]
        [Skip]
        public string STS_CDE { get; set; }
        public string Channel_Id { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        [Ignore]
        [Skip]
        public int isDraft { get; set; }
        [Ignore]
        [Skip]
        public int issubmitform { get; set; }
        [Skip]
        [Ignore]
        public TITLFTCHNG_RSPNS Title_Fetching_RSPNS { get; set; }

        //hold work
        [Skip]
        [Ignore]
        public Hold_RSPNS Hold_RSPNS { get; set; }

    }
    public class VU_TXN_MST
    {
        [Key]
        public int MST_TXN_ID { get; set; }
        public string TXN_Type { get; set; }
        public string TXN_Ref { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string MSG_Status { get; set; }
        public DateTime TXN_DTE_Time { get; set; }
        public string user_id { get; set; }
        public int Doc_status_sk { get; set; }
        public string Doc_Nbr { get; set; }

    }
}