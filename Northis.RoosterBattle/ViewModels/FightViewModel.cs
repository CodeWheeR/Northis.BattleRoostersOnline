using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Northis.RoosterBattle.Models;
using Catel.MVVM;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Catel.Collections;

namespace Northis.RoosterBattle.ViewModels
{
	/// <summary>
	/// Обеспечивает взаимодействие представления ... и модели RoosterModel
	/// </summary>
	/// <seealso cref="Catel.MVVM.ViewModelBase" />
	class FightViewModel : ViewModelBase
	{
		#region Fields

		private CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private ObservableCollection<RoosterModel> _sourceRoosters;

		#region Static

		/// <summary>
		/// Зарегистрированное свойство коллекции петухов.
		/// </summary>
		public static readonly PropertyData RoostersProperty = RegisterProperty(nameof(Roosters), typeof(ObservableCollection<RoosterModel>), () => new ObservableCollection<RoosterModel>());
		/// <summary>
		/// Зарегистрированное свойство выбранного петуха.
		/// </summary>
		public static readonly PropertyData SelectedRoosterProperty = RegisterProperty(nameof(SelectedRooster), typeof(RoosterModel));
		/// <summary>
		/// Зарегистрированное свойство первого бойца.
		/// </summary>
		public static readonly PropertyData FirstFighterProperty = RegisterProperty(nameof(FirstFighter), typeof(RoosterModel));
		///<summary>
		/// Зарегистрированное свойство второго бойца.
		/// </summary>
		public static readonly PropertyData SecondFighterProperty = RegisterProperty(nameof(SecondFighter), typeof(RoosterModel));
		/// <summary>
		/// Зарегистрированное свойство состояния жизни первого бойца.
		/// </summary>
		public static readonly PropertyData ShowDeadFirstProperty = RegisterProperty(nameof(ShowDeadFirst), typeof(bool));
		/// <summary>
		/// Зарегистрированное свойство состояния жизни второго бойца.
		/// </summary>
		public static readonly PropertyData ShowDeadSecondProperty = RegisterProperty(nameof(ShowDeadSecond), typeof(bool));

		#endregion

		#endregion

		#region .ctor

		/// <summary>
		/// Инициализирует новый объект <see cref="FightViewModel"/> класса.
		/// </summary>
		/// <param name="roosters">Коллекция петухов.</param>
		public FightViewModel(IEnumerable<RoosterModel> roosters)
		{
			_sourceRoosters = new FastObservableCollection<RoosterModel>(roosters);
			((ObservableCollection<RoosterModel>)Roosters).Clear();
			foreach (var rooster in roosters)
			{
				((ObservableCollection<RoosterModel>)Roosters).Add(rooster.Clone() as RoosterModel);
			}

			SelectFirstRoosterCommand = new TaskCommand(SelectFirstFighterAsync);
			SelectSecondRoosterCommand = new TaskCommand(SelectSecondFighterAsync);
			StartFightCommand = new TaskCommand(StartFight, () => (FirstFighter != null && FirstFighter.Health > 0) && (SecondFighter != null && SecondFighter.Health > 0));
		}

		#endregion

