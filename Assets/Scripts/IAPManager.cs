using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Purchasing;

    public class IAPManager :  Singleton<IAPManager>, IStoreListener
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        public string no_ads = "no_ads";
        public string pack_starter = "pack_starter";
        public string pack_starter_extra = "pack_starter_extra";
        public string pack_standard = "pack_standard";
        public string pack_standard_extra = "pack_standard_extra";
        public string pack_best = "pack_best";
        public string pack_premium = "pack_premium";
        public string pack_immortality = "pack_immortality";
        
        public storeScript _storeScript;




        void Start()
        {
            //Script has been removed due security reasons.
        }

        public void InitializePurchasing() 
        {
            //Script has been removed due security reasons.
        }


        public bool IsInitialized()
        {
            //Script has been removed due security reasons.
            return false;
        }


        public void BuyStarter()
        {
            BuyProductID(pack_starter);
        }
        public void BuyStarterExtra()
        {
            BuyProductID(pack_starter_extra);
        }
        
        public void BuyStandard()
        {
            BuyProductID(pack_standard);
        }

        public void BuyStandardExtra()
        {
            BuyProductID(pack_standard_extra);
        }

        public void BuyBest()
        {
            BuyProductID(pack_best);
        }
        
        public void BuyPremium()
        {
            BuyProductID(pack_premium);
        }
        
        public void BuyImmortality()
        {
            BuyProductID(pack_immortality);
        }
        
        public void BuyNoAds()
        {
            BuyProductID(no_ads);
        }


        public string GetProducePriceFromStore(string id)
        {
            //Script has been removed due security reasons.
            return null;
        }
        

        void BuyProductID(string productId)
        {
            //Script has been removed due security reasons.
        }


        public void RestorePurchases()
        {
            //Script has been removed due security reasons.
        }
        

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            //Script has been removed due security reasons.
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            //Script has been removed due security reasons.
        }


        private void GiveReward(int _diamonds, int _coins)
        {
         
            //Script has been removed due security reasons.
           
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
        {
            //Script has been removed due security reasons.
            return PurchaseProcessingResult.Complete;
        }

    


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            //Script has been removed due security reasons.
        }
    }
