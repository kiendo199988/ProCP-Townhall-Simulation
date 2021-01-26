using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TownHallUnitTests
{
    [TestClass]
    public class CounterListTest
    {
        [TestMethod]
        public void GetCounterById_2CountersAreOpening_ReturnTrue()
        {
            //arrange
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter2 = new TestAppTownHall.Counter(2, 500, 500, TestAppTownHall.CounterType.GENERAL, 20);
            var counterList = new TestAppTownHall.CountersList();
            counterList.AddCounter(counter1);
            counterList.AddCounter(counter2);

            //act
            var result = false;
            if(counter1 == counterList.GetCounter(1))
            {
                result = true;
            }

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OpenCounter_CounterIsClosed_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            counter1.IsOpened = false;
            counterList.AddCounter(counter1);
            var result = counterList.OpenCounter(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OpenCounter_CounterIsOpened_ReturnFalse()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            counter1.IsOpened = true;
            counterList.AddCounter(counter1);
            var result = counterList.OpenCounter(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CloseCounter_CounterIsOpened_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            counter1.IsOpened = true;
            counterList.AddCounter(counter1);
            var result = counterList.CloseCounter(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CloseCounter_CounterIsClosed_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            counter1.IsOpened = false;
            counterList.AddCounter(counter1);
            var result = counterList.CloseCounter(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SetProcessingTime_TimeIs50_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter2 = new TestAppTownHall.Counter(2, 500, 500, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            counterList.AddCounter(counter1);
            counterList.AddCounter(counter2);
            counterList.SetProcessingTime(50,TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID);

            var result = false;
            if(counter1.ProcessingTime == 50 && counter2.ProcessingTime == 50)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetCounterByType_2MembersAreDP_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter2 = new TestAppTownHall.Counter(2, 500, 500, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter3 = new TestAppTownHall.Counter(2, 500, 500, TestAppTownHall.CounterType.GENERAL, 23);
            counterList.AddCounter(counter1);
            counterList.AddCounter(counter2);
            counterList.AddCounter(counter3);

            var nr = 0;
            var result = false;

            foreach (TestAppTownHall.Counter c in counterList.GetCounterByType(TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID))
            {
                nr++;
            }
            if (nr == 2)
            {
                result = true;
            }
           
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetOpeningCounters_2MembersAreOpened_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter2 = new TestAppTownHall.Counter(2, 500, 500, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter3 = new TestAppTownHall.Counter(3, 500, 500, TestAppTownHall.CounterType.GENERAL, 23);
            counter1.IsOpened = false;
            counter2.IsOpened = true;
            counter3.IsOpened = true;
            counterList.AddCounter(counter1);
            counterList.AddCounter(counter2);
            counterList.AddCounter(counter3);

            var nr = 0;
            var result = false;

            foreach (TestAppTownHall.Counter c in counterList.GetOpeningCounters())
            {
                nr++;
            }
            if (nr == 2)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetNextCounter_nextIdIs3_ReturnTrue()
        {
            var counterList = new TestAppTownHall.CountersList();
            var counter1 = new TestAppTownHall.Counter(1, 233, 233, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter2 = new TestAppTownHall.Counter(2, 500, 500, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            var counter3 = new TestAppTownHall.Counter(3, 500, 500, TestAppTownHall.CounterType.DRIVERLICENSEandPASSPORTID, 23);
            counterList.AddCounter(counter1);
            counterList.AddCounter(counter2);
            counterList.AddCounter(counter3);

            var result = false;

            if (counter1 == counterList.GetNextCounter(counter3, counterList.GetAllCounters()))
            {
                result = true;
            }
            
            

            Assert.IsTrue(result);
        }

    }
}
