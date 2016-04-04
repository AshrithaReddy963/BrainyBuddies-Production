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

public partial class Admin_Edit : System.Web.UI.Page
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
            Session["ImagePath"] = "";
            getAdmitRegistationDetail();
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

           txtName.Text = ds11.Tables[0].Rows[0]["Name"].ToString();
            txtEmail.Text = ds11.Tables[0].Rows[0]["EmailId"].ToString();
            txtAddress.Text = ds11.Tables[0].Rows[0]["Address"].ToString();
            txtPhone.Text = ds11.Tables[0].Rows[0]["PhoneNo"].ToString();
          
            ImgPersion.ImageUrl = ds11.Tables[0].Rows[0]["Image"].ToString();
            Session["ImagePath"] = ds11.Tables[0].Rows[0]["Image"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FileImage.HasFile)
        {

          

            string p = FileImage.PostedFile.FileName.ToString();
            string e1 = Path.GetExtension(p);
            string Filename = System.IO.Path.GetFileNameWithoutExtension(p);
            Session["thisDir"] = Server.MapPath(".");
            string path = Session["thisDir"] + "\\Docs\\" + "User";
            {

                if (!System.IO.Directory.Exists(path))
                {
                    Session["PhotoCount"] = 0;
                }
                else
                {
                    Session["PhotoCount"] = Directory.GetFiles(path).Length;
                }

            }
            Session["PhotoCount"] = int.Parse(Session["PhotoCount"].ToString()) + 1;


            string fName = Filename + Session["PhotoCount"].ToString() + e1;
            string FileUploadNAme = fName;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(Session["thisDir"] + "\\Docs\\" + "User");
            }
            FileImage.PostedFile.SaveAs(Session["thisDir"] + "\\Docs\\" + "User" + "\\" + fName.ToString());
            Session["ImagePath"] = "Docs\\" + "User" + "\\" + fName.ToString();
            //l_jpg.Text = fName.ToString();

        }




        NameValueCollection objcollection11 = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds11 = new DataSet();
        objcollection11.Add("@TableId", Session["TableId"].ToString());
        objcollection11.Add("@Name", txtName.Text);
        objcollection11.Add("@Address", txtAddress.Text);
        objcollection11.Add("@PhoneNo", txtPhone.Text);
        objcollection11.Add("@EmailId", txtEmail.Text);
        objcollection11.Add("@Image", Session["ImagePath"].ToString());
        ds11 = objcls.GetdataWithPera("UpdateAdminRegistration", objcollection11);
        Response.Redirect("Admin_Profile.aspx");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CleanData();
    }

    public void CleanData()
    {
        txtName.Text = "";
        txtAddress.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        ImgPersion.ImageUrl = null;


    }
}