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
using System.Net.Mime;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using Catel.Collections;
using Northis.RoosterBattle.Callbacks;
using Northis.RoosterBattle.GameServer;

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
		private FindServiceClient _findServiceClient;
		private BattleServiceClient _battleServiceClient;

		private string _userToken;
		private string _matchToken;
		private bool _battleStarted;

		#region Static

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
		/// <summary>
		/// Зарегистрированное свойство состояния жизни второго бойца.
		/// </summary>
		public static readonly PropertyData IsFindingProperty = RegisterProperty(nameof(IsFinding), typeof(bool));

		#endregion

		#endregion

		#region .ctor

		/// <summary>
		/// Инициализирует новый объект <see cref="FightViewModel"/> класса.
		/// </summary>
		/// <param name="roosters">Коллекция петухов.</param>
		public FightViewModel(RoosterModel rooster)
		{
			FirstFighter = rooster;
			_findServiceClient = new FindServiceClient(new InstanceContext(new BattleServiceCallback(this)));
			_battleServiceClient = new BattleServiceClient(new InstanceContext(new BattleServiceCallback(this)));

			FindMatchCommand = new TaskCommand(FindMatch, () => !ShowDeadFirst && !IsFinding);
			CancelFindingCommand = new TaskCommand(CancelFinding, () => IsFinding);
			_userToken = (string)Application.Current.Resources["UserToken"];
		}

		#endregion

		#region Properties		

		/// <summary>
		/// Возвращает команду для начала боя.
		/// </summary>
		public ICommand StartFightCommand
		{
			get;
		}

		/// <summary>
		/// Возвращает команду для начала боя.
		/// </summary>
		public ICommand FindMatchCommand
		{
			get;
		}

		/// <summary>
		/// Возвращает команду для начала боя.
		/// </summary>
		public ICommand CancelFindingCommand
		{
			get;
		}

		public bool IsFinding
		{
			get => GetValue<bool>(IsFindingProperty);
			set => SetValue(IsFindingProperty, value);
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
		/// Запускает битву петухов.
		/// </summary>
		private async Task StartFight()
		{

		}

		/// <summary>
		/// Запускает битву петухов.
		/// </summary>
		private async Task FindMatch()
		{
			ShowDeadFirst = false;
			ShowDeadSecond = false;
			IsFinding = true;
			await _findServiceClient.FindMatchAsync(_userToken, FirstFighter.ToRoosterDto());
		}

		/// <summary>
		/// Запускает битву петухов.
		/// </summary>
		private async Task CancelFinding()
		{
			IsFinding = false;
			var res = await _findServiceClient.CancelFindingAsync(_userToken);
			Debug.WriteLine(res);
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
