using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;

namespace QianTai
{
    public class AuthorHelper
    {
        /// <summary>
        /// 查询歌手接口
        /// 
        /// </summary>
        /// <param name="type">歌手类型</param>
        /// <returns></returns>
        public static List<Author> GetAuthorByType(int type)
        {

            List<Author> list = new List<Author>();
            //Author tus = new Author();
            //tus.musics = new List<Music>();
            //tus.singer_name = "test";
            //list.Add(tus);
            //return list;
            //查询数据库
            //DB = SQL.EX()
            DBhelper dbh = new DBhelper();
            dbh.OpenConnection();
            string StcSql = "select * from singer_type,singer_info where singer_type.singertype_id=singer_type.singertype_id and singer_type.singertype_id=" + type + "";
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);

            //DB.READ()
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Author tu = new Author();
                tu.musics = new List<Music>();
                tu.singer_id = Convert.ToInt32(reader["singer_id"]);
                tu.singer_name = reader["singer_name"].ToString();
                tu.singertype_id = Convert.ToInt32(reader["singertype_id"]);
                tu.singer_sex = reader["singer_sex"].ToString();
                tu.singer_photo_url = reader["siinger_photo_url"].ToString();
                tu.musics = MusicHelper.GetMusicByAuthor(tu);
                list.Add(tu);
            }

            //返回集合

            return list;
        }

        //查询歌手信息 By 歌手名称
        internal static List<Author> GetAuthorByName(string GeShouName)
        //internal static List<Author> GetAuthorByName(string S)
        {
            List<Author> list = new List<Author>();
            //查询数据库
            DBhelper dbh = new DBhelper(); 
            dbh.OpenConnection();
           //根据歌手名字查找歌曲
            string StcSql = "select * from singer_info where singer_name like '%" + GeShouName + "%'";
            //根据歌手首字母拼音查找
            //string StcSql = "select * from singer_info where singer_ab='" + S +"'";
            SqlCommand comm = new SqlCommand(StcSql, dbh.Connection);
            //DB.READ()
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Author tu = new Author();
                tu.musics = new List<Music>();
                tu.singer_id = Convert.ToInt32(reader["singer_id"]);
                tu.singer_name = reader["singer_name"].ToString();
                tu.singertype_id = Convert.ToInt32(reader["singertype_id"]);
                tu.singer_sex = reader["singer_sex"].ToString();
                tu.singer_photo_url = reader["singer_photo_url"].ToString();
                tu.musics = MusicHelper.GetMusicByAuthor(tu);
                list.Add(tu);
            }
            //返回集合
            return list;
        }
    }
}
