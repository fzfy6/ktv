using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;  //有关TextBox的字符验证
using System.Data.SqlClient;

namespace MyKtvHouTai
{
    public partial class AddUpdateShop : Form
    {
        public AddUpdateShop()
        {
            InitializeComponent();
        }

        //文件管理器对象
        OpenFileDialog ofd;
        //数据库操作对象
        DBhelper helper = new DBhelper();
        //商品Id 
        public string shopid;

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


        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键

        private void AddUpdateShop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void AddUpdateShop_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void AddUpdateShop_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        //添加商品图片
        private void metroButton4_Click(object sender, EventArgs e)
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
            this.txtShopImgUrl.Text = fname;
            //把图片读取出来防到pictureBox1中
            pictureBox1.ImageLocation = ofd.FileName;
        }
        //关闭窗口
        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //窗体加载事件
        private void AddUpdateShop_Load(object sender, EventArgs e)
        {
            //面板默认样式
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];

            //商品类型
            string shoptype_id = null;
            if (shopid == null)
            {
                return;
            }
            helper.OpenConnection();
            string newSql = "select * from shop_info where shop_id=" + shopid + "";
            SqlCommand com = new SqlCommand(newSql, helper.Connection);
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                this.txtShopName.Text = read["shop_name"].ToString();
                this.txtShopMuch.Text = read["shop_much"].ToString();
                shoptype_id = read["shoptype_id"].ToString();
                this.txtShopImgUrl.Text = read["shop_photo_url"].ToString();
            }
            //把读取出来的照片信息显示到pictbox中
            pictureBox1.ImageLocation = this.txtShopImgUrl.Text;
            //判断商品类型并显示到面板中
            if (shoptype_id.Equals("1"))
            {
                TypeXiao();
            }
            else if (shoptype_id.Equals("2"))
            {
                TypeJiu();
            }
            else if (shoptype_id.Equals("3"))
            {
                TypeOther();
            }
            read.Close();
        }


        //商品类型ID
        int ShopId = 0;
        //商品类型：小吃
        private void panel1_Click(object sender, EventArgs e)
        {
            TypeXiao();
        }
        private void TypeXiao()
        {
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[1];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[2];
            ShopId = 1;
        }
        //商品类型：酒水
        private void panel2_Click(object sender, EventArgs e)
        {
            TypeJiu();
        }
        private void TypeJiu()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[1];
            this.panel3.BackgroundImage = imageList1.Images[2];
            ShopId = 2;
        }
        //商品类型：其他
        private void panel3_Click(object sender, EventArgs e)
        {
            TypeOther();
        }
        private void TypeOther()
        {
            this.panel1.BorderStyle = BorderStyle.None;
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.BackgroundImage = imageList1.Images[2];
            this.panel2.BackgroundImage = imageList1.Images[2];
            this.panel3.BackgroundImage = imageList1.Images[1];
            ShopId = 3;
        }



        //检查输入信息
        private bool CheckInput()
        {
            //判断商品名称
            if (this.txtShopName.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("商品名称没填写");
                this.txtShopName.Focus();
                return false;
            }
            //判断商品照片信息
            if (this.txtShopImgUrl.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("商品照片未添加");
                this.txtShopImgUrl.Focus();
                return false;
            }

            //获取商品价格的值
            string spMuch = this.txtShopMuch.Text.Trim();
            //C#中的正则表达式
            Regex regex = new Regex(@"^[0-9.]+$");
            if (spMuch.Equals(string.Empty))
            {
                MessageBox.Show("商品价格不能为空");
                this.txtShopMuch.Focus();
                return false;
            }
            else if (!regex.IsMatch(spMuch))
            {
                MessageBox.Show("商品价格只能为数字和小数点");
                this.txtShopMuch.Focus();
                return false;
            }

            //判断商品价格填写
            string ShopMuch = this.txtShopMuch.Text;
            //这里就是将String从第0个索引开始，取出长度为1的一个String
            string MeShopMuch = ShopMuch.Substring(0, 1);
            if (MeShopMuch.Equals("."))
            {
                MessageBox.Show("价格不能以“.”开头");
                this.txtShopMuch.Focus();
                return false;
            }

            //获取“.”出现的次数
            int count = 0;

            foreach (char num in ShopMuch)
            {
                if (num == '.')
                {
                    count++;
                }
            }
            if (count > 1)
            {
                MessageBox.Show("商品价格格式有误");
                this.txtShopMuch.Focus();
                return false;
            }

            //倒叙获取字符串最后一个字符
            string value=null;
            for (int i = ShopMuch.Length - 1; i >= 0; i--)
            {
                value += ShopMuch[i];
            }
           // MessageBox.Show(value);
            string MeShopMuch2 = value.Substring(0, 1);
            if (MeShopMuch2.Equals("."))
            {
                MessageBox.Show("价格不能以“.”结尾");
                this.txtShopMuch.Focus();
                return false;
            }
            //检查选中商品类型是否为空
            if (ShopId == 0)
            {
                MessageBox.Show("商品类型没选");
                return false;
            }

            return true;
          
        }
        //添加商品
        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                //获取商品名称
                string ShopName = this.txtShopName.Text;
                //获取商品图片地址
                string ShopImgUrl = this.txtShopImgUrl.Text;
                //获取商品价格
                string ShopMuch = this.txtShopMuch.Text;

                if (lblCheck.Text == "添加")
                {
                    try
                    {
                        helper.OpenConnection();
                        //Sql命令
                        string StrSql = "  insert into shop_info values('" + ShopName + "'," + ShopMuch + "," + ShopId + ",'" + ShopImgUrl + "');";
                        SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                        int count = (int)comm.ExecuteNonQuery();
                        if (count == 1)
                        {
                            MessageBox.Show("新增商品成功");
                        }
                        else
                        {
                            MessageBox.Show("新增商品失败");
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
                        string StrSql = "update [shop_info] set shop_name='" + ShopName + "',shop_much=" + ShopMuch + ",shoptype_id=" + ShopId + ",shop_photo_url='" + ShopImgUrl + "' where shop_id=" + shopid + "";
                        SqlCommand comm = new SqlCommand(StrSql, helper.Connection);
                        int count = (int)comm.ExecuteNonQuery();
                        if (count == 1)
                        {
                            MessageBox.Show("修改商品成功");

                        }
                        else
                        {
                            MessageBox.Show("修改商品失败");
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
