using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> CollectedItems = new();
}
