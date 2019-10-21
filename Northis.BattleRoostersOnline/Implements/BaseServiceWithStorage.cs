﻿using System;
using System.Threading.Tasks;
using CommonServiceLocator;
using Northis.BattleRoostersOnline.Models;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	public abstract class BaseServiceWithStorage
	{
		private readonly Random _rand = new Random();

		protected ServicesStorage Storage
		{
			get
			{
				if (ServiceLocator.IsLocationProviderSet)
				{
					return ServiceLocator.Current.GetInstance<ServicesStorage>();
				}

				throw new NullReferenceException("Storage is null");
			}
		}

		protected BaseServiceWithStorage()
		{
			if (!ServiceLocator.IsLocationProviderSet)
			{
				var container = new UnityContainer();
				container.RegisterInstance(new ServicesStorage());

				var locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
		}

		protected async Task<string> GetLoginAsync(string token) =>
			await Task.Run(() =>
			{
				if (token != null && Storage.LoggedUsers.ContainsKey(token))
				{
					return Storage.LoggedUsers[token];
				}

				return "";
			});

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

		protected Task<string> GenerateTokenAsync()
		{
			return Task.Run(() => GenerateToken());
		}
	}
}
