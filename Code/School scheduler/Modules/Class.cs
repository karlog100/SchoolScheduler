using System;
using System.Collections.Generic;

namespace Modules
{
    class Class : User
    {
        /// <summary>
        /// Students assigned to the Class
        /// Becourse a singel student could be assigned to a class for multiple reasons.
        /// </summary>
        public List<Student> AssignedStudents
        {
            get;
            set;
        }
        /// <summary>
        /// Team´s assigned to the Class
        /// </summary>
        public List<Team> AssignedTeam
        {
            get;
            set;
        }

        /// <summary>
        /// Teacher assigned to the Class
        /// </summary>
        public List<Teacher> AssignedTeacher
        {
            get;
            set;
        }
    }
}
