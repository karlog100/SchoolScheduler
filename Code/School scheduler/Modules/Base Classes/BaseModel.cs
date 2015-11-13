using System;

namespace Modules
{
    public abstract class BaseModel
    {
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
