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

public partial class View_User_Student : System.Web.UI.Page
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
}