using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class ThingConstructor : MonoBehaviour
{
    [SerializeField] public GameObject[] _itemsPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemPrefab(int prefab_index)
    {
        Vector3 RightIndexTipPosition = Vector3.zero;
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose poseRightIndexTip))
        {
            RightIndexTipPosition = poseRightIndexTip.Position;
        }

        GameObject itemPrefab;
        itemPrefab = Instantiate(_itemsPrefabs[prefab_index], this.transform.position, Quaternion.identity) as GameObject;

    }
}
