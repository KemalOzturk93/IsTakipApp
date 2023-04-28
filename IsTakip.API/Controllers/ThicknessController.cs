using AutoMapper;
using IsTakip.API.Filters;
using IsTakip.Core.Classes.CustomerClasses;
using IsTakip.Core.Classes.WareHouseClasses;
using IsTakip.Core.DTOs;
using IsTakip.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThicknessController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Thickness> _services;

        public ThicknessController(IMapper mapper, IService<Thickness> services)
        {
            _mapper = mapper;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var thicknesses = await _services.GetAllAsync();
            var thicknessesDtos = _mapper.Map<List<ThicknessDTO>>(thicknesses.ToList());
            return CreateActionResult(CustomResponseDTO<List<ThicknessDTO>>.Success(200, thicknessesDtos));
        }
        [ServiceFilter(typeof(NotFoundFilter<Thickness>))]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var thickness = await _services.GetByIdAsync(id);
            var thicknessesDto = _mapper.Map<ThicknessDTO>(thickness);
            return CreateActionResult(CustomResponseDTO<ThicknessDTO>.Success(200, thicknessesDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ThicknessDTO thicknessDto)
        {
            var thickness = await _services.AddAsync(_mapper.Map<Thickness>(thicknessDto));
            var thicknessesDto = _mapper.Map<ThicknessDTO>(thickness);
            return CreateActionResult(CustomResponseDTO<ThicknessDTO>.Success(201, thicknessesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ThicknessDTO thicknessDto)
        {
            await _services.UpdateAsync(_mapper.Map<Thickness>(thicknessDto));
            return CreateActionResult(CustomResponseDTO<List<NoContentDTO>>.Success(204));
        }
        [ServiceFilter(typeof(NotFoundFilter<Thickness>))]
        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            var thickness = await _services.GetByIdAsync(id);
            await _services.DeleteAsync(thickness);
            return CreateActionResult(CustomResponseDTO<List<NoContentDTO>>.Success(204));
        }
    }
}
