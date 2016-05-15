using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
using System;
using System.Collections.Generic;

public class Billing : MonoBehaviour ,IStoreListener
{
    const string Key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAphEEY6R5amo5bJN8dq5Mt7d8/0OLM0B1QOCBzVlPOj6VtxE6IUEHvTJz3WbskqRzhXrklJTj8CaodD/ucCS5MvD6vOrmahG7pntwXNJS5RygBOrkLhdVlkUdMkNH/rSUvQP1ZUhQZ3IDgZo0Yi4VhKh34Vv2UMmi5moZjdj2damS3JvbpNtm98gOiqm4TUJz0+2wnEBZo+k2sUqUYSbpc9Fqr3OmbKvQH4P7oXlBokpj3gmJnx+Ki3P6cRaylqTdWnFQJ7Ho8IJHKumHr6f0hHuRZcQMT/0sZ2X25F4F8AmYc2J3c1xKUPIuG4b60HNPA33UorsFuYZ8w3YvHc0E6QIDAQAB";
    const string ProductPrefix = "coins_";
    readonly string PackageName = "com.BlackX.TankSurvival";
    bool init;
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    public Dictionary<string, int> Coins = new Dictionary<string, int>();

    public Billing()
    {
        Coins.Add("coins_1", 6000);
        Coins.Add("coins_2", 10000);
        Coins.Add("coins_3", 15000);
        Coins.Add("coins_4", 20000);
        Coins.Add("coins_5", 30000);
        Coins.Add("coins_6", 35000);
    }

    void Start()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(ProductPrefix + "1", ProductType.Consumable);
        builder.AddProduct(ProductPrefix + "2", ProductType.Consumable);
        builder.AddProduct(ProductPrefix + "3", ProductType.Consumable);
        builder.AddProduct(ProductPrefix + "4", ProductType.Consumable);
        builder.AddProduct(ProductPrefix + "5", ProductType.Consumable);
        builder.AddProduct(ProductPrefix + "6", ProductType.Consumable);
        builder.Configure<IGooglePlayConfiguration>().SetPublicKey(Key);
        UnityPurchasing.Initialize(this, builder);
    }


    public void BuyProductID(string productId)
    {
        // If the stores throw an unexpected exception, use try..catch to protect my logic here.
        try
        {
            // If Purchasing has been initialized ...
            if (init)
            {
                // ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
                Product product = m_StoreController.products.WithID(productId);
                // If the look up found a product for this device's store and that product is ready to be sold ...
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                BuyButton.HidePanel(GameObject.Find("Fade"));
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        // Complete the unexpected exception handling ...
        catch (Exception e)
        {
            // ... by reporting any unexpected exception for later diagnosis.
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }





    public void OnInitializeFailed(InitializationFailureReason error)
    {
   
        m_StoreController = null;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = null;

        init = false;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;

        init = true;
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        int coins = Coins[args.purchasedProduct.definition.id];

        PlayerData pd = ScoreModule.GetPlayerData();
        pd.PlayerMoney += coins;
        ScoreModule.SavePlayerData(pd);

        BuyButton.HidePanel(GameObject.Find("Buy"));
        BuyButton.HidePanel(GameObject.Find("Fade"));


        
        return PurchaseProcessingResult.Complete;
    }
}
