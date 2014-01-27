using UnityEngine;
using System.Collections;

public class ShipCollision : MonoBehaviour
{
	[SerializeField] Damageable damageable;
	[SerializeField] float collisionDamage = 15f;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "CollectionArea" && other.gameObject.tag != "Weapon" && other.transform.root.gameObject.tag != transform.root.gameObject.tag)
		{
			damageable.ApplyDamage(collisionDamage);
		}
	}
}
