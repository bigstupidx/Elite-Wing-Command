using UnityEngine;
using System.Collections;

public class PurchasePromptRedirect : MonoBehaviour
{
	[SerializeField] GameObject promptPanel;
	[SerializeField] GameObject[] upgradeCategories;
	[SerializeField] GameObject purchaseRewardPointsPanel;

	void OnClick()
	{
		foreach (GameObject upgradePanel in upgradeCategories)
			upgradePanel.SetActive(false);

		purchaseRewardPointsPanel.SetActive(true);
		promptPanel.SetActive(false);
	}
}
