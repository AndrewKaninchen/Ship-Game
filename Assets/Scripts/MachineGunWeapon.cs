using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : Weapon
{
	#region Fields
	[SerializeField]
	private float cooldownSize;
	[SerializeField]
	[Tooltip("Spread angle of the bullets.")]
	[Range(0f, 180f)]
	private float bulletSpread;
	[SerializeField]
	[Tooltip("Amount of bullets per shot.")]
	[Range(1, 10)]
	private int amountOfBullets;

	[SerializeField]
	private GameObject bulletPrefab;

	private GameObject bulletTemplate;
	private BulletController bulletPrefabController;

	[SerializeField]
	private Transform barrel;

	private float cooldown;

	public bool friendly;
	#endregion

	public void Start()
	{
		cooldown = 0;
		bulletTemplate = Instantiate(bulletPrefab);
		bulletPrefabController = bulletTemplate.GetComponent<BulletController>();
		bulletPrefabController.friendly = friendly;
		bulletTemplate.SetActive(false);
	}
	public override void Shoot()
	{
		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
			return;
		}

		cooldown = cooldownSize;

		if (amountOfBullets == 1)
		{
			var b = Instantiate(bulletTemplate, barrel.position, barrel.rotation);
			b.SetActive(true);
		}
		else
		{
			for (int i = 0; i < amountOfBullets; i++)
			{
				var b = Instantiate(bulletTemplate, barrel.position, Quaternion.Euler(0f, 0f, (i * (bulletSpread / (amountOfBullets - 1)) - bulletSpread / 2)) * barrel.rotation);
				b.SetActive(true);
			}
		}
	}
}