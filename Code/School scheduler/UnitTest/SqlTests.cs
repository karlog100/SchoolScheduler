using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modules;

namespace UnitTest
{
    [TestClass]
    public class SqlTests
    {
        private class Sql_Test_Class : BaseModel { 
            
        }

        [TestMethod]
        public void SqlConnection_Test()
        {
            Sql_Test_Class test = new Sql_Test_Class();
            MySqlConnection Conn = test.SqlConnect();
            Assert.AreEqual(ConnectionState.Open, Conn.State);
        }
    }
}
