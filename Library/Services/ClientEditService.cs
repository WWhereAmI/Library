using Library.DataAccess;
using Library.Models;
using Library.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Services
{
    public class ClientEditService : BaseEditService<Client>
    {
        public ClientEditService(IRepository<Client> repository) : base(repository)
        {
        }
    }
}