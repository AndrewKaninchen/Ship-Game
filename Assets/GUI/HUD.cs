﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
	public class HUD : MonoBehaviour
	{
		private int maxHP = 0;
		private int currentHP = 0;

		private int maxSP = 0;
		private int currentSP = 0;

		public PlayerShipController playerShip;

		public GameObject healthBar;
		public GameObject shieldBar;

		public GameObject healthBlockPrefab;
		public GameObject shieldBlockPrefab;

		public Color healthBarColor, shieldBarColor;

		public List<GameObject> healthBlocks;
		public List<GameObject> shieldBlocks;

		private void Start()
		{
			for (int i = healthBlocks.Count - 1; i >= 0 ; i--)
			{
				Destroy (healthBlocks[i]);
				healthBlocks.Remove(healthBlocks[i]);
			}

			//for (int i = shieldBlocks.Count - 1; i >= 0; i--)
			//{
			//	Destroy(shieldBlocks[i]);
			//}

			UpdateHealthStats(playerShip.baseStats.maxHP, playerShip.baseStats.maxHP);

			playerShip.onDamage += () => UpdateHealthStats(playerShip.baseStats.maxHP, (int)playerShip.currentStats.HP);
			playerShip.onHeal += () => UpdateHealthStats(playerShip.baseStats.maxHP, (int)playerShip.currentStats.HP);
		}

		public void UpdateHealthStats(int newMaxHP, int newHP)
		{
			//Debug.Log("Current HP (HUD) = " + currentHP + ". New CurrentHP (HUD) = " + newHP);

			while (newMaxHP > maxHP)
			{
				AddHealthBlock();
				currentHP++;
				maxHP++;
			}

			for (int i = currentHP; i < newHP; i++)
			{
				Debug.Log("Healed 1");
				SetBlockUnDamaged(healthBlocks[i]);
			}

			for (int i = currentHP; i > newHP; i--)
			{
				SetBlockDamaged(healthBlocks[i-1]);
			}

			currentHP = newHP;
			maxHP = newMaxHP;
		}

		public void UpdateShieldStats(int newMaxSP, int newSP)
		{
			for (int i = 0; i < newMaxSP - maxSP; i++)
			{
				AddShieldBlock();
			}

			for (int i = currentSP; i < newSP; i++)
			{
				SetBlockUnDamaged(healthBlocks[i - 1]);
			}

			for (int i = currentSP; i > newSP; i--)
			{
				SetBlockDamaged(healthBlocks[i - 1]);
			}
		}

		private void AddHealthBlock()
		{
			var n = Instantiate(healthBlockPrefab, healthBar.transform);
			if (currentHP < maxHP)
			{
				SetBlockUnDamaged(healthBlocks[currentHP - 1]);
				SetBlockDamaged(n);
			}
			else
			{
				SetBlockUnDamaged(n);
			}

			healthBlocks.Add(n);

			LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
		}

		private void AddShieldBlock()
		{
			var n = Instantiate(shieldBlockPrefab, shieldBar.transform);
			if (currentSP < maxSP)
			{
				SetBlockUnDamaged(shieldBlocks[currentSP - 1]);
				SetBlockDamaged(n);
			}
			else
			{
				SetBlockUnDamaged(n);
			}

			shieldBlocks.Add(n);
			currentSP++;
			maxSP++;

			LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
		}

		private void SetBlockDamaged(GameObject block)
		{
			var i = block.GetComponent<Image>();
			i.color = new Color(i.color.r, i.color.g, i.color.b, .5f);
		}

		private void SetBlockUnDamaged(GameObject block)
		{
			var i = block.GetComponent<Image>();
			i.color = new Color(i.color.r, i.color.g, i.color.b, 1f);
		}
	}
}