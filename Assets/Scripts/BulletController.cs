using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
	#region Components
	[SerializeField]
	private Rigidbody2D rigidbody;
	#endregion

	#region Fields
	[SerializeField]
	private BulletStats stats;
	
	public bool friendly; 
	#endregion

	public void Start ()
	{
		Destroy(gameObject, 5f);
		if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = transform.up * stats.speed;

		if(friendly)
			gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
		else
			gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Bateu");
		ShipController hit = collision.gameObject.GetComponent<ShipController>();
		if (hit != null)
		{
			Debug.Log("Hit " + hit);
			hit.Damage(stats.damage);
			Explode();
		}
	}	

	private void Explode ()
	{
		var e = Instantiate(stats.explosionParticleSystem, transform.position, transform.rotation);
		Destroy(gameObject);
		Destroy(e, .5f);
	}
}
