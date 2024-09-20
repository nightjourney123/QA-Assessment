using InventoryMgmt.Model;
using InventoryMgmt.Service;
using InventoryMgmt.Interface;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace InventoryMgmtQA.Service
{
    [TestClass]
    public class OperationManagerTest
    {
        private IInventoryManager _inventoryManager = new InventoryManager();
        private IOperationManager _operationManager = new OperationManager();


        [TestMethod]
        public void TestStartOperationAddProduct()
        {
            // simulate the input for adding a product
            using (StringReader sr = new StringReader("TestProduct\n10\n15.60"))
            using (StringWriter sw = new StringWriter())
            {
                // set input and output for the console
                Console.SetIn(sr);
                Console.SetOut(sw);

                // select option 1 for AddOperation
                _operationManager.StartOperation(1);

                // check if the console output shows the correct messages
                Assert.IsTrue(sw.ToString().Contains("Add a product"));
                Assert.IsTrue(sw.ToString().Contains("Name:"));
                Assert.IsTrue(sw.ToString().Contains("Quantity:"));
                Assert.IsTrue(sw.ToString().Contains("Price:"));
            }
        }

        [TestMethod]
        public void TestStartOperationRemoveProduct()
        {
            // simulate the addition of a product 
            _inventoryManager.AddNewProduct("TestProduct", 5, 10.00M);

            // simulate the input for removing the product
            using (StringReader sr = new StringReader("1"))
            using (StringWriter sw = new StringWriter())
            {
                
                Console.SetIn(sr);
                Console.SetOut(sw);

                // select option 2 for RemoveOperation
                _operationManager.StartOperation(2);

                // check if the console output shows the correct messages
                Assert.IsTrue(sw.ToString().Contains("Remove a product"));
                Assert.IsTrue(sw.ToString().Contains("Product ID:"));
                
            }
        }


        [TestMethod]
        public void TestStartOperationUpdateProduct()
        {
            _inventoryManager.AddNewProduct("TestProduct", 5, 10.00M);

            // Simulate console input for removing a product
            using (StringReader sr = new StringReader("2\n10"))
            using (StringWriter sw = new StringWriter())
            {
                // Arrange: Set input and output for the console
                Console.SetIn(sr);
                Console.SetOut(sw);

                // select option 3 for Update Operation
                _operationManager.StartOperation(3);

                // check if the console output shows the correct messages
                Assert.IsTrue(sw.ToString().Contains("Update a product"));
                Assert.IsTrue(sw.ToString().Contains("Product ID:"));
                Assert.IsTrue(sw.ToString().Contains("New quantity:"));

            }
        }

        [TestMethod]
        public void TestStartOperationInvalid()
        {
            // Expect an InvalidOperationException to be thrown
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                // Act: Start an invalid operation (operationId = 10 is invalid)
                _operationManager.StartOperation(10);
            });

            // Assert: Ensure the exception message contains the expected error message
            Assert.AreEqual("Invalid operation! Please try again.", exception.Message);

        }



    }
}
   
