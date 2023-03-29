using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace PivotController.Controllers
{
    public class PivotController : ControllerBase
    {
        [HttpPost]
        [Route("Pivot/SaveReport")]
        public IActionResult SaveReport([FromBody] SaveReportDB reportDB)
        {
            return Ok((SaveReportToDB(reportDB.ReportName, reportDB.Report)));
        }

        [HttpPost]
        [Route("Pivot/FetchReport")]
        public IActionResult FetchReport()
        {
            return Ok((FetchReportListFromDB()));
        }

        [HttpPost]
        [Route("Pivot/RemoveReport")]
        public IActionResult RemoveReport([FromBody] ReportDB reportDB)
        {
            return Ok((RemoveReportFromDB(reportDB.ReportName)));
        }

        [HttpPost]
        [Route("Pivot/RenameReport")]
        public IActionResult RenameReport([FromBody] RenameReportDB reportDB)
        {
            return Ok((RenameReportInDB(reportDB.ReportName, reportDB.RenameReport)));
        }

        [HttpPost]
        [Route("Pivot/LoadReport")]
        public IActionResult LoadReport([FromBody] ReportDB reportDB)
        {
            return Ok((LoadReportFromDB(reportDB.ReportName)));
        }

        public class SaveReportDB
        {
            public String ReportName { get; set; }
            public String Report { get; set; }
        }

        public class ReportDB
        {
            public String ReportName { get; set; }
        }

        public class RenameReportDB
        {
            public String ReportName { get; set; }
            public String RenameReport { get; set; }
        }

        public class ErrorViewModel
        {
            public string RequestId { get; set; }
            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        }

        public Dictionary<string, object> SaveReportToDB(string reportName, string report)
        {
            string conSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory
                + @"\App_Data\Database1.mdf;Integrated Security=True";
            bool isDuplicate = true;
            SqlConnection sqlConn = new SqlConnection(conSTR);
            sqlConn.Open();
            SqlCommand cmd1 = null;
            foreach (DataRow row in GetDataTable(sqlConn).Rows)
            {
                if ((row.ItemArray[0] as string).Equals(reportName))
                {
                    isDuplicate = false;
                    cmd1 = new SqlCommand("update ReportTable set Report=@Report where ReportName like @ReportName", sqlConn);
                }
            }
            if (isDuplicate)
            {
                cmd1 = new SqlCommand("insert into ReportTable Values(@ReportName,@Report)", sqlConn);
            }
            cmd1.Parameters.AddWithValue("@ReportName", reportName);
            cmd1.Parameters.AddWithValue("@Report", report.ToString());
            cmd1.ExecuteNonQuery();
            sqlConn.Close();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("CurrentAction", "Save");
            return dictionary;
        }

        public Dictionary<string, object> RemoveReportFromDB(string reportName)
        {
            string conSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory
                + @"\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(conSTR);
            sqlConn.Open();
            SqlCommand cmd1 = null;
            foreach (DataRow row in GetDataTable(sqlConn).Rows)
            {
                if ((row.ItemArray[0] as string).Equals(reportName))
                {
                    cmd1 = new SqlCommand("DELETE FROM ReportTable WHERE ReportName LIKE '%" + reportName + "%'", sqlConn);
                }
            }
            cmd1.ExecuteNonQuery();
            sqlConn.Close();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("CurrentAction", "Remove");
            return dictionary;
        }

        public Dictionary<string, object> RenameReportInDB(string reportName, string renameReport)
        {
            string conSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory
                + @"\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(conSTR);
            sqlConn.Open();
            SqlCommand cmd1 = null;
            foreach (DataRow row in GetDataTable(sqlConn).Rows)
            {
                if ((row.ItemArray[0] as string).Equals(reportName))
                {
                    cmd1 = new SqlCommand("update ReportTable set ReportName=@RenameReport where ReportName like '%" + reportName + "%'", sqlConn);
                }
            }
            cmd1.Parameters.AddWithValue("@RenameReport", renameReport);
            cmd1.ExecuteNonQuery();
            sqlConn.Close();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("CurrentAction", "Rename");
            return dictionary;
        }

        public Dictionary<string, object> FetchReportListFromDB()
        {
            string conSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory
                + @"\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(conSTR);
            sqlConn.Open();
            string reportNames = string.Empty, currentRptName = string.Empty;
            foreach (System.Data.DataRow row in GetDataTable(sqlConn).Rows)
            {
                currentRptName = (row.ItemArray[0] as string);
                reportNames = reportNames == "" ? currentRptName : reportNames + "__" + currentRptName;
            }
            sqlConn.Close();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("ReportNameList", reportNames);
            dictionary.Add("CurrentAction", "Fetch");
            return dictionary;
        }

        public Dictionary<string, object> LoadReportFromDB(string reportName)
        {
            string conSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory
                + @"\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(conSTR);
            sqlConn.Open();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string currentRptName = string.Empty;
            string report = string.Empty;
            foreach (DataRow row in GetDataTable(sqlConn).Rows)
            {
                currentRptName = row.ItemArray[0] as string;
                if (currentRptName.Equals(reportName))
                {
                    report = row.ItemArray[1] as string;
                    dictionary.Add("ReportName", currentRptName);
                    dictionary.Add("Report", report);
                    dictionary.Add("CurrentAction", "Load");
                    break;
                }
            }
            sqlConn.Close();
            return dictionary;
        }

        private DataTable GetDataTable(SqlConnection sqlConn)
        {
            string xquery = "SELECT * FROM ReportTable";
            SqlCommand cmd = new SqlCommand(xquery, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}