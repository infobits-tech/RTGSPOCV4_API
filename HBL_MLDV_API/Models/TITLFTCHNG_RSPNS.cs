using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class TITLFTCHNG_RSPNS
    {
        public int RSPNS_ID { get; set; }
        public int RQST_ID { get; set; }
        public int DOC_SK { get; set; }
        public int DOC_TYP_SK { get; set; }
        [Skip]
        [Ignore]
        public string message { get; set; }
        public string accountNo { get; set; }
        public string accountTitle { get; set; }
        public string adviceAllCredit { get; set; }
        public string adviceAllDebit { get; set; }
        public string accountCurrency { get; set; }
        public Int64 availableBalance { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string API_STS_CD { get; set; }
        public string API_STS_DESC { get; set; }
        public string stream { get; set; }
        //public string errormsg { get; set; }
        public string customerAddress1 { get; set; }
        public string customerAddress2 { get; set; }
        public string customerAddress3 { get; set; }
        public int IS_ACTV { get; set; }
        public DateTime CRETD_ON { get; set; }
        public int CRETD_BY { get; set; }
        [Skip]
        [Ignore]
        public Data data { get; set; }
        [Skip]
        [Ignore]
        public Error error { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
    public class Data
    {
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public string accountTitle { get; set; }
        public string SC014_DECEASEDORLIQUIDATED { get; set; }
        public string PRESTIGECUSTOMER { get; set; }
        public string SC017_ACCOUNTSTATUSBLOCKED { get; set; }
        public string SC020_ACCOUNTSTATUSINACTIVE { get; set; }
        public string SC030_ACCOUNTCLOSING { get; set; }
        public string SC011_ADVICEALLCREDIT { get; set; }
        public string SC012_ADVICEALLDEBIT { get; set; }
        public string SC066_ADVICESCAI66 { get; set; }
        public string SC096_ADVICESCAI96 { get; set; }
        public string accountCurrency { get; set; }
        public string accountBranchCode { get; set; }
        public string hasCheckBook { get; set; }
        public string hasOverDraft { get; set; }
        public string hasJointAccount { get; set; }
        public string isMinorAccount { get; set; }
        public string hasSiFacility { get; set; }
        public string productName { get; set; }
        public string infoMessage { get; set; }
        public string responseCode { get; set; }
        public string availableBalance { get; set; }
        public string fullName { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string mobieleNo { get; set; }
        public string busstel { get; set; }
        public string tradeLicExp { get; set; }
        public string customerAddress1 { get; set; }
        public string customerAddress2 { get; set; }
        public string customerAddress3 { get; set; }
    }
    public class Error
    {
        public string reqId { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        //public DeveloperMessage developerMessage { get; set; }
    }
    public class DeveloperMessage
    {
        public string ACCOUNTTYPE { get; set; }
        public string ACCOUNTTITLE { get; set; }
        public string SC014_DECEASEDORLIQUIDATED { get; set; }
        public string PRESTIGECUSTOMER { get; set; }
        public string SC017_ACCOUNTSTATUSBLOCKED { get; set; }
        public string SC020_ACCOUNTSTATUSINACTIVE { get; set; }
        public string SC030_ACCOUNTCLOSING { get; set; }
        public string SC011_ADVICEALLCREDIT { get; set; }
        public string SC012_ADVICEALLDEBIT { get; set; }
        public string SC066_ADVICESCAI66 { get; set; }
        public string SC096_ADVICESCAI96 { get; set; }
        public string ACCOUNTCURRENCY { get; set; }
        public string ACCOUNTBRANCHCODE { get; set; }
        public string HASCHECKBOOK { get; set; }
        public string HASOVERDRAFT { get; set; }
        public string HASJOINTACCOUNT { get; set; }
        public string ISMINORACCOUNT { get; set; }
        public string HASSIFACILITY { get; set; }
        public string PRODUCTNAME { get; set; }
        public string INFOMESSAGE { get; set; }
        public string RESPONSECODE { get; set; }
        public string AVAILBLEBALANCE { get; set; }
        public string FULLNAME { get; set; }
        public string EMAIL1 { get; set; }
        public string EMAIL2 { get; set; }
        public string MOBILENO { get; set; }
        public string BUSSTEL { get; set; }
        public string TRADELICEXP { get; set; }
        public string CUSTOMERADDRESS1 { get; set; }
        public string CUSTOMERADDRESS2 { get; set; }
        public string CUSTOMERADDRESS3 { get; set; }
    }
}