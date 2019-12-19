using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItems : ScriptableObject
{
    public VariableUtilities.collectableType zemType;
    public VariableUtilities.PlayerNamesEnum playerNameEnum;
    public GameObject playerPrefab;
    public Sprite zemSprite;
    public Sprite playerSprite;
 
    public int requiredAmountToUnlock;

}
