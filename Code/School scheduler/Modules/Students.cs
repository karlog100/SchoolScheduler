using System;

namespace Modules
{
    class Students : User
    {
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
    }
}
