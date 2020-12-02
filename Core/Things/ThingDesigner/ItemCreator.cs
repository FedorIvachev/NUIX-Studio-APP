using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Provides function to instantiate an item using a defined prefab
    /// </summary>
    public class ItemCreator : MonoBehaviour
    {

        [SerializeField] public GameObject itemPrefab;

        /// <summary>
        /// Instantiate an item using a defined prefab
        /// </summary>
        public void CreateItemGameObject()
        {
            GameObject createdItem;
            createdItem = Instantiate(itemPrefab, this.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
