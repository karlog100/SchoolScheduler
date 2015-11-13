using System;

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
    }
}
