using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Application.Interfaces;
using PhotoStudio.Domain.Entities;
using PhotoStudio.ServicesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Services
{
    public interface IMaterialManager
    {
        Task<MaterialDTO> AddMaterial(MaterialDTO material);
        Task<MaterialDTO> UpdateMaterial(MaterialDTO material, int id);
        Task DeleteMaterial(int id);
        Task<List<MaterialDTO>> GetMaterials();
        Task<MaterialDTO> GetMaterialById(int id);
        Task<List<MaterialDTO>> GetMaterialByTextName(string name);

    }

    public class MaterialManager : IMaterialManager
    {
        private readonly ICommandRepository<Material> _cMaterialRepository;
        private readonly IQueryRepository<Material> _qMaterialRepository;
        private readonly IMapper _mapper;

        public MaterialManager(ICommandRepository<Material> cMaterialRepository, IQueryRepository<Material> qMaterialRepository, IMapper mapper)
        {

            _cMaterialRepository = cMaterialRepository;
            _qMaterialRepository = qMaterialRepository;
            _mapper = mapper;
        }

        public async Task<List<MaterialDTO>> GetMaterials()
        {

            var materials = await _qMaterialRepository.GetAll().ToListAsync();

            List<MaterialDTO> materialListDto = _mapper.Map<List<MaterialDTO>>(materials);

            return materialListDto;

        }

        public async Task<MaterialDTO> GetMaterialById(int id)
        {
            var material = await _qMaterialRepository.Find(m => m.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<MaterialDTO>(material);
        }

        public async Task<MaterialDTO> AddMaterial(MaterialDTO materialDto)
        {
            Material entityMaterial = _mapper.Map<Material>(materialDto);
            await _cMaterialRepository.AddAsync(entityMaterial);
            await _cMaterialRepository.UnitOfWork.Commit();
            materialDto = _mapper.Map<MaterialDTO>(entityMaterial);
            return materialDto;
        }

        public async Task DeleteMaterial(int id)
        {
            var material = await _cMaterialRepository.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (material == null)
                throw new ArgumentException($"The material to delete with id: {id} does no exist");
            
            await _cMaterialRepository.DeleteAsync(material);

            await _cMaterialRepository.UnitOfWork.Commit();
        }

        public async Task<MaterialDTO> UpdateMaterial(MaterialDTO materialDTO, int id)
        {
            var materialToUpdate = await _cMaterialRepository.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (materialToUpdate == null)
                throw new ArgumentException($"The material to update with id: {id} does no exist");

            _mapper.Map(materialDTO, materialToUpdate);

            materialToUpdate = await _cMaterialRepository.UpdateAsync(materialToUpdate);

            await _cMaterialRepository.UnitOfWork.Commit();

             materialDTO = _mapper.Map<MaterialDTO>(materialToUpdate);

            return materialDTO;

        }

        public async Task<List<MaterialDTO>> GetMaterialByTextName(string name)
        {
            var material = await _qMaterialRepository.Find(m => m.MaterialName.Contains(name)).ToListAsync();
            
            return _mapper.Map<List<MaterialDTO>>(material);
        }

    }


}
