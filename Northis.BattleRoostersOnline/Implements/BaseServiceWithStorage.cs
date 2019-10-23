using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.DataStorages;
using Northis.BattleRoostersOnline.Models;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	/// <summary>
	/// Абстрактный базовый класс реализаций контрактов сервиса. Инкапсулирует в себе свойство хранилища основных данных типа
	/// ServiceStorage.
	/// </summary>
	public abstract class BaseServiceWithStorage
	{
		#region Fields

		/// <summary>
		/// Генератор рандомных значений.
		/// </summary>
		private readonly Random _rand = new Random();

		#endregion

		#region Properties
		/// <summary>
		/// Возвращает хранилище основных данных.
		/// </summary>
		/// <value>
		/// Храни.
		/// </value>
		/// <exception cref="NullReferenceException">Хранилище не инициализированно экземпляром класса.</exception>
		protected IServicesStorage Storage
		{
			get
			{
				if (ServiceLocator.IsLocationProviderSet)
				{
					return ServiceLocator.Current.GetInstance<IServicesStorage>();
				}

				throw new NullReferenceException("Storage is null");
			}
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="BaseServiceWithStorage" /> класса.
		/// </summary>
		protected BaseServiceWithStorage()
		{
			if (!ServiceLocator.IsLocationProviderSet)
			{
				var container = new UnityContainer();
				container.RegisterType<IServicesStorage, ServicesStorage>();
				container.RegisterInstance(new ServicesStorage());

				var locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Асинхронно возвращает login пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>Логин.</returns>
		protected async Task<string> GetLoginAsync(string token) =>
			await Task.Run(() =>
			{
				if (token != null && Storage.LoggedUsers.ContainsKey(token))
				{
					return Storage.LoggedUsers[token];
				}

				return "";
			});

		/// <summary>
		/// Генерирует токен.
		/// </summary>
		/// <returns>Токен.</returns>
		protected string GenerateToken()
		{
			var tokenGeneratorSymbols = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var answer = "";

			for (var i = 0; i < 16; i++)
			{
				answer += tokenGeneratorSymbols[_rand.Next(0, tokenGeneratorSymbols.Length - 1)];
			}

			return answer;
		}

		/// <summary>
		/// Асинхронно генерирует токен.
		/// </summary>
		/// <returns>Токен.</returns>
		protected Task<string> GenerateTokenAsync()
		{
			return Task.Run(() => GenerateToken());
		}
		#endregion
	}
}
