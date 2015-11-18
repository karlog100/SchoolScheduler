using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Modules
{
    public abstract class BaseModel
    {
        public MySqlConnection SqlConnect()
        {
            string strMySqlConnectionString =
                @"SERVER=localhost;" +
                @"DATABASE=school_scheduler;" +
                @"UID=root;" +
                @"PASSWORD=root";

            MySqlConnection mSqlConn = new MySqlConnection(strMySqlConnectionString);
            mSqlConn.Open();

            return mSqlConn;
        }

        /// <summary>
        /// The unique Id assigned
        /// </summary>
        public int? Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the User
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
