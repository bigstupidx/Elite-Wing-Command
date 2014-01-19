using UnityEngine;
using System.Collections;

public class ObjectIdentifier : MonoBehaviour
{
	[SerializeField] string objectType = "Easy";
	public string ObjectType { get { return objectType; }}
}
