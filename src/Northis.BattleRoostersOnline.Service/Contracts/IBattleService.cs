﻿using System.ServiceModel;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Service.Contracts
{
	/// <summary>
	/// Контракт сервиса проведения боя. Отвечает за проведение битвы петухов.
	/// </summary>
	[ServiceContract(CallbackContract = typeof(IBattleServiceCallback), SessionMode = SessionMode.Required)]
	public interface IBattleService
	{
        #region Operation Contracts
        /// <summary>
        /// Контракт операции. Осуществляет поиск матча.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="rooster">Петух.</param>
        /// <returns>Task.</returns>
        [OperationContract(IsInitiating = true, IsOneWay = true)]
		void FindMatchAsync(string token, string rooster);

		/// <summary>
		/// Контракт операции. Отменяет поиск матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>true - в случае успешной отмены поиска, иначе - false.</returns>
		[OperationContract(IsTerminating = true)]
		Task<bool> CancelFinding(string token);

		/// <summary>
		/// Контракт операции. Запускает поединок петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsInitiating = true, IsOneWay = true)]
		void StartBattleAsync(string token, string matchToken);

		/// <summary>
		/// Контракт операции. Отвечает за сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>
		[OperationContract(IsTerminating = true, IsOneWay = true)]
		void GiveUpAsync(string token, string matchToken);
        #endregion
    }
}
