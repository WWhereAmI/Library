using Library.DataAccess;
using Library.Models;
using Library.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Services
{
    public class ClientReadService : BaseReadService<Client>
    {
        private readonly IRepository<Client> _clientRepository;
        public ClientReadService(IRepository<Client> clientRepository) : base(clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public List<Client> GetAll()
        {
            return _clientRepository.GetAll()
                .ToList();
        }
    }
}