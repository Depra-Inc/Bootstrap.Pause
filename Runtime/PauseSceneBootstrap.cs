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

		public override void Initialize(IScope scope)
		{
			_service = scope.Resolve<IPauseService>();
			_service.AddRange(GetComponents<IPauseListener>());

			foreach (var input in _inputs)
			{
				input.Initialize(_service);
				_service.Add(input);
			}
		}

		private void OnDestroy()
		{
			_service?.RemoveRange(_inputs);
			_service?.RemoveRange(GetComponents<IPauseListener>());
		}
	}
}