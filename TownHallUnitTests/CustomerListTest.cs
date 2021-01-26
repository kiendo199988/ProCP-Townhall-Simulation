using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TownHallUnitTests
{
    [TestClass]
    public class CustomerListTest
    {
        [TestMethod]
        public void GetAllCusInTRQ_2InTRQ_ReturnTrue()
        {
            TestAppTownHall.CustomersList customersList = new TestAppTownHall.CustomersList();
            TestAppTownHall.CircularPanel panel1 = new TestAppTownHall.CircularPanel();
            var cus1 = new TestAppTownHall.SimulatingCustomer(1, 2, 3, 4, 5, TestAppTownHall.CustomerType.TAX, panel1, TestAppTownHall.CustomerDirection.STEADY);
            cus1.Dir = TestAppTownHall.CustomerDirection.STEADY;
            TestAppTownHall.CircularPanel panel2 = new TestAppTownHall.CircularPanel();
            var cus2 = new TestAppTownHall.SimulatingCustomer(1, 2, 3, 4, 5, TestAppTownHall.CustomerType.ROOMRENTAL, panel2, TestAppTownHall.CustomerDirection.STEADY);
            TestAppTownHall.CircularPanel panel3 = new TestAppTownHall.CircularPanel();
            var cus3 = new TestAppTownHall.SimulatingCustomer(1, 2, 3, 4, 5, TestAppTownHall.CustomerType.TAX, panel3, TestAppTownHall.CustomerDirection.LEFT);

            customersList.AddCustomer(cus1,panel1);
            customersList.AddCustomer(cus2, panel2);
            customersList.AddCustomer(cus3, panel3);

            var nr = 0;
            var result = false;
            nr = customersList.GetCustomersInTRQueue().Count;
            

            if(nr == 0)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetCusPerType_2CusTR_ReturnTrue()
        {
            var nr = 0;
            var result = false;
            TestAppTownHall.CustomersList customersList = new TestAppTownHall.CustomersList();
            TestAppTownHall.CircularPanel panel1 = new TestAppTownHall.CircularPanel();
            var cus1 = new TestAppTownHall.SimulatingCustomer(1, 2, 3, 4, 5, TestAppTownHall.CustomerType.TAX, panel1, TestAppTownHall.CustomerDirection.STEADY);
            cus1.Dir = TestAppTownHall.CustomerDirection.STEADY;
            TestAppTownHall.CircularPanel panel2 = new TestAppTownHall.CircularPanel();
            var cus2 = new TestAppTownHall.SimulatingCustomer(1, 2, 3, 4, 5, TestAppTownHall.CustomerType.ROOMRENTAL, panel2, TestAppTownHall.CustomerDirection.STEADY);
            TestAppTownHall.CircularPanel panel3 = new TestAppTownHall.CircularPanel();
            var cus3 = new TestAppTownHall.SimulatingCustomer(1, 2, 3, 4, 5, TestAppTownHall.CustomerType.DRIVERLICENCSE, panel3, TestAppTownHall.CustomerDirection.LEFT);

            customersList.AddCustomer(cus1, panel1);
            customersList.AddCustomer(cus2, panel2);
            customersList.AddCustomer(cus3, panel3);


            nr = customersList.GetCustomersPerType(TestAppTownHall.CustomerType.TAX, TestAppTownHall.CustomerType.ROOMRENTAL).Count;

            if(nr == 2)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetCusInQueue_2CusBeforeTaxAndRoom_ReturnTrue()
        {
            
        }

        [TestMethod]
        public void CusToExit_ReturnTrue()
        {

        }

        [TestMethod]
        public void GetFirstQueue_ReturnTrue()
        {

        }

        [TestMethod]
        public void CusInAppoinment_ReturnTrue()
        {

        }
    }
}
