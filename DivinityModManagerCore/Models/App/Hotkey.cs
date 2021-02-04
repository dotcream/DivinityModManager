﻿using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DivinityModManager.Models.App
{
	public interface IHotkey
	{
		Key Key { get; set; }
		ModifierKeys Modifiers { get; set; }
		ICommand Command { get; set; }
		bool Enabled { get; set; }
	}
	public class Hotkey : ReactiveObject, IHotkey
	{
		private Key key;
		public Key Key
		{
			get => key;
			set
			{
				this.RaiseAndSetIfChanged(ref key, value);
			}
		}

		private ModifierKeys modifiers;
		public ModifierKeys Modifiers
		{
			get => modifiers;
			set
			{
				this.RaiseAndSetIfChanged(ref modifiers, value);
			}
		}

		private ICommand command;

		public ICommand Command
		{
			get => command;
			set { this.RaiseAndSetIfChanged(ref command, value); }
		}

		private bool enabled = true;

		public bool Enabled
		{
			get => enabled;
			set { this.RaiseAndSetIfChanged(ref enabled, value); }
		}

		private IObservable<bool> canExecute;

		public IObservable<bool> CanExecute
		{
			get => canExecute;
			set { this.RaiseAndSetIfChanged(ref canExecute, value); }
		}

		private List<Action> actions = new List<Action>();
		private List<IObservable<bool>> canExecuteList = new List<IObservable<bool>>();

		public void AddAction(Action action, IObservable<bool> actionCanExecute = null)
		{
			if(!actions.Contains(action))
			{
				actions.Add(action);
			}
			
			if(actionCanExecute != null && !canExecuteList.Contains(actionCanExecute))
			{
				canExecuteList.Add(actionCanExecute);
				CanExecute = Observable.Merge(canExecuteList);
				Command = ReactiveCommand.Create(Invoke, CanExecute);
			}
		}

		public void Invoke()
		{
			actions.ForEach(a => a.Invoke());
		}

		private void Init(Key key, ModifierKeys modifiers)
		{
			Key = key;
			Modifiers = modifiers;

			var canExecuteInitial = this.WhenAnyValue(x => x.Enabled, (b) => b == true);
			canExecuteList.Add(canExecuteInitial);
			CanExecute = Observable.Merge(canExecuteList);
			//var canExecute = this.WhenAnyValue(x => x.canExecuteList, (list) => list.Count == 0 || list.All(b => b.Latest().All(b2 => b2 == true)));
			Command = ReactiveCommand.Create(Invoke, CanExecute);
		}


		public Hotkey(Key key)
		{
			Init(key, ModifierKeys.None);
		}

		public Hotkey(Key key, ModifierKeys modifiers)
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
