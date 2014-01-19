using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour
{
	[SerializeField] Rigidbody vehicle;
	[SerializeField] Rigidbody weapon;
	[SerializeField] float initialWait = 0f;
	[SerializeField] float fireRate = 0.1f;
	[SerializeField] float force = 150f;
	[SerializeField] bool addForce = true;
	int nextNameNumber = 0;
	private bool firing = false;
	
	public void Fire()
	{
		firing = true;
		StartCoroutine("Firing");
	}
	
	IEnumerator Firing()
	{
		yield return new WaitForSeconds(initialWait);

		while (firing)
		{
			Vector3 fwd = transform.forward;
			Quaternion fwdRot = Quaternion.LookRotation(fwd);
			var weaponClone = (Rigidbody)Instantiate(weapon, transform.position, fwdRot);
			weaponClone.name = transform.gameObject.name + " " + nextNameNumber;
			nextNameNumber++;

			if(addForce)
			{
				weaponClone.velocity = vehicle.velocity;
				weaponClone.AddForce(weaponClone.transform.forward * force, ForceMode.VelocityChange);
			}
			else
				Debug.Log("No force applied");
			
			yield return new WaitForSeconds(fireRate);
		}
	}
	
	public void Stop()
	{
		firing = false;
	}
}
