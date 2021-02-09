using UnityEngine;
using EvtSource;
using System;
using System.Threading.Tasks;
using Proyecto26;
using System.Collections.Generic;

class ThingController : MonoBehaviour
{
    [SerializeField]
    private bool _isConnected = false;
    public string _serverUri = "http://localhost:8080";
    public string _name = "";
    public List<GameObject> _items;
    [SerializeField] public GameObject DimmerWidgetPrefab;

    void Start()
    {
        GetGroupItems();
    }

    public void GetGroupItems()
    {
        RestClient.Get(_serverUri + "/rest/items/" + _name).Then(res => {
            if (res.StatusCode >= 200 && res.StatusCode < 300)
            {
                EquipmentItemModel equipmentItems = JsonUtility.FromJson<EquipmentItemModel>(res.Text);
                foreach (ItemModel2 item in equipmentItems.members)
                {
                    if (item.type == "Dimmer")
                    {
                        GameObject createdItem;
                        createdItem = Instantiate(DimmerWidgetPrefab, this.transform.position + Vector3.one, Quaternion.identity) as GameObject;
                        createdItem.GetComponent<DimmerWidget>()._Item = item.name;

                    }
                }

            }
            else
            {
                Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
            }
        });
    }

    public void AddItemToThing(GameObject item)
    {
        if (_items == null) _items = new List<GameObject>();
        _items.Add(item);
    }

}
