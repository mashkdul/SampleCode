using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;

/// <summary>
/// Summary description for GlBookManager
/// </summary>
/// 
namespace icsl
{
    public class GlBookManager
    {
        public static DataTable GetGlBooks(string criteria)
        {
            string connectionString = DataManager.OraConnString();
            OracleConnection sqlCon = new OracleConnection(connectionString);
            string query = "select * from gl_set_of_books ";
            if (criteria != "")
            {
                query = query + " where " + criteria;
            }            
            DataTable dt = DataManager.ExecuteQuery(connectionString, query, "Glbook");
            return dt;
        }
        public static byte[] GetGlLogo(string book)
        {
            byte[] img = null;
            String ConnectionString = DataManager.OraConnString();
            OracleConnection myConnection = new OracleConnection(ConnectionString);
            string Query = "select logo from gl_set_of_books where book_name="+book+" ";
            myConnection.Open();
            OracleCommand myCommand = new OracleCommand(Query, myConnection);
            object maxValue = myCommand.ExecuteScalar();
            myConnection.Close();
            if (maxValue != System.DBNull.Value)
            {
                img = (byte[])maxValue;
            }            
            return img;
        }
        public static GlBook getBook(string book)
        {
            String connectionString = DataManager.OraConnString();
            OracleConnection sqlCon = new OracleConnection(connectionString);

            string query = "select * from gl_set_of_books where book_name='" + book + "'";
            DataTable dt = DataManager.ExecuteQuery(connectionString, query, "Glbook");
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return new GlBook(dt.Rows[0]);
        }
        public static void CreateGlBook(GlBook book)
        {
            String connectionString = DataManager.OraConnString();
            OracleConnection sqlCon = new OracleConnection(connectionString);
            string query = "";
            if (book.logo != null)
            {
                query = "insert into gl_set_of_books (book_name,book_desc,book_status,separator_type,company_address1, " +
                    " company_address2,retd_earn_acc,phone,fax,url,bank_code,cash_code,logo) values ( " +
                    " '" + book.BookName + "', " + " '" + book.BookDesc + "', " + " '" + book.BookStatus + "', " +
                    " " + " '" + book.SeparatorType + "', " + " '" + book.CompanyAddress1 + "', " + " '" + book.CompanyAddress2 + "', " +
                    " " + " '" + book.RetdEarnAcc + "', " + " '" + book.Phone + "', " + " '" + book.Fax + "', " +
                    " " + " '" + book.Url + "'," + " '" + book.BankCode + "'," + " '" + book.CashCode + "',:img ) ";
            }
            else
            {
                query = "insert into gl_set_of_books (book_name,book_desc,book_status,separator_type,company_address1, " +
                    " company_address2,retd_earn_acc,phone,fax,url,bank_code,cash_code,logo) values ( " +
                    " '" + book.BookName + "', " + " '" + book.BookDesc + "', " + " '" + book.BookStatus + "', " +
                    " " + " '" + book.SeparatorType + "', " + " '" + book.CompanyAddress1 + "', " + " '" + book.CompanyAddress2 + "', " +
                    " " + " '" + book.RetdEarnAcc + "', " + " '" + book.Phone + "', " + " '" + book.Fax + "', " +
                    " " + " '" + book.Url + "'," + " '" + book.BankCode + "'," + " '" + book.CashCode + "',empty_blob() ) ";
            }
            OracleCommand cmnd;
            OracleParameter img = new OracleParameter();
            img.OracleType = OracleType.Blob;
            img.ParameterName = "img";
            img.Value = book.logo;
            cmnd = new OracleCommand(query, sqlCon);
            cmnd.Parameters.Add(img);
            if (book.logo == null)
            {
                cmnd.Parameters.Remove(img);
            }
            sqlCon.Open();
            cmnd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public static void UpdateGlBook(GlBook book)
        {
            String connectionString = DataManager.OraConnString();
            OracleConnection sqlCon = new OracleConnection(connectionString);
            string query = "";
            if (book.logo != null)
            {
                query = "update gl_set_of_books set  book_desc='" + book.BookDesc + "', book_status= '" + book.BookStatus + "', " +
                    " separator_type= '" + book.SeparatorType + "', company_address1= '" + book.CompanyAddress1 + "', company_address2= '" + book.CompanyAddress2 + "', " +
                    " retd_earn_acc= '" + book.RetdEarnAcc + "', phone= '" + book.Phone + "', fax= '" + book.Fax + "', " +
                    " url= '" + book.Url + "', bank_code ='" + book.BankCode + "',cash_code= '" + book.CashCode + "',logo=:img where book_name= '" + book.BookName + "' ";
            }
            else
            {
                query = "update gl_set_of_books set  book_desc='" + book.BookDesc + "', book_status= '" + book.BookStatus + "', " +
                    " separator_type= '" + book.SeparatorType + "', company_address1= '" + book.CompanyAddress1 + "', company_address2= '" + book.CompanyAddress2 + "', " +
                    " retd_earn_acc= '" + book.RetdEarnAcc + "', phone= '" + book.Phone + "', fax= '" + book.Fax + "', " +
                    " url= '" + book.Url + "', bank_code ='" + book.BankCode + "',cash_code= '" + book.CashCode + "',logo=empty_blob() where book_name= '" + book.BookName + "' ";
            }
            OracleCommand cmnd;
            OracleParameter img = new OracleParameter();
            img.OracleType = OracleType.Blob;
            img.ParameterName = "img";
            img.Value = book.logo;
            cmnd = new OracleCommand(query, sqlCon);
            cmnd.Parameters.Add(img);
            if (book.logo == null)
            {
                cmnd.Parameters.Remove(img);
            }
            sqlCon.Open();
            cmnd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public static void DeleteGlBook(GlBook book)
        {
            String connectionString = DataManager.OraConnString();
            OracleConnection sqlCon = new OracleConnection(connectionString);

            string query = "delete from gl_set_of_books where book_name= '" + book.BookName + "' ";
            DataManager.ExecuteNonQuery(connectionString, query);
        }


    }
}
