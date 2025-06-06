﻿using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IUnitOfWork
	{
		IRepository<T> Repository<T>() where T : EntityMetadata;
		IJCERepository JCERepository { get; }
		IUsuarioRepository UsuarioRepository { get; }
		IRoleRepository RoleRepository { get; }
		IReportRepository ReportRepository { get; }
		ISipffaaRepository SipffaaRepository { get; }
		ITransaccionRepository TransaccionRepository { get; }
		ISecuenciaRepository SecuenciaRepository { get; }
		IEmpresaRepository EmpresaRepository { get; }
		IDepositoRepository DepositoRepository { get; }
	}
}
