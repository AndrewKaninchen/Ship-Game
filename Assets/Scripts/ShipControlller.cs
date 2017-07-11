using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipController : MonoBehaviour
{
	public struct CurrentStats
	{
		public float HP;

		public CurrentStats(float HP)
		{
			this.HP = HP;
		}	
	}

	[SerializeField]
	private ShipStats baseStats;
	private CurrentStats currentStats;

	protected virtual void Start()
	{
		currentStats = new CurrentStats(baseStats.maxHP);
		Debug.Log(name + " HP: " + currentStats.HP);
	}

	public void Damage(float damage)
	{
		currentStats.HP -= damage;
		if(currentStats.HP <= 0)
		{
			Die();
		}
	}

	public void Heal(float heal)
	{
		currentStats.HP = (int) Mathf.Clamp( (currentStats.HP + heal), 0f, baseStats.maxHP);
	}

	public abstract void Die();
}