using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyKtvHouTai
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        DBhelper helper = new DBhelper();


        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键



        //窗体加载 隐藏错误提示
        private void Form1_Load(object sender, EventArgs e)
        {
            pbxError1.Visible = false;
            lblError1.Visible = false;
            pbxError2.Visible = false;
            lblError2.Visible = false;
            pbxError3.Visible = false;
            lblError3.Visible = false;
        }
        //判断输入的方法
        private bool CheckInput(string srUser, string srPwd)
        {
            //判断输入情况
            if (srUser.Equals(string.Empty))
            {
                lblError2.Text = "";
                pbxError2.Visible = false;
                string error1 = "账号不能为空";
                this.lblError1.Text = error1;
                this.txtUser.Focus();
                pbxError1.Visible = true;
                lblError1.Visible = true;
                pbxError3.Visible = false;
                lblError3.Visible = false;
                return false;
            }
            //判断密码如果为空，则：显示图标2与提示2
            if (srPwd.Equals(string.Empty))
            {
                lblError1.Text = "";
                pbxError1.Visible = false;
                string error2 = "密码不能为空";
                this.lblError2.Text = error2;
                this.txtPwd.Focus();
                pbxError2.Visible = true;
                lblError2.Visible = true;
                pbxError3.Visible = false;
                lblError3.Visible = false;
                return false;
            }
            //判断账号如果不为空，则：图标1与提示1不显示
            if (!srUser.Equals(string.Empty))
            {
                pbxError1.Visible = false;
                lblError1.Visible = false;
            }
            //判断密码如果不为空，则：图标2与提示2不显示
            if (!srPwd.Equals(string.Empty))
            {
                pbxError2.Visible = false;
                lblError2.Visible = false;
            }
            return true;
        }

        private void LOgin()
        {
            
            //获取输入的账号
            string srUser = this.txtUser.Text;
            //获取输入的密码
            string srPwd = this.txtPwd.Text;
            //判断输入的方法
            if (CheckInput(srUser, srPwd))
            {
                try
                {
                    helper.OpenConnection();
                    string adminName=null;
                    string StrSql = "select Count(*) from admin_info where admin_user='" + srUser + "' and admin_pwd='" + srPwd + "'";
                    SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                    int count = (int)comm.ExecuteScalar();
                    if (count == 1)
                    {
                        string StrSql2 = "  select admin_name from admin_info where admin_user='" + srUser + "'";
                        SqlCommand comm2 = new SqlCommand(StrSql2, helper.Connection);
                        SqlDataReader reader = comm2.ExecuteReader();
                        while (reader.Read())
                        {
                           adminName = reader["admin_name"].ToString();
                        }
                        Main main = new Main();
                        main.AdminName = adminName;
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        lblError3.Text = "登陆失败,账号或密码有误\n请检查账号和密码后重新输入";
                        pbxError3.Visible = true;
                        lblError3.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    helper.CloseConnection();
                }
            }
        }
        //单机登陆事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LOgin();
        }
        //最小化
        private void metroMin1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void metroClose1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

       

    }
}
