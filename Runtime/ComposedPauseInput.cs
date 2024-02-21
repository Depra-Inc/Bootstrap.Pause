using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.Pause;

namespace Client.Bootstrap
{
	public sealed class ComposedPauseInput : IPauseInput, IDisposable
	{
		private readonly IEnumerable<IPauseInput> _inputs;

		public event Action Pause;
		public event Action Resume;

		public ComposedPauseInput(IEnumerable<IPauseInput> inputs)
		{
			_inputs = inputs;
			foreach (var input in _inputs)
			{
				input.Pause += InvokePause;
				input.Resume += InvokeResume;
			}
		}

		public void Dispose()
		{
			foreach (var input in _inputs)
			{
				input.Pause -= InvokePause;
				input.Resume -= InvokeResume;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void InvokePause() => Pause?.Invoke();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void InvokeResume() => Resume?.Invoke();
	}
}