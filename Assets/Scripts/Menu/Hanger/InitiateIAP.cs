using UnityEngine;
using System.Collections;

public class InitiateIAP : MonoBehaviour
{
	[SerializeField] IAPManager iapManager;
	[SerializeField] string productIdentifier;

	void OnClick()
	{
		iapManager.InitiatePurchase(productIdentifier);
	}
}
