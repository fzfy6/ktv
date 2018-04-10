using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace QianTai
{
    class DBhelper
    {
        //链接数据库字符串
        //string strCon = "Data Source=.\\sqlexpress;database=ImKtvDb;uid=dujun;pwd=123456";
        string strCon = "Data Source = .; Initial Catalog = ImKtvDb; Integrated Security = True";
        //数据库连接Connection对象
        private SqlConnection connection;

        //Connection对象
        public SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(strCon);
                }
                return connection;
            }
        }

        //打开数据库
        public void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
                Connection.Open();
            }
        }

        //关闭数据库
        public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
            }
        }

    }
}
