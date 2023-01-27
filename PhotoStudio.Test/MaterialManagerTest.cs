using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Application.Interfaces;
using PhotoStudio.Application.Mapper;
using PhotoStudio.Application.Services;
using PhotoStudio.Domain.Entities;
using PhotoStudio.Infrastructure.Data;
using PhotoStudio.Infrastructure.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Test
{
    

    public class MaterialManagerTest : IClassFixture<MaterialFixture>
    {
        private readonly ICommandRepository<Material> _cMaterialRepository;
        private readonly IQueryRepository<Material> _qMaterialRepository;
        private readonly MaterialFixture _materialFixture;
        private readonly MaterialManager _materialManager;


        public MaterialManagerTest(MaterialFixture materialFixture)
        {
            _materialFixture = materialFixture;
            _qMaterialRepository = new QueryRepository<Material>(_materialFixture.PhotoStudioContext);
            _cMaterialRepository = new CommandRepository<Material>(_materialFixture.PhotoStudioContext);            
            _materialManager = new MaterialManager(_cMaterialRepository, _qMaterialRepository, _materialFixture.Mapper);
        }


        //[Fact]
        //public async void GetMaterials_ReturnAllMaterials()
        //{
        //    //arrange
        //    var excpected = await _materialFixture.PhotoStudioContext.DbSet<Material>().ToListAsync();

        //    //act
        //    var result = await _materialManager.GetMaterials();

        //    //assert
        //    result.Should().BeEquivalentTo(excpected, opt => opt.ExcludingMissingMembers());

        //}
    }

}
