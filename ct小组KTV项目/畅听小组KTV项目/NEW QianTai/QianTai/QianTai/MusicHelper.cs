using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;

namespace QianTai
{
    class MusicHelper
    {
        /// <summary>
        /// 查询音乐  根据作者
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public static List<Music> GetMusicByAuthor(Author author)
        {
            List<Music> list = new List<Music>();
            //查询数据库
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            //查询歌曲名称 By 歌手id
            string StcSql = "  select * from song_info where singer_id =" + author.singer_id;
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //READ DB
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Music mu = new Music();
                mu.singer_id = author.singer_id;
                mu.song_name = reader["song_name"].ToString();
                mu.song_ab = reader["song_ab"].ToString();
                mu.song_word_count = Convert.ToInt32(reader["song_word_count"]);
                mu.songtype_id = Convert.ToInt32(reader["songtype_id"]);
                mu.singer_id = Convert.ToInt32(reader["singer_id"]);
                mu.song_url = reader["song_url"].ToString();
                mu.song_play_count = Convert.ToInt32(reader["song_play_count"]);

                list.Add(mu);
            }

            return list;

        }

        //根据类型 查询 歌曲信息
        internal static List<Music> GetMusicByType(int str)
        {
            List<Music> list = new List<Music>();
            //查询数据库
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            //查询歌曲名称 By 歌曲类型 songtype_id
            string StcSql = " select [song_id],[song_name],[song_ab],[song_word_count],[singer_name],[song_url],[song_play_count] from song_info,singer_info where  song_info.singer_id=singer_info.singer_id and songtype_id=" + str;
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //READ DB
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Music mu = new Music();
                mu.song_name = reader["song_name"].ToString();
                mu.song_ab = reader["song_ab"].ToString();
                mu.song_word_count = Convert.ToInt32(reader["song_word_count"]);
                //mu.songtype_id = Convert.ToInt32(reader["songtype_id"]);
                //mu.singer_id = Convert.ToInt32(reader["singer_id"]);
                mu.singer_name = reader["singer_name"].ToString();
                mu.song_url = reader["song_url"].ToString();
                mu.song_play_count = Convert.ToInt32(reader["song_play_count"]);
                list.Add(mu);            
            }
            return list;
        }

        //根据拼音 查询 歌曲信息
        internal static List<Music> GetMusicByPinYin(string str)
        {
            List<Music> list = new List<Music>();
            //查询数据库
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            //查询歌曲名称 By 歌曲拼音 song_ab
            string StcSql = " select [song_id],[song_name],[song_word_count],[singer_name],[song_url],[song_play_count] from song_info,singer_info where  song_info.singer_id=singer_info.singer_id and song_ab='" + str+"'";
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //READ DB
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Music mu = new Music();
                mu.song_name = reader["song_name"].ToString();
                mu.song_word_count = Convert.ToInt32(reader["song_word_count"]);
                mu.singer_name = reader["singer_name"].ToString();
                mu.song_url = reader["song_url"].ToString();
                mu.song_play_count = Convert.ToInt32(reader["song_play_count"]);
                list.Add(mu);
            }

            return list;
        }


        //通过字数查询歌曲
        internal static List<Music> GetMusicByNum(int song_word_count)
        {
            List<Music> list = new List<Music>();
            //查询数据库
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            //查询歌曲名称 By 歌曲类型 songtype_id
            string StcSql = " select [song_id],[song_name],[song_ab],[singer_name],[song_url],[song_play_count] from song_info,singer_info where  song_info.singer_id=singer_info.singer_id and song_word_count=" + song_word_count;
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //READ DB
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Music mu = new Music();
                mu.song_name = reader["song_name"].ToString();
                mu.song_ab = reader["song_ab"].ToString();
                //mu.song_word_count = Convert.ToInt32(reader["song_word_count"]);
                //mu.songtype_id = Convert.ToInt32(reader["songtype_id"]);
                //mu.singer_id = Convert.ToInt32(reader["singer_id"]);
                mu.singer_name = reader["singer_name"].ToString();
                mu.song_url = reader["song_url"].ToString();
                mu.song_play_count = Convert.ToInt32(reader["song_play_count"]);
                list.Add(mu);
            }
            return list;
        }

        //查询播放次数 By  song_play_count
        internal static List<Music> GetMusicByPlayCount(int singertype_id)
        {
            List<Music> list = new List<Music>();
            //查询数据库
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            //查询歌曲名称 By 歌曲类型 songtype_id
            string StcSql = "select [song_id],[song_name],[song_ab],[song_word_count],[singer_name],[song_url],[song_play_count] from song_info,singer_info where  song_info.singer_id=singer_info.singer_id and songtype_id=" + singertype_id + " order by song_play_count desc";
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //READ DB
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Music mu = new Music();
                mu.song_name = reader["song_name"].ToString();
                mu.song_ab = reader["song_ab"].ToString();
                mu.song_word_count = Convert.ToInt32(reader["song_word_count"]);
                //mu.songtype_id = Convert.ToInt32(reader["songtype_id"]);
                //mu.singer_id = Convert.ToInt32(reader["singer_id"]);
                mu.singer_name = reader["singer_name"].ToString();
                mu.song_url = reader["song_url"].ToString();
                mu.song_play_count = Convert.ToInt32(reader["song_play_count"]);
                list.Add(mu);
            }

            return list;
        }

        //通过作者查询歌曲 
        internal static List<Music> GetMusicByAuthors(int authorids)
        {
            List<Music> list = new List<Music>();
            //查询数据库
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            //查询歌曲名称 By 歌曲拼音 song_ab
            string StcSql = "   select * from song_info where singer_id =" + authorids;
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //READ DB
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Music mu = new Music();
                mu.song_name = reader["song_name"].ToString();
                mu.song_word_count = Convert.ToInt32(reader["song_word_count"]);
                mu.song_url = reader["song_url"].ToString();
                mu.song_play_count = Convert.ToInt32(reader["song_play_count"]);
                list.Add(mu);
            }

            return list;
        }
    }
}
