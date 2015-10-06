using System;
using UnityEngine;

namespace Batch.Internal
{
	public abstract class ModuleBase
	{
		internal Bridge bridge;

		internal ModuleBase (Bridge _bridge)
		{
			bridge = _bridge;
		}
	}
}

