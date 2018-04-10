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
    public partial class SearchGeQu : Form
    {
        public SearchGeQu()
        {
            InitializeComponent();
        }

        DBhelper helper = new DBhelper();
        //右上角 关闭窗口
        private void metroClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //最小化
        private void metroMin1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        //窗体加载 事件 默认加载全部歌曲信息
        private void SearchGeQu_Load(object sender, EventArgs e)
        {
            ReadSing();
        }
        //读取全部数据
        private void ReadSing()
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = "select song_id,song_name,singer_name,song_ab,song_word_count,songtype_name,song_play_count from song_info,singer_info,song_type where song_info.singer_id=singer_info.singer_id and song_info.songtype_id=song_type.songtype_id";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "songer");
            this.dataGridView1.DataSource = ds.Tables["songer"];
        }

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;

        private void SearchGeQu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void SearchGeQu_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void SearchGeQu_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        //单机 查询事件
        private void button5_Click(object sender, EventArgs e)
        {
            if (this.txtInputInfo.Text.Trim().Equals(string.Empty))
            {
                ReadSing();
            }
            else
            {
                SelectSing();
            }
        }
        private void SelectSing()
        {
            string srInfo = this.txtInputInfo.Text;
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = "select song_id,song_name,singer_name,song_ab,song_word_count,songtype_name,song_play_count from song_info,singer_info,song_type where song_info.singer_id=singer_info.singer_id and song_info.songtype_id=song_type.songtype_id and song_name like '%" + srInfo + "%'";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "songer");
            this.dataGridView1.DataSource = ds.Tables["songer"];
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ReadSing();
        }

        //按照歌名排序
        private void metroButton3_Click(object sender, EventArgs e)
        {
            SelectByName();
        }
        //按照歌名排序
        private void SelectByName()
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = "select song_id,song_name,singer_name,song_ab,song_word_count,songtype_name,song_play_count from song_info,singer_info,song_type where song_info.singer_id=singer_info.singer_id and song_info.songtype_id=song_type.songtype_id order by song_name";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "songer");
            this.dataGridView1.DataSource = ds.Tables["songer"];
        }

        //按照热度查询歌曲
        private void metroButton2_Click(object sender, EventArgs e)
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = "select song_id,song_name,singer_name,song_ab,song_word_count,songtype_name,song_play_count from song_info,singer_info,song_type where song_info.singer_id=singer_info.singer_id and song_info.songtype_id=song_type.songtype_id order by song_play_count desc";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "songer");
            this.dataGridView1.DataSource = ds.Tables["songer"];
        }

        //鼠标 右键 单机 修改事件
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string XuanZhong = this.dataGridView1.SelectedRows[0].Cells["song_id"].Value.ToString();
            if (XuanZhong.Equals(string.Empty))
            {
                MessageBox.Show("没有选中任何歌曲,请选中后再操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //创建对象
                AddUpdateGeQu aug = new AddUpdateGeQu();
                aug.songid = this.dataGridView1.SelectedRows[0].Cells["song_id"].Value.ToString();
                aug.lblUpdate.Text = "修改";
                aug.Show();
            }
        }
        //鼠标 右键 单机 删除事件
        private void 删除信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取歌曲名
            string geming = this.dataGridView1.SelectedRows[0].Cells["song_name"].Value.ToString();
            //获取歌曲Id
            string gequId = this.dataGridView1.SelectedRows[0].Cells["song_id"].Value.ToString();
            if (gequId.Equals(string.Empty))
            {
                MessageBox.Show("没有选中任何歌曲,请选中后再操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string deleteinfo = string.Format("歌曲名：\n你确定要删除吗？", geming);
                DialogResult result = MessageBox.Show(deleteinfo, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        helper.OpenConnection();
                        //Sql命令
                        string StrSql = " delete from song_info where song_id=" + gequId + "";
                        SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                        int count = (int)comm.ExecuteNonQuery();
                        if (count == 1)
                        {
                            MessageBox.Show("删除歌曲成功");
                            ReadSing();
                        }
                        else
                        {
                            MessageBox.Show("删除歌曲失败");
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



    }
}
