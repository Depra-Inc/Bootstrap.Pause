// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.IoC.Scope;
using Depra.Pause;
using UnityEngine;

namespace Depra.Bootstrap.Pause
{
	[DisallowMultipleComponent]
	public sealed class PauseSceneBootstrap : SceneBootstrapElement
	{
		[SerializeField] private List<ScenePauseInput> _inputs;
		[SerializeField] private List<ScenePauseListener> _listeners;

		private IPauseService _service;

		public override void Initialize(IScope scope)
		{
			_service = scope.Resolve<IPauseService>();
			_service.AddRange(_listeners);

			foreach (var input in _inputs)
			{
				input.Initialize(_service);
				_service.Add(input);
			}
		}

		public override void TearDown()
		{
			_service?.RemoveRange(_inputs);
			_service?.RemoveRange(_listeners);
		}
	}
}