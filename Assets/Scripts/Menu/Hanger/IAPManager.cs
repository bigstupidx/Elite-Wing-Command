using UnityEngine;
using System.Collections;

public class IAPManager : MonoBehaviour
{
	//the product identifiers defined in iTunes Connect
	[SerializeField] string[] productIdentifiers;
	[SerializeField] GameObject normalScreen;
	[SerializeField] GameObject loadingScreen;
	
	enum IAPState {Loading, Normal};
	IAPState iapState = IAPState.Normal;
	StoreKitProduct[] products;

	void Start()
	{
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
			break;
		case IAPState.Loading:
			normalScreen.SetActive(false);
			loadingScreen.SetActive(true);
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
					//valid product identifier. Do nothing, the event will be called once processing is complete
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
			}
		} 
		else 
		{
			Debug.Log("No internet connection available!");
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
		//Unlock feature based on the identifier
		//We are only updating the UI!
		Debug.Log("Successfully purchased: " + productIdentifier);

		switch(productIdentifier)
		{
		case "5000_reward_points":
			Debug.Log("5,000 RP Purchase Action");
			break;
		case "12000_reward_points":
			Debug.Log("12,000 RP Purchase Action");
			break;
		case "20000_reward_points":
			Debug.Log("20,000 RP Purchase Action");
			break;
		case "35000_reward_points":
			Debug.Log("35,000 RP Purchase Action");
			break;
		case "50000_reward_points":
			Debug.Log("50,000 RP Purchase Action");
			break;
		case "120000_reward_points":
			Debug.Log("120,000 RP Purchase Action");
			break;
		default:
			Debug.LogError("Not valid product identifier....");
			break;
		}

		iapState = IAPState.Normal;
		SetIAPLayout();
	}
	
	public void TransactionFailed(string productIdentifier,string errorMessage)
	{
		Debug.Log("Transaction failed for: " + productIdentifier + " :" + errorMessage);
		iapState = IAPState.Normal;
		SetIAPLayout();
	}
	
	public void TransactionCancelled(string productIdentifier)
	{
		//Remove any activity indicators as the user has cancelled the transaction
		Debug.Log("Transaction Cancelled for: " + productIdentifier);
		iapState = IAPState.Normal;
		SetIAPLayout();
	}
}
