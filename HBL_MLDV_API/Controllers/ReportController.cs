using HBL_MLDV_API.Providers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HBL_MLDV_API.Controllers
{
    [RoutePrefix("api/Report")]
    public class ReportController : ApiController
    {
        [HttpGet]
        [Route("SampleReport")]
        public IHttpActionResult SampleReport()

        {
            try
            {
                return Ok(GetData());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private List<SalesData> GetData()
        {
            List<SalesData> sales = new List<SalesData>();
            sales.Add(new SalesData(0, -1, "Western Europe", 30540, 33000, 32220, 35500, .70));
            sales.Add(new SalesData(1, 0, "Austria", 22000, 28000, 26120, 28500, .92));
            sales.Add(new SalesData(2, 0, "Belgium", 13000, 9640, 14500, 11200, .16));
            //...
            sales.Add(new SalesData(17, -1, "Eastern Europe", 22500, 24580, 21225, 22698, .62));
            sales.Add(new SalesData(18, 17, "Belarus", 7315, 18800, 8240, 17480, .34));
            sales.Add(new SalesData(19, 17, "Bulgaria", 6300, 2821, 5200, 4880, .8));
            //...
            return sales;
        }
        private class SalesData
        {
            public SalesData(int id, int regionId, string region, decimal marchSales, decimal septemberSales, decimal marchSalesPrev, decimal septermberSalesPrev, double marketShare)
            {
                ID = id;
                RegionID = regionId;
                Region = region;
                MarchSales = marchSales;
                SeptemberSales = septemberSales;
                MarchSalesPrev = marchSalesPrev;
                SeptemberSalesPrev = septermberSalesPrev;
                MarketShare = marketShare;
            }
            // A node's ID in the hierarchical structure
            public int ID { get; set; }
            // A node's ParentID in the hierarchical structure
            public int RegionID { get; set; }
            public string Region { get; set; }
            public decimal MarchSales { get; set; }
            public decimal SeptemberSales { get; set; }
            public decimal MarchSalesPrev { get; set; }
            public decimal SeptemberSalesPrev { get; set; }
            public double MarketShare { get; set; }
        }
    }
}
