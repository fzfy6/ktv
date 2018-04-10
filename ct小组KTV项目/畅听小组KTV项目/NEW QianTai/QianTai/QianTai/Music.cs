using System;
using System.Collections.Generic;

using System.Text;

namespace QianTai
{
  public  class Music
    {
        //歌曲编号
        public int song_id { get; set; }
        //歌曲名
        public string song_name { get; set; }
        //歌曲名拼音
        public string song_ab { get; set; }
        //歌曲名数字
        public int song_word_count { get; set; }
        //歌曲类型
        public int songtype_id { get; set; }
        //歌手编号
        public int singer_id { get; set; }
        //歌曲作者
        public string singer_name { get; set; }
        //歌曲文件地址
        public string song_url { get; set; }
        //歌曲播放次数
        public int song_play_count { get; set; }

    }
}