		#region Properties		
		/// <summary>
		/// Возвращает команду для выбора первого бойца.
		/// </summary>
		public ICommand SelectFirstRoosterCommand
		{
			get;
		}
		/// <summary>
		/// Возвращает команду для выбора второго бойца.
		/// </summary>
		public ICommand SelectSecondRoosterCommand
		{
			get;
		}
		/// <summary>
		/// Возвращает команду для начала боя.
		/// </summary>
		public ICommand StartFightCommand
		{
			get;
		}
		/// <summary>
		/// Возвращает или устанавливает значение, обозначающее жив ли первый боец.
		/// </summary>
		/// <value>
		///   <c>true</c> если умер, иначе <c>false</c>.
		/// </value>
		public bool ShowDeadFirst
		{
			get => GetValue<bool>(ShowDeadFirstProperty);
			set => SetValue(ShowDeadFirstProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает значение, обозначающее жив ли второй боец.
		/// </summary>
		/// <value>
		///   <c>true</c> если умер, иначе <c>false</c>.
		/// </value>
		public bool ShowDeadSecond
		{
			get => GetValue<bool>(ShowDeadSecondProperty);
			set => SetValue(ShowDeadSecondProperty, value);
		}

		/// <summary>
		/// Предоставляет или задает коллекцию петухов.
		/// </summary>
		/// <value>
		/// Коллекция петухов.
		/// </value>
		public IEnumerable<RoosterModel> Roosters
		{
			get => GetValue<ObservableCollection<RoosterModel>>(RoostersProperty);
			set => SetValue(RoostersProperty, value);
		}


		/// <summary>
		/// Предоставляет или задает выбранного петуха.
		/// </summary>
		/// <value>
		/// Выбранный петух.
		/// </value>
		public RoosterModel SelectedRooster
		{
			get => GetValue<RoosterModel>(SelectedRoosterProperty);
			set => SetValue(SelectedRoosterProperty, value);
		}
		/// <summary>
		/// Врзвращает или устанавливает ссылку на объект первого бойца.
		/// </summary>
		public RoosterModel FirstFighter
		{
			get => GetValue<RoosterModel>(FirstFighterProperty);
			set => SetValue(FirstFighterProperty, value);
		}
		/// <summary>
		/// Врзвращает или устанавливает ссылку на объект второго бойца.
		/// </summary>
		public RoosterModel SecondFighter
		{
			get => GetValue<RoosterModel>(SecondFighterProperty);
			set => SetValue(SecondFighterProperty, value);
		}

		#endregion

		#region Private Methods		
		/// <summary>
		/// Выполняет установку выбранного петуха в качестве первого бойца.
		/// </summary>
		private Task SelectFirstFighterAsync()
		{
			ShowDeadFirst = false;
			if (FirstFighter != null && FirstFighter.Health > 0)
				((ObservableCollection<RoosterModel>)Roosters).Add(FirstFighter);
			FirstFighter = SelectedRooster;
			((ObservableCollection<RoosterModel>)Roosters).Remove(SelectedRooster);
			return Task.CompletedTask;
		}
		/// <summary>
		/// Выполняет установку выбранного петуха в качестве второго бойца.
		/// </summary>
		private Task SelectSecondFighterAsync()
		{
			ShowDeadSecond = false;
			if (SecondFighter != null && SecondFighter.Health > 0)
				((ObservableCollection<RoosterModel>)Roosters).Add(SecondFighter);
			SecondFighter = SelectedRooster;
			((ObservableCollection<RoosterModel>)Roosters).Remove(SelectedRooster);
			return Task.CompletedTask;
		}
		/// <summary>
		/// Запускает битву петухов.
		/// </summary>
		private Task StartFight()
		{
			ShowDeadFirst = false;
			var token = _tokenSource.Token;
			return Task.Run(() =>
			{
				while (FirstFighter.Health > 0 && SecondFighter.Health > 0)
				{
					FirstFighter.TakeHit(SecondFighter);
					SecondFighter.TakeHit(FirstFighter);
					Task.WaitAll(Task.Delay(50, token));
				}

				if (FirstFighter.Health == 0 && SecondFighter.Health == 0)
				{
					ShowDeadFirst = true;
					ShowDeadSecond = true;
					SetWinstreak(FirstFighter, 0);
					SetWinstreak(SecondFighter, 0);

				}
				else if (FirstFighter.Health == 0)
				{
					ShowDeadFirst = true;
					SetBattleResults(SecondFighter, FirstFighter);
				}
				else
				{
					ShowDeadSecond = true;
					SetBattleResults(FirstFighter, SecondFighter);
				}

			}, token);
		}

		private void SetBattleResults(RoosterModel winner, RoosterModel looser)
		{
			SetWinstreak(looser, 0);
			SetWinstreak(winner, winner.WinStreak+1);
		}

		private void SetWinstreak(RoosterModel rooster, int winstreak)
		{
			rooster.WinStreak = winstreak;
			_sourceRoosters.First(x => x.Name == rooster.Name)
						   .WinStreak = winstreak;
		}
		#endregion
	}
}
