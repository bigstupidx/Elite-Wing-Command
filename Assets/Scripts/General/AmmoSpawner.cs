using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour
{
	[SerializeField] Rigidbody vehicle;
	[SerializeField] Rigidbody weapon;
	[SerializeField] bool isTurretAirWeapon = false;
	[SerializeField] Transform rotationPivot;
	[SerializeField] float initialWait = 0f;
	[SerializeField] float fireRate = 0.12f;
	[SerializeField] float cooldown = 0f;
	[SerializeField] float force = 60f;
	Vector3 fwd;
	int nextNameNumber = 0;
	float nextShootTime = 0f;
	private bool firing = false;
	public Rigidbody Weapon { get { return weapon; } set { weapon = value; }}
	public float InitialWait { get { return initialWait; } set { initialWait = value; }}
	public float FireRate { get { return fireRate; } set { fireRate = value; }}
	public float Cooldown { get { return cooldown; } set { cooldown = value; }}
	public float Force { get { return force; } set { force = value; }}
	
	public void Fire()
	{
		if (Time.time >= nextShootTime)
		{
			nextShootTime = Time.time + cooldown;
			firing = true;
			StartCoroutine("Firing");
		}
	}
	
	IEnumerator Firing()
	{
		yield return new WaitForSeconds(initialWait);

		while (firing)
		{
			if (isTurretAirWeapon)
				fwd = rotationPivot.forward;
			else
				fwd = transform.forward;

			Quaternion fwdRot = Quaternion.LookRotation(fwd);
			var weaponClone = (Rigidbody)Instantiate(Weapon, transform.position, fwdRot);
			weaponClone.name = transform.gameObject.name + " " + nextNameNumber;
			nextNameNumber++;
			weaponClone.velocity = vehicle.velocity;
			weaponClone.AddForce(weaponClone.transform.forward * force, ForceMode.VelocityChange);
			
			yield return new WaitForSeconds(fireRate);
		}
	}
	
	public void Stop()
	{
		firing = false;
	}
}
