// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Inspector.SerializedReference;
using Depra.IoC.QoL.Builder;
using Depra.IoC.QoL.Composition;
using Depra.Pause;
using UnityEngine;

namespace Depra.Bootstrap.Pause
{
	[DisallowMultipleComponent]
	public sealed class PauseBootstrap : MonoBehaviour, IInstaller
	{
		[SubtypeDropdown] [SerializeReference] private IPauseListener[] _listeners;

		void IInstaller.Install(IContainerBuilder container)
		{
			var inputs = new List<IPauseInput>(GetComponents<IPauseInput>());
			container.RegisterSingleton(inputs);
			var listeners = new List<IPauseListener>(_listeners);
			listeners.AddRange(GetComponents<IPauseListener>());
			container.RegisterSingleton(listeners);
			container.RegisterSingleton<IPauseService, PauseService>();
		}
	}
}