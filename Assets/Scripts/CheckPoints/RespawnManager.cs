using System.Collections.Generic;
using UnityEngine;

namespace CheckPoints
{
    using System.Collections.Generic;
    using UnityEngine;

    public class RespawnManager : MonoBehaviour
    {
        [SerializeField] private List<string> respawnTags = new List<string>();
        private List<GameObject> respawnableObjects = new List<GameObject>();

        private void Start()
        {
            CollectRespawnableObjects();
        }

        /// <summary>
        /// Collect all objects with a specific Tag
        /// </summary>
        private void CollectRespawnableObjects()
        {
            respawnableObjects.Clear();

            foreach (var tag in respawnTags)
            {
                var objects = GameObject.FindGameObjectsWithTag(tag);
                respawnableObjects.AddRange(objects);
            }
        }

        /// <summary>
        /// Reset the status of all collected objects
        /// </summary>
        public void RespawnAll()
        {
            foreach (var obj in respawnableObjects)
            {
                if (obj != null)
                {
                    ResetObject(obj);
                }
            }
        }

        /// <summary>
        /// Resets the state of a single object
        /// </summary>
        /// <param name="obj"></param>
        private void ResetObject(GameObject obj)
        {
            obj.SetActive(true); // reactivate
            obj.transform.position = obj.GetComponent<InitialPosition>()?.InitialPos ?? obj.transform.position; // Reset position
            var ipc = obj.GetComponent<IRespawnable>();
            if (ipc != null)
            {
                ipc.ResetState();
            }
        }
    }

}