using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyKtvHouTai
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        //获取登陆的管理员账号
        public string AdminName;

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键


        //窗体加载事件
        private void Main_Load(object sender, EventArgs e)
        {
            //获取管理员名称
            this.lblTipsUser.Text = "管理员: " + AdminName +" 您好";
            //获取当前时间
            lblTime.Text = "当前时间：" + DateTime.Now.ToString();
        }
        
        //动态读取时间
        #region
        private void ReadNowTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "当前时间：" + DateTime.Now.ToString();
        }
        #endregion

        //版权所有单机事件 跳转到官网
        private void lblCopyright_Click(object sender, EventArgs e)
        {
            //调用默认浏览器，跳转到指定网页
            System.Diagnostics.Process.Start("http://www.qq.com");
        }

        //关闭程序
        private void metroClose1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //最小化
        private void metroMin1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        //增加歌手 事件
        private void AddGeshou_Click(object sender, EventArgs e)
        {
            AddUpdateGeShou augs = new AddUpdateGeShou();
            augs.ShowDialog();
        }
        //查询歌手 删除歌手
        private void searchDelGeShou_Click(object sender, EventArgs e)
        {
            SearchGeShou sgs = new SearchGeShou();
            sgs.ShowDialog();
        }

        //添加歌曲
        private void AddGeQu_Click(object sender, EventArgs e)
        {
            AddUpdateGeQu augq = new AddUpdateGeQu();
            augq.ShowDialog();
        }

        //查询歌曲
        private void SearchGeQu_Click(object sender, EventArgs e)
        {
            SearchGeQu sgq = new SearchGeQu();
            sgq.ShowDialog();
        }

        //关于我们
        private void About_Click(object sender, EventArgs e)
        {
            //调用默认浏览器，跳转到指定网页
            System.Diagnostics.Process.Start("C:\\Users\\DuJun\\Desktop\\ImKtv\\ImKtv\\NewHouTai\\MyKtvHouTai\\MyKtvHouTai\\bin\\KTVHTML\\index.html");
            
        }

        //注册产品
        private void Insert_Click(object sender, EventArgs e)
        {
            InstallCp icp = new InstallCp();
            icp.ShowDialog();
        }

        //添加商品
        private void lblShop_Click(object sender, EventArgs e)
        {
            AddUpdateShop aus = new AddUpdateShop();
            aus.ShowDialog();
        }

        //查询商品
        private void label1_Click(object sender, EventArgs e)
        {
            SearchShop ss = new SearchShop();
            ss.ShowDialog();
        }
        //帮助文档
        private void panelHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(new Control(), "C:\\Users\\DuJun\\Desktop\\ImKtv\\ImKtv\\NewHouTai\\MyKtvHouTai\\MyKtvHouTai\\bin\\畅听KTV后台帮助.CHM", "0.html"); 
        }



      


    }
}
