using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
     public class TV
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int ScreenSize { get; set; }
        public string Resolution { get; set; }
        public bool IsSmart { get; set; }
        public float Area
        {
            get
            {
                double x = Math.Sqrt(ScreenSize + ScreenSize / 337);
                return Convert.ToSingle(16 * 9 * x * x);
            }
        }

        public void ChangePrice(float newPrice)
        {
            if (newPrice >= 0)
                Price = newPrice;
        }
        public TV()
        {

        }

        public TV(string model, string brand, int screenSize, string resolution, bool isSmart, string color, double price)
        {
            Model = model;
            Brand = brand;
            ScreenSize = screenSize;
            Resolution = resolution;
            IsSmart = isSmart;
            Color = color;
            Price = price;
        }
    }
}
