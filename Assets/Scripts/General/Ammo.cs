using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
	[SerializeField] string ammoSource;
	[SerializeField] float damageAmount = 20f;
	[SerializeField] float bulletLife = 1f;
	[SerializeField] bool GroundWeapon = false;
	[SerializeField] bool Bomb = false;
	[SerializeField] GameObject bombExplosion;
	[SerializeField] GameObject bombExplosionImpact;
	const float bulletHeightModifier = 70f;
	
	void Awake()
	{
		if(Bomb)
		{
			var modifyHeight = transform.position;
			modifyHeight.y = 0f;
			transform.position = modifyHeight;
		}

		Destroy(gameObject, bulletLife);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "CollectionArea" && other.tag != "MapBoundary" && other.tag != "Weapon" && other.tag != ammoSource)
		{
			if (bombExplosion != null)
			{
				Vector3 bombPosition = transform.position;
				bombPosition.y = transform.position.y + 1.08f;

				if (bombPosition.y > -8.5f)
					Instantiate(bombExplosion, bombPosition, transform.rotation);
				else
					Instantiate(bombExplosionImpact, bombPosition, transform.rotation);
			}

			Destroy(gameObject);
		}

		var hit = other.transform.GetComponentInChildren<Damageable>();
		
		if (hit != null && other.transform.root.tag != ammoSource)
			hit.ApplyDamage(damageAmount);
	}

	void FixedUpdate()
	{
		if (!Bomb && !GroundWeapon)
		{
			var modifyHeight = transform.position;

			if (modifyHeight.y < 0f)
				modifyHeight.y += Time.deltaTime * bulletHeightModifier;
			else
				modifyHeight.y = 0f;

			transform.position = modifyHeight;
		}
	}
}
