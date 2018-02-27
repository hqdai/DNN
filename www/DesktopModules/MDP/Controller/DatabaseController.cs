using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.Controller
{
    class DatabaseController
    {
        /// <summary>
        /// The private field holding the instance 
        /// </summary>
        private static DatabaseController _instance;

        /// <summary>
        /// Gets the instance (singleton pattern).
        /// </summary>
        /// <value>The instance.</value>
        public static DatabaseController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseController();
                }
                return _instance;
            }
        }


    }
}
