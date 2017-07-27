using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShipController : ShipController {

	#region External Components
	//public SpaceShooter.UI.HUD hud;
	#endregion

	#region Components
	private Rigidbody2D rb;
	private Animator anim;

	[SerializeField]
	private Weapon weapon;
	#endregion

	#region Fields
	[SerializeField]
	private float
		moveSpeed;
	[SerializeField]
	[Header("Dash Parameters")]
	private float
		dashForce;
	[SerializeField]
	private float
		dashLinearDrag,
		dashDuration,
		dashDoublePressSpeed;
	#endregion

	#region Variables
	private Vector2 inputDir;

	private float
		inputHorizontal,
		inputVertical;
	private bool
		waitingForDash = false,
		dashing = false,
		shooting = false;
	private KeyCode waitingForDashKeyCode;
	private Coroutine waitingForDashCoroutine;

	
	#endregion

	#region Events
	public Action onDamage;
	public Action onHeal;
	#endregion

	protected override void Start ()
	{
		base.Start();
		if (rb == null) rb = GetComponent<Rigidbody2D>();
		if (anim == null) anim = GetComponentInChildren<Animator>();

		//hud.UpdateHealthStats(baseStats.maxHP, baseStats.maxHP);
	}

	private void Update ()
	{
		#region Cheats
		if (Input.GetKeyDown(KeyCode.J))
		{
			Heal(1);
		}
		#endregion

		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");
		inputDir = new Vector2(inputHorizontal, inputVertical);
		shooting = false;
		
		if (Input.GetButton("Fire1"))
		{
			weapon.Shoot();
			shooting = true;
		}

		if (!dashing && !waitingForDash)
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				waitingForDashKeyCode = KeyCode.A;
				waitingForDashCoroutine = StartCoroutine(WaitForDash(waitingForDashKeyCode));
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				waitingForDashKeyCode = KeyCode.D;
				waitingForDashCoroutine = StartCoroutine(WaitForDash(waitingForDashKeyCode));
			}
		}

		if (waitingForDash)
		{
			if (Input.GetKeyDown(waitingForDashKeyCode))
			{
				waitingForDash = false;
				StopCoroutine(waitingForDashCoroutine);
				StartCoroutine(Dash(waitingForDashKeyCode == KeyCode.A ? Vector2.left : Vector2.right));
			}
		}		
	}

	private void FixedUpdate()
	{
		if(!dashing)
			rb.velocity = Vector2.ClampMagnitude(inputDir * moveSpeed, moveSpeed);

		anim.SetFloat("Horizontal Speed", inputHorizontal, .1f, Time.fixedDeltaTime);
	}

	private IEnumerator WaitForDash(KeyCode code)
	{
		float waitTime = dashDoublePressSpeed;
		yield return new WaitForEndOfFrame();
		waitingForDash = true;
		while (waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		waitingForDash = false;
	}

	private IEnumerator Dash(Vector2 direction)
	{
		dashing = true;

		if(direction == Vector2.right)
			anim.SetTrigger("Roll Right");
		if (direction == Vector2.left)
			anim.SetTrigger("Roll Left");
		
		rb.velocity = Vector2.zero;
		rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
		rb.drag = dashLinearDrag;

		yield return new WaitForSeconds(dashDuration);

		rb.drag = 0;
		dashing = false;
	}

	public override void Die()
	{
		base.Die();
		Debug.Log("U Dead");
	}

	public void ChangeWeapon (GameObject weaponPrefab)
	{
		Destroy(weapon.gameObject);
		weapon = Instantiate(weaponPrefab, transform).GetComponent<Weapon>();
	}

	public override void Damage(float damage)
	{
		base.Damage(damage);
		//Debug.Log("Player damaged by " + damage + " points. Current HP = " + currentStats.HP);
		onDamage();
	}

	public override void Heal(float heal)
	{
		base.Heal(heal);
		//Debug.Log("Player healed by " + heal + " points. Current HP = " + currentStats.HP);
		onHeal();

	}
}