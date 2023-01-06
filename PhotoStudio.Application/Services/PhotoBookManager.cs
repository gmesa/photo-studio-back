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
    public interface IPhotoBookManager
    {
        Task<PhotoBookDTO> AddPhotoBook(PhotoBookDTO photoBook);
        Task<PhotoBookDTO> UpdatePhotoBook(PhotoBookDTO photoBook, int id);
        Task DeletePhotoBook(int id);
        Task<List<PhotoBookDTO>> GetPhotoBooks();
        Task<PhotoBookDTO> GetPhotoBookById(int id);
    }

    public class PhotoBookManager : IPhotoBookManager
    {
        private readonly ICommandRepository<PhotoBook> _cPhotoBookRepository;
        private readonly IQueryRepository<PhotoBook> _qPhotoBookRepository;
        private readonly IMapper _mapper;

        public PhotoBookManager(ICommandRepository<PhotoBook> cPhotoBookRepository, IQueryRepository<PhotoBook> qPhotoBookRepository, IMapper mapper)
        {

            _cPhotoBookRepository = cPhotoBookRepository;
            _qPhotoBookRepository = qPhotoBookRepository;
            _mapper = mapper;
        }

        public async Task<List<PhotoBookDTO>> GetPhotoBooks()
        {

            var photoBooks = await _qPhotoBookRepository.GetAll().ToListAsync();

            List<PhotoBookDTO> photoBookListDto = _mapper.Map<List<PhotoBookDTO>>(photoBooks);

            return photoBookListDto;

        }

        public async Task<PhotoBookDTO> GetPhotoBookById(int id)
        {
            var photoBook = await _qPhotoBookRepository.Find(m => m.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<PhotoBookDTO>(photoBook);
        }

        public async Task<PhotoBookDTO> AddPhotoBook(PhotoBookDTO photoBookDto)
        {
            PhotoBook entityPhotoBook = _mapper.Map<PhotoBook>(photoBookDto);
            await _cPhotoBookRepository.AddAsync(entityPhotoBook);
            await _cPhotoBookRepository.UnitOfWork.Commit();
            photoBookDto = _mapper.Map<PhotoBookDTO>(entityPhotoBook);
            return photoBookDto;
        }

        public async Task DeletePhotoBook(int id)
        {
            var photoBook = await _cPhotoBookRepository.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (photoBook == null)
                throw new ArgumentException($"The photoBook to delete with id: {id} does no exist");

            await _cPhotoBookRepository.DeleteAsync(photoBook);

            await _cPhotoBookRepository.UnitOfWork.Commit();
        }

        public async Task<PhotoBookDTO> UpdatePhotoBook(PhotoBookDTO photoBookDTO, int id)
        {
            var photoBookToUpdate = await _cPhotoBookRepository.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (photoBookToUpdate == null)
                throw new ArgumentException($"The photoBook to update with id: {id} does no exist");

            _mapper.Map(photoBookDTO, photoBookToUpdate);

            photoBookToUpdate = await _cPhotoBookRepository.UpdateAsync(photoBookToUpdate);

            await _cPhotoBookRepository.UnitOfWork.Commit();

            photoBookDTO = _mapper.Map<PhotoBookDTO>(photoBookToUpdate);

            return photoBookDTO;

        }

    }
}
