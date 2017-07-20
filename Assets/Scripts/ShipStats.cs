using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Type Stats", menuName = "2BS/Ship Type Stats")]
public class ShipStats : ScriptableObject
{
	public GameObject prefab;
	public int maxHP;
}
