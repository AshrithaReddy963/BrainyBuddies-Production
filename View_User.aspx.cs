using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using BussinessAccessLayer1;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
public partial class View_User : System.Web.UI.Page
{
    ClsKrisnAgr objcls;
    CommonFunctions objcommon = new CommonFunctions();
    // clsPage objclsPage;
    NameValueCollection objcollection = new NameValueCollection();
    NameValueCollection objName = new NameValueCollection();
    NameValueCollection objuserName = new NameValueCollection();
    public string strConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["FrostByteDatabase"].ConnectionString;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["Password"] == null)
        {
            Response.Redirect("Login.aspx");

        }
        else
        {

        }
        if (IsPostBack == false)
        {
            GetGridData();
        }
    }


    public void GetGridData()
    {
        NameValueCollection objcollection = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds = new DataSet();
        ds = objcls.Getdata("SelectRegistration");
        if (ds.Tables[0].Rows.Count > 0)
        {
            //GrdUser.DataSource = ds;
            //GrdUser.DataBind();



            RepterDetails.DataSource = ds;
            RepterDetails.DataBind();
        }
        else
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
        }

    }
    protected void dr_Graduatyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        NameValueCollection objcollection = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds = new DataSet();
        objcollection.Add("@Year", dr_Graduatyear.SelectedValue);
        ds = objcls.GetdataWithPera("SelectRegistrationWithAllowserchbyYear", objcollection);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //GrdUser.DataSource = ds;
            //GrdUser.DataBind();



            RepterDetails.DataSource = ds;
            RepterDetails.DataBind();
        }
        else
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
        }

    }
    protected void dr_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        NameValueCollection objcollection = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds = new DataSet();
        objcollection.Add("@Type", dr_Type.SelectedValue);
        ds = objcls.GetdataWithPera("SelectRegistrationWithAllowserchbyType", objcollection);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //GrdUser.DataSource = ds;
            //GrdUser.DataBind();



            RepterDetails.DataSource = ds;
            RepterDetails.DataBind();
        }
        else
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
        }
    }
    protected void dr_course_SelectedIndexChanged(object sender, EventArgs e)
    {
        NameValueCollection objcollection = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds = new DataSet();
        objcollection.Add("@Course", dr_course.SelectedValue);
        ds = objcls.GetdataWithPera("SelectRegistrationWithAllowserchbyCourse", objcollection);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //GrdUser.DataSource = ds;
            //GrdUser.DataBind();



            RepterDetails.DataSource = ds;
            RepterDetails.DataBind();
        }
        else
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
        }
    }



    public void RepterDetails_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //NameValueCollection objcollection = new NameValueCollection();
            //objcls = new ClsKrisnAgr(strConnectionString);
            //DataSet ds = new DataSet();
            //objcollection.Add("@RId", e.CommandArgument.ToString());
            //ds = objcls.GetdataWithPera("UpdateRegistrationStatus", objcollection);
            //GetGridData();

            Response.Redirect("Admin_Edit_Student.aspx?Rid=" + e.CommandArgument.ToString());






        }
        else if (e.CommandName == "Delete")
        {


            NameValueCollection objcollection11 = new NameValueCollection();
            objcls = new ClsKrisnAgr(strConnectionString);
            DataSet ds11 = new DataSet();
            objcollection11.Add("@RId", e.CommandArgument.ToString());
            ds11 = objcls.GetdataWithPera("DeleteDataFromRegistration", objcollection11);

            NameValueCollection objcollection1111 = new NameValueCollection();
            DataSet ds1111 = new DataSet();
            objcls = new ClsKrisnAgr(strConnectionString);



            NameValueCollection objcollection111 = new NameValueCollection();
            objcls = new ClsKrisnAgr(strConnectionString);
            DataSet ds111 = new DataSet();
            objcollection111.Add("@TableId", e.CommandArgument.ToString());
            ds111 = objcls.GetdataWithPera("selectStudentRegistration", objcollection111);

            if (ds111.Tables[0].Rows[0]["Type"].ToString() == "Student")
            {
                objcollection1111.Add("@EnterType", "S");
            }
            if (ds111.Tables[0].Rows[0]["Type"].ToString() == "Professor")
            {
                objcollection1111.Add("@EnterType", "P");
            }
            else
            {
                objcollection1111.Add("@EnterType", "A");
            }
            objcollection1111.Add("@UserId", e.CommandArgument.ToString());
            ds1111 = objcls.GetdataWithPera("DeleteForumCommentByUserIdAndEnterType", objcollection1111);

            GetGridData();
        }
    }
}