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
    public class MaterialTypeController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<MaterialType> _services;

        public MaterialTypeController(IMapper mapper, IService<MaterialType> services)
        {
            _mapper = mapper;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var materialTypes = await _services.GetAllAsync();
            var materialTypesDtos = _mapper.Map<List<MaterialTypeDTO>>(materialTypes.ToList());
            return CreateActionResult(CustomResponseDTO<List<MaterialTypeDTO>>.Success(200, materialTypesDtos));
        }
        [ServiceFilter(typeof(NotFoundFilter<MaterialType>))]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var materialType = await _services.GetByIdAsync(id);
            var materialTypesDto = _mapper.Map<MaterialTypeDTO>(materialType);
            return CreateActionResult(CustomResponseDTO<MaterialTypeDTO>.Success(200, materialTypesDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(MaterialTypeDTO materialtypeDto)
        {
            var materialType = await _services.AddAsync(_mapper.Map<MaterialType>(materialtypeDto));
            var materialTypesDto = _mapper.Map<MaterialTypeDTO>(materialType);
            return CreateActionResult(CustomResponseDTO<MaterialTypeDTO>.Success(201, materialTypesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(MaterialTypeDTO materialtypeDto)
        {
            await _services.UpdateAsync(_mapper.Map<MaterialType>(materialtypeDto));
            return CreateActionResult(CustomResponseDTO<List<NoContentDTO>>.Success(204));
        }
        [ServiceFilter(typeof(NotFoundFilter<MaterialType>))]
        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            var materialType = await _services.GetByIdAsync(id);
            await _services.DeleteAsync(materialType);
            return CreateActionResult(CustomResponseDTO<List<NoContentDTO>>.Success(204));
        }
    }
}
