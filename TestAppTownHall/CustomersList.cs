    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestAppTownHall
{
    public class CustomersList
    {
        private List<SimulatingCustomer> customers;
        //private List<SimulatingCustomer> customersInTRQueue;
        private List<CircularPanel> circularPanels;
        private CountersList counters;
        public CustomersList()
        {
            customers = new List<SimulatingCustomer>();
            circularPanels = new List<CircularPanel>();
            counters = new CountersList();
        }

        public void PaintAllCustomers(Graphics gr)
        {
            foreach (SimulatingCustomer sm in customers)
            {
                sm.PaintCustomer(gr);
            }
        }

        public void MoveAllCustomers()
        {
            foreach (SimulatingCustomer sm in this.customers)
            {
                sm.MovePanel();
            }
        }

        public void AddCustomer(SimulatingCustomer cus, CircularPanel cPanel)
        {
            this.customers.Add(cus);
            this.circularPanels.Add(cPanel);
        }

        public List<SimulatingCustomer> GetAllCustomers()
        {
            return this.customers;
        }
        public List<SimulatingCustomer> GetUnhandledCustomers(CustomerType t1, CustomerType t2)
        {
            List<SimulatingCustomer> temp = new List<SimulatingCustomer>();
            foreach(SimulatingCustomer c in GetCustomersPerType(t1, t2))
            {
                if (!c.FinishHandlingAppoitment)
                {
                    temp.Add(c);
                }
            }
            return temp;
        }

        //get list of people coming to the tax and room rental counter
        public List<SimulatingCustomer> GetCustomersInTRQueue()
        {
            List<SimulatingCustomer> returnList = new List<SimulatingCustomer>();

            foreach (SimulatingCustomer c in this.customers)
            {
                if (c.Dir == CustomerDirection.STEADY && c.Type == CustomerType.TAX && c.Type == CustomerType.ROOMRENTAL)
                {
                    returnList.Add(c);
                }
            }
            return returnList;
        }

        //get list of people coming to the driver license
        public List<SimulatingCustomer> GetCustomersInDriverLicenseQueue()
        {
            List<SimulatingCustomer> returnList = new List<SimulatingCustomer>();

            foreach (SimulatingCustomer c in this.customers)
            {
                if (c.Type == CustomerType.DRIVERLICENCSE)
                {
                    returnList.Add(c);
                }
            }
            return returnList;
        }


        public List<SimulatingCustomer> GetCustomersPerType(CustomerType t1, CustomerType t2)
        {
            List<SimulatingCustomer> cusWithType = new List<SimulatingCustomer>();
            foreach (SimulatingCustomer c in this.customers)
            {
                
                    if (c.Type == t1 || c.Type == t2)
                    {
                        cusWithType.Add(c);
                    }
                
            }
            return cusWithType;
        }

        private int index = 0;


        //public void SetCustomersInQueue(int steadyPoint)
        //{
        //     for(int i=0; i < this.customers.Count; i++)
        //     {
        //         if (i==0)
        //         {
        //             if(customers[0].CPanel.Location.Y == steadyPoint)
        //             customers[0].Dir = CustomerDirection.STEADY;

        //         }
        //         else
        //         {
        //             if (customers[i].CPanel.Location.Y == customers[i - 1].CPanel.Location.Y + 40)
        //             {
        //                 customers[i].Dir = CustomerDirection.STEADY;
        //             }
        //         }
        //     }
        // }

        //processing appointment
        public void ProcessingAppointment(Counter selectedCounter, CustomerType t1, CustomerType t2)
        {
            foreach (SimulatingCustomer cus in this.GetCustomersPerType(t1, t2))
            {
                SimulatingCustomer nextCus = customers[customers.IndexOf(cus) + 1];
                int currentPosition = cus.Y;

                foreach (CircularPanel c in circularPanels)
                {
                    if (cus.Dir == CustomerDirection.STEADY && selectedCounter.IsOccupied == false)
                    {
                        cus.Dir = CustomerDirection.UP;
                        selectedCounter.IsOccupied = true;
                    }
                    else if (cus.Dir == CustomerDirection.UP && c.Location.Y == selectedCounter.Y + 30)
                    {
                        cus.Dir = CustomerDirection.STEADY;
                        nextCus.Dir = CustomerDirection.UP;

                        if (nextCus.Y == currentPosition)
                        {
                            nextCus.Dir = CustomerDirection.STEADY;
                        }
                    }
                }
            }
        }

        //2 visitors have a distance between them
        public void SocialDistancingForTRQueue()
        {
            for (int i = 0; i < this.GetCustomersInTRQueue().Count - 1; i++)
            {
                this.GetCustomersInTRQueue()[i].Y = this.GetCustomersInTRQueue()[i + 1].Y + 65;
            }
        }

        public void SocialDistancingForDriverLicenseQueue()
        {
            for (int i = 0; i < this.GetCustomersInDriverLicenseQueue().Count - 1; i++)
            {
                this.GetCustomersInDriverLicenseQueue()[i].Y = this.GetCustomersInDriverLicenseQueue()[i + 1].Y + 65;
            }
        }

        public List<SimulatingCustomer> GetCustomersInQueue(Counter counter)
        {
            List<SimulatingCustomer> temp = new List<SimulatingCustomer>();
            foreach (SimulatingCustomer s in customers)
            {
                if (counter.CounterType == CounterType.TAXandROOMRENTAL)
                {
                    if (s.CPanel.Location.X > counter.X + 48 && s.StandingInTheQueue && (s.Type == CustomerType.TAX || s.Type == CustomerType.ROOMRENTAL))
                    {
                       
                            temp.Add(s);
                        
                        
                    }
                }
                else if (counter.CounterType == CounterType.DRIVERLICENSEandPASSPORTID)
                {
                    if (s.CPanel.Location.X > counter.X + 48 && s.StandingInTheQueue && (s.Type == CustomerType.DRIVERLICENCSE || s.Type == CustomerType.PASSPORTANDID))
                    {
                        temp.Add(s);
                    }
                }

            }
            return temp;
        }

        public void DirectCustomer(Counter selectedCounter, List<SimulatingCustomer> cusPerType)
        {
            //int steadyPForTR = 0;
            //foreach (SimulatingCustomer s in cusPerType)
            //{
            //    //steadyPForTR += 30;
            //    foreach (CircularPanel cP in circularPanels)
            //    {
            //        if (cusPerType.IndexOf(s) == 0)
            //        {
            //            if (s.FinishHandlingAppoitment == false && s.CPanel.Location.X >= 359
            //                && s.CPanel.Location.X <= selectedCounter.X + 48 && s.StandingInTheQueue == false)
            //            {
            //                //CPanel.Location = new Point(CPanel.Location.X + 1, CPanel.Location.Y);
            //                s.Dir = CustomerDirection.RIGHT;
            //            }
            //            else if (s.FinishHandlingAppoitment == false &&
            //                     s.CPanel.Location.Y == selectedCounter.Y + 65 &&
            //                     s.StandingInTheQueue == false)
            //            {

            //                s.Dir = CustomerDirection.STEADY;
            //                s.StandingInTheQueue = true;

            //            }

            //            else if (s.FinishHandlingAppoitment == false
            //               && s.CPanel.Location.X > selectedCounter.X + 48 && s.Dir != CustomerDirection.STEADY
            //               && s.StandingInTheQueue == false && s.CPanel.Location.Y >= selectedCounter.Y + 65)
            //            {
            //                s.Dir = CustomerDirection.UP;
            //            }
            //        }
            //        else
            //        {
            //            if (s.FinishHandlingAppoitment == false && s.CPanel.Location.X >= 359
            //        && s.CPanel.Location.X <= selectedCounter.X + 48 && s.StandingInTheQueue == false)
            //            {
            //                //CPanel.Location = new Point(CPanel.Location.X + 1, CPanel.Location.Y);
            //                s.Dir = CustomerDirection.RIGHT;
            //            }
            //            else if (s.FinishHandlingAppoitment == false &&
            //               s.CPanel.Location.Y == cusPerType[cusPerType.IndexOf(s) - 1].CPanel.Location.Y + 30 &&
            //                 s.StandingInTheQueue == false)
            //            {

            //                s.Dir = CustomerDirection.STEADY;
            //                s.StandingInTheQueue = true;

            //            }

            //            else if (s.FinishHandlingAppoitment == false
            //               && s.CPanel.Location.X > selectedCounter.X + 48 && s.Dir != CustomerDirection.STEADY
            //               && s.StandingInTheQueue == false)
            //            {
            //                s.Dir = CustomerDirection.UP;
            //            }
            //        }
            //    }

            int steadyPForTheRest = 0;
            selectedCounter.IsOpened = true;
            for (int i=0; i<cusPerType.Count;i++)
            {
                
                //steadyPForTR += 30;
                foreach (CircularPanel cP in circularPanels)
                {
                    if ((i % 3) == 0)
                    {
                        steadyPForTheRest = 0;
                        if (cusPerType[i].FinishHandlingAppoitment == false && cusPerType[i].CPanel.Location.X >= 359
                            && cusPerType[i].CPanel.Location.X <= selectedCounter.X + 48 && cusPerType[i].StandingInTheQueue == false)
                        {
                            //CPanel.Location = new Point(CPanel.Location.X + 1, CPanel.Location.Y);
                            cusPerType[i].Dir = CustomerDirection.RIGHT;
                        }
                        else if (cusPerType[i].FinishHandlingAppoitment == false &&
                           cusPerType[i].CPanel.Location.Y == selectedCounter.Y + 65 &&
                             cusPerType[i].StandingInTheQueue == false)
                        {

                            cusPerType[i].Dir = CustomerDirection.STEADY;
                            cusPerType[i].StandingInTheQueue = true;

                        }

                        else if (cusPerType[i].FinishHandlingAppoitment == false
                           && cusPerType[i].CPanel.Location.X > selectedCounter.X + 48 && cusPerType[i].Dir != CustomerDirection.STEADY
                           && cusPerType[i].StandingInTheQueue == false && cusPerType[i].CPanel.Location.Y >= selectedCounter.Y + 65)
                        {
                            cusPerType[i].Dir = CustomerDirection.UP;
                        }

                    }
                    else
                    {
                        for(int x=0; x < 3; x++)
                        {
                            if (cusPerType[i].FinishHandlingAppoitment == false && cusPerType[i].CPanel.Location.X >= 359
                                 && cusPerType[i].CPanel.Location.X <= selectedCounter.X + 48 && cusPerType[i].StandingInTheQueue == false)
                            {
                                //CPanel.Location = new Point(CPanel.Location.X + 1, CPanel.Location.Y);
                                cusPerType[i].Dir = CustomerDirection.RIGHT;
                            }
                            else if (cusPerType[i].FinishHandlingAppoitment == false &&
                               cusPerType[i].CPanel.Location.Y == cusPerType[i - 1].CPanel.Location.Y + 25 &&
                                 cusPerType[i].StandingInTheQueue == false)
                            {

                                cusPerType[i].Dir = CustomerDirection.STEADY;
                                cusPerType[i].StandingInTheQueue = true;

                            }

                            else if (cusPerType[i].FinishHandlingAppoitment == false
                               && cusPerType[i].CPanel.Location.X > selectedCounter.X + 48 && cusPerType[i].Dir != CustomerDirection.STEADY
                               && cusPerType[i].StandingInTheQueue == false && cusPerType[i].CPanel.Location.Y >= cusPerType[i - 1].CPanel.Location.Y+25)
                            {
                                cusPerType[i].Dir = CustomerDirection.UP;
                            }
                        }
                            
                        //     else if(i>=5 && i< cusPerType.Count - i)
                        //     {
                        //         List <Counter> countersByType= counters.GetCounterByType(CounterType.TAXandROOMRENTAL);
                        //         selectedCounter = counters.GetNextCounter(selectedCounter, countersByType);
                        //         if (cusPerType[i].FinishHandlingAppoitment == false && cusPerType[i].CPanel.Location.X >= 359
                        //&& cusPerType[i].CPanel.Location.X <= selectedCounter.X + 48 && cusPerType[i].StandingInTheQueue == false)
                        //         {
                        //            // CPanel.Location = new Point(CPanel.Location.X + 1, CPanel.Location.Y);
                        //             cusPerType[i].Dir = CustomerDirection.RIGHT;
                        //         }
                        //         else if (cusPerType[i].FinishHandlingAppoitment == false &&
                        //            cusPerType[i].CPanel.Location.Y == selectedCounter.Y + 65 + Math.Abs(30 - steadyPForTR) &&
                        //              cusPerType[i].StandingInTheQueue == false)
                        //         {

                        //             cusPerType[i].Dir = CustomerDirection.STEADY;
                        //             cusPerType[i].StandingInTheQueue = true;

                        //         }

                        //         else if (cusPerType[i].FinishHandlingAppoitment == false
                        //            && cusPerType[i].CPanel.Location.X > selectedCounter.X + 48 && cusPerType[i].Dir != CustomerDirection.STEADY
                        //            && cusPerType[i].StandingInTheQueue == false && cusPerType[i].CPanel.Location.Y >= selectedCounter.Y + 65 + Math.Abs(30 - steadyPForTR))
                        //         {
                        //             cusPerType[i].Dir = CustomerDirection.UP;
                        //         }


                        //     }

                    }
                }

            }
            
        }

        public void GoIntoTheCounter(Counter selectedCounter, List<SimulatingCustomer> cusPerType, int exit)
        {
            for(int i=0; i < cusPerType.Count; i++)
            {
                if ((i % 3) == 0)
                {
                    //if (cusPerType[i].CPanel.Location.Y == selectedCounter.Y + 30 && cusPerType[i].CPanel.Location.X != selectedCounter.X - 20 && cusPerType[i].FinishHandlingAppoitment==false)
                    if (cusPerType[i].CPanel.Location.Y == selectedCounter.Y + 30 && cusPerType[i].CPanel.Location.X != selectedCounter.Y-50)
                    {
                        //this.dir = CustomerDirection.STEADY;
                        selectedCounter.IsOccupied = true;
                        cusPerType[i].Dir = CustomerDirection.UP;
                        cusPerType[i].FinishHandlingAppoitment = true;
                        cusPerType[i].StandingInTheQueue = false;

                    }

                    //if (cusPerType[i].StandingInTheQueue == true && !selectedCounter.IsOccupied
                    //    && cusPerType[i].CPanel.Location.Y < selectedCounter.Y + 30 && !cusPerType[i].FinishHandlingAppoitment)
                    //{
                    //    cusPerType[i].Dir = CustomerDirection.UP;
                    //    selectedCounter.IsOccupied = true;
                    //    //if (selectedCounter.IsOccupied)
                    //    //{
                    //    //    if (CPanel.Location.Y < selectedCounter.Y + 100 + Math.Abs(steadyP2 - steadyP1))
                    //    //        this.dir = CustomerDirection.UP;
                    //    //    else if (CPanel.Location.Y == selectedCounter.Y + 100 + Math.Abs(steadyP2 - steadyP1))
                    //    //        this.dir = CustomerDirection.STEADY;
                    //    //}


                    //}
                    if (cusPerType[i].Dir == CustomerDirection.STEADY && selectedCounter.IsOpened)
                    {
                        cusPerType[i].Dir = CustomerDirection.UP;
                        selectedCounter.IsOccupied = true;
                    }

                    if (cusPerType[i].FinishHandlingAppoitment && cusPerType[i].CPanel.Location.Y == selectedCounter.Y - 50)
                    {
                        selectedCounter.IsOccupied = false;
                        cusPerType[i].Dir = CustomerDirection.RIGHT;
                    }


                    //if (cusPerType[i].FinishHandlingAppoitment && cusPerType[i].CPanel.Location.Y == selectedCounter.Y + 380
                    //    && cusPerType[i].CPanel.Location.X == selectedCounter.Y - 20)
                    //{
                    //    cusPerType[i].Dir = CustomerDirection.LEFT;

                    //}
                    if (cusPerType[i].CPanel.Location.X == exit)
                    {
                        cusPerType.Remove(cusPerType[i]);
                        //customers.Remove(cusPerType[i]);
                    }
                }
                else
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (cusPerType[i].CPanel.Location.Y == selectedCounter.Y + 30 && cusPerType[i].CPanel.Location.X != selectedCounter.Y - 50)
                        {
                            //this.dir = CustomerDirection.STEADY;
                            //Should be waiting here 
                            selectedCounter.IsOccupied = true;
                            cusPerType[i].Dir = CustomerDirection.UP;
                            cusPerType[i].FinishHandlingAppoitment = true;
                            cusPerType[i].StandingInTheQueue = false;

                        }
                        if (cusPerType[i].FinishHandlingAppoitment == true && cusPerType[i].CPanel.Location.Y == selectedCounter.Y - 50)
                        {
                            selectedCounter.IsOccupied = false;
                            cusPerType[i].Dir = CustomerDirection.RIGHT;
                        }
                        if (cusPerType[i].StandingInTheQueue == true && !selectedCounter.IsOccupied
                            && cusPerType[i].CPanel.Location.Y < selectedCounter.Y + 65 + cusPerType[i - 1].CPanel.Location.Y + 15 && !cusPerType[i].FinishHandlingAppoitment && selectedCounter.IsOpened)
                        {
                            cusPerType[i].Dir = CustomerDirection.UP;
                            selectedCounter.IsOccupied = true;
                            //if (selectedCounter.IsOccupied)
                            //{
                            //    if (CPanel.Location.Y < selectedCounter.Y + 100 + Math.Abs(steadyP2 - steadyP1))
                            //        this.dir = CustomerDirection.UP;
                            //    else if (CPanel.Location.Y == selectedCounter.Y + 100 + Math.Abs(steadyP2 - steadyP1))
                            //        this.dir = CustomerDirection.STEADY;
                            //}


                        }


                        //if (cusPerType[i].FinishHandlingAppoitment && cusPerType[i].CPanel.Location.Y == door + 70
                        //    && cusPerType[i].CPanel.Location.X == selectedCounter.X - 20)
                        //{
                        //    cusPerType[i].Dir = CustomerDirection.LEFT;

                        //}
                        if (cusPerType[i].CPanel.Location.X == exit)
                        {
                            cusPerType.Remove(cusPerType[i]);
                            //customers.Remove(cusPerType[i]);
                        }
                    }
                }
                
            }
            
        }

        public List<SimulatingCustomer> CusToExit(List<SimulatingCustomer> cusPerType, int exit, Counter selectedCounter)
        {
            List<SimulatingCustomer> temp = new List<SimulatingCustomer>();
            foreach(SimulatingCustomer s in cusPerType)
            {
                if (cusPerType.IndexOf(s) == 0)
                {
                    if (s.FinishHandlingAppoitment && s.CPanel.Location.X == exit && s.Dir == CustomerDirection.RIGHT)
                        temp.Add(s);
                }

                else 
                {
                    if (s.FinishHandlingAppoitment && s.CPanel.Location.Y == exit && s.Dir == CustomerDirection.RIGHT)
                        temp.Add(s);
                }
            }
            return temp;
        }

        public List<SimulatingCustomer> CusInAppoinment(List<SimulatingCustomer> cusPerType, Counter selectedCounter)
        {
            List<SimulatingCustomer> temp = new List<SimulatingCustomer>();
            foreach(SimulatingCustomer s in cusPerType)
            {
                if(s.CPanel.Location.Y == selectedCounter.Y + 30 && s.CPanel.Location.X != selectedCounter.X)
                {
                    temp.Add(s);
                }
            }
            return temp;
        }


        public List<CircularPanel> GetCircularPanels()
        {
            return this.circularPanels;
        }

        public SimulatingCustomer GetFirstInQueue(Counter selected)
        {
            if (GetCustomersInQueue(selected).Count == 0) { return null; }
            else
            {
                SimulatingCustomer s = GetCustomersInQueue(selected)[0];
                GetCustomersInQueue(selected).RemoveAt(0);
                return s;
            }

        }

        public void Relocate(Counter selectedCounter)
        {
            SimulatingCustomer first = this.GetFirstInQueue(selectedCounter);
            if (first != null)
            {
                if(first.FinishHandlingAppoitment == false &&
                           first.CPanel.Location.Y >= selectedCounter.Y + 65 && first.StandingInTheQueue)
                {
                    first.Dir = CustomerDirection.UP;
                }
                else if (first.FinishHandlingAppoitment == false &&
                           first.CPanel.Location.Y == selectedCounter.Y + 65 && first.StandingInTheQueue)
                {
                    first.Dir = CustomerDirection.STEADY;
                }
            }
        }
    }
}
