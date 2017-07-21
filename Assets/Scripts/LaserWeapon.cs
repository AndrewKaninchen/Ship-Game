using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
	#region Fields
	[SerializeField]
	int dps;

	[SerializeField]
	[Tooltip("Particle System used for laser hits.")]
	private ParticleSystem hitParticleSystem;

	[SerializeField]
	[Tooltip("Particle System used for laser muzzle.")]
	private ParticleSystem muzzleParticleSystem;

	[SerializeField]
	private Transform barrel;

	[SerializeField]
	[Tooltip("Whether this weapon is friendly to the player or used by an enemy.")]
	private bool friendly;

	
	private LineRenderer lineRenderer;

	private bool isShooting;
	#endregion

	public void Start()
	{
		if (lineRenderer == null) lineRenderer = GetComponentInChildren<LineRenderer>();
		lineRenderer.enabled = false;

		//Se não fizer isso os gizmos da Unity bugam. Vai entender.
		//lineRenderer.transform.SetParent(null);
		//hitParticleSystem.transform.SetParent(null);
		//muzzleParticleSystem.transform.SetParent(null);
	}

	private void Update()
	{
		if (isShooting)
		{
			lineRenderer.enabled = true;
			isShooting = false;
		}
		else
		{
			lineRenderer.enabled = false;
		}
	}

	public override void Shoot()
	{
		isShooting = true;
		RaycastHit2D hit = Physics2D.Raycast(barrel.position, barrel.up, 100f, friendly ? 1 << LayerMask.NameToLayer("Enemy") : 1 << LayerMask.NameToLayer("Player"));

		muzzleParticleSystem.transform.position = barrel.position;
		muzzleParticleSystem.transform.LookAt(barrel.position + barrel.up);
		muzzleParticleSystem.Emit((UnityEngine.Random.Range(3, 10)));

		if (hit.collider != null)
		{
			//Debug.DrawLine(barrel.position, hit.point, Color.blue);
			lineRenderer.SetPositions(new Vector3[2] { barrel.position, hit.point});

			hitParticleSystem.transform.position = hit.point;
			hitParticleSystem.transform.LookAt(transform);
			hitParticleSystem.Emit((UnityEngine.Random.Range(3, 10)));

			hit.transform.GetComponent<ShipController>().Damage(dps * Time.deltaTime);
		}

		else
		{
			lineRenderer.SetPositions(new Vector3[2] { barrel.position, barrel.position + barrel.up * 100f });
			//Debug.DrawRay(barrel.position, barrel.up * 100f, Color.red);
		}
	}

	private void OnDestroy()
	{
	}
}
