using System;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace Northis.BattleRoostersOnline.Client.Extensions
{
	/// <summary>
	/// Предоставляет новые методы расширения для объектов класса Enum.
	/// </summary>
	public static class EnumExtension
	{
		#region Public Methods
		/// <summary>
		/// Возвращает значение ресурса с заданным в Display атрибуте Enum'a ключом.
		/// </summary>
		/// <param name="enumElement"></param>
		/// <returns>Содержимое ресурса с заданным ключом.</returns>
		public static string GetDisplayFromResource(this Enum enumElement)
		{
			var type = enumElement.GetType();

			var memInfo = type.GetMember(enumElement.ToString());

			var members = type.GetMembers();

			if (memInfo.Length > 0)
			{
				var attrs = memInfo[0]
					.GetCustomAttributes(typeof(DisplayAttribute), false);
				if (attrs.Length > 0)
				{
					var attribute = (DisplayAttribute) attrs[0];
					return GetValueFromResources(attribute.Name, attribute.ResourceType);
				}
			}

			return enumElement.ToString();
		}
		#endregion

		#region Private Methods		
		/// <summary>
		/// Возвращает значение ресурса по переданному ключу.
		/// </summary>
		/// <param name="resourceKey">Ключ.</param>
		/// <param name="resourceType">Тип ресурса.</param>
		/// <returns>Если запись по ключу имеется - значение, иначе сам ключ.</returns>
		private static string GetValueFromResources(string resourceKey, Type resourceType)
		{
			var resourceManager = new ResourceManager(resourceType);
			var resourceValue = resourceManager.GetString(resourceKey);
			return string.IsNullOrEmpty(resourceValue) ? resourceKey : resourceValue;
		}
		#endregion
	}
}
