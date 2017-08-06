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
	public ShipStats baseStats;
	public CurrentStats currentStats;

	protected virtual void Start()
	{
		currentStats = new CurrentStats(baseStats.maxHP);
		//Debug.Log(name + " HP: " + currentStats.HP);
	}

	public virtual void Damage(float damage)
	{
		currentStats.HP = Mathf.Clamp(currentStats.HP - damage, 0f, baseStats.maxHP);
		if(currentStats.HP == 0)
		{
			Die();
		}
	}

	public virtual void Heal(float heal)
	{
		currentStats.HP = (int) Mathf.Clamp( (currentStats.HP + heal), 0f, baseStats.maxHP);
	}

	public virtual void Die()
	{
		Destroy(gameObject);
	}
}