using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartyMember 
{
    public Battler battler;
    public int startX;
    public int startY;

    public PartyMember(Battler battler, int startX, int startY)
    {
        this.battler = battler;
        this.startX = startX;
        this.startY = startY;
    }
}
