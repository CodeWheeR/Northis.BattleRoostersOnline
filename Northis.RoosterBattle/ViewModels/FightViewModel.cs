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
using System.Net;
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
		private BattleServiceClient _battleServiceClient;
		private readonly string _userToken;

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
		/// Зарегистрированное свойство состояния поиска матча.
		/// </summary>
		public static readonly PropertyData IsFindingProperty = RegisterProperty(nameof(IsFinding), typeof(bool));
		/// <summary>
		/// Зарегистрированное свойство начала боя.
		/// </summary>
		public static readonly PropertyData BattleStartedProperty = RegisterProperty(nameof(BattleStarted), typeof(bool));
		/// <summary>
		/// Зарегистрированное свойство окончания боя.
		/// </summary>
		public static readonly PropertyData BattleEndedProperty = RegisterProperty(nameof(BattleEnded), typeof(bool));
		/// <summary>
		/// Зарегистрированное свойство значения токена матча.
		/// </summary>
		public static readonly PropertyData MatchTokenProperty = RegisterProperty(nameof(MatchToken), typeof(string));
		/// <summary>
		/// Зарегистрированное свойство содержимого боевого чата.
		/// </summary>
		public static readonly PropertyData BattleLogProperty = RegisterProperty(nameof(BattleLog), typeof(string));

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

			FindMatchCommand = new TaskCommand(FindMatchAsync, () => !ShowDeadFirst && !IsFinding && String.IsNullOrWhiteSpace(MatchToken));
			CancelFindingCommand = new TaskCommand(CancelFindingAsync, () => IsFinding && String.IsNullOrWhiteSpace(MatchToken));
			StartFightCommand = new TaskCommand(StartFightAsync, () => !String.IsNullOrWhiteSpace(MatchToken) && !BattleStarted && !BattleEnded);
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

		/// <summary>
		/// Возвращает или задает содерджимое боевого чата
		/// </summary>
		public string BattleLog
		{
			get => GetValue<string>(BattleLogProperty);
			set => SetValue(BattleLogProperty, value);
		}
		/// <summary>
		/// Возвращает или задает состояния начала боя.
		/// </summary>
		/// <value>
		///   <c>true</c> Если бой начался, иначе <c>false</c>.
		/// </value>
		public bool BattleStarted
		{
			get => GetValue<bool>(BattleStartedProperty);
			set => SetValue(BattleStartedProperty, value);
		}
		/// <summary>
		/// Возвращает или задает состояния окончания боя.
		/// </summary>
		/// <value>
		///   <c>true</c>  Если бой закончился, иначе <c>false</c>.
		/// </value>
		public bool BattleEnded
		{
			get => GetValue<bool>(BattleEndedProperty);
			set => SetValue(BattleEndedProperty, value);
		}
		/// <summary>
		/// Возвращает или задает состояние поиска матча.
		/// </summary>
		/// <value>
		///   <c>true</c> Если поиск начался, иначе <c>false</c>.
		/// </value>
		public bool IsFinding
		{
			get => GetValue<bool>(IsFindingProperty);
			set => SetValue(IsFindingProperty, value);
		}
		/// <summary>
		/// Возвращает или задает значения токена матча.
		/// </summary>
		/// <value>
		/// The match token.
		/// </value>
		public string MatchToken
		{
			get => GetValue<string>(MatchTokenProperty);
			set => SetValue(MatchTokenProperty, value);
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

		/// <summary>
		/// Called when the view model is about to be closed.
		/// <para />
		/// This method also raises the <see cref="E:Catel.MVVM.ViewModelBase.ClosingAsync" /> event.
		/// </summary>
		protected override async Task OnClosingAsync()
		{
			if (!BattleEnded)
			{
				await _battleServiceClient.GiveUpAsync(_userToken, MatchToken);
			}

			await base.OnClosingAsync();
		}

		#region Private Methods		
		/// <summary>
		/// Запускает битву петухов.
		/// </summary>
		private async Task StartFightAsync()
		{
			BattleStarted = true;
			await _battleServiceClient.StartBattleAsync(_userToken, MatchToken);
		}

		/// <summary>
		/// Запускает битву петухов.
		/// </summary>
		private async Task FindMatchAsync()
		{
			_battleServiceClient = new BattleServiceClient(new InstanceContext(new BattleServiceCallback(this)));
			ShowDeadFirst = false;
			ShowDeadSecond = false;
			IsFinding = true;
			await _battleServiceClient.FindMatchAsync(_userToken, FirstFighter.ToRoosterDto());
		}

		/// <summary>
		/// Запускает битву петухов.
		/// </summary>
		private async Task CancelFindingAsync()
		{
			IsFinding = false;
			await _battleServiceClient.CancelFindingAsync(_userToken);
			_battleServiceClient = null;
		}
		#endregion
	}
}
