﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;

namespace SiAB.Core.Abstraction
{
	public abstract class EntityMetadata
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
	}
}
