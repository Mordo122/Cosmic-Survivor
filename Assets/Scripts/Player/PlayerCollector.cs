using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
private void OnTriggerEnter2D (Collider2D col){
    //check for Icollectible interface
    if(col.gameObject.TryGetComponent(out ICollectible collectible)){
        //if yes, collect
        collectible.Collect();
    }
}
}
