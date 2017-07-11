using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Type Stats", menuName = "2BS/Bullet Type Stats")]
public class BulletStats : ScriptableObject
{	
	public float damage;
	public float speed;
	public GameObject explosionParticleSystem;
}
