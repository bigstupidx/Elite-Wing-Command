using UnityEngine;
using System.Collections;

public class IAPManager : MonoBehaviour
{
	//The product identifiers defined in iTunes Connect
	[SerializeField] string[] productIdentifiers;
	[SerializeField] GameObject normalScreen;
	[SerializeField] GameObject loadingScreen;
	[SerializeField] GameObject errorScreen;
	[SerializeField] UILabel errorLabel;
	[SerializeField] UILabel rewardPointsLabel;
	
	enum IAPState {Loading, Normal, Error};
	IAPState iapState = IAPState.Normal;
	StoreKitProduct[] products;

	void Start()
	{
		errorLabel.text = "Error: Application not\nallowed to make payments";
		ConfigureStoreKitEvents();
		EasyStoreKit.AssignIdentifiers(productIdentifiers);
		EasyStoreKit.LoadProducts();
	}

	void SetIAPLayout()
	{
		switch(iapState)
		{
		case IAPState.Normal:
			normalScreen.SetActive(true);
			loadingScreen.SetActive(false);
			errorScreen.SetActive(false);
			break;
		case IAPState.Loading:
			normalScreen.SetActive(false);
			loadingScreen.SetActive(true);
			errorScreen.SetActive(false);
			break;
		case IAPState.Error:
			normalScreen.SetActive(false);
			loadingScreen.SetActive(false);
			errorScreen.SetActive(true);
			break;
		}
	}

	public void InitiatePurchase(string productIdentifier)
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			if(EasyStoreKit.CanMakePayments())
			{
				iapState = IAPState.Loading;
				SetIAPLayout();

				if(EasyStoreKit.BuyProductWithIdentifier(productIdentifier, 1))
				{
					//Valid product identifier. Do nothing, the event will be called once processing is complete.
				}
				else
				{
					Debug.Log("Invalid product identifier: " + productIdentifier);
					iapState = IAPState.Normal;
					SetIAPLayout();
				}
				
			} 
			else 
			{
				Debug.Log("Application is not allowed to make payments!");
				errorLabel.text = "Error: Application not\nallowed to make payments";
				iapState = IAPState.Error;
				SetIAPLayout();
			}
		} 
		else 
		{
			Debug.Log("No internet connection available!");
			errorLabel.text = "Error: No internet connection";
			iapState = IAPState.Error;
			SetIAPLayout();
		}
	}

	//EasyStoreKit event handlers
	void ConfigureStoreKitEvents()
	{
		EasyStoreKit.productsLoadedEvent += ProductsLoaded;
		EasyStoreKit.transactionPurchasedEvent += TransactionPurchased;
		EasyStoreKit.transactionFailedEvent += TransactionFailed;
		EasyStoreKit.transactionCancelledEvent += TransactionCancelled;
	}
	
	public void ProductsLoaded(StoreKitProduct[] products)
	{
		Debug.Log("Products loaded....");
		iapState = IAPState.Normal;
		SetIAPLayout();
	}

	public void TransactionPurchased(string productIdentifier)
	{
		Debug.Log("Successfully purchased: " + productIdentifier);

		switch(productIdentifier)
		{
		case "5000_reward_points":
			PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + 5000f);
			break;
		case "12000_reward_points":
			PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + 12000f);
			break;
		case "20000_reward_points":
			PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + 20000f);
			break;
		case "35000_reward_points":
			PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + 35000f);
			break;
		case "50000_reward_points":
			PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + 50000f);
			break;
		case "120000_reward_points":
			PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + 120000f);
			break;
		default:
			Debug.LogError("Not valid product identifier....");
			break;
		}

		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		iapState = IAPState.Normal;
		SetIAPLayout();
	}
	
	public void TransactionFailed(string productIdentifier,string errorMessage)
	{
		Debug.Log("Transaction failed for: " + productIdentifier + " :" + errorMessage);
		errorLabel.text = "An error occured\nduring the transaction.\nPlease try again.";
		iapState = IAPState.Error;
		SetIAPLayout();
	}
	
	public void TransactionCancelled(string productIdentifier)
	{
		Debug.Log("Transaction Cancelled for: " + productIdentifier);
		iapState = IAPState.Normal;
		SetIAPLayout();
	}
}
