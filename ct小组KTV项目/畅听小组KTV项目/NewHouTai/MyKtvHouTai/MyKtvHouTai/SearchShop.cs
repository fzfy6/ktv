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
    public partial class SearchShop : Form
    {
        public SearchShop()
        {
            InitializeComponent();
        }
        DBhelper helper = new DBhelper();


        //右上角关闭
        private void metroClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //最小化
        private void metroMin1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        //窗体加载事件 默认加载全部商品
        private void SearchShop_Load(object sender, EventArgs e)
        {
            EverySearchShop();
        }
        //查询所有商品
        private void EverySearchShop()
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = "select shop_id,shop_name,shop_much,shoptype_name from shop_info,shop_type where shop_info.shoptype_id=shop_type.shoptype_id";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "shop");
            this.dataGridView1.DataSource = ds.Tables["shop"];
        }
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void SearchShop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void SearchShop_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void SearchShop_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        //查询
        private void button5_Click(object sender, EventArgs e)
        {
            SelectSingerInfo();
        }
        private void SelectSingerInfo()
        {
            //获取输入信息
            string srInfo = this.txtInputInfo.Text;
            if (srInfo.Trim().Equals(string.Empty))
            {
                EverySearchShop();
            }
            else
            {
                //创建数据集对象
                DataSet ds = new DataSet();
                //Sql语句
                string StrSql = " select shop_id,shop_name,shop_much,shoptype_name from shop_info,shop_type where shop_info.shoptype_id=shop_type.shoptype_id and shop_name like '%" + srInfo + "%'";
                //创建数据适配器对象
                SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
                //填充数据集
                sd.Fill(ds, "shop");
                this.dataGridView1.DataSource = ds.Tables["shop"];
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //按照id排序（查询所有商品）
            EverySearchShop();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //按照商品名称排序
            SearchShopByName();
        }
        private void SearchShopByName()
        {
            //创建数据集对象
            DataSet ds = new DataSet();
            //Sql语句
            string StrSql = "select shop_id,shop_name,shop_much,shoptype_name from shop_info,shop_type where shop_info.shoptype_id=shop_type.shoptype_id order by shop_name";
            //创建数据适配器对象
            SqlDataAdapter sd = new SqlDataAdapter(StrSql, helper.Connection);
            //填充数据集
            sd.Fill(ds, "shop");
            this.dataGridView1.DataSource = ds.Tables["shop"];
        }

        private void 修改信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string XuanZhong = this.dataGridView1.SelectedRows[0].Cells["shop_id"].Value.ToString();
            if (XuanZhong.Equals(string.Empty))
            {
                MessageBox.Show("没有选中任何商品,请选中后再操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //创建对象
                AddUpdateShop aus = new AddUpdateShop();
                aus.shopid = this.dataGridView1.SelectedRows[0].Cells["shop_id"].Value.ToString();
                aus.lblCheck.Text = "修改";
                aus.Show();
            }
        }

        private void 删除信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取商品名称
            string shopId = this.dataGridView1.SelectedRows[0].Cells["shop_id"].Value.ToString();
            string shopName = this.dataGridView1.SelectedRows[0].Cells["shop_name"].Value.ToString();
            if (shopId.Equals(string.Empty))
            {
                MessageBox.Show("没有选中任何商品,请选中后再操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string deleteinfo = string.Format("你选择的商品是:{0}\n你确定要删除吗？", shopName);
                DialogResult result = MessageBox.Show(deleteinfo, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        helper.OpenConnection();
                        //Sql命令
                        //删除商品 [song_info]
                        string newSql = "delete from shop_info where shop_id=" + shopId + "";
                        //执行Command命令
                        SqlCommand com = new SqlCommand(newSql, helper.Connection);
                        int num = com.ExecuteNonQuery();
                        if (num == 1)
                        {
                            MessageBox.Show("删除商品成功");
                        }
                        else
                        {
                            MessageBox.Show("删除商品失败");
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
