// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.IoC.Scope;
using Depra.Pause;
using UnityEngine;

namespace Depra.Bootstrap.Pause
{
	public sealed class PauseSceneBootstrap : SceneBootstrap
	{
		[SerializeField] private List<ScenePauseInput> _inputs;

		private IPauseService _service;
		private IPauseListener[] _listeners;

		public override void Initialize(IScope scope)
		{
			_service = scope.Resolve<IPauseService>();
			_listeners = GetComponents<IPauseListener>();
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