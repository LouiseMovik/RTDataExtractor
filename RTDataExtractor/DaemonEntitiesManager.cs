using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTDataExtractor
{
    class DaemonEntitiesManager
    {
        private List<Daemon> daemonList;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DaemonEntitiesManager()
        {
            daemonList = new List<Daemon>();
        }

        // Public property to access daemonList
        public List<Daemon> DaemonList
        {
            get { return daemonList; }
        }

        /// <summary>
        /// Mathod for calculating the current number of entities.
        /// </summary>
        public int CurrentNumberOfEntities()
        {
            int numberOfEntitiess = daemonList.Count;
            return numberOfEntitiess;
        }

        /// <summary>
        /// Method for getting a entity at a specified index.
        /// </summary>
        public Daemon GetEntityAt(int index)
        {
            if (CheckIndex(index))
                return daemonList[index];
            else
                return null;
        }

        /// <summary>
        /// Method for checking if the index used as argument is within the array daemonList.
        /// </summary>
        private bool CheckIndex(int index)
        {
            bool isWithin = false;
            if (index >= 0 && index < daemonList.Count)
            {
                isWithin = true;
            }
            return isWithin;
        }

        /// <summary>
        /// Method for adding a daemon entity. Returns a boolean indicating if the entity was added or not. 
        /// </summary>
        public bool AddDaemonEntity(Daemon newDaemon)
        {
            bool ok = false;
            if (newDaemon != null)
            {
                daemonList.Add(newDaemon);
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method for changing an entity at the specified index. 
        /// </summary>
        public bool ChangeEntityAt(int index, Daemon daemon)
        {
            bool ok = false;
            if (CheckIndex(index) && daemon != null)
            {
                daemonList[index] = daemon;
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method for deleting an entity at the specified index. 
        /// </summary>
        public bool DeleteEntityAt(int index)
        {
            bool ok = false;
            if (CheckIndex(index))
            {
                daemonList.RemoveAt(index);
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method for creating a list of strings that can be displayed in the DaemonEntitiesForm
        /// </summary>
        public string[] EntitiesListToString()
        {
            string[] listOfEntities = new string[CurrentNumberOfEntities()];
            for (int i = 0; i < listOfEntities.Length; i++)
            {
                listOfEntities[i] = daemonList[i].FormatString(i);
            }
            return listOfEntities;
        }
    }
}
