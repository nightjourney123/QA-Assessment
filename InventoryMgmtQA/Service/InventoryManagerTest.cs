using InventoryMgmt.Model;
using InventoryMgmt.Service;
using InventoryMgmt.Interface;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

// guide: https://www.aligrant.com/web/blog/2020-07-20_capturing_console_outputs_with_microsoft_test_framework

namespace InventoryMgmtQA.Service
{
    [TestClass]
    public class InventoryManagerTest
    {
        private IInventoryManager _inventoryManager = new InventoryManager();

        [TestMethod]
        public void TestAddProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    1.23M
                );

                // console output should contain 'success'
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        public void TestAddProductPriceNegative()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    -1.0M
                );
                Assert.IsFalse(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        public void TestGetTotalValue()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    2.56M
                );
                _inventoryManager.GetTotalValue();
                Assert.IsTrue(sw.ToString().Contains("2.56"));
            }
        }

        // add more test cases

        // adding a product with 0 price 

        public void TestAddProductZeroPrice()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    1,
                    0
                );

                // console output should contain 'success'
                Assert.IsTrue(sw.ToString().Contains("success"));
            }

        }


        // adding a product with negative quantity

        [TestMethod]
        public void TestAddProductNegativeQuantity()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    -1,
                    2.6M
                );

                // console output should contain 'success'
                Assert.IsFalse(sw.ToString().Contains("success"));
            }

        }


        // removing a product

        [TestMethod]
        public void TestRemoveProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    2,
                    2.46M
                );

                // console output should contain 'success' and the list of product must not contain TestProduct1
                Assert.IsTrue(sw.ToString().Contains("success"));

                _inventoryManager.RemoveProduct(1
                );

                _inventoryManager.ListProducts();

                Assert.IsFalse(sw.ToString().Contains("TestProduct1"));
            }

        }

        // removing a product with a negative product ID

        [TestMethod]
        public void TestRemoveProductNegativeID()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    2,
                    2.46M
                );

                // console output should contain 'success'
                Assert.IsTrue(sw.ToString().Contains("success"));

                _inventoryManager.RemoveProduct(-1
                );


                // console output should contain TestProduct1
                
                _inventoryManager.ListProducts();
                Assert.IsTrue(sw.ToString().Contains("TestProduct1"));
            }

        }

        // removing a product with non existing product ID

        [TestMethod]
        public void TestRemoveNonExistingProductID()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    2,
                    2.46M
                );

                // console output should contain 'success'
                Assert.IsTrue(sw.ToString().Contains("success"));

                _inventoryManager.RemoveProduct(5
                );

                // console output should contain TestProduct1 

                _inventoryManager.ListProducts();

                Assert.IsTrue(sw.ToString().Contains("TestProduct1"));
            }

        }

        // updating a product 

        [TestMethod]

        public void TestUpdateProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    312,
                    2.46M
                );

                // console output should contain 'success' and 312
                Assert.IsTrue(sw.ToString().Contains("success"));

                _inventoryManager.ListProducts();

                Assert.IsTrue(sw.ToString().Contains("312"));

                // updating the product

                _inventoryManager.UpdateProduct(1,
                    507
                );

                // console output should contain 507
                _inventoryManager.ListProducts();
                Assert.IsTrue(sw.ToString().Contains("507"));


            }

        }

        // updating a product with non existing product ID
        

        [TestMethod]

        public void TestUpdateProductNonExistingProductID()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                _inventoryManager.UpdateProduct(-1,
                    5
                );


                // console output should contain 'Product not found, please try again'
                Assert.IsTrue(sw.ToString().Contains("Product not found, please try again"));
            }

        }

        // geting the total value with multiple products

        [TestMethod]
        public void TestGetTotalValueWithMultipleProducts()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    5,
                    11M
                );

                _inventoryManager.GetTotalValue();

                // console output should contain with total value = 5 * 11M = 55M
                Assert.IsTrue(sw.ToString().Contains("55"));

                _inventoryManager.AddNewProduct(
                    "TestProduct2",
                    1,
                    20M
                );

                _inventoryManager.GetTotalValue();

                // console output should contain with total value = 55M + 20M = 75M
                Assert.IsTrue(sw.ToString().Contains("75"));

            }

        }

        // getting the total value with an invalid price

        [TestMethod]
        public void TestGetTotalValueWithInvalidPrice()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    1,
                    0
                );

                _inventoryManager.GetTotalValue();

                // console output should with total value = 0
                Assert.IsTrue(sw.ToString().Contains("0"));

                _inventoryManager.AddNewProduct(
                    "TestProduct2",
                    1,
                    -1M
                );

                _inventoryManager.GetTotalValue();

                // since adding a product with negative price is not allowed, the total value should still be 0
                Assert.IsTrue(sw.ToString().Contains("0"));

            }

        }

        // getting the list of products of an empty inventory

        [TestMethod]
        public void TestListProductsNoProducts() {
            using (StringWriter sw = new StringWriter())
            {
              
                Console.SetOut(sw);

                _inventoryManager.ListProducts();

                // console output should contain "No products in here"
                Assert.IsTrue(sw.ToString().Contains("No products in here."));
            }
        }

        // get the list of products of inventory with multiple products

        [TestMethod]
        public void TestListProductsWithProducts()
        {
            using (StringWriter sw = new StringWriter())
            {

                Console.SetOut(sw);

                // Add a product to the inventory
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    3,
                    9.99M
                );

                _inventoryManager.AddNewProduct(
                    "TestProduct2",
                    3,
                    0.01M
                );

                _inventoryManager.AddNewProduct(
                    "TestProduct3",
                    3,
                    2
                );

                // Call ListProducts after adding a product
                _inventoryManager.ListProducts();

                // Check if console output contains the products
                Assert.IsTrue(sw.ToString().Contains("TestProduct1"));
                Assert.IsTrue(sw.ToString().Contains("TestProduct2"));
                Assert.IsTrue(sw.ToString().Contains("TestProduct3"));


            }
        }

        // create read update and delete operation

        [TestMethod]
        public void TestWholeService()
        {
            using (StringWriter sw = new StringWriter())
            {
                // Capture the console output
                Console.SetOut(sw);


                // Add a product to the inventory
                _inventoryManager.AddNewProduct(
                    "TestProduct1",
                    25,
                    24.99M
                );

                _inventoryManager.AddNewProduct(
                    "TestProduct2",
                    20,
                    1000
                );

                _inventoryManager.AddNewProduct(
                    "TestProduct3",
                    8999,
                    60
                );
               
                _inventoryManager.ListProducts();

                // check console output contains the products
                Assert.IsTrue(sw.ToString().Contains("TestProduct1"));
                Assert.IsTrue(sw.ToString().Contains("TestProduct2"));
                Assert.IsTrue(sw.ToString().Contains("TestProduct3"));

                _inventoryManager.GetTotalValue();

                // (25*24.99) + (20*1000) + (8999*60)
                Assert.IsTrue(sw.ToString().Contains("560,564.75"));

                _inventoryManager.UpdateProduct(
                    3, 
                    5000
                );

                _inventoryManager.GetTotalValue();

                // (25*24.99) + (20*1000) + (5000*60)
                Assert.IsTrue(sw.ToString().Contains("320,624.75"));


                _inventoryManager.RemoveProduct(1);
                _inventoryManager.RemoveProduct(2);
                _inventoryManager.RemoveProduct(3);

                _inventoryManager.ListProducts();

                // console output should contain "No products in here"
                Assert.IsTrue(sw.ToString().Contains("No products in here."));

            }
        }


    }
}