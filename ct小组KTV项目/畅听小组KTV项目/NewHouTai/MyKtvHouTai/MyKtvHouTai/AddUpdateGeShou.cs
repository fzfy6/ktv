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
    public partial class AddUpdateGeShou : Form
    {
        public AddUpdateGeShou()
        {
            InitializeComponent();
        }
        //文件管理器对象
        OpenFileDialog ofd;
        //数据库操作对象
        DBhelper helper = new DBhelper();
        //歌手编号
        public string singer_id;

        //添加照片事件
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //new一个打开文件类
            ofd = new OpenFileDialog();
            //选择要打开的文件类型
            ofd.Filter = @"图像文件|*.jpg;*.gif;*.png;*.jpeg;*.bmp";
            //把选择文件的窗口加载出来
            ofd.ShowDialog();
            //获取文件路径
            string fname = ofd.FileName;
            //把文件路径显示到 textbox2 中
            this.textBox2.Text = fname;
            //把图片读取出来防到pictureBox1中
            pictureBox1.ImageLocation = ofd.FileName;
        }

        //关闭窗口
        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //右上角 窗口关闭
        private void myClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //最小化
        private void metroMin1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        //获取歌手的性别（默认为：男）
        string singSex = "未知";
        //歌手性别 选中男
        private void panel1_Click(object sender, EventArgs e)
        {
            SexBoy();
        }
        private void SexBoy()
        {
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[1];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];
            singSex = "男";
        }
        //歌手性别 选中女
        private void panel2_Click(object sender, EventArgs e)
        {
            SexGirl();
        }
        private void SexGirl()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[1];
            this.panel3.BackgroundImage = imageList1.Images[2];
            singSex = "女";
        }
        //歌手性别 选中组合
        private void panel3_Click(object sender, EventArgs e)
        {
            SexAnd();
        }
        private void SexAnd()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[1];
            singSex = "组合";
        }
        //歌手类型
        int singType = 0;
        //歌手类别 选中大陆
        private void panel4_Click(object sender, EventArgs e)
        {
            TypeChinaDaLu();
        }
        private void TypeChinaDaLu()
        {
            this.panel5.BorderStyle = BorderStyle.None;
            this.panel6.BorderStyle = BorderStyle.None;
            this.panel7.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.FixedSingle;
            this.panel4.BackgroundImage = imageList1.Images[1];
            this.panel5.BackgroundImage = imageList1.Images[2];
            this.panel6.BackgroundImage = imageList1.Images[2];
            this.panel7.BackgroundImage = imageList1.Images[2];
            singType = 1;
        }
        //歌手类别 选中港台
        private void panel5_Click(object sender, EventArgs e)
        {
            TypeChinaTaiwan();
        }
        private void TypeChinaTaiwan()
        {
            this.panel7.BorderStyle = BorderStyle.None;
            this.panel6.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.None;
            this.panel5.BorderStyle = BorderStyle.FixedSingle;
            this.panel4.BackgroundImage = imageList1.Images[2];
            this.panel5.BackgroundImage = imageList1.Images[1];
            this.panel6.BackgroundImage = imageList1.Images[2];
            this.panel7.BackgroundImage = imageList1.Images[2];
            singType = 2;
        }
        //歌手类别 选中日韩
        private void panel6_Click(object sender, EventArgs e)
        {
            TypeRiHan();
        }
        private void TypeRiHan()
        {
            this.panel5.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.None;
            this.panel7.BorderStyle = BorderStyle.None;
            this.panel6.BorderStyle = BorderStyle.FixedSingle;
            this.panel4.BackgroundImage = imageList1.Images[2];
            this.panel5.BackgroundImage = imageList1.Images[2];
            this.panel6.BackgroundImage = imageList1.Images[1];
            this.panel7.BackgroundImage = imageList1.Images[2];
            singType = 3;
        }
        //歌手类别 选中欧美
        private void panel7_Click(object sender, EventArgs e)
        {
            TypeOuMei();
        }
        private void TypeOuMei()
        {
            this.panel5.BorderStyle = BorderStyle.None;
            this.panel6.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.None;
            this.panel7.BorderStyle = BorderStyle.FixedSingle;
            this.panel4.BackgroundImage = imageList1.Images[2];
            this.panel5.BackgroundImage = imageList1.Images[2];
            this.panel6.BackgroundImage = imageList1.Images[2];
            this.panel7.BackgroundImage = imageList1.Images[1];
            singType = 4;
        }

        //点击 添加 或者 修改 事件
        private void metroButton2_Click(object sender, EventArgs e)
        {
            //判断歌手姓名
            if (this.textBox1.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("歌手姓名没填");
                this.textBox1.Focus();
                return;
            }
            //判断歌手照片信息
            if (this.textBox2.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("歌手照片没选");
                this.textBox2.Focus();
                return;
            }
            //判断歌手性别
            if (singSex.Equals("未知"))
            {
                MessageBox.Show("性别没选中");
                return;
            }
            //判断歌手类型
            if (singType == 0)
            {
                MessageBox.Show("歌手类型没选中");
                return;
            }
            if (lblCheck.Text == "添加")
            {
                try
                {
                    helper.OpenConnection();
                    //Sql命令
                    string StrSql = "  insert into singer_info values('" + this.textBox1.Text + "'," + singType + ",'" + singSex + "','" + this.textBox2.Text + "')";
                    SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                    int count = (int)comm.ExecuteNonQuery();
                    if (count == 1)
                    {
                        MessageBox.Show("新增歌手成功");
                    }
                    else
                    {
                        MessageBox.Show("新增歌手失败");
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
            else if (lblCheck.Text == "修改")
            {
                try
                {
                    helper.OpenConnection();
                    //Sql命令
                    string StrSql = "update [singer_info] set singer_name='" + this.textBox1.Text + "',singertype_id=" + singType + ",singer_sex='" + singSex + "',siinger_photo_url='" + this.textBox2.Text + "' where singer_id=" + singer_id + "";
                    SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                    int count = (int)comm.ExecuteNonQuery();
                    if (count == 1)
                    {
                        MessageBox.Show("修改歌手成功");
                    }
                    else
                    {
                        MessageBox.Show("修改歌手失败");
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

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void AddUpdateGeShou_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void AddUpdateGeShou_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void AddUpdateGeShou_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
       
        private void AddUpdateGeShou_Load(object sender, EventArgs e)
        {
            //面板默认样式
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];
            this.panel4.BackgroundImage = imageList1.Images[2];
            this.panel5.BackgroundImage = imageList1.Images[2];
            this.panel6.BackgroundImage = imageList1.Images[2];
            this.panel7.BackgroundImage = imageList1.Images[2];
            
            //歌手类型
            string singertype_id = null;
            //歌手性别
            string singer_sex = null;
            if (singer_id ==null)
            {
                return;
            }
            helper.OpenConnection();
            string newSql = "select *from singer_info where singer_id=" + singer_id + "";
            SqlCommand com = new SqlCommand(newSql, helper.Connection);
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                this.textBox1.Text = read["singer_name"].ToString();
                this.textBox2.Text = read["siinger_photo_url"].ToString();
                singertype_id = read["singertype_id"].ToString();
                singer_sex = read["singer_sex"].ToString();
            }
            //把读取出来的照片信息显示到pictbox中
            pictureBox1.ImageLocation = this.textBox2.Text;
            //判断歌手性别并显示到面板中
            if (singer_sex.Equals("男"))
            {
                SexBoy();
            }
            else if (singer_sex.Equals("女"))
            {
                SexGirl();
            }
            else if (singer_sex.Equals("组合"))
            {
                SexAnd();
            }
            //判断歌手类型并显示到面板中
            if (singertype_id == "1")
            {
                TypeChinaDaLu();
            }
            else if (singertype_id == "2")
            {
                TypeChinaTaiwan();
            }
            else if (singertype_id == "3")
            {
                TypeRiHan();
            }
            else if (singertype_id == "4")
            {
                TypeOuMei();
            }
            //关闭read连接
            read.Close();
            //断开数据库连接
            helper.CloseConnection();
        }






    }
}
