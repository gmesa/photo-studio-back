using PhotoStudio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Interfaces
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}
