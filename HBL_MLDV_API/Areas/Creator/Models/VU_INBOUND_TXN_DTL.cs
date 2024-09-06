using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class VU_INBOUND_TXN_DTL
	{
        //public int Inbound_TXN_DTL_ID { get; set; }
		public int DOC_SK { get; set; }
		public string Cbuae_MSG_TYP { get; set; }
		public int Inbound_FIL_INFO_ID { get; set; }
		public int DOC_TYP_SK { get; set; }
		public string USR_ID { get; set; }
		public string REC_TYP { get; set; }
		public string MSG_TYP_ID { get; set; }
		public string ACCT_ID { get; set; }
		public string ADTNL_TXN_RLT_DTLs { get; set; }
		public string ANSR_LNE_1 { get; set; }
		public string BNK_OPER_CDE { get; set; }
		public string BNFCY_ACCT_NME_and_ADDR { get; set; }
		public string BNFCY_ACCT_with_Receiving_PRTicipant { get; set; }
		public string C2B_INFO { get; set; }
		public string CRNCY_CDE { get; set; }
		public string DTLs_of_CHRGs { get; set; }
		public decimal EXCHG_RTE { get; set; }
		public string GEN_FIL_NME { get; set; }
		public decimal Instructed_AMT { get; set; }
		public string Instructed_CRNCY { get; set; }
		public string InTERMediary_INSTN { get; set; }
		public string Narrative_1 { get; set; }
		public string Narrative_2 { get; set; }
		public string Narrative_3 { get; set; }
		public string Narrative_4 { get; set; }
		public string Narrative_5 { get; set; }
		public string ORDing_CUST_ACCT { get; set; }
		public string ORDing_CUST_NME_and_ADDR { get; set; }
		public string ORDing_CUST_TYP { get; set; }
		public string ORDing_INSTN { get; set; }
		public DateTime? ORIG_MSG_DTE { get; set; }
		//public string Dte_tme { get; set; }
		public string ORIG_MSG_DTL_01 { get; set; }
		public string ORIG_MSG_DTL_02 { get; set; }
		public string ORIG_MSG_DTL_03 { get; set; }
		public string ORIG_MSG_DTL_04 { get; set; }
		public string ORIG_MSG_DTL_05 { get; set; }
		public string ORIG_MSG_DTL_06 { get; set; }
		public string ORIG_MSG_DTL_07 { get; set; }
		public string ORIG_MSG_DTL_08 { get; set; }
		public string ORIG_MSG_DTL_09 { get; set; }
		public string ORIG_MSG_NBR { get; set; }
		public string ORIG_MSG_REF { get; set; }
		public string ORIG_MSG_UAEFTS_REF { get; set; }
		public string ORIG_QRY_MSG_UAEFTS_FIL_ID { get; set; }
		public string P2P1_INFO { get; set; }
		public string P2P10_INFO { get; set; }
		public string P2P11_INFO { get; set; }
		public string P2P12_INFO { get; set; }
		public string P2P13_INFO { get; set; }
		public string P2P14_INFO { get; set; }
		public string P2P15_INFO { get; set; }
		public string P2P16_INFO { get; set; }
		public string P2P17_INFO { get; set; }
		public string P2P2_INFO { get; set; }
		public string P2P3_INFO { get; set; }
		public string P2P4_INFO { get; set; }
		public string P2P5_INFO { get; set; }
		public string P2P6_INFO { get; set; }
		public string P2P7_INFO { get; set; }
		public string P2P8_INFO { get; set; }
		public string P2P9_INFO { get; set; }
		public string QRY_LNE_1 { get; set; }
		public string QRY_LNE_2 { get; set; }
		public string QRY_LNE_3 { get; set; }
		public string QRY_LNE_4 { get; set; }
		public string QRY_LNE_5 { get; set; }
		public string QRY_LNE_6 { get; set; }
		public string RECVD_FIL_NME { get; set; }
		public string RECVrs_CORSPDT { get; set; }
		public string RLT_REF { get; set; }
		public string RLT_REF_NBR { get; set; }
		public string REMIT_INFO { get; set; }
		public string SNDer_to_RECVr_INFO { get; set; }
		public string SNDers_CORSPDT { get; set; }
		public decimal SNDers_CHRGs { get; set; }
		public string SNDing_INSTN_REF { get; set; }
		public string SEQ_NBR { get; set; }
		public decimal SETLd_TXN_AMT { get; set; }
		public string SPCL_INSTs_for_BNFCY_BNK { get; set; }
		public decimal TOT_VAL { get; set; }
		public decimal TXN_AMT { get; set; }
		public string TXN_Batch_Id { get; set; }
		public string TXN_REF_NBR { get; set; }
		public string TXN_TYP_CDE { get; set; }
		public string UAEFTS_FIL_Id { get; set; }
		public string UAEFTS_TXN_REF_NBR { get; set; }
		public string ULT_BNFCY_DTLs { get; set; }
		public decimal UNDRLNG_CUST_CR_Instructed_AMT { get; set; }
		public string UNDRLNG_CUST_CR_Instructed_CRNCY { get; set; }
		public string UNDRLNG_CUST_CR_TXN_TYP_CDE { get; set; }
		public string VAL_DTE { get; set; }
		public string TXN_STAT { get; set; }
		public string TXN_DTE_TME { get; set; }
		public string ACCT_With_INSTN { get; set; }
		public string BNFCY_INSTN_ID { get; set; }
		public string SNDing_INSTN_REF_for_this_INDV_TXN { get; set; }
		public string FTR_Use { get; set; }
		public string SNDing_INSTN_ID { get; set; }
		public string BLANK_FIELD_1 { get; set; }
		public string BLANK_FIELD_2 { get; set; }
		public string BLANK_FIELD_3 { get; set; }
		public string BLANK_FIELD_4 { get; set; }
		public string IS_TXN_WRTTN { get; set; }
		public string MSG_Receiving_INSTN_ID { get; set; }
		public string org_msg_nbr_ff { get; set; }
		public string org_msg_nbr_dte_ff { get; set; }
		public string org_msg_uaefts_ff { get; set; }
		public string p2p1_info_ff { get; set; }
		public string p2p2_info_ff { get; set; }
		public string p2p3_info_ff { get; set; }
		public string p2p4_info_ff { get; set; }
		public string p2p5_info_ff { get; set; }
		public string p2p6_info_ff { get; set; }
		public string p2p7_info_ff { get; set; }
		public string p2p8_info_ff { get; set; }
		public string p2p9_info_ff { get; set; }
		public string p2p10_info_ff { get; set; }
		public string p2p11_info_ff { get; set; }
		public string p2p12_info_ff { get; set; }
		public string p2p13_info_ff { get; set; }
		public string p2p14_info_ff { get; set; }
		public string p2p15_info_ff { get; set; }
		public string p2p16_info_ff { get; set; }
		public string p2p17_info_ff { get; set; }
		public string uaefts_trans_ref_no_ff { get; set; }
		public string recived_file_name_ff { get; set; }
		public string generated_file_name_ff { get; set; }
		public string future_use_ff { get; set; }
		public string QRY_LN1_qq { get; set; }
		public string QRY_LN2_qq { get; set; }
		public string QRY_LN3_qq { get; set; }
		public string QRY_LN4_qq { get; set; }
		public string QRY_LN5_qq { get; set; }
		public string QRY_LN6_qq { get; set; }
		public string Narr1_qq { get; set; }
		public string Narr2_qq { get; set; }
		public string Narr3_qq { get; set; }
		public string Narr4_qq { get; set; }
		public string Narr5_qq { get; set; }
		public string org_msg_nbr_qq { get; set; }
		public string org_msg_dte_qq { get; set; }
		public string org_msg_dtl01_qq { get; set; }
		public string org_msg_dtl02_qq { get; set; }
		public string org_msg_dtl03_qq { get; set; }
		public string org_msg_dtl04_qq { get; set; }
		public string org_msg_dtl05_qq { get; set; }
		public string org_msg_dtl06_qq { get; set; }
		public string org_msg_dtl07_qq { get; set; }
		public string org_msg_dtl08_qq { get; set; }
		public string org_msg_dtl09_qq { get; set; }
		public string org_qry_msg_ufts_qq { get; set; }
		public string ftr_use_qq { get; set; }
		public string TXN_REF { get; set; }
		public string RSN_RQST_CNCL { get; set; }
		public string RECIVING_INSTN_IDENTIFIER { get; set; }
		public string ORDing_INSTN_1 { get; set; }
		public string InTERMediary_INSTN_1 { get; set; }
		public string ACCT_With_INSTN_1 { get; set; }
		public string BNFCY_ACCT_NME_and_ADDR_1 { get; set; }
		public string WPS_REC_TYP { get; set; }
		public string WPS_MSG_TYP_ID { get; set; }
		public string WPS_SEQ_NBR_from_ORIG_MSG { get; set; }
		public string WPS_fullfilling_Instition { get; set; }
		public string WPS_Value_Date { get; set; }
		public string WPS_CRNCY_CDE { get; set; }
		public decimal WPS_TXN_AMT { get; set; }
		public string WPS_MOL_EMPLR_Id { get; set; }
		public string WPS_MOL_EMPLR_NME { get; set; }
		public string WPS_SIF_FIL_NME { get; set; }
		public string WPS_FTR_USE { get; set; }
		public string Email_Gnrtd { get; set; }
		public string IS_EXCEPTION_TRX { get; set; }
		public string Trans_Exception { get; set; }
		public string VALDTE_CRNCYCDE_AMT { get; set; }
		public string CRNCYCDE_AMT { get; set; }
		public string ORDing_CUST_50K { get; set; }
		public string ORDing_CUST_50A { get; set; }
		public string ORDing_CUST_50F { get; set; }
		public string BNFCY_CUST_59 { get; set; }
		public string BNFCY_CUST_59A { get; set; }
		public string ORDing_INSTN_52A { get; set; }
		public string ACCT_with_INSTN_57A { get; set; }
		public string ACCT_with_INSTN_57B { get; set; }
		public string ACCT_with_INSTN_57C { get; set; }
		public string ACCT_with_INSTN_57D { get; set; }
		public string BNFCY_INSTN_58A { get; set; }
		public string BNFCY_INSTN_58D { get; set; }
		public string INST_CDE { get; set; }
		public string InTERMediary_56A { get; set; }
		public string InTERMediary_56D { get; set; }
		public string ORDing_INSTN_52D { get; set; }
		public string RECVrs_CORSPDT_54A { get; set; }
		public string RECVrs_CORSPDT_54B { get; set; }
		public string RECVrs_CORSPDT_54D { get; set; }
		public string SNDers_CORSPDT_53A { get; set; }
		public string SNDers_CORSPDT_53B { get; set; }
		public string SNDers_CORSPDT_53D { get; set; }
		public string SNDers_REF { get; set; }
		public string SNDing_INSTN { get; set; }
		public string TME_Indication { get; set; }
		public string AMT_33B { get; set; }
		public string CRNCY_CDE_32A { get; set; }
		public string AMT_32A { get; set; }
		public string ORDing_CUST_50K_2 { get; set; }
		public string ORDing_CUST_50K_3 { get; set; }
		public string BNFCY_CUST_59_1 { get; set; }
		public string BNFCY_CUST_59_2 { get; set; }
		public string Amount_PKR_32B { get; set; }
		public string Val_77B { get; set; }
		public string BNK_OPER_CDE_2 { get; set; }
		public string Msg_Status { get; set; }
		public string EMAIL_ADDR { get; set; }
		public string ORDing_CUST_50A_1 { get; set; }
		public string ORDing_CUST_50F_1 { get; set; }
		public string ORDing_CUST_50K_1 { get; set; }
		public string Is_Ready { get; set; }
		public int Ser_No { get; set; }
		public string BeneffMobileNo { get; set; }
		public string BenefBankBranchAddress1 { get; set; }
		public string BenefBankBranchAddress2 { get; set; }
		public string BenefBankBranchAddress3 { get; set; }
		public string RemittingBankCode { get; set; }
		public string BenefBankCode { get; set; }
		public int Channel_Id { get; set; }
		public int Routing_Id { get; set; }
		public string Remarks { get; set; }
		public string Narration { get; set; }
		public string BenefBranchName { get; set; }
		public string BenefIdentificationNo { get; set; }
		public string ApplicantIdentificationNo { get; set; }
		public string ApplicantContactNo { get; set; }
		public string ApplicantAddress { get; set; }
		public string Narrative { get; set; }
		public string DrTrxCode { get; set; }
		public string CRDT_CNCY { get; set; }
		public string DBT_CNCY { get; set; }
		public string CRDT_AMT { get; set; }
		public string Cr_Acc { get; set; }
		public string CrTrxCode { get; set; }
		public string RESERVE_ACC { get; set; }

		[Ignore]
		[Skip]
		public string sts_desc { get; set; }
		[Ignore]
		[Skip]
		public string STS_CDE { get; set; }
		[Ignore]
		[Skip]
		public string Is_editable { get; set; }
		[Ignore]
		[Skip]
		public string state { get; set; }
		[Ignore]
		[Skip]
		public int isDraft { get; set; }
		[Ignore]
		[Skip]
		public int isCancle { get; set; }
		[Ignore]
		[Skip]
		public int issubmitform { get; set; }
		[Ignore]
		[Skip]
		public string Action { get; set; }
		[Skip]
		[Ignore]
		public TITLFTCHNG_RSPNS Title_Fetching_RSPNS { get; set; }
	}	
}