using AutoMapper;
using PhotoStudio.Application.Interfaces;
using PhotoStudio.Domain.Entities;
using PhotoStudio.ServicesDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Services
{
    public interface ISizeManager
    {
        Task<List<SizeDTO>> GetSizes();
        Task<SizeDTO> GetSizeById(int id);
        Task<SizeDTO> AddSize(SizeDTO size);
        Task<SizeDTO> UpdateSize(SizeDTO size, int id);
        Task DeleteSize(int id);        
    }

    public class SizeManager : ISizeManager
    {
        private readonly ICommandRepository<Size> _cSizeRepository;
        private readonly IQueryRepository<Size> _qSizeRepository;
        private readonly IMapper _mapper;

        public SizeManager(ICommandRepository<Size> cSizeRepository, IQueryRepository<Size> qSizeRepository, IMapper mapper)
        {

            _cSizeRepository = cSizeRepository;
            _qSizeRepository = qSizeRepository;
            _mapper = mapper;
        }

        public async Task<List<SizeDTO>> GetSizes()
        {

            var sizes = await _qSizeRepository.GetAll().ToListAsync();

            List<SizeDTO> sizeListDto = _mapper.Map<List<SizeDTO>>(sizes);

            return sizeListDto;

        }

        public async Task<SizeDTO> GetSizeById(int id)
        {
            var size = await _qSizeRepository.Find(m => m.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<SizeDTO>(size);
        }

        public async Task<SizeDTO> AddSize(SizeDTO SizeDTO)
        {
            Size entitySize = _mapper.Map<Size>(SizeDTO);
            await _cSizeRepository.AddAsync(entitySize);
            await _cSizeRepository.UnitOfWork.Commit();
            SizeDTO = _mapper.Map<SizeDTO>(entitySize);
            return SizeDTO;
        }

        public async Task DeleteSize(int id)
        {
            var size = await _cSizeRepository.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (size == null)
                throw new ArgumentException($"The size to delete with id: {id} does no exist");

            await _cSizeRepository.DeleteAsync(size);

            await _cSizeRepository.UnitOfWork.Commit();
        }

        public async Task<SizeDTO> UpdateSize(SizeDTO SizeDTO, int id)
        {
            var sizeToUpdate = await _cSizeRepository.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (sizeToUpdate == null)
                throw new ArgumentException($"The size to update with id: {id} does no exist");

            _mapper.Map(SizeDTO, sizeToUpdate);

            sizeToUpdate = await _cSizeRepository.UpdateAsync(sizeToUpdate);

            await _cSizeRepository.UnitOfWork.Commit();

            SizeDTO = _mapper.Map<SizeDTO>(sizeToUpdate);

            return SizeDTO;

        }

    }
}
