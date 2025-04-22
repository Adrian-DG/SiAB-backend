using SiAB.Core.DTO;
using SiAB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
    public interface IDepositoRepository
    {
		Task<List<GroupEntityModel>> GetStockByInventario();

	}
}
