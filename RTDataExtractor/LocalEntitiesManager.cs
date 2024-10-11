using EvilDICOM.Network.SCUOps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTDataExtractor
{
    class LocalEntitiesManager
    {
        private List<Local> localList;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocalEntitiesManager()
        {
            localList = new List<Local>();
        }

        /// <summary>
        /// Mathod for calculating the current number of entities.
        /// </summary>
        public int CurrentNumberOfEntities()
        {
            int numberOfEntitiess = localList.Count;
            return numberOfEntitiess;
        }

        /// <summary>
        /// Method for getting a entity at a specified index.
        /// </summary>
        public Local GetEntityAt(int index)
        {
            if (CheckIndex(index))
                return localList[index];
            else
                return null;
        }

        /// <summary>
        /// Method for checking if the index used as argument is within the array localList.
        /// </summary>
        private bool CheckIndex(int index)
        {
            bool isWithin = false;
            if (index >= 0 && index < localList.Count)
            {
                isWithin = true;
            }
            return isWithin;
        }

        /// <summary>
        /// Method for adding a local entity. Returns a boolean indicating if the entity was added or not. 
        /// </summary>
        public bool AddLocalEntity(Local newLocal)
        {
            bool ok = false;
            if (newLocal != null)
            {
                localList.Add(newLocal);
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method for changing an entity at the specified index. 
        /// </summary>
        public bool ChangeEntityAt(int index, Local local)
        {
            bool ok = false;
            if (CheckIndex(index) && local != null)
            {
                localList[index] = local;
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
                localList.RemoveAt(index);
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method for creating a list of strings that can be displayed in the LocalEntitiesForm
        /// </summary>
        public string[] EntitiesListToString()
        {
            string[] listOfEntities = new string[CurrentNumberOfEntities()];
            for (int i = 0; i < listOfEntities.Length; i++)
            {
                listOfEntities[i] = localList[i].FormatString(i);
            }
            return listOfEntities;
        }
    }
}
