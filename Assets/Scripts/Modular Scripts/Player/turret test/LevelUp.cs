using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
   [Header("Experience/Level")]
    public int Level = 1;
    public int exp = 0;
    public int xpCap;
    

    [System.Serializable]
    public class LevelRange{
        public int startLevel;
        public int endLevel;
        public int xpCapIncrease;

    }

    public List<LevelRange> levelRanges;

   void Start(){
    xpCap = levelRanges[0].xpCapIncrease;
    }
 
    public void AddXP (int amount){
        exp += amount;
        CheckLevelUp();
    }

   void CheckLevelUp(){
        if (exp >= xpCap){
            Level ++;
            exp -= xpCap;
            int xpCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {if (Level >= range.startLevel && Level <= range.endLevel){
                xpCapIncrease = range.xpCapIncrease;
                break;
            }
                xpCap += xpCapIncrease;
            }
        }
    }

}
