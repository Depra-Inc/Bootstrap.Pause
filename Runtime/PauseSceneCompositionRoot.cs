// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Bootstrap.Scenes;
using Depra.IoC.Scope;
using Depra.Pause;
using UnityEngine;

namespace Depra.Bootstrap.Pause
{
	[DisallowMultipleComponent]
	public sealed class PauseSceneCompositionRoot : SceneCompositionRoot
	{
		[SerializeField] private List<ScenePauseInput> _inputs;
		[SerializeField] private List<ScenePauseListener> _listeners;

		private PauseInput _input;
		private IPauseState _state;
		private PauseNotifications _notifications;

		public override void Compose(IScope scope)
		{
			_state = scope.Resolve<IPauseState>();

			_notifications = scope.Resolve<PauseNotifications>();
			_notifications.AddRange(_listeners);

			_input = scope.Resolve<PauseInput>();
			foreach (var inputSource in _inputs)
			{
				inputSource.Initialize(_state);
				_input.Add(inputSource);
			}
		}

		public override void Release()
		{
			_input?.RemoveRange(_inputs);
			_notifications?.RemoveRange(_listeners);
		}
	}
}