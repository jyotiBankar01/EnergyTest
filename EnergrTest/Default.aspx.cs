using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EnergrTest
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            createnewrow();
        }

        public void createnewrow()
        {
            try
            {
                DataTable datatbl = new DataTable();
				if (ViewState["Row"] != null)
                {
				   datatbl = (DataTable)ViewState["Row"];
				}else{
				    datatbl.Columns.Add("Date", typeof(DateTime));
                    datatbl.Columns.Add(new DataColumn("Energy Type", typeof(string))); 
                    datatbl.Columns.Add("Prise", typeof(double));
				}
				        string dateinput = txtDate.Text;
                        DateTime datetime = DateTime.Parse(dateinput);
                        DayOfWeek date = datetime.DayOfWeek;
						double prise = Double.Parse(txtPrise.Text);
						
						DataRow dr = datatbl.NewRow();
                        dr["Date"] = datetime; //DateTime.Parse(txtDate.Text);
                        dr["Energy Type"] = DropDownList1.SelectedValue;
						dr["Prise"] = (date == DayOfWeek.Saturday || date == DayOfWeek.Sunday) ? (prise - (prise * 10 / 100)) : prise;
                        datatbl.Rows.Add(dr);
                        ViewState["Row"] = datatbl;
                        GridView1.DataSource = datatbl;
                        GridView1.DataBind();

            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception Caught", ex);
            }

        }


       protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt1 = (DataTable)ViewState["Row"];
            if (dt1.Rows.Count > 0)
            {
                dt1.Rows[e.RowIndex].Delete();
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }

        }

        
    }
}
