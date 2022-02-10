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
                    DataRow dr = null;
                    dr = datatbl.NewRow();
                    if (datatbl.Rows.Count > 0)
                    {
                        string dateinput = txtDate.Text;
                        DateTime datetime = DateTime.Parse(dateinput);
                        DayOfWeek date = datetime.DayOfWeek;

                        dr["Date"] = datetime; //DateTime.Parse(txtDate.Text);
                        dr["Energy Type"] = DropDownList1.SelectedValue;

                        if (date == DayOfWeek.Saturday || date == DayOfWeek.Sunday)
                        {
                            double prise = Double.Parse(txtPrise.Text);
                            double discount = prise - (prise * 10 / 100);
                            dr["Prise"] = discount;
                        }
                        else
                        {
                            dr["Prise"] = Double.Parse(txtPrise.Text);
                        }
                        
                        datatbl.Rows.Add(dr);
                        ViewState["Row"] = datatbl;
                        GridView1.DataSource = ViewState["Row"];
                        GridView1.DataBind();

                    }
                }
                else
                {
                    datatbl.Columns.Add("Date", typeof(DateTime));
                    datatbl.Columns.Add(new DataColumn("Energy Type", typeof(string))); 
                    datatbl.Columns.Add("Prise", typeof(double));

                    DataRow dr1 = datatbl.NewRow();
                    dr1 = datatbl.NewRow();

                    string dateinput = txtDate.Text;
                    DateTime dt = DateTime.Parse(dateinput);
                    DayOfWeek date = dt.DayOfWeek;

                    dr1["Date"] = dt;
                    dr1["Energy Type"] = DropDownList1.SelectedValue;

                    if (date == DayOfWeek.Saturday || date == DayOfWeek.Sunday)
                    {
                        double prise = Double.Parse(txtPrise.Text);
                        double discount = prise - (prise * 10 / 100);
                        dr1["Prise"] = discount;
                    }
                    else
                    {
                        dr1["Prise"] = Double.Parse(txtPrise.Text);
                    }

                    datatbl.Rows.Add(dr1);
                    ViewState["Row"] = datatbl;
                    GridView1.DataSource = ViewState["Row"];
                    GridView1.DataBind();
                }

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