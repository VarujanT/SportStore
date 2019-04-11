using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using System.Web.Routing;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;

namespace SportStore.MVC.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        IKernel kernel;

        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            AddBinding();
        }

        public void AddBinding()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name = "Gndak", Price = 35.54m},
                new Product {Name = "Car", Price = 7435.700m},
                new Product {Name = "Kolbas Kam Gone Yershik", Price = 450.54m},
                new Product {Name = "Botas", Price = 75.55m}
            }.AsQueryable<Product>
            );

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (controllerType== null ? null: (IController)kernel.Get(controllerType));
        }
    }
}