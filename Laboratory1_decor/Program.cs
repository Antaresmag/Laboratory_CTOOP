using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory1
{
    class Program_2
    {
        static void Main_2(string[] args)
        {
            mycar toyotaCar1 = new toyota();
            toyotaCar1.ManufactureCar();
            Console.WriteLine(toyotaCar1 + "\n");
            Diesel carWithDieselEngine = new Diesel(toyotaCar1);
            carWithDieselEngine.ManufactureCar();
            Console.WriteLine();
            mycar toyotaCar2 = new toyota();
            Petrol carWithPetrolEngine = new Petrol(toyotaCar2);
            carWithPetrolEngine.ManufactureCar();
            Console.ReadKey();
        }

        class Diesel : CarDecorator
        {
            public Diesel(mycar car) : base(car)
            {
            }
            public override mycar ManufactureCar()
            {
                car.ManufactureCar();
                AddEngine(car);
                return car;
            }
            public void AddEngine(mycar car)
            {
                if (car is toyota)
                {
                    toyota toyotaCar = (toyota)car;
                    toyotaCar.Engine = "Дизельний двигун";
                    Console.WriteLine("Diesel  додав двигун до авто : " + car);
                }
            }
        }

        class CarDecorator : mycar
        {
            protected mycar car;
            public CarDecorator(mycar car)
            {
                this.car = car;
            }
            public virtual mycar ManufactureCar()
            {
                return car.ManufactureCar();
            }
        }

        class toyota : mycar
        {
            private string CarName = "toyota";
            public string CarBody { get; set; }
            public string CarDoor { get; set; }
            public string CarWheels { get; set; }
            public string CarGlass { get; set; }
            public string Engine { get; set; }
            public override string ToString()
            {
                return "toyota [Марка автомобiля = " + CarName + ", Складається з = " + CarBody + ", Кiлькiсть дверей= " + CarDoor + ", Кiлькiсть колес = "
                                + CarWheels + ", Кiлькiсть вiкон = " + CarGlass + ", Марка двигуна = " + Engine + "]";
            }
            public mycar ManufactureCar()
            {
                CarBody = "Метал";
                CarDoor = "2 пари дверей";
                CarWheels = "4 колеса ";
                CarGlass = "6 вiкон";
                return this;
            }
        }

        public interface mycar
        {
            mycar ManufactureCar();
        }
    }
}

