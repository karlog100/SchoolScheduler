using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Modules
{

    public class User : BaseModel
    {
        /// <summary>
        /// The address of the user.
        /// </summary>
        public string Address
        {
            get;
            set;
        }
        /// <summary>
        /// The postcode of the user.
        /// </summary>
        public string PostCode
        {
            get;
            set;
        }
        /// <summary>
        /// The phone-number of the user.
        /// </summary>
        public string Phone
        {
            get;
            set;
        }
        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email
        {
            get;
            set;
        }


        public enum User_Type { 
            User = 0,
            Student = 1,
            Teacher = 2
        }
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// Function to validate User Email
        /// </summary>
        public bool Validate_Email()
        {
            try
            {
                return Validate_Email(Email);
            }
            catch (Exception ex)
            { 
                throw(ex);
            }
        }

        /// <summary>
        /// Function to validate User Email
        /// </summary>
        /// <param name="Email">Email to validate</param>
        /// <returns>ArgumentException on fail : true</returns>
        public bool Validate_Email(string Email)
        {
            ArgumentException ex = null;
            if (!Email.Contains("@") || new Regex(Regex.Escape("@")).Matches(Email).Count > 1)
            {
                ex = new ArgumentException("String not containing, or have to many \'@\'");
            }
            else if (!Email.Contains(".") || new Regex(Regex.Escape(".")).Matches(Email).Count > 1) 
            {
                ex = new ArgumentException("String not containing, or have to many \'.\'");
            }
            else if (Email.Split('@')[0].Length <= 0) 
            {
                ex = new ArgumentException("Email does not contain a sender name");
            }
            else if (Email.Split('@')[1].Split('.')[0].Length <= 0)
            {
                ex = new ArgumentException("Email does not contain a Domain");
            }
            else if (Email.Split('.')[1].Length <= 0)
            {
                ex = new ArgumentException("Email does not contain a Top level Domain");
            }
            if (ex != null)
            {
                throw (ex);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Upload the user info to the database
        /// </summary>
        /// <returns>Sucess</returns>
        public virtual bool Save_User()
        {
            try
            {
                Validate_Email();
            }
            catch (AggregateException ex)
            {
                throw (new ArgumentException("Email not valid", ex));
            }
            if (String.IsNullOrEmpty(Name))
            {
                throw(new ArgumentNullException("Name is missing"));
            }
            else if (String.IsNullOrEmpty(Address))
            {
                throw (new ArgumentNullException("Address is missing"));
            }
            else if (String.IsNullOrEmpty(PostCode))
            {
                throw (new ArgumentNullException("PostCode is missing"));
            }
            else if (String.IsNullOrEmpty(Phone))
            {
                throw (new ArgumentNullException("Phone is missing"));
            }
            else 
            {
                MySqlConnection Conn = base.SqlConnect();

                string id = (base.Id == 0 || base.Id == null) ? "NULL" : base.Id.ToString();

                string cmdString = "INSERT INTO `users` (`id`, `name`, `address`, `postcode`, `phone`, `email`)"
                                 + "VALUES(" + id + ",@name,@address,@postcode,@phone,@email)"
                                 + "ON DUPLICATE KEY UPDATE `name` = @name, `address` = @address, `postcode` = @postcode, `phone` = @phone, `email` = @email;";
                MySqlCommand cmd = new MySqlCommand(cmdString);

                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@address", Address);
                cmd.Parameters.AddWithValue("@postcode", PostCode);
                cmd.Parameters.AddWithValue("@phone", Phone);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = Conn;
                cmd.ExecuteNonQuery();
                Conn.Close();
            }
            return true;
        }

        protected void GetUser_Id() {
            MySqlConnection Conn = base.SqlConnect();

            string cmdString = "SELECT `users`.`id` FROM `school_scheduler`.`users` WHERE `users`.`email` = @email LIMIT 1;";

            MySqlCommand cmd = new MySqlCommand(cmdString);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conn;

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.Read() && Reader.HasRows)
                {
                    Id = Reader.GetInt32(Reader.GetOrdinal("id"));
                }
            }
        }

        /// <summary>
        /// Get The user information by user email
        /// </summary>
        /// <param name="Email">The Email to lockup</param>
        /// <returns>User Information</returns>
        public virtual User GetUser_ByEmail()
        {
            MySqlConnection Conn = base.SqlConnect();

            string cmdString = "SELECT `users`.`id`, `users`.`name`, `users`.`address`, `users`.`postcode`, `users`.`phone`, `users`.`email` FROM `school_scheduler`.`users` INNER JOIN `school_scheduler`.`student` ON `users`.`id` = `student`.`user_id` WHERE `users`.`email` = @email LIMIT 1;";

            MySqlCommand cmd = new MySqlCommand(cmdString);
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conn;

            bool GotData = false;
            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read() && Reader.HasRows)
            {
                GotData = true;
                this.Type = (int)User_Type.Student;
            }
            else {
                Reader.Dispose();
                cmdString = "SELECT `users`.`id`, `users`.`name`, `users`.`address`, `users`.`postcode`, `users`.`phone`, `users`.`email` FROM `school_scheduler`.`users` INNER JOIN `school_scheduler`.`teacher` ON `users`.`id` = `teacher`.`user_id` WHERE `users`.`email` = @email LIMIT 1;";
                cmd = new MySqlCommand(cmdString);
                cmd.Parameters.AddWithValue("@email", this.Email);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Conn;
                Reader = cmd.ExecuteReader();
                if (Reader.Read() && Reader.HasRows)
                {
                    GotData = true;
                    this.Type = (int)User_Type.Teacher;
                }
            }
            if (GotData)
            {
                base.Id = Reader.GetInt32(Reader.GetOrdinal("id"));
                base.Name = Reader.GetString(Reader.GetOrdinal("name"));
                this.Email = Reader.GetString(Reader.GetOrdinal("email"));
                this.Address = Reader.GetString(Reader.GetOrdinal("address"));
                this.PostCode = Reader.GetString(Reader.GetOrdinal("postcode"));
                this.Phone = Reader.GetString(Reader.GetOrdinal("phone"));
            }
            else {
                throw (new ArgumentNullException("User does not exists"));
            }
            Reader.Dispose();
            Conn.Close();
            return this;
        }

        public bool Delete_User() 
        {
            if (Id == 0 || Id == null) return false;

            MySqlConnection Conn = base.SqlConnect();
            string cmdString = "DELETE FROM `school_scheduler`.`users` WHERE `id` = @id;";

            MySqlCommand cmd = new MySqlCommand(cmdString);
            cmd.Parameters.AddWithValue("@id", base.Id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conn;
            cmd.ExecuteNonQuery();

            return true;
        }
    }
}
