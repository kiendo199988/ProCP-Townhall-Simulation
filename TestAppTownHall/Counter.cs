using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestAppTownHall
{
   public enum CounterType
    {
        TAXandROOMRENTAL, DRIVERLICENSEandPASSPORTID, GENERAL
    }
   public class Counter
    {
        //The type of task which will be processed in the counter
        private CounterType counterType;
        //processing time for each task
        private double processingTime;
        //determine if the counter is opened or not
        private bool isOpened;
        //determine if the counter is occupied
        private bool isOccupied;

        //properties

        public int Id { get; set; }
        public CounterType CounterType
        {
            get{ return this.counterType; }
            set { this.counterType = value; }
        }
        public double ProcessingTime
        {
            get { return this.processingTime; }
            set { this.processingTime = value; }
        }
        public bool IsOpened
        {
            get { return this.isOpened; }
            set { this.isOpened = value; }
        }
        public bool IsOccupied
        {
            get { return this.isOccupied; }
            set { this.isOccupied = value; }
        }


        public int X { get; set; }
        public int Y { get; set; }

        public Counter(int id, int x, int y, CounterType counterType, double processcingTime)
        {
            this.Id = id;
            this.counterType = counterType;
            this.processingTime = processcingTime;
            this.IsOccupied = false;
            this.IsOpened = false;
            this.X = x;
            this.Y = y;
        }

        public void PaintCounter(Graphics gr)
        {
            // gr.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.Black)),4F);


            Point[] points = new Point[4];

            points[0] = new Point(0, 0);
            points[1] = new Point(0, X);
            points[2] = new Point(Y, X);
            points[3] = new Point(Y, 0);


            Brush brush;

            if (this.counterType == CounterType.TAXandROOMRENTAL)
            {
                brush = new SolidBrush(Color.FromArgb(255, 153, 51));
            }
            else
            {
                brush = new SolidBrush(Color.FromArgb(255, 51, 153));
            }
            
            gr.FillPolygon(brush, points);
        }

        public void MoveCounter(Panel p, int x, int y)
        {
            p.Location = new Point(x, y);
        }
        
        

    }
}
