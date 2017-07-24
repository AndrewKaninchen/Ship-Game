using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderEnforcement : MonoBehaviour {

	public PlayerShipController playerShip;
	private Transform playerTransform;
	private Rigidbody2D playerRigidbody;
	[Header("Borders")]
	public Vector2 offset;
	public Vector2 size;

	private void Start()
	{
		playerTransform = playerShip.transform;
		playerRigidbody = playerShip.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		playerTransform.position = new Vector3
		(
			Mathf.Clamp(playerTransform.position.x, offset.x - size.x / 2, offset.x + size.x / 2),
			Mathf.Clamp(playerTransform.position.y, offset.y - size.y / 2, offset.y + size.y / 2),
			playerTransform.position.z
		);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green * 0.7f;

		var corners = new Vector2[]
		{
			new Vector2(offset.x - size.x / 2, offset.y - size.y / 2),
			new Vector2(offset.x - size.x / 2, offset.y + size.y / 2),
			new Vector2(offset.x + size.x / 2, offset.y - size.y / 2),
			new Vector2(offset.x + size.x / 2, offset.y + size.y / 2)
		};

		Gizmos.DrawLine(corners[0], corners[1]);
		Gizmos.DrawLine(corners[0], corners[2]);
		Gizmos.DrawLine(corners[1], corners[3]);
		Gizmos.DrawLine(corners[2], corners[3]);
	}

}
