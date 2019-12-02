using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CristalReportLib
{
    public class CRHelperClass
    {
        public DataSet GetData()
        {
            SqlConnection con = new SqlConnection(@"metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-TPII2Q9\SQLEXPRESS;initial catalog=IDBTest04;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;");
            string sql = @"select P.ID as ProductID, P.Name as ProductName, p.brand,
                            P.Price, P.StockQuantity, C.Name as CategoryName 
                            from Products as P 
                            left join Categories as C on C.ID = P.CategoryID";
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter adapt = new SqlDataAdapter(sql, con);
            adapt.Fill(ds, "Products");
            con.Close();

            return ds;
        }
    }
}
