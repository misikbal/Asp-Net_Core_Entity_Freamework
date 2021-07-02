using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemekSepetiMediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();

            Dominos MardinBayisi = new Dominos(mediator);
            MardinBayisi.Name = "İkbal";
            mediator.Dominos = MardinBayisi;
            Customer hasan = new Customer(mediator);
            hasan.Name = "Hasan YILMAZ";
            Customer cem = new Customer(mediator);
            cem.Name = "Cem Serdar AKKOCAOĞLU";

            mediator.Customers = new List<Customer> { hasan, cem };

            MardinBayisi.SendNewDiscount("Gel Al","%40");
            Console.WriteLine("****************************************************");

            cem.Ordered("Sosyal Pizza", cem);
            MardinBayisi.Order("Sosyal Pizza", cem);
            MardinBayisi.OrderCompleted("Sosyal Pizza", cem);
            Console.WriteLine("****************************************************");
            hasan.Ordered("Konyalım Pizza", hasan);
            MardinBayisi.Order("Konyalım Pizza", hasan);
            MardinBayisi.OrderCompleted("Konyalım Pizza", hasan);
            Console.ReadLine();
        }
    }

    abstract class YemekSepeti
    {
        protected Mediator Mediator;
        protected YemekSepeti(Mediator mediator)
        {
            Mediator = mediator;
        }
    }
    class Dominos : YemekSepeti
    {
        public Dominos(Mediator mediator) : base(mediator){}
        
        public void Order(string order, Customer customer)
        {
            Console.WriteLine("Dominos {0} adlı müşterinin {1} siparişini onayladı ", customer.Name, order);
        }
        public void SendNewDiscount(string indirim,string indirimyuzdesi)
        {
            Console.WriteLine("Mesaj: Dominos {0} 'da tüm pizalarda {1} indirim yaptı", indirim,indirimyuzdesi);
            Mediator.Discount(indirim,indirimyuzdesi);
        }
        public void OrderCompleted(string order, Customer customer)
        {
            Console.WriteLine("{0} adlı müşterinin {1} siparişi ulaştı.Müşteri yemeksepeti.com yorum yaptı.", customer.Name, order);
        }
        public string Name { get; set; }
    }
    class Customer : YemekSepeti
    {
        public Customer(Mediator mediator) : base(mediator)
        {

        }
        public void GelAl(string indirim, string indirimyuzdesi)
        {
            Console.WriteLine("{1} adlı müşteri {0}'da {2} indirim kazandı", indirim, Name,indirimyuzdesi);
        }
        public void Ordered(string order, Customer customer)
        {
            Console.WriteLine("{0} adlı müşterinin {1} adlı şiparişi gönderdi.", customer.Name, order);
        }

        public string Name { get; set; }
    }
    class Mediator
    {
        public Dominos Dominos { get; set; }
        public List<Customer> Customers { get; set; }
        public void Discount(string indirim,string indirimyuzdesi)
        {
            foreach (var customer in Customers)
            {
                customer.GelAl(indirim, indirimyuzdesi);
            }
        }
        public void GetOrder(string order, Customer customer)
        {
            Dominos.Order(order, customer);
        }
        public void SendOrder(string order, Customer customer)
        {
            customer.Ordered(order, customer);
        }

    }
}
