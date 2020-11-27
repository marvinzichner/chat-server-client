using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace csharp_webserver.Views
{
    public partial class ServerLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillLogs();
        }

        protected void fillLogs()
        {

            logTable.Rows.Clear();
            Global.logger.getLogMessages().ForEach(message =>
            {

                HtmlTableRow row = new HtmlTableRow();
                row.Cells.Add(simplecell(message.getCreationTimeAsString()));
                row.Cells.Add(simplecell(message.getLogLevel().ToString()));
                row.Cells.Add(simplecell(message.getMessage()));
                row.Cells.Add(simplecell(message.getTraceType()));

                if (message.getLogLevel().Equals(LogLevel.INFO))
                    row.Attributes.Add("clas", "info");

                if (message.getLogLevel().Equals(LogLevel.WARNING))
                    row.Attributes.Add("clas", "warn");

                if (message.getLogLevel().Equals(LogLevel.ERROR))
                    row.Attributes.Add("clas", "error");

                logTable.Rows.Add(row);

            });
        }

        private HtmlTableCell simplecell(string text)
        {

            HtmlTableCell cell = new HtmlTableCell();
            cell.InnerText = text;
            return cell;
        }
    }
}