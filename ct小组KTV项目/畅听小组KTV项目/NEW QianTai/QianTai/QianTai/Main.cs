using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Player;
using System.IO;

namespace QianTai
{
    public partial class Main :Form
    { /// <summary>
        /// 播放状态 - 参数
        /// </summary>
        private PlayState playState;
        /// <summary>
        /// 播放器对象
        /// </summary>
        public VlcPlayer Player;
        /// <summary>
        /// 播放状态 - 枚举
        /// </summary>
        enum PlayState
        {
            is_Playinig,    //播放
            is_PlayPause,   //暂停
            is_PlayEnd      //停止
        }

        //播放列表索引
        int PlayerID;
        /// <summary>
        /// 初始化方法
        /// </summary>
        private void Init()
        {

            //加载解码器DLL 判断播放器库文件是否存在
            //真实路径
            string pluginPath = System.Environment.CurrentDirectory + "\\plugins\\";
            if (Directory.Exists(pluginPath))//判断真实路径
            {
                //虚拟路径
                string XupluginPath = System.Environment.CurrentDirectory + "\\vlc\\plugins\\";
                Player = new VlcPlayer(XupluginPath);

                //将播放器的句柄实例化到窗体
                IntPtr render_wnd = PlayPanel.Handle;
                Player.SetRenderWindow((int)render_wnd);

                //设置播放器的状态
                playState = PlayState.is_PlayEnd;
            }
            else
            {
                //插件丢失
                MessageBox.Show("VLCLIB解码器DLL丢失!程序自动退出!");
                this.Close();
                Application.Exit();
            }
        }
        public Main()
        {
            InitializeComponent();
            Init();
           
        }
        //无边框移动
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键

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
    

        //显示隐藏 已点歌曲
        int YidianGeQuShow=0;
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (YidianGeQuShow == 0)
            {
                panelYiDianList.Visible = true;
                YidianGeQuShow = 1;
            }
            else if (YidianGeQuShow == 1)
            {
                panelYiDianList.Visible = false;
                YidianGeQuShow = 0;
            }
           

        }
        //显示隐藏播放列表
        private void TYPE_Click(object sender, EventArgs e)
        {
            panelYiDianList.Visible = false;
            panelHuanFu.Visible = false;
        }
        //静音
        int JinYin = 0;
        int yinliang;
        private void pboJingYin_Click(object sender, EventArgs e)
        {
           yinliang=Player.GetVolume();
           
            if (JinYin == 0)
            {
                //设置 播放器的音量为0
                Player.SetVolume(0);
                this.pboJingYin.Image = this.imgListJingYin.Images[1];
                JinYin = 1;
            }
            else if (JinYin ==1)
            {
                this.pboJingYin.Image = this.imgListJingYin.Images[0];
                JinYin = 0;
                //设置 最大音量
                Player.SetVolume(100);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile("Skin\\skin1.jpg");
            this.BackgroundImage = img;
        }

        int HuanFu = 0;
        private void pictureBox7_Click(object sender, EventArgs e)
        {

            if (HuanFu == 0)
            {
                panelHuanFu.Visible = true;
                HuanFu = 1;
            }
            else if (HuanFu == 1)
            {
                panelHuanFu.Visible =false;
                HuanFu = 0;
            }

        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            Image img = Image.FromFile("Skin\\skin2.png");
            this.BackgroundImage = img;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile("Skin\\skin3.png");
            this.BackgroundImage = img;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile("Skin\\skin4.png");
            this.BackgroundImage = img;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile("Skin\\skin5.png");
            this.BackgroundImage = img;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile("Skin\\skin6.jpg");
            this.BackgroundImage = img;
        }
        //退出程序
        private void metroClose1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }



