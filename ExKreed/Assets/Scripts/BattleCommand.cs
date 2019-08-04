using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : ScriptableObject
{
    public string title = "nameless technique";
    public int delay = 1;
    public float delayPerStrain = .5f;
    public int strainBoost = 10;
    public GameObject fxPrefab;
    public float fxWaitDuration = .8f;
    public bool shouldTargetSelf = false;

    public TargettingPattern rangePattern;
    public TargettingPattern damagePattern;

    public abstract IEnumerator execute(Battler user, Tile targets);
    public abstract bool isValidTargetTile(Battler user, Tile targets);

    public int calcDelay(Battler user)
    {
        return Mathf.FloorToInt(delay + (user.stats.strain) * delayPerStrain);
    }

    public IEnumerator playFX(List<Tile> targets)
    {
        if (fxPrefab != null)
        {
            foreach (var tile in targets)
            {
                Instantiate(fxPrefab, tile.transform.position - new Vector3(0,0,1.6f) , Quaternion.identity);
            }

            yield return new WaitForSeconds(fxWaitDuration);
        }
    }
}
