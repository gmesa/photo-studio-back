using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PhotoStudio.Application.Mapper;
using PhotoStudio.Application.Services;
using PhotoStudio.Domain.Entities;
using PhotoStudio.Infrastructure.Commons.Configurations;
using PhotoStudio.Infrastructure.Data.DBContext;
using PhotoStudio.ServicesDTO;
using PhotoStudio.WebApi.Controllers.v1;
using System.Collections;

namespace PhotoStudio.Test
{
    public class MaterialFixture : IDisposable
    {
        public Mock<ILogger<MaterialController>> Logger { get; set; }
        public Mock<IMaterialManager> MockMaterialManager { get; set; }
        public MaterialController MaterialController { get; set; }
        public PhotoStudioContext PhotoStudioContext { get; private set; }
        public IMapper Mapper { get; private set; }

        public MaterialFixture()
        {
            MockMaterialManager = new Mock<IMaterialManager>();
            Logger = new Mock<ILogger<MaterialController>>();
            MaterialController = new MaterialController(MockMaterialManager.Object);

            SetContext();

            var mapConfig = new MapperConfiguration(mc => mc.AddProfile(new PhotoStudioMapperConfiguration()));
            Mapper = mapConfig.CreateMapper();
        }

        public void Dispose()
        {
            PhotoStudioContext.Database.EnsureDeleted();
            PhotoStudioContext.Dispose();
        }

        public PhotoStudioContext SetContext()
        {
            if (PhotoStudioContext != null)
                return PhotoStudioContext;

            var options = new DbContextOptionsBuilder<PhotoStudioContext>().UseInMemoryDatabase(databaseName: "photo").Options;

            PhotoStudioContext = new PhotoStudioContext(options);

            PhotoStudioContext.Database.EnsureCreated();

            var defaultMaterials = BuilderUtils.BuildListMaterialDto().Select((m) => new Material() { Id= m.Id, MaterialName = m.MaterialName, RowVersion = new byte[2]}).ToList();

            PhotoStudioContext.DbSet<Material>().AddRange(defaultMaterials);
            PhotoStudioContext.SaveChanges();
            return PhotoStudioContext;
        }
    }

    public class MaterialControllerTests : IClassFixture<MaterialFixture>
    {             
        public MaterialFixture MaterialFixture { get; set; }


        public MaterialControllerTests(MaterialFixture materialFixture)
        {
            MaterialFixture = materialFixture;
        }



        [Fact]
        [Trait("UI", "Front")]
        public async void GetMaterialById_ReturnMaterial()
        {
            //arrange 
            var expected = BuilderUtils.BuildMaterialDto();
            MaterialFixture.MockMaterialManager.Setup(mock => mock.GetMaterialById(It.IsAny<int>())).ReturnsAsync(expected);


            //act
            var result = await MaterialFixture.MaterialController.GetMaterialById(0);

            //assert
            var material = (result as OkObjectResult)?.Value;
            material.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<MaterialDTO>());
            //Assert.IsType<MaterialDTO>(material);
            //Assert.Equal(expected, material);
        }

        [Fact]
        [Trait("UI", "Back")]
        public async void GetMaterialById_NoMaterial_ReturnNotFound()
        {
            //arrange
            _ = MaterialFixture.MockMaterialManager.Setup(materialManager => materialManager.GetMaterialById(It.IsAny<int>())).ReturnsAsync(null as MaterialDTO);


            //act 
            var result = await MaterialFixture.MaterialController.GetMaterialById(0);

            //assert
            result.Should().BeOfType<NotFoundResult>();
            //Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("UI", "Front")]
        public async void GetAllMaterials_ExpectedMaterials()
        {

            //arrange
            var expectedMaterials = new List<MaterialDTO> { BuilderUtils.BuildMaterialDto() };
            MaterialFixture.MockMaterialManager.Setup(mock => mock.GetMaterials()).ReturnsAsync(() => expectedMaterials);

            //actions
            var result = await MaterialFixture.MaterialController.GetMaterials();

            //asserts
            var materials = (result as OkObjectResult)?.Value;
            materials.Should().BeEquivalentTo(expectedMaterials, opt => opt.ComparingByMembers<MaterialDTO>());
        }

        [Fact]
        [Trait("UI", "Front")]
        public async void AddMaterial_ReturnAddedMaterial()
        {
            //arrange
            var expectedMaterials = BuilderUtils.BuildMaterialDto();
            MaterialFixture.MockMaterialManager.Setup(mock => mock.AddMaterial(It.IsAny<MaterialDTO>())).ReturnsAsync(() => expectedMaterials);

            //actions
            var result = await MaterialFixture.MaterialController.AddMaterial(expectedMaterials);

            //asserts
            var materials = (result as CreatedAtActionResult)?.Value;
            materials.Should().BeEquivalentTo(expectedMaterials, opt => opt.ComparingByMembers<MaterialDTO>().ExcludingMissingMembers());
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async void UpdateMaterial_ReturnUpdatedMaterial()
        {
            //arrange
            var expectedMaterials = BuilderUtils.BuildMaterialDto();
            MaterialFixture.MockMaterialManager.Setup(mock => mock.UpdateMaterial(It.IsAny<MaterialDTO>(), It.IsAny<int>())).ReturnsAsync(() => expectedMaterials);

            //actions
            var result = await MaterialFixture.MaterialController.UpdateMaterial(expectedMaterials, new Random().Next());

            //asserts
            var material = (result as OkObjectResult)?.Value as MaterialDTO;
            material.Should().BeEquivalentTo(expectedMaterials, opt => opt.ComparingByMembers<MaterialDTO>().ExcludingMissingMembers());
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        [Trait("UI", "Back")]
        public async void UpdateMaterial_ReturnNotFound()
        {
            //arrange
            var expectedMaterials = BuilderUtils.BuildMaterialDto();
            MaterialFixture.MockMaterialManager.Setup(mock => mock.UpdateMaterial(It.IsAny<MaterialDTO>(), It.IsAny<int>())).ReturnsAsync(() => null);

            //actions
            var result = await MaterialFixture.MaterialController.UpdateMaterial(expectedMaterials, new Random().Next());

            //asserts
            var material = (result as OkObjectResult)?.Value as MaterialDTO;
            material.Should().BeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void DeleteMaterial_ReturnOk()
        {
            //arrange

            MaterialFixture.MockMaterialManager.Setup(mock => mock.DeleteMaterial(It.IsAny<int>())).Verifiable();

            //actions
            var result = await MaterialFixture.MaterialController.DeleteMaterial(new Random().Next());

            //asserts
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void GetMaterialByName_ReturnMaterials()
        {
            //arrange
            string name = "p";
            var expectedMaterials = BuilderUtils.BuildListMaterialDto().Where(m => m.MaterialName.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
            MaterialFixture.MockMaterialManager.Setup(mock => mock.GetMaterialByTextName(It.IsAny<string>())).ReturnsAsync(() => expectedMaterials);

            //actions
            var result = await MaterialFixture.MaterialController.GetMaterialsWithName(name);

            //asserts
            var materials = (result as OkObjectResult)?.Value;
            materials.Should().BeEquivalentTo(expectedMaterials);
        }       

    }    
}