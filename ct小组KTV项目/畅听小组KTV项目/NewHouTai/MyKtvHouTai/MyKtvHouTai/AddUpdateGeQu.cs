using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;  //有关TextBox的字符验证

namespace MyKtvHouTai
{
    public partial class AddUpdateGeQu : Form
    {
        public AddUpdateGeQu()
        {
            InitializeComponent();
        }
        public string songid;
        DBhelper helper = new DBhelper();
        OpenFileDialog ofd;

        //窗体加载事件 获取歌手并绑定到comebox控件
        private void AddUpdateGeQu_Load(object sender, EventArgs e)
        {
            //读取歌手并绑定 到Comebox
            ReadSongName();
            //面板默认样式
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];
            this.panel4.BackgroundImage = imageList1.Images[2];
            //如果是修改，则获取歌曲的信息
            ReadGeQuInfo();


        }
        //获取歌曲信息
        private void ReadGeQuInfo()
        {
            string songType = null;
            string Gradename = null;
           // ReadSongName();
            if (songid == null)
            {
                return;
            }
            helper.OpenConnection();
            string newSql = "select song_name,singer_name,song_ab,songtype_id,song_url,song_play_count from song_info,singer_info where song_info.singer_id=singer_info.singer_id and song_id=" + songid + "";
            SqlCommand com = new SqlCommand(newSql, helper.Connection);
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                this.textBox1.Text = read["song_name"].ToString();
                //获取拼音
                this.txtab.Text = read["song_ab"].ToString();
                //获取路径
                this.txtUrl.Text = read["song_url"].ToString();
                songType = read["songtype_id"].ToString();
                //获取播放次数
                this.textBox2.Text = read["song_play_count"].ToString();
                //获取歌手名称
                Gradename = read["singer_name"].ToString();
            }
            //添加到组合框
            comboBox1.Text = Gradename;
            if (songType.Equals("1"))
            {
                TypeHot();
            }
            else if (songType.Equals("2"))
            {
                TypeOld();
            }
            else if (songType.Equals("3"))
            {
                TypeKing();
            }
            else if (songType.Equals("4"))
            {
                TypeGame();
            }
            read.Close();
            helper.CloseConnection();
        }


        //读取歌手姓名
        private void ReadSongName()
        {
            //从数据库中获取歌手数据并绑定到ComeBox控件中
            DataSet ds = new DataSet();
            string strsql = "  select * from singer_info";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(strsql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "singerInfo");
            //获得数据源
            this.comboBox1.DataSource = ds.Tables["singerInfo"];
            //显示的值
            this.comboBox1.DisplayMember = "singer_name";
            //实际的值
            this.comboBox1.ValueMember = "singer_id";
        }

        //右上角关闭
        private void myClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //最小化
        private void metroMin1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void AddUpdateGeQu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void AddUpdateGeQu_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void AddUpdateGeQu_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        //点击刷新事件
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //读取全部歌手
            ReadSongName();
        }
        //增加歌手
        private void metroButton2_Click(object sender, EventArgs e)
        {
            AddUpdateGeShou augs = new AddUpdateGeShou();
            augs.Show();
        }
        //获取视频路径
        private void metroButton3_Click(object sender, EventArgs e)
        {
            //new一个打开文件类
            ofd = new OpenFileDialog();
            //选择要打开的文件类型
            ofd.Filter = @"视频文件|*.mp4;*.avi;*.wma;*.rmvb,*rm;*.mp4;*.mid;*.3GP;*.flv;";
            //把选择文件的窗口加载出来
            ofd.ShowDialog();
            //获取文件路径
            string fname = ofd.FileName;
            //把文件路径显示到 textbox3 中
            this.txtUrl.Text = fname;
        }

        //返回（关闭当前窗口）
        private void metroButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //歌曲类型
        int TypeId = 0;
        //歌曲类型：热门流行
        private void panel1_Click(object sender, EventArgs e)
        {
            TypeHot();
        }
        //热门流行
        private void TypeHot()
        {
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.None;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[1];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];
            this.panel4.BackgroundImage = imageList1.Images[2];
            TypeId = 1;
        }
        //歌曲类型：经典老歌
        private void panel2_Click(object sender, EventArgs e)
        {
            TypeOld();
        }
        //经典老歌
        private void TypeOld()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[1];
            this.panel3.BackgroundImage = imageList1.Images[2];
            this.panel4.BackgroundImage = imageList1.Images[2];
            TypeId = 2;
        }
        //歌曲类型：影视金曲
        private void panel3_Click(object sender, EventArgs e)
        {
            TypeKing();
        }
        //影视金曲
        private void TypeKing()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[1];
            this.panel4.BackgroundImage = imageList1.Images[2];
            TypeId = 3;
        }
        //歌曲类型：游戏动漫
        private void panel4_Click(object sender, EventArgs e)
        {
            TypeGame();
        }
        //游戏动漫
        private void TypeGame()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel4.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];
            this.panel4.BackgroundImage = imageList1.Images[1];
            TypeId = 4;
        }




        //点击添加.修改 事件
        private void metroButton6_Click(object sender, EventArgs e)
        {
            //判断输入情况
            if (CheckInput())
            {
                if (lblUpdate.Text == "添加")
                {
                    //获取歌手名称
                    string singName = this.comboBox1.SelectedValue.ToString();
                    //获取歌曲名称
                    string songName = this.textBox1.Text;
                    //获取歌曲拼音
                    string songPy = this.txtab.Text;
                    //获取字数
                    int songZishu = this.txtab.Text.Length;
                    //获取视频地址
                    string videoUrl = this.txtUrl.Text;
                    //获取播放次数
                    int countci2 = int.Parse(this.textBox2.Text);
                    //string countci = this.numericUpDown1.Value.ToString();
                    //int countci2 = int.Parse(countci);
                    try
                    {
                        helper.OpenConnection();
                        //Sql命令
                        string StrSql = " insert into song_info values('" + songName + "','" + songPy + "'," + songZishu + "," + TypeId + "," + singName + ",'" + videoUrl + "'," + countci2 + ")";
                        SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                        int count = (int)comm.ExecuteNonQuery();
                        if (count == 1)
                        {
                            MessageBox.Show("新增歌曲成功");
                        }
                        else
                        {
                            MessageBox.Show("新增歌曲失败");
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
                else if (lblUpdate.Text == "修改")
                {
                    //获取歌手名称
                    string singName = this.comboBox1.SelectedValue.ToString();
                    int singName2 = Convert.ToInt32(singName);
                    //获取歌曲名称
                    string songName = this.textBox1.Text;
                    //获取歌曲拼音
                    string songPy = this.txtab.Text;
                    //获取字数
                    int songZishu = this.txtab.Text.Length;
                    //获取视频地址
                    string videoUrl = this.txtUrl.Text;
                    //获取播放次数
                    int countci2 = int.Parse(this.textBox2.Text);
                    //string countci = this.numericUpDown1.Value.ToString();
                    //int countci2 = int.Parse(countci);
                    try
                    {
                        helper.OpenConnection();
                        //Sql命令
                        string StrSql = " update song_info set song_name='" + songName + "',song_ab='" + songPy + "',song_word_count=" + songZishu + ",songtype_id=" + TypeId + ",singer_id=" + singName2 + ",song_url='" + videoUrl + "',song_play_count=" + countci2 + " where song_id=" + songid + "";
                        SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                        int count = (int)comm.ExecuteNonQuery();
                        if (count == 1)
                        {
                            MessageBox.Show("修改歌曲成功");
                        }
                        else
                        {
                            MessageBox.Show("修改歌曲失败");
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
        }
        //验证 输入
        private bool CheckInput()
        {
            //获取歌曲名称的值
            string songName = this.textBox1.Text.Trim();
            if (songName.Equals(string.Empty))
            {
                MessageBox.Show("歌曲名称没填");
                this.textBox1.Focus();
                return false;
            }
            //获取歌曲拼音的值
            string songPy = this.txtab.Text.Trim();
            //C#中的正则表达式
            Regex regex = new Regex(@"^[A-Za-z]+$");
            if (songPy.Equals(string.Empty))
            {
                MessageBox.Show("歌曲拼音不能为空");
                this.txtab.Focus();
                return false;
            }
            else if (!regex.IsMatch(songPy))
            {
                MessageBox.Show("歌曲拼音只能为字母");
                this.txtab.Focus();
                return false;
            }
            //判断歌曲类型
            if (TypeId == 0)
            {
                MessageBox.Show("歌曲类型没选择");
                return false;
            }
            //判断视频信息是否为空
            if (this.txtUrl.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("没有添加视频信息");
                this.txtUrl.Focus();
                return false;
            }
            //获取播放次数的值
            string songCount = this.textBox2.Text.Trim();
            //C#中的正则表达式
            Regex regex2 = new Regex(@"^[0-9]+$");
            if (songCount.Equals(string.Empty))
            {
                MessageBox.Show("播放次数不能为空");
                this.textBox2.Text = "0";
                this.txtab.Focus();
                return false;
            }
            else if (!regex2.IsMatch(songCount))
            {
                MessageBox.Show("播放次数只能为数字");
                this.txtab.Focus();
                return false;
            }
            return true;
        }

       



    }
}
