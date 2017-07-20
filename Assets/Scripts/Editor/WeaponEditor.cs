using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MachineGunWeapon))]
public class WeaponEditor : Editor {

	public override void OnInspectorGUI()
	{
		Weapon w = target as Weapon;
		base.OnInspectorGUI();
		if (GUILayout.Button("Shoot"))
		{
			w.Shoot();
		}
	}
}
