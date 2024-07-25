using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtils
{
    public class PooledObjectItem
    {
        private string _poolName;
        private GameObject _item;
        private int _amtOfCopies;
        private Stack<GameObject> _copies;
        private GameObject _parent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parent"></param>
        /// <param name="amtOfCopies"></param>
        public PooledObjectItem(GameObject item, GameObject parent, int amtOfCopies = 10)
        {
            this._poolName = $"{item.name}(Clone)";
            this._item = item;
            this._amtOfCopies = amtOfCopies;
            this._parent = parent;
            _copies = new Stack<GameObject>();
            CreateCopies(this._amtOfCopies);
        }


        
        private void CreateCopies(int amtOfCopies)
        {
            if (_parent == null)
            {
                _parent = new GameObject();
                _parent.name = $"{_item.name} pool";
            }

            for (int i = 0; i < amtOfCopies; i++)
            {
                GameObject copy = Object.Instantiate(_item) as GameObject;
                copy.SetActive(false);
                _copies.Push(copy);
                copy.transform.SetParent(_parent.transform);
            }
        }

        /// <summary>
        /// Gets the object from the pool & then sets the gameobject activity to true
        /// </summary>
        /// <returns>pooled Gameobject</returns>
        public GameObject GetObject()
        {
            if  (_copies.Count == 0)
            {
                CreateCopies(_amtOfCopies);
            }
            GameObject toUse = _copies.Pop();
            toUse.transform.SetParent(null);
            toUse.SetActive(true);
            return toUse;
        }

        private bool BelongToPool(GameObject checkObject)
        {
            return $"{ _item.name}(Clone)" == checkObject.name;
        }


        /// <summary>
        /// Brings the object back to the pool and disables it afterwards.
        /// </summary>
        /// <param name="returnObject">object to return</param>
        public void ReturnToPool(GameObject returnObject)
        {
            if (!BelongToPool(returnObject))
            {
                Error($"{returnObject.name} doesn't belong to this pool or doesn't have instanced pool");
                return;
            }
            returnObject.transform.SetParent(_parent.transform);
            returnObject.SetActive(false);
            _copies.Push(returnObject);
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Warning(string message)
        {
            Debug.LogWarning(message);
        }

        public void Error(string message)
        {
            Debug.LogError(message);
        }
    }

}
