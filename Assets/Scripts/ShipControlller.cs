using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipController : MonoBehaviour 
{	
	protected virtual void Start() {}

	public virtual void Damage(float damage) {}

	public virtual void Heal(float heal) {}

	public virtual void Die() {}
}

public abstract class ShipController <Stats> : ShipController where Stats : ShipStats
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
	public Stats baseStats;
	public CurrentStats currentStats;

	protected override void Start()
	{		
		currentStats = new CurrentStats(baseStats.maxHP);
		//Debug.Log(name + " HP: " + currentStats.HP);
	}

	public override void Damage(float damage)
	{
		currentStats.HP = Mathf.Clamp(currentStats.HP - damage, 0f, baseStats.maxHP);
		if(currentStats.HP == 0)
		{
			Die();
		}
	}

	public override void Heal(float heal)
	{
		currentStats.HP = (int) Mathf.Clamp( (currentStats.HP + heal), 0f, baseStats.maxHP);
	}

	public override void Die()
	{
		Destroy(gameObject);
	}
}