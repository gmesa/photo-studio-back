using PhotoStudio.Domain.Entities;
using PhotoStudio.ServicesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Test
{
    public class BuilderUtils
    {

        public const string MATERIAL_NAME = "Cartulina";
        public const int ID = 0;
        public static MaterialDTO BuildMaterialDto() { 
        
            MaterialDTO material = new MaterialDTO();
            material.Id = -1;
            material.MaterialName = MATERIAL_NAME;
            
            return material;

        }

        public static List<MaterialDTO> BuildListMaterialDto()
        {
            List<MaterialDTO> materials = new List<MaterialDTO> 
            { new MaterialDTO() { Id = -1, MaterialName = MATERIAL_NAME }, 
              new MaterialDTO() { Id = -2, MaterialName = "PVC" }, 
              new MaterialDTO() { Id = -3, MaterialName = "Plastic" },
              new MaterialDTO() { Id = -4, MaterialName = "Cera" }};

            return materials;
        }
    }
}
