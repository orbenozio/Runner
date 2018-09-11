using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUBManager : MonoBehaviour
{
    private static HUBManager _instance;
    
    [SerializeField] private Text _boosts;
    [SerializeField] private Text _distance;

    private void Start()
    {
        _instance = this;
    }

    public static void SetBoosts(int boosts)
    {
        _instance._boosts.text = boosts.ToString();
    }
    
    public static void SetDistance(float distance)
    {
        _instance._distance.text = distance.ToString("f0");
    }
}