        public void QueryMusic(string str)
        {
            int singertype_id = 0;
            //作者 音乐属于类型
            switch (str)
            {
                case "热门流行":
                    singertype_id = 1;
                    break;
                case "经典老歌":
                    singertype_id = 2;
                    break;
                case "游戏动漫":
                    singertype_id = 4;
                    break;
                case "影视金曲":
                    singertype_id = 3;
                    break;
            }
            List<Music> list = MusicHelper.GetMusicByType(singertype_id);
            if (list != null)
            {
                //清空已查询出来的数据
               MusicList.Items[0].SubItems.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    //遍历  添加
                    MySkin.Controls.MyListSubItem sub = new MySkin.Controls.MyListSubItem(list[i].song_name, list[i].singer_id + "");
                    sub.Info = list[i].singer_name;
                    sub.Url = list[i].song_url;
                    MusicList.Items[0].SubItems.Add(sub);
                }
            }
        }


        private void TurnToAuthorList(object sender, EventArgs e)
        {
            Panel mp = (Panel)sender;
            string str = mp.Controls[0].Text;
            QueryMusic(str);
        }

        //列表对象
        List<Author> AuthLists = new List<Author>();

        private void TurnToAuthorListByLabel(object sender, EventArgs e)
        {
            Label mp = (Label)sender;
            string str = mp.Text;
            QueryMusic(str);
        }

      


        //播放的方法
        public void Play(string path)
        {
            if (File.Exists(path))
            {
                //播放器加载路径
                Player.PlayFile(path);
                playState = PlayState.is_Playinig;
                //获取文件的名字 并且 显示
              //  PlayerLabInfo.Text = Path.GetFileNameWithoutExtension(path);
            }
            else
            {
                //歌曲不存在
            }

        }

        //播放选中的歌曲
        private void Play(MySkin.Controls.MyListSubItem PlayList)
        {
            if (File.Exists(PlayList.Url))
            {
                Play(PlayList.Url);
            }
            //显示播放器
        }

        //开始与暂停
        bool zanting = true;
    int index = 0;
        private void pboMenuBoFang_Click(object sender, EventArgs e)
        {    
            
            if (zanting)
            {
                if (PlayList.Items[0].SubItems.Count > 0)
                {
                    Play(PlayList.Items[0].SubItems[0]);
                    //this.pboMenuBoFang.Image = this.imgListStart.Images[1];
                    zanting = false;
                    index = 1;
                } 
            }
            else if (zanting == false)
            {             
                Player.Pause();
                if (playState == PlayState.is_PlayPause)
                {
                    playState = PlayState.is_Playinig;
                    index = 1;
                }
                else if (playState == PlayState.is_Playinig)
                {
                    playState = PlayState.is_PlayPause;
                    index = 0;
                }
               
            }
            this.pboMenuBoFang.Image = this.imgListStart.Images[index];
        }

        //类型点歌
        private void panDGbyType_Click(object sender, EventArgs e)
        {
            TypeDianGeZhuPanl.Visible = true;
        }

        //双击时将对应的歌曲 添加 到播放列表
        private void MusicList_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (MusicList.SelectSubItem!=null)
            {
                PlayList.Items[0].SubItems.Add(MusicList.SelectSubItem);
            }
        }

        //点击 【首页】
        private void pboMenuHome_Click(object sender, EventArgs e)
        {
            //隐藏类型点歌
            TypeDianGeZhuPanl.Visible = false;
            //隐藏拼音点歌
            PinYinDianGePanel.Visible = false;
            //隐藏数字点歌
            NumDianGePanel.Visible = false;
            //隐藏金曲排行
            KingPanel.Visible = false;
            //隐藏
            GeShouDianGePanel.Visible = false;
        }

        //点击 【下一首】
        private void pboMenuXiaYiShou_Click(object sender, EventArgs e)
        {
            NextYiShou();
        }

