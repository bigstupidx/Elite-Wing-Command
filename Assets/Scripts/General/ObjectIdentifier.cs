using UnityEngine;
using System.Collections;

public class ObjectIdentifier : MonoBehaviour
{
	[SerializeField] string objectType = "";
	public string ObjectType { get { return objectType; }}
}
