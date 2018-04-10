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
    public partial class SearchGeShou : Form
    {
        public SearchGeShou()
        {
            InitializeComponent();
        }
        DBhelper helper = new DBhelper();


        //窗体加载事件 默认加载全部歌手信息
        private void SearchGeShou_Load(object sender, EventArgs e)
        {
            ShowsingerInfo();
        }
        //加载全部歌手信息
        private void ShowsingerInfo()
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = " select singer_id,singer_name,singer_sex,singertype_name from singer_info,singer_type where singer_info.singertype_id=singer_type.singertype_id";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "singer");
            this.dataGridView1.DataSource = ds.Tables["singer"];
        }

        //查询 事件
        private void button5_Click(object sender, EventArgs e)
        {
            //查询全部
            SelectSingerInfo();
        }
        //查询方法
        private void SelectSingerInfo()
        {
            //获取输入信息
            string srInfo = this.txtInputInfo.Text;
            if (srInfo.Trim().Equals(string.Empty))
            {
                ShowsingerInfo();
            }
            else
            {
                //创建数据集对象
                DataSet ds = new DataSet();
                //Sql语句
                string StrSql = " select singer_id,singer_name,singer_sex,singertype_name from singer_info,singer_type where singer_info.singertype_id=singer_type.singertype_id and singer_name like '%" + srInfo + "%'";
                //创建数据适配器对象
                SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
                //填充数据集
                sd.Fill(ds, "singer");
                this.dataGridView1.DataSource = ds.Tables["singer"];
            }
        }

        //按照姓名排序
        private void metroButton2_Click(object sender, EventArgs e)
        {
            ShowsingerInfoByName();
        }
        //按照姓名排序
        private void ShowsingerInfoByName()
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = " select singer_id,singer_name,singer_sex,singertype_name from singer_info,singer_type where singer_info.singertype_id=singer_type.singertype_id order by singer_name";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "singer");
            this.dataGridView1.DataSource = ds.Tables["singer"];
        }

        //按照编号排序
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //查询全部
            ShowsingerInfo();
        }

        //修改信息
        private void 修改信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string XuanZhong = this.dataGridView1.SelectedRows[0].Cells["singer_id"].Value.ToString();
            if (XuanZhong.Equals(string.Empty))
            {
                MessageBox.Show("没有选中任何歌手,请选中后再操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //创建对象
                AddUpdateGeShou aug = new AddUpdateGeShou();
                aug.singer_id = this.dataGridView1.SelectedRows[0].Cells["singer_id"].Value.ToString();
                aug.lblCheck.Text = "修改";
                aug.Show();
            }
        }

        //删除信息
        private void 删除信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取歌手名称
            string geshouid = this.dataGridView1.SelectedRows[0].Cells["singer_id"].Value.ToString();
            string geshouname = this.dataGridView1.SelectedRows[0].Cells["singer_name"].Value.ToString();
            if (geshouid.Equals(string.Empty))
            {
                MessageBox.Show("没有选中任何歌手,请选中后再操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string deleteinfo = string.Format("你选择的歌手是:{0}删除歌手并且会删除该歌手的歌曲\n你确定要删除吗？", geshouname);
                DialogResult result = MessageBox.Show(deleteinfo, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        helper.OpenConnection();
                        //Sql命令
                        //先删除歌曲 [song_info]
                        string newSql = "delete from song_info where singer_id='" + geshouid + "'";
                        //再删除歌手 [singer_info]
                        string newSql1 = "delete from singer_info where singer_id='" + geshouid + "'";
                        //执行Command命令
                        SqlCommand com = new SqlCommand(newSql, helper.Connection);
                        SqlCommand com1 = new SqlCommand(newSql1, helper.Connection);
                        int num = com1.ExecuteNonQuery();
                        if (num == 1)
                        {
                            MessageBox.Show("删除歌手成功");
                        }
                        else
                        {
                            MessageBox.Show("删除歌手失败");
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

        //右上角 窗口关闭
        private void metroClose1_Click(object sender, EventArgs e)
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
        private void SearchGeShou_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void SearchGeShou_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void SearchGeShou_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        



    }
}
