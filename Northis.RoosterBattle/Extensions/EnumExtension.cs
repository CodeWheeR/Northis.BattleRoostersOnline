using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Catel;

namespace Northis.RoosterBattle.Extensions
{
	/// <summary>
	/// Предоставляет новые методы расширения для объектов класса Enum.
	/// </summary>
	public static class EnumExtension
    {
		#region Public Methods
		/// <summary>
		/// Возвращает значение Description аттрибута Enum'а.
		/// </summary>
		/// <param name="enumElement">The enum element.</param>
		/// <returns></returns>
		public static string GetDescription(this Enum enumElement)
		{
			Type type = enumElement.GetType();

			MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
			if (memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs.Length > 0)
					return ((DescriptionAttribute)attrs[0]).Description;
			}

			return enumElement.ToString();
		}

		/// <summary>
		/// Возвращает значение ресурса с заданным в Display атрибуте Enum'a ключом.
		/// </summary>
		/// <param name="enumElement"></param>
		/// <returns></returns>
		public static string GetDisplayFromResource(this Enum enumElement) 
		{
			Type type = enumElement.GetType();

			MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
			if (memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
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
		private static string GetValueFromResources(string resourceKey, Type resourceType)
		{
			var resourceManager = new ResourceManager(resourceType);
			string resourceValue = resourceManager.GetString(resourceKey);
			return string.IsNullOrEmpty(resourceValue) ? resourceKey : resourceValue;
		}
		#endregion
	}
}
