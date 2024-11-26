using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : MonoBehaviour, ICollectible
{
    public int exp;
    public void Collect()
    {   Debug.Log("Item Collected!");
        LevelUp target = FindAnyObjectByType<LevelUp>();
        target.AddXP(exp);
        Destroy(gameObject);
    }

}
