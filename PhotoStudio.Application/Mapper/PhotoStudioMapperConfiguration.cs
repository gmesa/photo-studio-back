using AutoMapper;
using PhotoStudio.Domain.Entities;
using PhotoStudio.ServicesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Mapper
{
    public class PhotoStudioMapperConfiguration : Profile
    {
        public PhotoStudioMapperConfiguration()
        {

            CreateMap<MaterialDTO, Material>().ForMember(source => source.Id, dest => dest.Ignore()).ReverseMap();

            CreateMap<SizeDTO, Size>()
            .ForMember(s => s.Id, d => d.Ignore())
            .ForMember(s => s.SizeName, opt => opt.MapFrom(src => src.Size)).ReverseMap();

            CreateMap<PhotoBookDTO, PhotoBook>().ForMember(s => s.Id, d => d.Ignore()).ReverseMap();

        }
    }
}
