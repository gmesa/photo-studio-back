using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    [Collection("Database collection")]
    public class MaterialManagerTest : IClassFixture<MaterialFixture>
    {
        private readonly ICommandRepository<Material> _cMaterialRepository;
        private readonly IQueryRepository<Material> _qMaterialRepository;
        private readonly MaterialFixture _materialFixture;
        private readonly MaterialManager _materialManager;
        public readonly DBContextFixture _dBContextFixture;


        public MaterialManagerTest(MaterialFixture materialFixture, DBContextFixture DBContextFixture)
        {
            _dBContextFixture = DBContextFixture;
            _materialFixture = materialFixture;
            _qMaterialRepository = new QueryRepository<Material>(_dBContextFixture.PhotoStudioContext);
            _cMaterialRepository = new CommandRepository<Material>(_dBContextFixture.PhotoStudioContext);            
            _materialManager = new MaterialManager(_cMaterialRepository, _qMaterialRepository, _materialFixture.Mapper, _materialFixture.LoggerManager.Object);
        }


        [Fact]
        public async void GetMaterials_ReturnAllMaterials()
        {
            //arrange
            var excpected = await _dBContextFixture.PhotoStudioContext.DbSet<Material>().ToListAsync();

            //act
            var result = await _materialManager.GetMaterials();

            //assert
            result.Should().BeEquivalentTo(excpected, opt => opt.ExcludingMissingMembers());

        }
    }

}
