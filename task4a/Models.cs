using SQLite;
using System;

namespace task4a
{
    public class Store
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Cost { get; set; }
        public double Weight { get; set; }
        public string Colour { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name,-10}{Id,-10} {Cost,-10} {Weight,-5} {Colour,-10}";
        }
    }

    [Table("Cow")]
    public class Cow : Store
    {
        public double Milk { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" {Milk}";
        }
    }

    [Table("Sheep")]
    public class Sheep : Store
    {
        public double Wool { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" {Wool}";
        }
    }
}