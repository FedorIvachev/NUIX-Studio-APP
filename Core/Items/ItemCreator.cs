using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    public class ItemCreator : MonoBehaviour
    {

        [SerializeField] public GameObject itemPrefab;

        public void CreateItemGameObject()
        {
            GameObject createdItem;
            createdItem = Instantiate(itemPrefab, this.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
