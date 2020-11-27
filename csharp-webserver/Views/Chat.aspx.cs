using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace csharp_webserver.Views
{
    public partial class Chat : System.Web.UI.Page
    {
        private ServerRequestController serverRequestController;
        private string storedEndpointUrl = "";
        private int lastMessageCount = 0;
        private string targetAdress = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            serverRequestController = new ServerRequestController();
            displayMessages();

            // Global.messageStoreService.subscribe(checkSync);

        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session[Property.ENDPOINT_ADRESS_SESSION] != null)
            {
                storedEndpointUrl = Session[Property.ENDPOINT_ADRESS_SESSION].ToString();

                if (!IsPostBack)
                    newRequestUrl.Text = storedEndpointUrl;
            }
            else
            {
                Session[Property.ENDPOINT_ADRESS_SESSION] = storedEndpointUrl;
            }
            
        }

        protected void sendMessage(object sender, EventArgs e)
        {
            if (targetAdress == "")
                targetAdress = newRequestUrl.Text;

            HttpStatusCode response = 
                serverRequestController.sendMessage(
                    targetAdress,
                    messageField.Text);

            Session[Property.ENDPOINT_ADRESS_SESSION] = targetAdress;

            Response.Redirect(Request.RawUrl);
        }

        private void checkSync()
        {
            if (lastMessageCount != 
                Global.messageStoreService.countMessages())
            {
                displayMessages();
            }
        }

        protected void selectedListClient(object sender, EventArgs e)
        {

            Button senderBtn = sender as Button;
            string value = senderBtn.Attributes["data"];

            Session[Property.ENDPOINT_ADRESS_SESSION] = value;
            newRequestUrl.Text = value;
        }

        private void displayMessages()
        {

            historyTable.Rows.Clear();
            chatList.Rows.Clear();

            Global.messageStoreService
                .getOrigins()
                .ForEach(origin =>
            {

                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                Button button = new Button();
                button.Attributes.Add("data", origin);
                button.Attributes.Add("class", "hideButton");
                button.Click += new EventHandler(selectedListClient);
                button.Text = origin;

                cell.Controls.Add(button);
                row.Cells.Add(cell);
                chatList.Rows.Add(row);
            });

          
            Global.messageStoreService
                .getMessages()
                .ForEach(message =>
            {

                HtmlTableRow row = new HtmlTableRow();

                if (message.mySelf)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 10%"); 
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 40%");
                    row.Cells.Add(cell);
                    
                    cell = new HtmlTableCell();
                    if (message.getDelivered()) { 
                        cell.InnerText = message.getContent();
                        cell.Attributes.Add("style", "width: 40%; text-align: right;");
                    } 
                    else
                    {
                        cell.InnerHtml = $"{message.getContent()} <br><i>Diese Nachricht konnte nicht zugestellt werden.</i>";
                        cell.Attributes.Add("style", "width: 40%; text-align: right; color: gray;");
                    }
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 10%; text-align: right;");
                    cell.InnerHtml = $"<span class=\"name\">Du</span>";
                    row.Cells.Add(cell);

                    historyTable.Rows.Add(row);
                }
                else
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 10%");
                    cell.InnerHtml = $"<span class=\"name\">{message.getOrigin()}</span>";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 40%");
                    cell.InnerText = message.getContent();
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 40%");
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.Attributes.Add("style", "width: 10%");
                    row.Cells.Add(cell);

                    historyTable.Rows.Add(row);
                }
            });
        }
    }
}