using AutoMapper;
using IsTakip.API.Filters;
using IsTakip.Core.Classes.CustomerClasses;
using IsTakip.Core.DTOs;
using IsTakip.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRestrictionController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<CustomerRestriction> _services;

        public CustomerRestrictionController(IMapper mapper, IService<CustomerRestriction> services)
        {
            _mapper = mapper;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var customerrestrictions = await _services.GetAllAsync();
            var customerrestrintionsDtos = _mapper.Map<List<CustomerRestrictionDTO>>(customerrestrictions.ToList());
            return CreateActionResult(CustomResponseDTO<List<CustomerRestrictionDTO>>.Success(200, customerrestrintionsDtos));
        }
        [ServiceFilter(typeof(NotFoundFilter<CustomerRestriction>))]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var customerrestriction = await _services.GetByIdAsync(id);
            var customerrestrintionsDto = _mapper.Map<CustomerRestrictionDTO>(customerrestriction);
            return CreateActionResult(CustomResponseDTO<CustomerRestrictionDTO>.Success(200, customerrestrintionsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerRestrictionDTO customerrstrictionDto)
        {
            var customerrestriction = await _services.AddAsync(_mapper.Map<CustomerRestriction>(customerrstrictionDto));
            var customerrestrintionsDto = _mapper.Map<CustomerRestrictionDTO>(customerrestriction);
            return CreateActionResult(CustomResponseDTO<CustomerRestrictionDTO>.Success(201, customerrestrintionsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerRestrictionDTO customerrstrictionDto)
        {
            await _services.UpdateAsync(_mapper.Map<CustomerRestriction>(customerrstrictionDto));
            return CreateActionResult(CustomResponseDTO<List<NoContentDTO>>.Success(204));
        }
        [ServiceFilter(typeof(NotFoundFilter<CustomerRestriction>))]
        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            var customerrestriction = await _services.GetByIdAsync(id);
            await _services.DeleteAsync(customerrestriction);
            return CreateActionResult(CustomResponseDTO<List<NoContentDTO>>.Success(204));
        }
    }
}
