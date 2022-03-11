using System;

namespace YoutubeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ioc Container, Ninject, Autofac
            CustomerManager customerManager = new CustomerManager(new CustomerDal(), new FakeLogger());
            customerManager.Save(new Customer());
            Console.ReadLine();
        }
    }

    class CustomerDal : ICustomerDal
    {
        public void Save()
        {
            Console.WriteLine("Customer Added With Ef");
        }
    }

    class NhCustomerDal : ICustomerDal
    {
        public void Save()
        {
            Console.WriteLine("Customer Added With Nh");
        }
    }

    interface ICustomerDal
    {
        void Save();
    }

    class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        private ILogger _logger;
        
        public CustomerManager(ICustomerDal customerDal, ILogger logger)
        {
            _logger = logger;
            _customerDal = customerDal;
        }

        public void Save(Customer customer)
        {            
            _customerDal.Save();
            _logger.Log();
        }
    }

    interface ICustomerService
    {
        void Save(Customer customer);
    }

    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }

    interface IEntity
    {
    }    

    interface ILogger
    {
        void Log();
    }

    class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged to db");
        }
    }

    class EmailLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged to email");
        }
    }


    class FakeLogger : ILogger
    {
        public void Log()
        {
            
        }
    }

    class MainLoggerAdapter : ILogger
    {
        public void Log()
        {
            MainLogger mainLogger = new MainLogger();
            mainLogger.LogToMain();
        }
    }

    //Microservice
    class MainLogger
    {
        public void LogToMain()
        {
            Console.WriteLine("Logged to main");
        }
    }

}
