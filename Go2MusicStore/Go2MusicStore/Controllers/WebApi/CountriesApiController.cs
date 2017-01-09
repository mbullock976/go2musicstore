using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Go2MusicStore.Controllers.WebApi
{
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.Models;

    public class CountriesApiController : BaseApiController
    {
        public CountriesApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {

        }

        // GET: api/Countries
        public IEnumerable<Country> Get()
        {
            return this.StoreAccountManager.Get<Country>();
        }

        // GET: api/Countries/5
        public Country Get(int countryId)
        {
            return this.StoreAccountManager.GetById<Country>(countryId);
        }       
    }
}
