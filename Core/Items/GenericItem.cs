using System.Collections;
using System.Collections.Generic;

namespace Tsinghua.HCI.IoThingsLab
{

    public class GenericItem : IItem
    {
        public GenericItem(string type, string name)
        {
            this.name = name;
            this.type = type;
        }


        public string type { get; set; }
        public string name { get; set; }
        public string state { get; set; }

        /// <summary>
        /// returns the name of the item
        /// </summary>
        /// <returns>the name of the item</returns>
        public string getName()
        {
            return name;
        }

        /// <summary>
        /// returns the state of the item
        /// </summary>
        /// <returns>the state of the item</returns>
        public string getState()
        {
            return state;
        }

        /// <summary>
        /// returns the type of the item
        /// </summary>
        /// <returns>the type of the item</returns>
        public string getType()
        {
            return type;
        }

        public int HashCode()
        {
            int n = 31;
            return n + ((name == null) ? 0 : name.GetHashCode());
        }
    }

}