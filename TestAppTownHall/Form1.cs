using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;


namespace TestAppTownHall
{
    public partial class Form1 : Form
    {
        private CustomersList customers;
        //private List<CircularPanel> circularPanels;
        private List<Panel> panels;
        private CountersList counters;
        private List<Label> countersLabels;
        private int waitingTime=0;
        private static int id = 0;
        private static Clock clock;
        private static Counter nextCounter;

        private static double totalTRHandlingTime;
        private static double totalDPHandlingTime;

        private Counter current;
        private Counter current2;

        //variables for the charts and data table
        private int totalTRWaitingTime;
        private int totalDPWaitingTime;
        private int totalTRVisitorsHandled;
        private int totalDPVisitorsHandled;

        public int TotalTRVisitorsHandled { get { return this.totalTRVisitorsHandled; } set { this.totalTRVisitorsHandled = value; } }
        public int TotalDPVisitorsHandled { get { return this.totalDPVisitorsHandled; } set { this.totalDPVisitorsHandled = value; } }

        public int TotalTRWaitingTime { get { return this.totalTRWaitingTime; } set { this.totalTRWaitingTime = value; } }
        public int TotalDPWaitingTime { get { return this.totalDPWaitingTime; } set { this.totalDPWaitingTime = value; } }
        
        public Form1()
        {
            InitializeComponent();
            //this.timerBlinking.Enabled = true;
            //this.timerStart.Start();
            //If this one triggered error, add pictures to .../bin/Debug/
            this.pbxDoor.Image = Image.FromFile("Doors.jpg");
            this.pbxChairs1.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs2.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs3.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs4.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs5.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs6.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs7.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs8.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs9.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs10.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs11.Image = Image.FromFile("chairs.jpg");
            this.pbxChairs12.Image = Image.FromFile("chairs.jpg");
            this.pbxAppointmentRoom.Image = Image.FromFile("appointmentRoom3.png");
            this.pbxAppointmentRoom.BackColor = Color.Transparent;
            this.pbxExit.Image = Image.FromFile("exitSymbol.jpg");
            this.pbxExit.Location = new Point(1465, 0);
            this.timerStart.Enabled = false;

            //
            this.WindowState = FormWindowState.Maximized;
            

            //this.cbxCountersTypes.Items.Add(CounterType.GENERAL);
            this.cbxCountersTypes.Items.Add(CounterType.TAXandROOMRENTAL);
            this.cbxCountersTypes.Items.Add(CounterType.DRIVERLICENSEandPASSPORTID);
            this.cbxCountersTypes.Items.Add(CounterType.DRIVERLICENSEandPASSPORTID);

            this.quantityBox.Maximum = 5;

            customers = new CustomersList();
            //circularPanels = new List<CircularPanel>();
            panels = new List<Panel>();
            counters = new CountersList();
            countersLabels = new List<Label>();
            //this.timerStart.Interval = 16;
            //this.circularPanel1.Visible = false;

            
            

            //limit for numeric box hour, minute, second
            this.numericHour.Maximum = 16;
            this.numericHour.Minimum = 8;
            this.numericMinute.Maximum = 59;
            this.numericSecond.Maximum = 59;



            this.AddDefault();

            //create new clock
            clock = new Clock(8, 00, 00);
            Time();

            this.TotalTRVisitorsHandled = 0;
            this.TotalDPVisitorsHandled = 0;

            this.TotalTRWaitingTime = 0;
            this.TotalDPWaitingTime = 0;

            this.btnLoadChartAndTable.Visible = false;

            current = counters.GetCounterByType(CounterType.TAXandROOMRENTAL)[0];
            current2 = counters.GetCounterByType(CounterType.DRIVERLICENSEandPASSPORTID)[0];


            this.timerStart.Enabled = true;

            this.cbxTaxPerSec.Items.Add(5);
            this.cbxTaxPerSec.Items.Add(10);
            this.cbxTaxPerSec.Items.Add(15);

            this.cbxDLPerSec.Items.Add(5);
            this.cbxDLPerSec.Items.Add(10);
            this.cbxDLPerSec.Items.Add(15);

            this.cbxPIDPerSec.Items.Add(5);
            this.cbxPIDPerSec.Items.Add(10);
            this.cbxPIDPerSec.Items.Add(15);

            this.cbxRoomPerSec.Items.Add(5);
            this.cbxRoomPerSec.Items.Add(10);
            this.cbxRoomPerSec.Items.Add(15);

            this.panTax.BackColor = Color.BlueViolet;
            this.panDL.BackColor = Color.LightPink;
            this.panPID.BackColor = Color.LightGray;
            this.panRoom.BackColor = Color.Maroon;

            this.panTRCounter.BackColor = Color.FromArgb(255, 153, 51);
            this.panDPCounter.BackColor = Color.FromArgb(255, 51, 153);

        }

