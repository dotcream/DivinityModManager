﻿using DynamicData;
using DynamicData.Binding;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DivinityModManager.Models.App
{
	public class MultiReactiveCommandHotkey : ReactiveObject, IHotkey
	{
		[Reactive] public string DisplayName { get; set; }
		[Reactive] public Key Key { get; set; }

		[Reactive] public ModifierKeys Modifiers { get; set; }

		[Reactive] public ICommand Command { get; set; }

		[Reactive] public bool Enabled { get; set; }

		[Reactive] public IObservable<bool> CanExecute { get; private set; }

		private ObservableCollection<ReactiveCommand<Unit, Unit>> commands = new ObservableCollection<ReactiveCommand<Unit, Unit>>();

		public ObservableCollection<ReactiveCommand<Unit, Unit>> Commands
		{
			get => commands;
		}

		public void Add(ReactiveCommand<Unit, Unit> command)
		{
			Commands.Add(command);
		}

		public void Invoke()
		{
			foreach (var cmd in Commands)
			{
				Observable.Start(() => { }).InvokeCommand(cmd);
			}
		}

		private void Init(Key key, ModifierKeys modifiers)
		{
			Key = key;
			Modifiers = modifiers;

			var canExecuteInitial = this.WhenAnyValue(x => x.Enabled, (b) => b == true);
			var anyCommandsCanExecute = Commands.ToObservableChangeSet().AutoRefreshOnObservable(c => c.CanExecute).ToCollection().Select(x => x.Any());
			CanExecute = Observable.Merge(new IObservable<bool>[2] { canExecuteInitial, anyCommandsCanExecute });
			Command = ReactiveCommand.Create(Invoke, CanExecute);
		}


		public MultiReactiveCommandHotkey(Key key)
		{
			Init(key, ModifierKeys.None);
		}

		public MultiReactiveCommandHotkey(Key key, ModifierKeys modifiers)
		{
			Init(key, modifiers);
		}

		public override string ToString()
		{
			var str = new StringBuilder();

			if (Modifiers.HasFlag(ModifierKeys.Control))
				str.Append("Ctrl + ");
			if (Modifiers.HasFlag(ModifierKeys.Shift))
				str.Append("Shift + ");
			if (Modifiers.HasFlag(ModifierKeys.Alt))
				str.Append("Alt + ");
			if (Modifiers.HasFlag(ModifierKeys.Windows))
				str.Append("Win + ");

			str.Append(Key);

			return str.ToString();
		}
	}
}
