using System;
using System.Collections.Generic;

using System.Text;

namespace QianTai
{
   public class Author
    {
        //歌手编号
        public int singer_id { get; set; }
        //歌手名称
        public string singer_name { get; set; }
        //歌手类别
        public int singertype_id { get; set; }
        //歌手性别
        public string singer_sex { get; set; }
        //歌手照片
        public string singer_photo_url { get; set; }
        //歌手拼音
        public string singer_ab { get; set; }
        public List<Music> musics { get; set; }
    }
}
