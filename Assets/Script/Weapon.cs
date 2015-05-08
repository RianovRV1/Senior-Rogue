using UnityEngine;
using System.Collections;
/// <summary>
/// Weapons collision logic to "kill" the enemy
/// </summary>
public class Weapon : MonoBehaviour {
void OnTriggerEnter2D (Collider2D other) 
	{
		var enemy = other.GetComponent<Enemy> (); // see if collision was an enemy
		if (enemy == null)// if not return
			return;
        
		enemy.gameObject.SetActive(false); //set enemy to false

	}

}
