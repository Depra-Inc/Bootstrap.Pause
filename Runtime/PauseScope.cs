// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Bootstrap.Project;
using Depra.IoC.QoL.Builder;
using Depra.Pause;
using Depra.SerializeReference.Extensions;
using UnityEngine;
using static Depra.Bootstrap.Pause.Module;

namespace Depra.Bootstrap.Pause
{
	[CreateAssetMenu(menuName = MENU_PATH + nameof(ProjectScope), fileName = nameof(PauseScope), order = DEFAULT_ORDER)]
	public sealed class PauseScope : ProjectScope
	{
		[SerializeReferenceDropdown]
		[UnityEngine.SerializeReference]
		private List<IPauseInput> _inputs;

		[SerializeReferenceDropdown]
		[UnityEngine.SerializeReference]
		private List<IPauseListener> _listeners;

		public override void Configure(IContainerBuilder builder) => builder
			.RegisterSingleton(_inputs)
			.RegisterSingleton(_listeners)
			.RegisterSingleton<IPauseService, PauseService>();
	}
}