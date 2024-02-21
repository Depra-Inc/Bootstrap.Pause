// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Bootstrap;
using Depra.Inspector.SerializedReference;
using Depra.IoC.QoL.Builder;
using Depra.IoC.Scope;
using Depra.Pause;
using UnityEngine;

namespace Client.Bootstrap
{
	[DisallowMultipleComponent]
	public sealed class PauseBootstrap : MonoBehaviour, IBootstrapElement
	{
		[SerializeField] private ScenePauseInput[] _inputs;
		[SubtypeDropdown] [SerializeReference] private IPauseListener[] _listeners;

		void IBootstrapElement.InstallBindings(IContainerBuilder container)
		{
			var input = new ComposedPauseInput(_inputs);
			container.RegisterSingleton<IPauseInput>(input);

			var listeners = new List<IPauseListener>(_listeners);
			listeners.AddRange(GetComponents<IPauseListener>());
			container.RegisterSingleton<IEnumerable<IPauseListener>>(listeners);

			container.RegisterSingleton<IPauseService, PauseService>();
		}

		void IBootstrapElement.Initialize(IScope scope)
		{
			// TODO: Replace initialization with NonLazy.
			scope.Resolve<IPauseService>();
		}
	}
}