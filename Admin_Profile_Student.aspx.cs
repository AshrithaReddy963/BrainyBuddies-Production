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

public partial class Admin_Profile_Student : System.Web.UI.Page
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
            getAdmitRegistationDetail();
        }
    }
    public void getAdmitRegistationDetail()
    {
        NameValueCollection objcollection11 = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds11 = new DataSet();
        objcollection11.Add("@TableId", Session["TableId"].ToString());
        //objcollection11.Add("@Type", Session["Type"].ToString());
        ds11 = objcls.GetdataWithPera("selectStudentRegistration", objcollection11);
        if (ds11.Tables[0].Rows.Count > 0)
        {
            lblName.Text = ds11.Tables[0].Rows[0]["Name"].ToString();
            lblEmail.Text = ds11.Tables[0].Rows[0]["Email"].ToString();
            lblAddress.Text = ds11.Tables[0].Rows[0]["Address"].ToString();
            lblPhone.Text = ds11.Tables[0].Rows[0]["Phone"].ToString();
            lblUserName.Text = ds11.Tables[0].Rows[0]["username"].ToString();
            lblPassword.Text = ds11.Tables[0].Rows[0]["Password"].ToString();
            ImgProfile.ImageUrl = ds11.Tables[0].Rows[0]["Image"].ToString();
            lblCourse.Text=ds11.Tables[0].Rows[0]["Course"].ToString();
            lblID.Text=ds11.Tables[0].Rows[0]["ProfId"].ToString();
            lblType.Text=ds11.Tables[0].Rows[0]["Type"].ToString();
            lblYear.Text = ds11.Tables[0].Rows[0]["GraduatedYear"].ToString();
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Student_Edit_Student.aspx");
    }
}