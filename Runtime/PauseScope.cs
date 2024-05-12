// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.IoC.Composition;
using Depra.IoC.QoL.Builder;
using Depra.Pause;
using Depra.SerializeReference.Extensions;
using UnityEngine;

namespace Depra.Bootstrap.Pause
{
	[DisallowMultipleComponent]
	public sealed class PauseScope : MonoBehaviour, ILifetimeScope
	{
		[SerializeReferenceDropdown]
		[UnityEngine.SerializeReference]
		private IPauseListener[] _listeners;

		void ILifetimeScope.Configure(IContainerBuilder container)
		{
			var inputs = new List<IPauseInput>(GetComponents<IPauseInput>());
			var listeners = new List<IPauseListener>(_listeners);
			listeners.AddRange(GetComponents<IPauseListener>());

			container
				.RegisterSingleton(inputs)
				.RegisterSingleton(listeners)
				.RegisterSingleton<IPauseService, PauseService>();
		}
	}
}