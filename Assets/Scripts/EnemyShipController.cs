using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAction
{
	None,
	Shoot,	
}

[System.Serializable]
public class EnemyShipController : ShipController <ShipStats>
{
	[SerializeField]
	private GameObject explosionPrefab;

	//private Rigidbody2D rb;
	private Collider2D col;

	[SerializeField]
	private Weapon[] weapons;

	public EnemyAction currentAction = EnemyAction.None;

	protected override void Start()
	{
		base.Start();
		//rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
	}

	private void Update()
	{
		if (currentAction == EnemyAction.Shoot)
			foreach (Weapon w in weapons)
				w.Shoot();
		currentAction = EnemyAction.None;
	}

	public override void Die()
	{
		var e = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		col.enabled = false;

		GameManager.playerShip.AddExperience(baseStats.xp);		

		Destroy(gameObject, .1f);
		Destroy(e, 1f);
	}
}
