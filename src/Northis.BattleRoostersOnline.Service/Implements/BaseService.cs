using System;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Service.Implements
{
    /// <summary>
    /// Предоставляет функционал генерирования токенов.
    /// </summary>
    public class BaseService
	{
		#region Fields

		/// <summary>
		/// Генератор рандомных значений.
		/// </summary>
		private Random _rand = new Random();

        #endregion

        #region Protected Methods
        /// <summary>
        /// Генерирует токен.
        /// </summary>
        /// <returns>Токен.</returns>
        protected string GenerateToken(Func<string, bool> keyCheck = null)
		{
			_rand = new Random();
			var tokenGeneratorSymbols = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var answer = "";
			do
			{
				answer = "";
				for (var i = 0; i < 16; i++)
				{
					answer += tokenGeneratorSymbols[_rand.Next(0, tokenGeneratorSymbols.Length - 1)];
				}

			}
			while (keyCheck != null && keyCheck(answer));

			return answer;
		}

		/// <summary>
		/// Асинхронно генерирует токен.
		/// </summary>
		/// <returns>Токен.</returns>
		protected Task<string> GenerateTokenAsync(Func<string, bool> keyCheck = null)
		{
			return Task.Run(() => GenerateToken(keyCheck));
		}
        #endregion
    }
}