        //Method for running the clock 
        public async void Time()
        {
            while (true)
            {
                if (clock.Hour >= 8 && clock.Hour <= 16)
                {
                    clock.Tick();
                    lblClock.Text = clock.displayTime();
                    await Task.Delay(1);
                }
                else
                {
                    clock.Hour = 8;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //this.customers.PaintAllCustomers(e.Graphics);
            //this.counters.PaintAllCounters(e.Graphics);
            
        }

        private void button1_Click(object sender, EventArgs e) //testButton
        {
            //CustomerType cType;
            //SimulatingCustomer nwCus;
            //Panel nwPanel = new Panel();
            //nwPanel.BackColor = Color.BlueViolet;
            //nwPanel.Size = new Size(21, 21);
            //this.Controls.Add(nwPanel);
            //this.buttons.Add(nwPanel);
            

            //if (this.comboBox1.SelectedIndex == 0) { cType = CustomerType.TAX; }
            //else if (this.comboBox1.SelectedIndex == 1) { cType = CustomerType.DRIVERLICENCSE; }
            //else if (this.comboBox1.SelectedIndex == 2) { cType = CustomerType.PASSPORTANDID; }
            //else { cType = CustomerType.ROOMRENTAL; }

            //nwCus = new SimulatingCustomer(800, 300, 20, -30, 50, cType);
            //this.customers.AddCustomer(nwCus);
            //this.circularPanel1.Paint += new PaintEventHandler(circularPanel1_Paint);
            //this.circularPanel1.Refresh();
            //this.circularPanel1.Visible = true;
            //this.timerStart.Start();
            //this.Invalidate();
        }


        private void listBox2_Paint(object sender, PaintEventArgs e)
        {
            this.customers.PaintAllCustomers(e.Graphics);
        }

        private void panel1_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.customers.PaintAllCustomers(e.Graphics);
        }

        private void circularPanel1_Paint(object sender, PaintEventArgs e)
        {
            //this.customers.PaintAllCustomers(e.Graphics);
        }

        private void btnSpawnTax_Click(object sender, EventArgs e) //real one
        {
            // MessageBox.Show((Convert.ToInt32(this.cbxTaxPerSec.SelectedItem) * 1000).ToString());
            if (this.customers.GetAllCustomers().Count < 10)
            {
                if (this.cbxTaxPerSec.SelectedIndex >= 0)
                {
                    Delayed(Convert.ToInt32(this.cbxTaxPerSec.SelectedItem) * 2000, () => btnSpawnTax_Click(sender, e));

                }

                this.btnSpawnTax.Enabled = false;

                Random ran = new Random();


                int x = ran.Next(pbxDoor.Location.X + 30, pbxDoor.Location.X + 100);
                int y = ran.Next(pbxDoor.Location.Y + 50, pbxDoor.Location.Y + 70);
                SimulatingCustomer nwCus;
                CircularPanel nwPan = new CircularPanel();

                nwCus = new SimulatingCustomer(800, 300, 20, -30, 50, CustomerType.TAX, nwPan, CustomerDirection.RIGHT);
                this.SetBackColor(nwCus, nwPan);
                nwPan.Location = new Point(x, y);

                //this.circularPanels.Add(nwPan);

                this.customers.AddCustomer(nwCus, nwPan);
                this.Controls.Add(nwPan);
                nwPan.BringToFront();

                TotalTRVisitorsHandled = this.customers.GetCustomersPerType(CustomerType.TAX, CustomerType.ROOMRENTAL).Count();

                this.btnLoadChartAndTable_Click(sender, e);


                this.Invalidate();
                this.timerStart.Start();
            }
            
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            this.Moving();
            waitingTime++;
            


            //this.timerStart.Stop();
            //this.customers.MoveAllCustomers();

        }

        private void btnSpawnDriverLicense_Click(object sender, EventArgs e)
        {
            if (this.customers.GetAllCustomers().Count < 10)
            {
                if (this.cbxDLPerSec.SelectedIndex >= 0)
                {
                    Delayed(Convert.ToInt32(this.cbxDLPerSec.SelectedItem) * 1000, () => btnSpawnDriverLicense_Click(sender, e));

                }

                this.btnSpawnDriverLicense.Enabled = false;
                Random ran = new Random();


                int x = ran.Next(pbxDoor.Location.X + 30, pbxDoor.Location.X + 100);
                int y = ran.Next(pbxDoor.Location.Y + 50, pbxDoor.Location.Y + 70);
                SimulatingCustomer nwCus;
                CircularPanel nwPan = new CircularPanel();
                nwCus = new SimulatingCustomer(800, 300, 20, -30, 50, CustomerType.DRIVERLICENCSE, nwPan, CustomerDirection.RIGHT);
                this.SetBackColor(nwCus, nwPan);
                nwPan.Location = new Point(x, y);

                //this.circularPanels.Add(nwPan);
                this.Controls.Add(nwPan);
                nwPan.BringToFront();

                this.customers.AddCustomer(nwCus, nwPan);

                TotalDPVisitorsHandled = this.customers.GetCustomersPerType(CustomerType.DRIVERLICENCSE, CustomerType.PASSPORTANDID).Count();
                this.btnLoadChartAndTable_Click(sender, e);



                //foreach (CircularPanel p in circularPanels)
                //{
                //    this.Controls.Add(p);
                //    p.BringToFront();
                //}
                //MessageBox.Show(customers.GetAllCustomers().Count().ToString());
                //this.Moving();
                this.Invalidate();
                this.timerStart.Start();
            }
        }

        private void btnSpawnPassID_Click(object sender, EventArgs e)
        {
            if (this.customers.GetAllCustomers().Count < 10)
            {
                if (this.cbxPIDPerSec.SelectedIndex >= 0)
                {
                    Delayed(Convert.ToInt32(this.cbxPIDPerSec.SelectedItem) * 1000, () => btnSpawnPassID_Click(sender, e));

                }

                this.btnSpawnPassID.Enabled = false;
                Random ran = new Random();


                int x = ran.Next(pbxDoor.Location.X + 30, pbxDoor.Location.X + 100);
                int y = ran.Next(pbxDoor.Location.Y + 50, pbxDoor.Location.Y + 70);
                SimulatingCustomer nwCus;
                CircularPanel nwPan = new CircularPanel();

                nwCus = new SimulatingCustomer(800, 300, 20, -30, 50, CustomerType.PASSPORTANDID, nwPan, CustomerDirection.RIGHT);
                this.SetBackColor(nwCus, nwPan);
                nwPan.Location = new Point(x, y);

                //this.circularPanels.Add(nwPan);
                this.Controls.Add(nwPan);
                nwPan.BringToFront();

                this.customers.AddCustomer(nwCus, nwPan);

                TotalDPVisitorsHandled = this.customers.GetCustomersPerType(CustomerType.DRIVERLICENCSE, CustomerType.PASSPORTANDID).Count();
                this.btnLoadChartAndTable_Click(sender, e);


                this.Invalidate();
                this.timerStart.Start();
            }
        }

        private void btnSpawnRental_Click(object sender, EventArgs e)
        {
            if (this.customers.GetAllCustomers().Count < 10)
            {
                if (this.cbxRoomPerSec.SelectedIndex >= 0)
                {
                    Delayed(Convert.ToInt32(this.cbxRoomPerSec.SelectedItem) * 1000, () => btnSpawnRental_Click(sender, e));
                }

                this.btnSpawnRental.Enabled = false;
                Random ran = new Random();


                int x = ran.Next(pbxDoor.Location.X + 30, pbxDoor.Location.X + 100);
                int y = ran.Next(pbxDoor.Location.Y + 50, pbxDoor.Location.Y + 70);
                SimulatingCustomer nwCus;
                CircularPanel nwPan = new CircularPanel();

                nwCus = new SimulatingCustomer(800, 300, 20, -30, 50, CustomerType.ROOMRENTAL, nwPan, CustomerDirection.RIGHT);
                this.SetBackColor(nwCus, nwPan);
                nwPan.Location = new Point(x, y);

                //this.circularPanels.Add(nwPan);
                this.Controls.Add(nwPan);
                nwPan.BringToFront();

                this.customers.AddCustomer(nwCus, nwPan);

                TotalTRVisitorsHandled = this.customers.GetCustomersPerType(CustomerType.TAX, CustomerType.ROOMRENTAL).Count();
                this.btnLoadChartAndTable_Click(sender, e);


                this.Invalidate();
                this.timerStart.Start();
            }
        }

       
        private void openCounterBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void pbxCounter1_Click(object sender, EventArgs e)
        {

        }

        private void openTaxCounterBtn_Click(object sender, EventArgs e)
        {
            this.timerBlinking.Start();
            if (this.cbxCounterId.SelectedIndex >= 0)
            {
                int counterId = this.cbxCounterId.SelectedIndex + 1;
                if (clock.Hour >= 13 && clock.Hour < 17)
                {
                    if (counters.GetOpeningCounters().Count() < 3)
                    {
                        if (counters.OpenCounter(counterId))
                        {
                            MessageBox.Show("Successfully opened!");
                        }
                        else { MessageBox.Show("This counter is already opened!"); }
                    }
                    else
                    {
                        MessageBox.Show("Maximum number of counters can be opened in the afternoon is 3");
                    }
                }
                else if (clock.Hour >= 12 && clock.Hour < 13)
                {
                    if (counters.GetOpeningCounters().Count() >= 0)
                    {
                        MessageBox.Show("Cannot open a counter during break time");
                    }
                }
                else
                {
                    if (counters.OpenCounter(counterId))
                    {
                        MessageBox.Show("Successfully opened!");
                    }
                    else { MessageBox.Show("This counter is already opened!"); }
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void createNewCounterBtn_Click(object sender, EventArgs e)
        {
            //CounterType type;
            //double processingTime = 0;
            //if (this.cbxCountersTypes.SelectedIndex >= 0)
            //{

            //    if (this.cbxCountersTypes.SelectedIndex == 0)
            //    {
            //        type = CounterType.GENERAL;
            //    }
            //    else if (this.cbxCountersTypes.SelectedIndex == 1)
            //    {
            //        type = CounterType.TAXandROOMRENTAL;
            //    }
            //    else { type = CounterType.DRIVERLICENSEandPASSPORTID; }

            //    if (tbProcessingTime.Text != "")
            //    {
            //        processingTime = Convert.ToDouble(this.tbProcessingTime.Text);
            //    }
            //    else MessageBox.Show("Please enter processing time!");
            //    if ((this.quantityBox.Value + this.counters.GetAllCounters().Count) <= 5)
            //    {
            //        for(int i=0; i< this.quantityBox.Value; i++)
            //        {
            //            id++;
            //            MovePanelToLeft();
            //            Label nwLbl = new Label();
            //            Counter nwCounter = new Counter(id,this.GetLastPanelX() + 208, 74, type, processingTime);
            //            Panel nwPanel = new Panel();
            //            nwPanel.Size = new Size(108, 50);
            //            nwPanel.Location = new Point(nwCounter.X, nwCounter.Y);
            //            this.SetCounterColor(nwCounter, nwPanel);
            //            nwLbl.Location = new Point(nwPanel.Location.X + 23, 51);
            //            nwLbl.Text = $"Counter {id}";
            //            //-Time: { processingTime}                     
            //            nwLbl.Font = new Font("Microsoft Sans Serif", 10);
            //            nwLbl.BackColor = Color.Pink;
            //            nwLbl.AutoSize = true;
                        
            //            //processing time

                        

            //            this.counters.AddCounter(nwCounter);
            //            this.Controls.Add(nwPanel);
            //            this.Controls.Add(nwLbl);
            //            this.panels.Add(nwPanel);
            //            this.countersLabels.Add(nwLbl);

            //            foreach (Panel p in panels)
            //            {
            //                p.BringToFront();
            //            }

            //            foreach(Label lbl in countersLabels)
            //            {
            //                lbl.BringToFront();
            //            }

            //            this.AddIdToCombobox();
            //            this.Invalidate();
            //        }
                   
            //    }
            //    else { MessageBox.Show("The maximum of counters is 5! You cannot create more!"); }
                

            //}
            //else { MessageBox.Show("Please select type of counter you want to create!"); }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void numericTaxUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void closeTaxCounterBtn_Click(object sender, EventArgs e)
        {
            this.timerBlinking.Start();
            if (this.cbxCounterId.SelectedIndex >= 0)
            {
                int counterId = this.cbxCounterId.SelectedIndex+1;
                if (counters.CloseCounter(counterId))
                {
                    panels[counterId - 1].BackColor = Color.DarkGray;
                    MessageBox.Show("Successfully closed!");
                }
                else { MessageBox.Show("This counter is already closed!"); }
            }
        }

        private void SetBackColor(SimulatingCustomer c, CircularPanel p)
        {
            if (c.Type == CustomerType.TAX)
            {
                p.BackColor = Color.BlueViolet;
            }
            else if (c.Type == CustomerType.DRIVERLICENCSE)
            {
                p.BackColor = Color.LightPink;
            }
            else if (c.Type == CustomerType.PASSPORTANDID)
            {
                p.BackColor = Color.LightGray;
            }
            else
            {
                p.BackColor = Color.Maroon;
            }
        }

        private int GetLastPanelX()
        {
            Panel lastPanel = panels[panels.Count - 1];
            
            return lastPanel.Location.X;
        }


        //process a new appointment
        private void GoIntoCounter(CustomersList steadyCustomers)
        {
            //if (this.timerProcessTax.Enabled)
            //{
                //timerProcessTax.Start();
                Counter selectedCounter;

                List<CustomerType> types = new List<CustomerType>();
            //List<SimulatingCustomer> movingTRCus = this.customers.GetCustomersPerType(CustomerType.TAX, CustomerType.ROOMRENTAL);
            //List<SimulatingCustomer> movingDPCus = this.customers.GetCustomersPerType(CustomerType.DRIVERLICENCSE, CustomerType.PASSPORTANDID);
            //List<SimulatingCustomer> movingGCus = this.customers.GetCustomersPerType(CustomerType.TAX, CustomerType.TAX);

                List<SimulatingCustomer> movingTRCus = steadyCustomers.GetCustomersPerType(CustomerType.TAX, CustomerType.ROOMRENTAL);
                List<SimulatingCustomer> movingDPCus = steadyCustomers.GetCustomersPerType(CustomerType.DRIVERLICENCSE, CustomerType.PASSPORTANDID);
                List<SimulatingCustomer> movingGCus = steadyCustomers.GetCustomersPerType(CustomerType.TAX, CustomerType.TAX);
            this.customers.MoveAllCustomers();


            //foreach (SimulatingCustomer cus in movingTRCus)
            //{
            //    selectedCounter = counters.GetCounter(counters.GetSmallestCounterIdInAType(CounterType.TAXandROOMRENTAL));

            //    foreach (CircularPanel c in circularPanels)
            //    {
            //        if (selectedCounter.IsOccupied == false)
            //        {
            //            cus.GoIntoTheCounter(selectedCounter);
            //        }
            //    }
            //    selectedCounter.IsOccupied = true;

            //}

            //foreach (SimulatingCustomer cus in movingDPCus)
            //{
            //    selectedCounter = counters.GetCounter(counters.GetSmallestCounterIdInAType(CounterType.DRIVERLICENSEandPASSPORTID));
            //    foreach (CircularPanel c in circularPanels)
            //    {
            //        if (selectedCounter.IsOccupied == false)
            //        {
            //            cus.GoIntoTheCounter(selectedCounter);

            //        }
            //    }
            //    selectedCounter.IsOccupied = true;
            //}
            //this.Invalidate();
        }



        private int i = 0;
        private void Moving()
        {
            if (timerStart.Enabled)
            {
                this.timerStart.Start();
                

                int steadyPForTR = 0;
                int steadyPForDP = 0;
                int steadyPForG = 0;
                List<CustomerType> types = new List<CustomerType>();
                List<SimulatingCustomer> movingTRCus1 = this.customers.GetCustomersPerType(CustomerType.TAX, CustomerType.ROOMRENTAL);
                List<SimulatingCustomer> movingTRCus2 = new List<SimulatingCustomer>();
                List<SimulatingCustomer> movingDPCus = this.customers.GetCustomersPerType(CustomerType.DRIVERLICENCSE, CustomerType.PASSPORTANDID);
                List<SimulatingCustomer> movingGCus = this.customers.GetCustomersPerType(CustomerType.TAX, CustomerType.TAX);
                //selectedCounter = counters.GetCounter(counters.GetSmallestCounterIdInAType(CounterType.TAXandROOMRENTAL));
                //selectedCounter2 = counters.GetCounter(counters.GetSmallestCounterIdInAType(CounterType.DRIVERLICENSEandPASSPORTID));

                this.customers.MoveAllCustomers();
                List<Counter> countersWithTRType = counters.GetCounterByType(CounterType.TAXandROOMRENTAL);
                List<Counter> countersWithDPType = counters.GetCounterByType(CounterType.DRIVERLICENSEandPASSPORTID);
                //int x = 4;
                Counter nextTR1 = current;
                Counter nextTR2 = current;

                Counter nextDP1 = current2;
                Counter nextDP2 = current2;

                if (customers.GetCustomersInQueue(current).Count < 2)
                {
                    nextTR1 = current;
                    nextTR2 = current;
                    this.customers.DirectCustomer(current, movingTRCus1);
                    this.customers.GoIntoTheCounter(current, movingTRCus1, pbxExit.Location.X);
                }
                else if(customers.GetCustomersInQueue(current).Count>=2)
                {
                    nextTR1 = counters.GetNextCounter(current, countersWithTRType);
                    current = nextTR1;
                    nextTR2 = nextTR1;
                        if (customers.GetCustomersInQueue(nextTR1).Count < 2)
                        {
                            current = nextTR2;
                            nextTR1 = nextTR2;
                            this.customers.DirectCustomer(nextTR1, movingTRCus1);
                            this.customers.GoIntoTheCounter(nextTR1, movingTRCus1, pbxExit.Location.X);
                        }
                        else if (customers.GetCustomersInQueue(nextTR1).Count >= 2)
                        {
                            nextTR2 = counters.GetCounterByType(CounterType.GENERAL)[0];
                            this.customers.DirectCustomer(nextTR2, movingTRCus1);
                            this.customers.GoIntoTheCounter(nextTR2, movingTRCus1, pbxExit.Location.X);
                        }
                    //}
                    //else
                    //{
                    //    next = counters.GetCounterByType(CounterType.GENERAL)[0];
                    //    this.customers.DirectCustomer(next, movingTRCus1);
                    //    this.customers.GoIntoTheCounter(next, movingTRCus1, pbxExit.Location.X);
                    //}
                  
                    
                   
                }
                //else if(customers.GetUnhandledCustomers(CustomerType.TAX,CustomerType.ROOMRENTAL).Count>=10)
                //{
                //    Counter next = counters.GetCounterByType(CounterType.GENERAL)[0];
                //    this.customers.DirectCustomer(next, movingTRCus1);
                //    this.customers.GoIntoTheCounter(next, movingTRCus1, pbxExit.Location.X);
                //}

                if (customers.GetCustomersInQueue(current2).Count < 2)
                {
                    nextDP1 = current2;
                    nextDP2 = current2;
                    this.customers.DirectCustomer(current2, movingDPCus);
                    this.customers.GoIntoTheCounter(current2, movingDPCus, pbxExit.Location.X-30);

                }
                else if(customers.GetCustomersInQueue(current2).Count >= 2)
                {
                    nextDP1 = counters.GetNextCounter(current2, countersWithDPType);
                    if (customers.GetCustomersInQueue(nextDP1).Count < 2)
                    {
                        current2 = nextDP1;
                        nextDP2 = nextDP1;
                        this.customers.DirectCustomer(nextDP1, movingDPCus);
                        this.customers.GoIntoTheCounter(nextDP1, movingDPCus, pbxExit.Location.X);
                    }
                    else if (customers.GetCustomersInQueue(nextDP1).Count >= 2)
                    {
                        nextDP2 = counters.GetCounterByType(CounterType.GENERAL)[0];
                        
                        this.customers.DirectCustomer(nextTR2, movingTRCus1);
                        this.customers.GoIntoTheCounter(nextTR2, movingTRCus1, pbxExit.Location.X);
                    }
                    
                }
                //else if (customers.GetUnhandledCustomers(CustomerType.DRIVERLICENCSE, CustomerType.PASSPORTANDID).Count >= 10)
                //{
                //    Counter next = counters.GetCounterByType(CounterType.GENERAL)[0];
                //    this.customers.DirectCustomer(next, movingDPCus);
                //    this.customers.GoIntoTheCounter(next, movingDPCus, pbxExit.Location.X);
                //}


                foreach (SimulatingCustomer s in customers.CusToExit(movingTRCus1,pbxExit.Location.X-30,current))
                {
                    s.CPanel.SendToBack();
                    movingTRCus1.Remove(s);
                    customers.GetAllCustomers().Remove(s);
                }

                foreach (SimulatingCustomer s in customers.CusToExit(movingDPCus, pbxExit.Location.X-30, current2))
                {
                    s.CPanel.SendToBack();
                    movingDPCus.Remove(s);
                    customers.GetAllCustomers().Remove(s);
                }

                foreach(Counter c in counters.GetAllCounters())
                {
                    if (c.IsOccupied)
                    {
                        panels[this.counters.GetAllCounters().IndexOf(c)].BackColor = Color.Red;
                    }
                    else
                    {
                        this.SetCounterColor(c, panels[this.counters.GetAllCounters().IndexOf(c)]);
                    }
                }

                //int count = 0;
                //count++;
                if (current != null)
                {
                    foreach (SimulatingCustomer s in customers.CusInAppoinment(movingTRCus1, current))
                    {
                        s.Dir = CustomerDirection.STEADY;

                        Delayed(Convert.ToInt32(current.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(current, movingTRCus1, pbxExit.Location.X - 30));
                    }
                }
                else if (nextTR1 != null)
                {
                    foreach (SimulatingCustomer s in customers.CusInAppoinment(movingTRCus1, nextTR1))
                    {
                        s.Dir = CustomerDirection.STEADY;

                        Delayed(Convert.ToInt32(nextTR1.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(nextTR1, movingTRCus1, pbxExit.Location.X - 30));
                    }
                }
                else if (nextTR2 != null)
                {
                    foreach (SimulatingCustomer s in customers.CusInAppoinment(movingTRCus1, nextTR2))
                    {
                        s.Dir = CustomerDirection.STEADY;

                        Delayed(Convert.ToInt32(nextTR2.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(nextTR2, movingTRCus1, pbxExit.Location.X - 30));
                    }
                }

                if (current2 != null)
                {
                    foreach (SimulatingCustomer s in customers.CusInAppoinment(movingDPCus, current2))
                    {
                        s.Dir = CustomerDirection.STEADY;

                        Delayed(Convert.ToInt32(current2.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(current2, movingDPCus, pbxExit.Location.X - 30));
                    }
                }
                else if (nextDP1 != null)
                {
                    foreach (SimulatingCustomer s in customers.CusInAppoinment(movingDPCus, nextDP1))
                    {
                        s.Dir = CustomerDirection.STEADY;

                        Delayed(Convert.ToInt32(nextDP1.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(nextDP1, movingDPCus, pbxExit.Location.X - 30));
                    }
                }
                else if (nextDP2 != null)
                {
                    foreach (SimulatingCustomer s in customers.CusInAppoinment(movingTRCus1, nextDP2))
                    {
                        s.Dir = CustomerDirection.STEADY;

                        Delayed(Convert.ToInt32(nextDP2.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(nextDP2, movingDPCus, pbxExit.Location.X - 30));
                    }
                }




                foreach (SimulatingCustomer s in customers.CusInAppoinment(movingDPCus, current2))
                {
                    s.Dir = CustomerDirection.STEADY;

                    Delayed(Convert.ToInt32(current2.ProcessingTime) * 1000, () => customers.GoIntoTheCounter(current2, movingDPCus, pbxExit.Location.X - 30));
                }

                this.customers.SocialDistancingForDriverLicenseQueue();
                this.Invalidate();
            }
        }

        private void NwTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ProcessingAppointment(Counter selectedCounter, CustomerType t1, CustomerType t2)
        {
            List<SimulatingCustomer> cusPerType = this.customers.GetCustomersPerType(t1, t2);
            foreach (SimulatingCustomer cus in cusPerType)
            {
                int currentPosition = cus.Y;
                if (cusPerType.IndexOf(cus)<cusPerType.Count()-1)
                {
                    SimulatingCustomer nextCus = cusPerType[cusPerType.IndexOf(cus) + 1];

                    foreach (CircularPanel c in customers.GetCircularPanels())
                    {
                        if (selectedCounter.IsOccupied == false)
                        {
                            cus.Dir = CustomerDirection.UP;
                            if (c.Location.Y == selectedCounter.Y + 30)
                            {
                                cus.Dir = CustomerDirection.STEADY;
                                nextCus.Dir = CustomerDirection.UP;
                                if (nextCus.Y == currentPosition)
                                {
                                    nextCus.Dir = CustomerDirection.STEADY;
                                }
                            }
                            selectedCounter.IsOccupied = true;
                        }                        
                    }
                }
                else
                {

                    foreach (CircularPanel c in customers.GetCircularPanels())
                    {
                        if (selectedCounter.IsOccupied == false)
                        {
                            cus.Dir = CustomerDirection.UP;
                            if (c.Location.Y == selectedCounter.Y + 30)
                            {
                                cus.Dir = CustomerDirection.STEADY;
                            }
                            selectedCounter.IsOccupied = true;
                        }
                    }
                }
                
            }
        }

        


        private void AddDefault()
            {
            //counter 1
            id++;
            Counter c1 = new Counter(id,376, 74, CounterType.DRIVERLICENSEandPASSPORTID, 15);
            Panel p1 = new Panel();
            p1.Location = new Point(c1.X, c1.Y);
            p1.Size = new Size(108, 50);
            Label lblC1 = new Label();
            lblC1.Location = new Point(p1.Location.X+23, 51);
            lblC1.Text = $"Counter {id}";
            lblC1.Font = new Font("Microsoft Sans Serif", 10);
            lblC1.BackColor = Color.Pink;
            lblC1.AutoSize = true;
            
            //counter 2
            id++;
            Counter c2 = new Counter(id,c1.X+178, 74, CounterType.TAXandROOMRENTAL, 20);
            Panel p2 = new Panel();
            p2.Location = new Point(c2.X, c2.Y);
            p2.Size = new Size(108, 50);
            Label lblC2 = new Label();
            lblC2.Location = new Point(p2.Location.X+23, 51);
            lblC2.Text = $"Counter {id}";
            lblC2.Font = new Font("Microsoft Sans Serif", 10);
            lblC2.BackColor = Color.Pink;
            lblC2.AutoSize = true;

            //counter 3
            id++;
            Counter c3 = new Counter(id,c2.X + 178, 74, CounterType.TAXandROOMRENTAL, 20);
            Panel p3 = new Panel();
            p3.Location = new Point(c3.X, c3.Y);
            p3.Size = new Size(108, 50);
            Label lblC3 = new Label();
            lblC3.Location = new Point(p3.Location.X+23, 51);
            lblC3.Text = $"Counter {id}";
            lblC3.Font = new Font("Microsoft Sans Serif", 10);
            lblC3.BackColor = Color.Pink;
            lblC3.AutoSize = true;

            //counter 4
            id++;
            Counter c4 = new Counter(id, c3.X + 178, 74, CounterType.DRIVERLICENSEandPASSPORTID, 15);
            Panel p4 = new Panel();
            p4.Location = new Point(c4.X, c4.Y);
            p4.Size = new Size(108, 50);
            Label lblC4 = new Label();
            lblC4.Location = new Point(p4.Location.X + 23, 51);
            lblC4.Text = $"Counter {id}";
            lblC4.Font = new Font("Microsoft Sans Serif", 10);
            lblC4.BackColor = Color.Pink;
            lblC4.AutoSize = true;
            
            //counter 5
            id++;
            Counter c5 = new Counter(id, c4.X + 178, 74, CounterType.DRIVERLICENSEandPASSPORTID, 15);
            Panel p5 = new Panel();
            p5.Location = new Point(c5.X, c5.Y);
            p5.Size = new Size(108, 50);
            Label lblC5 = new Label();
            lblC5.Location = new Point(p5.Location.X + 23, 51);
            lblC5.Text = $"Counter {id}";
            lblC5.Font = new Font("Microsoft Sans Serif", 10);
            lblC5.BackColor = Color.Pink;
            lblC5.AutoSize = true;

            //counter 6
            id++;
            Counter c6 = new Counter(id, c5.X + 178, 74, CounterType.GENERAL, 10);
            Panel p6 = new Panel();
            p6.Location = new Point(c6.X, c6.Y);
            p6.Size = new Size(108, 50);
            Label lblC6 = new Label();
            lblC6.Location = new Point(p6.Location.X + 23, 51);
            lblC6.Text = $"Counter {id}";
            lblC6.Font = new Font("Microsoft Sans Serif", 10);
            lblC6.BackColor = Color.Pink;
            lblC6.AutoSize = true;


            //set sounter color
            this.SetCounterColor(c1,p1);
            this.SetCounterColor(c2,p2);
            this.SetCounterColor(c3,p3);
            this.SetCounterColor(c4, p4);
            this.SetCounterColor(c5, p5);
            this.SetCounterColor(c6, p6);

            this.counters.AddCounter(c1);
            this.counters.AddCounter(c2);
            this.counters.AddCounter(c3);
            this.counters.AddCounter(c4);
            this.counters.AddCounter(c5);
            this.counters.AddCounter(c6);

            this.panels.Add(p1);
            this.panels.Add(p2);
            this.panels.Add(p3);
            this.panels.Add(p4);
            this.panels.Add(p5);
            this.panels.Add(p6);

            this.countersLabels.Add(lblC1);
            this.countersLabels.Add(lblC2);
            this.countersLabels.Add(lblC3);
            this.countersLabels.Add(lblC4);
            this.countersLabels.Add(lblC5);
            this.countersLabels.Add(lblC6);

            foreach (Panel p in panels)
            {
                this.Controls.Add(p);
                p.BringToFront();
            }

            foreach(Label l in countersLabels)
            {
                this.Controls.Add(l);
                l.BringToFront();
            }
            this.AddIdToCombobox();
            this.Invalidate();

        }

        private void SetCounterColor(Counter c, Panel p)
        {
            if (c.CounterType == CounterType.DRIVERLICENSEandPASSPORTID)
            {
                if (c.IsOpened)
                    p.BackColor = Color.FromArgb(255, 51, 153);
                else
                    p.BackColor = Color.Gray;
            }
            else if(c.CounterType == CounterType.TAXandROOMRENTAL)
            {
                if (c.IsOpened)
                    p.BackColor = Color.FromArgb(255, 153, 51);
                else
                    p.BackColor = Color.Gray;
            }
            else
            {
                if (c.IsOpened)
                    p.BackColor = Color.Green;
                else
                    p.BackColor = Color.Gray;
            }
        }

        private void MovePanelToLeft()
        {
            int firstCounterX = panels[0].Location.X;
            panels[0].Location = new Point(firstCounterX - 54, 74);

            countersLabels[0].Location = new Point(panels[0].Location.X + 15, 51);
            for (int i = 1; i < panels.Count; i++) 
            {
                for (int n = 1; n < countersLabels.Count; n++)
                {
                    panels[i].Location = new Point(panels[i - 1].Location.X + 108 + 110, 74);
                    countersLabels[n].Location = new Point(countersLabels[n-1].Location.X+218, 51);
                }
                
            }
            this.Invalidate();
        }

        private int GetNrOfCounters()
        {
            return this.counters.GetAllCounters().Count;
        }

        private void AddIdToCombobox()
        {
            this.cbxCounterId.Items.Clear();
            foreach (Counter c in counters.GetAllCounters())
            {
                this.cbxCounterId.Items.Add($"{c.Id}-Type:{c.CounterType}");
            }
        }

        private void timerBlinking_Tick(object sender, EventArgs e)
        {

            Random r = new Random();

            int a = r.Next(144, 245);
            int b = r.Next(192, 238);
            int c = r.Next(144, 203);
            int d = r.Next(0, 0);

           foreach(Counter counter in counters.GetAllCounters())
            {
                if (counter.IsOpened == true)
                {
                    countersLabels[counter.Id-1].BackColor = Color.FromArgb(a, b, c, d);

                }
                else
                {
                    countersLabels[counter.Id-1].BackColor = Color.Pink;

                }
           }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Do you really want to end the simulation?", "Exit", MessageBoxButtons.YesNo);
            //MessageBox.Show("Do you really want to end the simulation?", "Exit", MessageBoxButtons.YesNo);
            
            if(dialog == DialogResult.No)
            { 

                e.Cancel = true;
            }
        }

        private void changeProcessingTimeBtn_Click(object sender, EventArgs e)
        {
            
            
        }

        private void cbxCounterId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxCounterId.SelectedIndex >= 0)
            {
                string s = "";
                int counterId = this.cbxCounterId.SelectedIndex + 1;
                s += counters.GetCounter(counterId).ProcessingTime;
                processTimeTb.Text = s;
                
                
            }
        }

        private void changeProcessingTimeBtn_Click_1(object sender, EventArgs e)
        {
            double processingTime = Convert.ToDouble(changedProcessingTimeTb.Text);
            CounterType type;

            if (this.changedTimeForTypeCb.SelectedIndex == 1)
            {
                type = CounterType.TAXandROOMRENTAL;
            }
            else { type = CounterType.DRIVERLICENSEandPASSPORTID; }


            if (changedProcessingTimeTb.Text != string.Empty)
            {
            counters.SetProcessingTime(processingTime, type);    
            MessageBox.Show("Processing time changed to " + processingTime);
            }
            else
            {
                MessageBox.Show("Please input the processcing time");
            }

        }

        private void taskCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void changedProcessingTimeTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private bool IsTimerStarted()
        {
            if (this.timerStart.Enabled == true)
            {
                return true;
            }
            else { return false; }
        }

        private void timerWait_Tick(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timerProcessTax_Tick(object sender, EventArgs e)
        {

            //this.GoIntoCounter();
        }

        private void btnSetNewTime_Click(object sender, EventArgs e)
        {
            int hour = Convert.ToInt32(this.numericHour.Value);
            int minute = Convert.ToInt32(this.numericMinute.Value);
            int second = Convert.ToInt32(this.numericSecond.Value);

            clock = new Clock(hour, minute, second);
            Time();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "Statistics";
            saveFileDialog1.Filter = "Excel Files(.xlsx)|*.xlsx";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                //MAKE SURE TO ADD THE REFERENCE "Microsoft.Office.Interop.Excel" in order to run this
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                // Change properties of the Workbook
                excelApp.Columns.ColumnWidth = 30;

                // Storing header part in Excel
                for (int i = 1; i < dataGridViewTableStats.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dataGridViewTableStats.Columns[i - 1].HeaderText;
                }

                //Storing each row and column value to Excel sheet
                for (int i = 0; i < dataGridViewTableStats.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridViewTableStats.Columns.Count; j++)
                    {
                        excelApp.Cells[i + 2, j + 1] = dataGridViewTableStats.Rows[i].Cells[j].Value.ToString();
                    }
                }

                excelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                excelApp.ActiveWorkbook.Saved = true;
                excelApp.Quit();
            }
        }

        private void btnLoadChartAndTable_Click(object sender, EventArgs e)
        {
            Form1.totalTRHandlingTime = counters.GetCounterByType(CounterType.TAXandROOMRENTAL)[0].ProcessingTime * totalTRVisitorsHandled;
            Form1.totalDPHandlingTime = counters.GetCounterByType(CounterType.DRIVERLICENSEandPASSPORTID)[0].ProcessingTime * totalDPVisitorsHandled;
            this.chartStats.Series["Total handling time"].Points.Clear();
            this.chartStats.Series["Total visitors handled"].Points.Clear();
            this.chartStats.Series["Average handling time"].Points.Clear();
            //CREATE THE BAR CHART 
            //Set interval for y-axis
            this.chartStats.ChartAreas[0].AxisY.Interval = 2;
            this.dataGridViewTableStats.Rows.Clear();

            if (TotalTRVisitorsHandled > 0 && TotalDPVisitorsHandled == 0)
            {
                //add rows and columns for counter type "Tax and room rental"
                this.chartStats.Series["Total handling time"].Points.AddXY("Tax and Room Rental", totalTRHandlingTime);
                this.chartStats.Series["Total visitors handled"].Points.AddXY("Tax and Room Rental", TotalTRVisitorsHandled);
                this.chartStats.Series["Average handling time"].Points.AddXY("Tax and Room Rental", totalTRHandlingTime / TotalTRVisitorsHandled);


                //CREATE THE TABLE

                dataGridViewTableStats.Rows.Add();

                dataGridViewTableStats.Rows[0].Cells[0].Value = "Tax and room rental";
                dataGridViewTableStats.Rows[0].Cells[1].Value = totalTRHandlingTime;
                dataGridViewTableStats.Rows[0].Cells[2].Value = TotalTRVisitorsHandled;
                dataGridViewTableStats.Rows[0].Cells[3].Value = totalTRHandlingTime / TotalTRVisitorsHandled;

            }





            else if (TotalDPVisitorsHandled > 0 && TotalTRVisitorsHandled == 0)
            {
                //add rows and columns for counter type "Driver License and Passport ID"
                this.chartStats.Series["Total handling time"].Points.AddXY("Driver License and Passport ID", totalDPHandlingTime);
                this.chartStats.Series["Total visitors handled"].Points.AddXY("Driver License and Passport ID", TotalDPVisitorsHandled);
                this.chartStats.Series["Average handling time"].Points.AddXY("Driver License and Passport ID", totalDPHandlingTime / TotalDPVisitorsHandled);

                dataGridViewTableStats.Rows.Add();
                dataGridViewTableStats.Rows[0].Cells[0].Value = "Driver License and Passport ID";
                dataGridViewTableStats.Rows[0].Cells[1].Value = totalDPHandlingTime;
                dataGridViewTableStats.Rows[0].Cells[2].Value = TotalDPVisitorsHandled;
                dataGridViewTableStats.Rows[0].Cells[3].Value = totalDPHandlingTime / TotalDPVisitorsHandled;
            }
            else
            {
                this.chartStats.Series["Total handling time"].Points.AddXY("Tax and Room Rental", totalTRHandlingTime);
                this.chartStats.Series["Total visitors handled"].Points.AddXY("Tax and Room Rental", TotalTRVisitorsHandled);
                this.chartStats.Series["Average handling time"].Points.AddXY("Tax and Room Rental", totalTRHandlingTime / TotalTRVisitorsHandled);

                this.chartStats.Series["Total handling time"].Points.AddXY("Driver License and Passport ID", totalDPHandlingTime);
                this.chartStats.Series["Total visitors handled"].Points.AddXY("Driver License and Passport ID", TotalDPVisitorsHandled);
                this.chartStats.Series["Average handling time"].Points.AddXY("Driver License and Passport ID", totalDPHandlingTime / TotalDPVisitorsHandled);



                //CREATE THE TABLE

                dataGridViewTableStats.Rows.Add();

                dataGridViewTableStats.Rows[0].Cells[0].Value = "Tax and room rental";
                dataGridViewTableStats.Rows[0].Cells[1].Value = totalTRHandlingTime;
                dataGridViewTableStats.Rows[0].Cells[2].Value = TotalTRVisitorsHandled;
                dataGridViewTableStats.Rows[0].Cells[3].Value = totalTRHandlingTime / TotalTRVisitorsHandled;


                dataGridViewTableStats.Rows.Add();
                dataGridViewTableStats.Rows[1].Cells[0].Value = "Driver License and Passport ID";
                dataGridViewTableStats.Rows[1].Cells[1].Value = totalDPHandlingTime;
                dataGridViewTableStats.Rows[1].Cells[2].Value = TotalDPVisitorsHandled;
                dataGridViewTableStats.Rows[1].Cells[3].Value = totalDPHandlingTime / TotalDPVisitorsHandled;

            }

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string info = "";
            for(int i=0; i < this.counters.GetAllCounters().Count; i++)
            {
                info += $"Counter: {counters.GetAllCounters()[i].Id}- Number of customers: {customers.GetCustomersInQueue(counters.GetAllCounters()[i]).Count.ToString()}\n";
                
            }
            MessageBox.Show(info);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.timerStart.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timerStart.Stop();
        }

        public void Delayed(int delay, Action action)
        {
            System.Windows.Forms.Timer timerWaiting = new System.Windows.Forms.Timer();
            timerWaiting.Interval = delay;
            timerWaiting.Tick += (s, e) => {
                action();
                timerWaiting.Stop();
            };
            timerWaiting.Start();
        }

        private void comboBox2_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            this.btnSpawnTax.Enabled = true;
        }

        private void cbxDLPerSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSpawnDriverLicense.Enabled = true;

        }

        private void cbxPIDPerSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSpawnPassID.Enabled = true;
        }

        private void cbxRoomPerSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSpawnRental.Enabled = true;

        }

        private void chartStats_Click(object sender, EventArgs e)
        {

        }
    }
}
