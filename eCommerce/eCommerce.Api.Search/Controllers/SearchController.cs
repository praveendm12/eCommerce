using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await _searchService.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
                return Ok(result.Result);
            return NotFound();

        }
    }
}
