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

public partial class Admin_Edit_Student : System.Web.UI.Page
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
        Session["UserImage"] = "";
        NameValueCollection objcollection11 = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds11 = new DataSet();
        objcollection11.Add("@TableId", Request.QueryString["Rid"].ToString());
        //objcollection11.Add("@Type", Session["Type"].ToString());
        ds11 = objcls.GetdataWithPera("selectStudentRegistration", objcollection11);
        if (ds11.Tables[0].Rows.Count > 0)
        {

            dr_Type.SelectedValue = ds11.Tables[0].Rows[0]["Type"].ToString();
            txtStudentId.Text=ds11.Tables[0].Rows[0]["ProfId"].ToString();
            txtName.Text=ds11.Tables[0].Rows[0]["Name"].ToString();
            dr_Graduatyear.SelectedValue=ds11.Tables[0].Rows[0]["GraduatedYear"].ToString();
            dr_course.SelectedValue=ds11.Tables[0].Rows[0]["Course"].ToString();
            txtPhone.Text=ds11.Tables[0].Rows[0]["Phone"].ToString();
            txtEmailId.Text=ds11.Tables[0].Rows[0]["Email"].ToString();
            txtParmanaantaddress.Text=ds11.Tables[0].Rows[0]["Address"].ToString();
            txtPossition.Text=ds11.Tables[0].Rows[0]["Workposition"].ToString();
            ImgPersion.ImageUrl = ds11.Tables[0].Rows[0]["Image"].ToString();
            Session["UserImage"] = ds11.Tables[0].Rows[0]["Image"].ToString();


            
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FileImage.HasFile)
        {

            if (Session["UserImage"] == "Upload_File/DefultImage_123.png")
            {

            }
            else
            {
               // File.Delete(Session["UserImage"].ToString());
            }

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
            Session["UserImage"] = "Docs\\" + "User" + "\\" + fName.ToString();
            //l_jpg.Text = fName.ToString();

        }




        NameValueCollection objcollection11 = new NameValueCollection();
        objcls = new ClsKrisnAgr(strConnectionString);
        DataSet ds11 = new DataSet();
        objcollection11.Add("@RId", Request.QueryString["Rid"].ToString());
        objcollection11.Add("@ProfId", txtStudentId.Text);
        objcollection11.Add("@Type", dr_Type.SelectedValue);
        objcollection11.Add("@Name", txtName.Text);
        objcollection11.Add("@GraduatedYear", dr_Graduatyear.SelectedValue);
        objcollection11.Add("@Course",dr_course.SelectedValue);
        objcollection11.Add("@Phone", txtPhone.Text);
        objcollection11.Add("@Email", txtEmailId.Text);
        objcollection11.Add("@Address", txtParmanaantaddress.Text);
        objcollection11.Add("@Workposition", txtPossition.Text);
       
        objcollection11.Add("@Image", Session["UserImage"].ToString());
        ds11 = objcls.GetdataWithPera("UpdateRegistration", objcollection11);
        Response.Redirect("View_User.aspx");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dr_Type.SelectedIndex = 0;
        txtStudentId.Text = "";
        txtName.Text = "";
        dr_Graduatyear.SelectedIndex = 0;
        dr_course.SelectedIndex = 0;
        txtEmailId.Text = "";
        txtPhone.Text = "";
        txtParmanaantaddress.Text = "";
        txtPossition.Text = "";
        ImgPersion.ImageUrl = null;
    }
}