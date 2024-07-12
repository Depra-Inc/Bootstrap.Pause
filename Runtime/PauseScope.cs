// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using Depra.IoC.Composition;
using Depra.IoC.QoL.Builder;
using Depra.Pause;
using Depra.SerializeReference.Extensions;

namespace Depra.Bootstrap.Pause
{
	[Serializable]
	[SerializeReferenceMenuPath(nameof(PauseScope))]
	public sealed class PauseScope : ILifetimeScope
	{
		[SerializeReferenceDropdown]
		[UnityEngine.SerializeReference]
		private List<IPauseInput> _inputs;

		[SerializeReferenceDropdown]
		[UnityEngine.SerializeReference]
		private List<IPauseListener> _listeners;

		void ILifetimeScope.Configure(IContainerBuilder builder) => builder
			.RegisterSingleton(_inputs)
			.RegisterSingleton(_listeners)
			.RegisterSingleton<IPauseService, PauseService>();
	}
}