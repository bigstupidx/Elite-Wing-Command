using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
	[SerializeField] bool allyAirBullet = false;
	[SerializeField] bool allyGroundWeapon = false;
	[SerializeField] string ammoSource;
	[SerializeField] float damageAmount = 20f;
	[SerializeField] float bulletLife = 1f;
	[SerializeField] bool GroundWeapon = false;
	[SerializeField] bool Bomb = false;
	[SerializeField] bool missile = false;
	[SerializeField] GameObject bombExplosion;
	[SerializeField] GameObject bombExplosionImpact;
	[SerializeField] string damageObjectiveAirTag;
	[SerializeField] string damageObjectiveGroundTag;
	const float bulletHeightModifier = 70f;
	float bulletMultiplier = 1f;
	
	void Awake()
	{
		if(Bomb)
		{
			var modifyHeight = transform.position;
			modifyHeight.y = 0f;
			transform.position = modifyHeight;
		}

		if (allyAirBullet)
		{
			bulletMultiplier = PlayerPrefs.GetFloat("Ally Air Weapon Multiplier", 1f);
			damageAmount *= bulletMultiplier;
		}

		if (allyGroundWeapon)
		{
			bulletMultiplier = PlayerPrefs.GetFloat("Ally Ground Weapon Multiplier", 1f);
			damageAmount *= bulletMultiplier;
		}

		if (missile)
			StartCoroutine(MissileSelfDestruct());

		Destroy(gameObject, bulletLife);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "MapBoundary" && other.tag != "Weapon" && other.tag != ammoSource)
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
		
		if (hit != null && other.transform.root.tag != ammoSource && other.transform.root.tag != damageObjectiveAirTag && other.transform.root.tag != damageObjectiveGroundTag)
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

	IEnumerator MissileSelfDestruct()
	{
		yield return new WaitForSeconds(bulletLife -  0.05f);
		Instantiate(bombExplosion, transform.position, transform.rotation);
	}
}
