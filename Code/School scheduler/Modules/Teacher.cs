using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Modules
{
    public class Teacher : User
    {
        public static Teacher CopyToTeacher(User user)
        {
            return new Teacher
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PostCode = user.PostCode,
                Phone = user.Phone,
                Type = user.Type,
                Payrole = 0
            };
        }

        /// <summary>
        /// The Teacher´s mounthley payment
        /// </summary>
        public int Payrole
        {
            get;
            set;
        }

        /// <summary>
        /// Save the user in database
        /// </summary>
        /// <returns>Success</returns>
        public override bool Save_User()
        {
            bool Res = false;
            try
            {
                Res = base.Save_User();
            }
            catch (AggregateException ex)
            {
                throw (ex);
            }
            if (Res)
            {
                if (Payrole == null)
                {
                    throw (new ArgumentNullException("Payrole is missing"));
                }
                try
                {
                    GetUser_Id();

                    MySqlConnection Conn = base.SqlConnect();
                    string cmdString = "INSERT INTO `teacher` (`user_id`,`payrole`)"
                                     + "VALUES(@userid, @payrole)"
                                     + "ON DUPLICATE KEY UPDATE `payrole` = @payrole;";
                    MySqlCommand cmd = new MySqlCommand(cmdString);

                    cmd.Parameters.AddWithValue("@userid", base.Id);
                    cmd.Parameters.AddWithValue("@payrole", Payrole);

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = Conn;
                    cmd.ExecuteNonQuery();
                    Conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return false;
        }

        /// <summary>
        /// Get The Student information by user email
        /// </summary>
        /// <param name="Email">The Email to lockup</param>
        /// <returns>Student Information</returns>
        public override User GetUser_ByEmail()
        {
            MySqlConnection Conn = base.SqlConnect();

            string cmdString = "SELECT `users`.`id`, `users`.`name`, `users`.`address`, `users`.`postcode`, `users`.`phone`, `users`.`email`, `teacher`.`payrole` "
                             + "FROM `school_scheduler`.`users` INNER JOIN `school_scheduler`.`teacher` ON `users`.`id` = `teacher`.`user_id` "
                             + "WHERE `users`.`email` = @email LIMIT 1;";
                
            MySqlCommand cmd = new MySqlCommand(cmdString);
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conn;

            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read() && Reader.HasRows)
            {
                base.Id = Reader.GetInt32(Reader.GetOrdinal("id"));
                base.Name = Reader.GetString(Reader.GetOrdinal("name"));
                this.Email = Reader.GetString(Reader.GetOrdinal("email"));
                this.Address = Reader.GetString(Reader.GetOrdinal("address"));
                this.PostCode = Reader.GetString(Reader.GetOrdinal("postcode"));
                this.Phone = Reader.GetString(Reader.GetOrdinal("phone"));
                this.Type = (int)User_Type.Student;
                this.Payrole = Reader.GetInt32(Reader.GetOrdinal("payrole"));
            }
            else
            {
                throw (new ArgumentNullException("Student does not exists"));
            }
            Reader.Dispose();
            Conn.Close();
            return this;
        }
    }
}
