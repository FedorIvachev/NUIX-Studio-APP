using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tsinghua.NUIX
{

    [CreateAssetMenu]
    /// <summary>
    /// Main model object for items.
    /// Items can be buttons, switches,
    /// Lights, Images,
    /// Gestures, Location,
    /// Sets of other items.
    /// </summary>
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public ItemType type;
        public Vector3 location;
        public Environment environment;

        public GameObject itemPrefab;

        public UnityEvent actionToPerform;

        public void OnClickAction()
        {

        }

    }

    public enum ItemType
    {
        Switch,
        Dimmer,
        Button,
        Label,
        String,
        Number,
        Light,
        Gesture,
        Image,
        Action
    }

    public enum Environment
    {
        Home,
        Car,
        Classroom,
        Empty
    }
}