using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public abstract class Household
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public abstract float Area
        {
            get;
        }

        void ChangePrice(float newPrice)
        {
            if (newPrice >= 0)
                Price = newPrice;
        }
    }
    public class TV : Household
    {
        public int ScreenSize { get; set; }

        public string Resolution { get; set; }
        public bool IsSmart { get; set; }

        public double x;
        public override float Area => Convert.ToSingle(16 * 9 * x * x);


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
            x = Math.Sqrt(ScreenSize * ScreenSize / 337);
        }
    }
}
