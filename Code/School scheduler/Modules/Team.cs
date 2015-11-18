using System;
using System.Collections.Generic;

namespace Modules
{
    class Team : User
    {
        /// <summary>
        /// Students assigned to the Team
        /// </summary>
        public List<Student> AssignedStudents
        {
            get;
            set;
        }
        /// <summary>
        /// Contact Teachers for the Team
        /// </summary>
        public List<Teacher> ContactTeachers {
            get;
            set;
        }
        /// <summary>
        /// Classes in the Team
        /// </summary>
        public List<Class> Classes
        {
            get;
            set;
        }
        /// <summary>
        /// Team Start Date
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// Team End Date
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
    }
}
