using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ShopUnitTests
    {
        private ShopGridView gridView; //This is the grid buy view we want to test

        //Setup the test scene
        [OneTimeSetUp]
        public void LoadShopScene()
        {
            // Load the Scene to do unit test. In the scope of this project, this is fine. In a more complicated project, a game scene could take
            // a long time to load, in which case it's better to create test scenes to do unit tests
            SceneManager.LoadScene(0);
        }

        //Setup the unit tests here
        [UnitySetUp]
        public IEnumerator SetupTests()
        {
            yield return
                null; //yield return null skips one frame, this is to make sure that this happens after the scene is loaded

            //The shop scene only contains one grid buy view, we use Resources.FindObjectsOfTypeAll to get the reference to it,
            //Resources.FFindObjectsOfTypeAll is used instead of GameObject.Find because the later can't find disabled objects
            gridView = Resources.FindObjectsOfTypeAll<ShopGridView>()[0];

            //Active the gridView game object to initialize the class, if we don't do this 'void Start()' won't be called
            //You should active all the game objects that are involved in the test before testing the functions from their components
            gridView.gameObject.SetActive(true);
        }

        // Use meaningful name for your test cases, this case tests if the ShopGridView component has initialized its shopModel property 
        [UnityTest]
        public IEnumerator ShopGridBuyViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a shopModel is assigned to gridView
            Assert.IsNotNull(ShopCreator.Instance.shopModel, "No BuyModel is assigned in ShopGridView");
        }

        //This case tests if the grid buy view displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopGridBuyViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the gridView game object was activated in SetupTests(), we can use GameObject.Find
            //to find the game object we want to test
            GameObject gridItemsPanel = GameObject.Find("GridItemsPanel");

            yield return
                new WaitForEndOfFrame(); //Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
            //so that the view finished updating and rendering everything 

            int itemCount = gridItemsPanel.transform.childCount;
            Assert.AreEqual(ShopCreator.Instance.shopModel.inventory.GetItemCount(), itemCount,
                "The generated item count is not equal to shopModel's itemCount");
        }

        //This case tests if the buyModel can throw an ArgumentOutOfRangeException when it's asked to select an item by a negative
        //index. Incorrect indexes can be generated from bugs in views or controllers, throwing the correct type of exceptions is
        //better than failing silently for debugging. Your unit tests should cover exception handlings
        [UnityTest]
        public IEnumerator ShopModelThrowsExceptionsWhenSelectingNegativeIndex()
        {
            //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
            yield return null;

            //Creates a delegate that call gridView.shopModel.SelectItemByIndex(-1), the test runner will run the function, and
            //check if an ArgumentOutOfRangeException is thrown, the unit test would fail if no ArgumentOutOfRangeException
            //was thrown
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate
            {
                ShopCreator.Instance.shopModel.SelectItemByIndex(-1);
            });
        }

        [UnityTest]
        public IEnumerator InventoryThrowsExceptionsWhenSelectingNegativeIndex()
        {
            yield return null;

            Assert.Throws<System.ArgumentOutOfRangeException>(delegate
            {
                ShopCreator.Instance.shopModel.inventory.GetItemByIndex(-1);
            });
        }

        [UnityTest]
        public IEnumerator InventoryThrowsExceptionsWhenSelectingExceedingIndex()
        {
            yield return null;

            int itemCount = ShopCreator.Instance.shopModel.inventory.GetItemCount();

            Assert.Throws<System.ArgumentOutOfRangeException>(delegate
            {
                //Get item with an index that will never be part of the list.
                ShopCreator.Instance.shopModel.inventory.GetItemByIndex(itemCount + 1);
            });
        }

        [UnityTest]
        public IEnumerator BuyModelThrowsExceptionWhenPriceTooHigh()
        {
            yield return null;

            //Retrieve current balance
            int moneyBefore = ShopCreator.MoneyCount;

            //Make sure we don't have any money.
            ShopCreator.CalculateBalance(-moneyBefore);

            Assert.Throws<System.ArgumentException>(delegate
            {
                //Perform transaction
                ShopCreator.Instance.shopModel.ConfirmTransactionSelectedItem(ShopActions.PURCHASED);
            });

            //Reset balance to not mess up other tests.
            ShopCreator.CalculateBalance(moneyBefore);
        }

        [UnityTest]
        public IEnumerator ShopViewUpdateItemListAfterPurchase()
        {
            yield return null;

            //Find the ShopGridView GameObject & component.
            ShopGridView gridView = GameObject.Find("ShopGridView").GetComponent<ShopGridView>();

            //Store the ListCount before performing any altering actions
            int countBefore = gridView.GetGridItemListCount();

            //Pass on correct Shop Action parameter
            ShopCreator.Instance.shopModel.ConfirmTransactionSelectedItem(ShopActions.PURCHASED);

            //Store the ListCount after performing the ListCount-altering action.
            int countAfter = gridView.GetGridItemListCount();

            //Compare the 2 ListCounts, saying that 2nd ListCount is 1 less than the 1st.
            Assert.That(countAfter, Is.EqualTo(countBefore - 1));
        }

        [UnityTest]
        public IEnumerator ShopViewUpdateItemListAfterSell()
        {
            yield return null;

            //Find the InventoryGridView GameObject & ShopGridView component.
            ShopGridView gridView = GameObject.Find("InventoryGridView").GetComponent<ShopGridView>();

            //Store the ListCount before performing any altering actions
            int countBeforeTransaction = gridView.GetGridItemListCount();

            //Pass on correct Shop Action parameter to Inventory Model (which has the "selling" functionality)
            ShopCreator.Instance.inventoryModel.ConfirmTransactionSelectedItem(ShopActions.SOLD);

            //Store the ListCount after performing the ListCount-altering action.
            int countAfterTransaction = gridView.GetGridItemListCount();

            //Compare the 2 ListCounts, saying that 2nd ListCount is 1 less than the 1st.
            Assert.That(countAfterTransaction, Is.EqualTo(countBeforeTransaction - 1));
        }

        [UnityTest]
        public IEnumerator ShopListUpdateItemListAfterSell()
        {
            yield return null;

            int inventoryItemCountBefore = ShopCreator.Instance.inventoryModel.inventory.GetItemCount();

            ShopCreator.Instance.shopModel.ConfirmTransactionSelectedItem(ShopActions.PURCHASED);

            int inventoryItemCountAfter = ShopCreator.Instance.inventoryModel.inventory.GetItemCount();

            Assert.That(inventoryItemCountAfter, Is.EqualTo(inventoryItemCountBefore + 1));
        }

        [UnityTest]
        public IEnumerator InventoryListUpdateItemListAfterPurchase()
        {
            yield return null;

            int itemCountBefore = ShopCreator.Instance.inventoryModel.inventory.GetItemCount();

            ShopCreator.Instance.shopModel.ConfirmTransactionSelectedItem(ShopActions.PURCHASED);

            int itemCountAfter = ShopCreator.Instance.inventoryModel.inventory.GetItemCount();

            Assert.That(itemCountAfter, Is.EqualTo(itemCountBefore + 1));
        }

        [UnityTest]
        public IEnumerator InventoryViewUpdateItemListAfterPurchase()
        {
            yield return null;

            GameObject inventoryView = GameObject.Find("InventoryGridView");
            inventoryView.SetActive(true);

            ShopGridView inventoryGrid = inventoryView.GetComponent<ShopGridView>();

            int itemCountBefore = inventoryGrid.GetGridItemListCount();

            ShopCreator.Instance.shopModel.ConfirmTransactionSelectedItem(ShopActions.PURCHASED);

            int itemCountAfter = inventoryGrid.GetGridItemListCount();

            Assert.That(itemCountAfter, Is.EqualTo(itemCountBefore));
        }

        [UnityTest]
        public IEnumerator ShopViewUpdateMoneyBalanceAfterPurchase()
        {
            yield return null;

            //Store balance before purchase
            int balanceBeforePurchase = ShopCreator.MoneyCount;

            //Select an item to purchase.
            ShopCreator.Instance.shopModel.SelectItemByIndex(1);

            //Get index of that item, for the sake of testing the procedure.
            int index = ShopCreator.Instance.shopModel.GetSelectedItemIndex();

            //Returns the correct item, from which we can store the price (= the change for the Money Balance)
            Item item = ShopCreator.Instance.shopModel.inventory.GetItemByIndex(index);
            int itemPrice = item.BasePrice;

            //Execute the transaction. This also executes the CalculateBalance method in the Inventory.
            ShopCreator.Instance.shopModel.ConfirmTransactionSelectedItem(ShopActions.PURCHASED);

            //Store balance after purchase
            int balanceAferPurchase = ShopCreator.MoneyCount;

            //Execute check
            Assert.That(balanceAferPurchase, Is.EqualTo(balanceBeforePurchase - itemPrice));
        }

        [UnityTest]
        public IEnumerator ShopViewUpdateMoneyBalanceAfterSell()
        {
            yield return null;


        }

        [UnityTest]
        public IEnumerator ShopViewUpdateMoneyBalanceAfterUpgrade()
        {
            yield return null;


        }
    }
}