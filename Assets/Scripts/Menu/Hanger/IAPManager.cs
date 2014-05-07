using UnityEngine;
using System.Collections;

public class IAPManager : MonoBehaviour
{
	//The product identifiers defined in iTunes Connect
	[SerializeField] string[] productIdentifiers;
	[SerializeField] GameObject normalScreen;
	[SerializeField] GameObject loadingScreen;
	[SerializeField] GameObject errorScreen;
	[SerializeField] GameObject successScreen;
	[SerializeField] UILabel errorLabel;
	[SerializeField] UILabel rewardPointsLabel;
	
	enum IAPState {Loading, Normal, Error, Success};
	IAPState iapState = IAPState.Normal;
	StoreKitProduct[] products;

	void Start()
	{
		errorLabel.text = "An error occured\nduring the transaction.\nPlease try again.";
		ConfigureStoreKitEvents();
		EasyStoreKit.AssignIdentifiers(productIdentifiers);
		EasyStoreKit.LoadProducts();
	}

	void SetIAPLayout()
	{
		switch(iapState)
		{
		case IAPState.Normal:
			if (normalScreen != null)
			{
				normalScreen.SetActive(true);
				loadingScreen.SetActive(false);
				errorScreen.SetActive(false);
				successScreen.SetActive(false);
			}
			break;
		case IAPState.Loading:
			normalScreen.SetActive(false);
			loadingScreen.SetActive(true);
			errorScreen.SetActive(false);
			successScreen.SetActive(false);
			break;
		case IAPState.Error:
			normalScreen.SetActive(false);
			loadingScreen.SetActive(false);
			errorScreen.SetActive(true);
			successScreen.SetActive(false);
			break;
		case IAPState.Success:
			normalScreen.SetActive(false);
			loadingScreen.SetActive(false);
			errorScreen.SetActive(false);
			successScreen.SetActive(true);
			break;
		}
	}

	public void InitiatePurchase(string productIdentifier)
	{
		iapState = IAPState.Loading;
		SetIAPLayout();

		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			if(EasyStoreKit.CanMakePayments())
			{
				if(EasyStoreKit.BuyProductWithIdentifier(productIdentifier, 1))
				{
					//Valid product identifier. Do nothing, the event will be called once processing is complete.
				}
				else
				{
					Debug.Log("Invalid product identifier: " + productIdentifier);
					errorLabel.text = "An error occured\nduring the transaction.\nPlease try again.";
					iapState = IAPState.Error;
					SetIAPLayout();
				}
			}
			else 
			{
				Debug.Log("Application is not allowed to make payments");
				errorLabel.text = "Error: Application not\nallowed to make payments";
				iapState = IAPState.Error;
				SetIAPLayout();
			}
		} 
		else 
		{
			Debug.Log("No internet connection available");
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
		switch(productIdentifier)
		{
		case "5000_reward_points":
			EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 12000f);
			break;
		case "12000_reward_points":
			EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 20000f);
			break;
		case "20000_reward_points":
			EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 35000f);
			break;
		case "35000_reward_points":
			EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 60000f);
			break;
		case "50000_reward_points":
			EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 100000f);
			break;
		case "120000_reward_points":
			EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 250000f);
			break;
		default:
			Debug.LogError("Not valid product identifier....");
			errorLabel.text = "An error occured\nduring the transaction.\nPlease try again.";
			iapState = IAPState.Error;
			SetIAPLayout();
			break;
		}

		PlayerPrefs.Save();
		rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		iapState = IAPState.Success;
		SetIAPLayout();
	}
	
	public void TransactionFailed(string productIdentifier, string errorMessage)
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
