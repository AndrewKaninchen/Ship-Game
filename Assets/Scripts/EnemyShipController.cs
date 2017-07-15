using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : ShipController
{
	[SerializeField]
	private GameObject explosionPrefab;

	private Rigidbody2D rb;
	private Collider2D col;

	[SerializeField]
	private Weapon weapon;

	private bool isShooting = false;

	public float shooting = 0f;

	protected override void Start()
	{
		base.Start();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();

		//weapon.gameObject.SetActive(true);
	}

	private void Update()
	{
		if (shooting > 0)
		{
			weapon.Shoot();			
		}
	}

	public void ToggleShooting()
	{
		isShooting = !isShooting;
	}
	public override void Die()
	{
		var e = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		col.enabled = false;
		
		Destroy(gameObject, .1f);
		Destroy(e, 1f);
	}
}
