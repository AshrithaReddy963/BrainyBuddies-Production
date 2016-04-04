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

public partial class AdminMaster : System.Web.UI.MasterPage
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
            string curlink = Request.RawUrl;
            if (curlink.Contains("textPage.aspx"))
            {
                dashboard.Attributes["class"] = "active";
            }
            else if (curlink.Contains("View_User.aspx"))
            {
                 viewstudent.Attributes["class"] = "active";
            }
            else if (curlink.Contains("Pending_Request.aspx"))
            {
                pending_authorize.Attributes["class"] = "active";
            }
            else if (curlink.Contains("Upload_Managment.aspx"))
            {
                upload_management.Attributes["class"] = "active";
            }
            else if (curlink.Contains("Event_Managment.aspx"))
            {
                event_management.Attributes["class"] = "active";
            }
            else if (curlink.Contains("Forum_Managment_Add.aspx"))
            {
                forum_management.Attributes["class"] = "active";
            }
            else if (curlink.Contains("Admin_Profile.aspx"))
            {
                profile_management.Attributes["class"] = "active";
            }
            getAdmitRegistationDetail();
            getpendingrequestdata();
        }
    }
    public void getAdmitRegistationDetail()
    {
        NameValueCollection objcollection11 = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds11 = new DataSet();
        objcollection11.Add("@TableId", Session["TableId"].ToString());
        objcollection11.Add("@Type", Session["Type"].ToString());
        ds11 = objcls.GetdataWithPera("selectAdminRegistration", objcollection11);
        if (ds11.Tables[0].Rows.Count > 0)
        {

            lblUserName.Text = ds11.Tables[0].Rows[0]["Name"].ToString();
            

            imgUserImage.ImageUrl = ds11.Tables[0].Rows[0]["Image"].ToString();
            
        }
    }
    public void getpendingrequestdata()
    {
        NameValueCollection objcollection = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds = new DataSet();
        ds = objcls.Getdata("Selectpendingrequestdata");
        if (ds.Tables[0].Rows.Count > 0)
        {

            lblCountRequest.Text = ds.Tables[0].Rows.Count.ToString();


        }
        else
        {
            lblCountRequest.Text = "0";
        }

    }
}
