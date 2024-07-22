using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="EnemyList",menuName = "Inventory/EnemyList")]

public class EnemyList_SO : ScriptableObject
{
    public List<EnemyDetails> enemyDetailsList;
}
