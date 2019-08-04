using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new jump attack")]
public class JumpAttack : AttackCommand
{
    public override IEnumerator execute(Battler user, Tile target)
    {
        Debug.Log(title + " was executed by " + user.title);

        user.curTile.removeOccupant();
        target.placeOccupant(user);

        List<Tile> targets = new List<Tile>();
        targets.Add(target);
        damagePattern.selectTargets(user.targeter.grid.tiles, targets, user, target.x, target.y);
        foreach (var tile in targets)
        {
            Debug.Log("jumpers" + tile.gameObject);
            if (tile.occupant != null)
                tile.occupant.takeDamage(damageBonus + user.stats.attack);
        }
        yield return null;
    }

    public override bool isValidTargetTile(Battler user, Tile target)
    {
        return target.occupant == null;//jump if tile is empty
    }
}