using System;

namespace Laboratory1
{
    class Client
    {
        public string Id { get; set; }
        public string NameBank { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Клієнт Банку");
        }
        public void Request(CreditCart creditCard)
        {
            creditCard.Request();
        }
    }

    class CreditCart
    {
        public string infoBank { get; set; }
        public string infoCreditCart { get; set; }

        public virtual void Request() 
        { }
    }

    class Adapter : CreditCart
    {
        private Adaptee adaptee = new Adaptee();

        public override void Request()
        {
            adaptee.SpecificRequest();
        }

    }

    class Adaptee
    {

        public void SpecificRequest()
        {
            Console.WriteLine("SpecifiRequest");
        }
    }
}