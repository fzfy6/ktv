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
    public partial class InstallCp : Form
    {
        public InstallCp()
        {
            InitializeComponent();
        }

        DBhelper helper = new DBhelper();
        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            //右边的Loading显示
            this.myLoading1.Visible = true;
            //获取用户输入信息
            string inputinfo = this.textBox1.Text;
            if (CheceInput(inputinfo))
            {
                helper.OpenConnection();
                string StrSql = "select Count(*) from ZCM where zcm='" + inputinfo + "'";
                SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                int count = (int)comm.ExecuteScalar();
                if (count == 1)
                {
                    MessageBox.Show("注册成功！密钥已应用到本产品\n本窗口将自动关闭", "畅响Ktv注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("注册失败!请检查你的密钥", "畅响Ktv注册", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //判断输入信息
        private static bool CheceInput(string inputinfo)
        {
            if (inputinfo.Trim().Equals(string.Empty))
            {
                MessageBox.Show("您输入的注册码有误", "畅响Ktv注册", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (inputinfo.Length < 16)
            {
                MessageBox.Show("注册证码长度出错", "畅响Ktv注册", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
       

        private void label4_Click_1(object sender, EventArgs e)
        {
            //调用默认浏览器，跳转到指定网页
            System.Diagnostics.Process.Start("http://www.baidu.com");
        }

        private void linkLabel1_Click_1(object sender, EventArgs e)
        {
            //调用默认浏览器，跳转到指定网页
            System.Diagnostics.Process.Start("http://www.baidu.com");
        }

        //右上角关闭
        private void myClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //最小化
        private void metroMin1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;
        private void InstallCp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void InstallCp_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void InstallCp_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }


    }
}