private void NextYiShou()
{
            if (PlayList.Items.Count < 1)//歌单没有
            {
                return;
            }
            PlayerID = PlayerID + 1;
            if (PlayerID < PlayList.Items[0].SubItems.Count)//第一首或者正常水平
            {
                Play(PlayList.Items[0].SubItems[PlayerID]);
            }
            else//跳到最后一首
            {
                PlayerID = 0;
                Play(PlayList.Items[0].SubItems[0]);
            }
}

        //播放容器全屏
       
        private void PlayPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          
        }
        //恢复正常屏幕大小
        private void PlayPanel_Click(object sender, EventArgs e)
        {
          
        }

        //拼音点歌
        private void panDGbyPinYin_Click(object sender, EventArgs e)
        {
            PinYinDianGePanel.Visible = true;
        }
        //歌星点歌
        private void panDGbyGeXing_Click(object sender, EventArgs e)
        {
            GeShouDianGePanel.Visible = true;
        }
        //金曲排行
        private void panDGbyPaiHang_Click(object sender, EventArgs e)
        {
            KingPanel.Visible = true;
        }
        //数字点歌
        private void panDGbyNum_Click(object sender, EventArgs e)
        {
            NumDianGePanel.Visible = true;
        }
        //商品购买
        private void panShop_Click(object sender, EventArgs e)
        {

        }




        ///////拼音点歌

        //拼音点歌：搜索
        private void btnPinYinSearch_Click(object sender, EventArgs e)
        {
            //获取输入的值；为空什么也不做
            string InputPinYin = this.srPinYin.Text;
            if (InputPinYin.Trim().Equals(string.Empty))
            {
                return;
            }
            else
            {
                List<Music> list = MusicHelper.GetMusicByPinYin(InputPinYin);
                if (list != null)
                {
                    //清空已查询出来的数据
                    PyMusicList.Items[0].SubItems.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        //遍历  添加
                        MySkin.Controls.MyListSubItem subpy = new MySkin.Controls.MyListSubItem(list[i].song_name, list[i].singer_id + "");
                        subpy.Info = list[i].singer_name;
                        subpy.Url = list[i].song_url;
                        PyMusicList.Items[0].SubItems.Add(subpy);
                    }
                }
            }
        }
        //查询出来的 歌曲 双击 添加到 播放列表
        private void myList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PyMusicList.SelectSubItem != null)
            {
                PlayList.Items[0].SubItems.Add(PyMusicList.SelectSubItem);
            }
        }

        private void TurnToAuthorListNum(object sender, EventArgs e)
        {
            Panel mp2 = (Panel)sender;
            string str = mp2.Controls[0].Text;
            QueryMusic1(str);
        }
        private void TurnToAuthorListNumByLabel(object sender, EventArgs e)
        {
            Label mp2 = (Label)sender;
            string str = mp2.Text;
            QueryMusic1(str);
        }

        public void QueryMusic1(string str)
        {
            int song_word_count = 0;
            //作者 音乐属于类型
            switch (str)
            {
                case "一个字":
                    song_word_count = 1;
                    break;
                case "两个字":
                    song_word_count = 2;
                    break;
                case "三个字":
                    song_word_count = 3;
                    break;
                case "四个字":
                    song_word_count = 4;
                    break;
                case "无个字":
                    song_word_count = 5;
                    break;
                case "六个字":
                    song_word_count = 6;
                    break;
                case "七个字":
                    song_word_count = 7;
                    break;
                case "八个字":
                    song_word_count = 8;
                    break;
            }
            List<Music> list = MusicHelper.GetMusicByNum(song_word_count);
            if (list != null)
            {
                //清空已查询出来的数据
                NumMusicList.Items[0].SubItems.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    //遍历  添加
                    MySkin.Controls.MyListSubItem sub = new MySkin.Controls.MyListSubItem(list[i].song_name, list[i].singer_id + "");
                    sub.Info = list[i].singer_name;
                    sub.Url = list[i].song_url;
                    NumMusicList.Items[0].SubItems.Add(sub);
                }
            }
        }

        private void NumMusicList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (NumMusicList.SelectSubItem != null)
            {
                PlayList.Items[0].SubItems.Add(NumMusicList.SelectSubItem);
            }
        }


        private void TurnToAuthorListKing(object sender, EventArgs e)
        {
            Panel mp3 = (Panel)sender;
            string str = mp3.Controls[0].Text;
            QueryMusic2(str);
        }
        private void TurnToAuthorListKingByLabel(object sender, EventArgs e)
        {
            Label mp3 = (Label)sender;
            string str = mp3.Text;
            QueryMusic2(str);
        }

        public void QueryMusic2(string str)
        {
            int singertype_id = 0;
            //播放 次数
            switch (str)
            {
                case "热门流行排行":
                    singertype_id = 1;
                    break;
                case "经典老歌排行":
                    singertype_id = 2;
                    break;
                case "影视金曲排行":
                    singertype_id = 3;
                    break;
                case "游戏动漫排行":
                    singertype_id = 4;
                    break;
            }
            List<Music> list = MusicHelper.GetMusicByPlayCount(singertype_id);
            if (list != null)
            {
                //清空已查询出来的数据
                KingMusicList.Items[0].SubItems.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    //遍历  添加
                    MySkin.Controls.MyListSubItem sub = new MySkin.Controls.MyListSubItem(list[i].song_name, list[i].singer_id + "");
                    sub.Info = list[i].singer_name;
                    sub.Url = list[i].song_url;
                    KingMusicList.Items[0].SubItems.Add(sub);
                }
            }
        }

        private void KingMusicList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (KingMusicList.SelectSubItem != null)
            {
                PlayList.Items[0].SubItems.Add(KingMusicList.SelectSubItem);
            }
        }

        //全屏
        private void panel24_DoubleClick(object sender, EventArgs e)
        {
            this.panelYiDianList.Visible = false;
            if (this.PlayPanel.Dock == DockStyle.Fill)
            {
                this.panel24.Height = 100;
                this.panel24.Width = 160;
                PlayPanel.Dock = DockStyle.None;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                //修改了分辨率 要修改这里！！！
                this.panel24.Height =500;
                this.panel24.Width = 800;
                PlayPanel.Dock = DockStyle.Fill;
                WindowState = FormWindowState.Maximized;
            }
        }

        
        //查询 歌手
        private void btnGeShouSearch_Click(object sender, EventArgs e)
        {
            string GeShouName = this.srGeShou.Text;
            if (GeShouName.Trim().Equals(string.Empty))
            {
                return;
            }
            else
            {
                List<Author> list = AuthorHelper.GetAuthorByName(GeShouName);
                if (list != null)
                {
                    //清空已查询出来的数据
                    AuthorLists.Items[0].SubItems.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        //遍历  添加
                        MySkin.Controls.MyListSubItem subpy = new MySkin.Controls.MyListSubItem(list[i].singer_name, list[i].singer_id + "");
                        subpy.Info = list[i].singer_sex;
                        subpy.Info = list[i].singer_name;
                        subpy.Url = list[i].singer_photo_url;
                        subpy.ID = list[i].singer_id;
                        AuthorLists.Items[0].SubItems.Add(subpy);

                    }
                }
            
            }
        }

        //选定当前 所选的歌手 查询该歌手的 歌曲
        private void AuthorLists_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //获取当前 歌手的ID 调用查询方法 进行查询

            if (AuthorLists.SelectSubItem != null)
            {
                int authorids = AuthorLists.SelectSubItem.ID;
                string authornames = AuthorLists.SelectSubItem.Name;
                List<Music> list = MusicHelper.GetMusicByAuthors(authorids);
                    //清空已查询出来的数据
                    GeShouDianGeList.Items[0].SubItems.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        //遍历  添加
                        MySkin.Controls.MyListSubItem sub = new MySkin.Controls.MyListSubItem(list[i].song_name, list[i].singer_id + "");
                        sub.Info = authornames;
                        sub.Url = list[i].song_url;
                        GeShouDianGeList.Items[0].SubItems.Add(sub);
                    }

            }
           
        }

        // 歌手点歌 双击 添加到播放列表
        private void GeShouDianGeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (GeShouDianGeList.SelectSubItem != null)
            {
                PlayList.Items[0].SubItems.Add(GeShouDianGeList.SelectSubItem);
            }
        }

        //重唱 
        private void pboMenuChongChang_Click(object sender, EventArgs e)
        {
           //获取当前正在 播放的音乐
            if (PlayList.Items.Count < 1)//歌单没有
            {
                return;
            }
           // PlayerID = PlayerID;
            if (PlayerID < PlayList.Items[0].SubItems.Count)//第一首或者正常水平
            {
                Play(PlayList.Items[0].SubItems[PlayerID]);
            }
            else//跳到最后一首
            {
                PlayerID = 0;
                Play(PlayList.Items[0].SubItems[0]);
            }

        }



       // double ZongTime=0;

        private void Playertime_Tick(object sender, EventArgs e)
        {

        }



    }
}
