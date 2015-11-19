using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Modules
{
    public class Student : User
    {
        public Student()
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Time = DateTime.Parse(dateTime);
            Education_StartDate = Time;
            Education_EndDate = Time;
        }
        public static Student CopyToStudents(User user)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Time = DateTime.Parse(dateTime);

            return new Student {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PostCode = user.PostCode,
                Phone = user.Phone,
                Type = user.Type,
                Education_StartDate = Time,
                Education_EndDate = Time
            };
        }

        /// <summary>
        /// Education Start Date
        /// </summary>
        public DateTime Education_StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// Education End Date
        /// </summary>
        public DateTime Education_EndDate
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
                if (Education_StartDate == null)
                {
                    throw (new ArgumentNullException("Education StartDate is missing"));
                }
                else if (Education_EndDate == null)
                {
                    throw (new ArgumentNullException("Education EndDate is missing"));
                }
                try
                {
                    GetUser_Id();

                    MySqlConnection Conn = base.SqlConnect();
                    string cmdString = "INSERT INTO `student` (`user_id`,`education_start_date`, `education_end_date`)"
                                     + "VALUES(@userid, @startdate, @enddate)"
                                     + "ON DUPLICATE KEY UPDATE `education_start_date` = @startdate, `education_end_date` = @enddate;";
                    MySqlCommand cmd = new MySqlCommand(cmdString);

                    cmd.Parameters.AddWithValue("@userid", base.Id);
                    cmd.Parameters.AddWithValue("@startdate", Education_StartDate);
                    cmd.Parameters.AddWithValue("@enddate", Education_EndDate);

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = Conn;
                    cmd.ExecuteNonQuery();
                    Conn.Close();
                    return true;
                }
                catch (Exception ex) 
                {
                    throw(ex);
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

            string cmdString = "SELECT `users`.`id`, `users`.`name`, `users`.`address`, `users`.`postcode`, `users`.`phone`, `users`.`email`, `student`.`education_start_date`, `student`.`education_end_date` "
                             + "FROM `school_scheduler`.`users` INNER JOIN `school_scheduler`.`student` ON `users`.`id` = `student`.`user_id` "
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
                this.Education_StartDate = Reader.GetDateTime(Reader.GetOrdinal("education_start_date"));
                this.Education_EndDate = Reader.GetDateTime(Reader.GetOrdinal("education_end_date"));
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
