using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> _userManager) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _LazyproductService.Value;

        private readonly Lazy<IBasketService> _LazybasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _LazybasketService.Value;


        private readonly Lazy<IAuthenticationService> _LazyauthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager));
        public IAuthenticationService authenticationService => _LazyauthenticationService.Value;
    }
}
