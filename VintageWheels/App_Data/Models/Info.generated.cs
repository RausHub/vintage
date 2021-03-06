//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.7.99
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace Umbraco.Web.PublishedContentModels
{
	/// <summary>Info</summary>
	[PublishedContentModel("info")]
	public partial class Info : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "info";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public Info(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Info, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// InfoPart1Text
		///</summary>
		[ImplementPropertyType("infoPart1Text")]
		public string InfoPart1Text
		{
			get { return this.GetPropertyValue<string>("infoPart1Text"); }
		}

		///<summary>
		/// InfoPart1Title
		///</summary>
		[ImplementPropertyType("infoPart1Title")]
		public string InfoPart1Title
		{
			get { return this.GetPropertyValue<string>("infoPart1Title"); }
		}

		///<summary>
		/// InfoPart2Text
		///</summary>
		[ImplementPropertyType("infoPart2Text")]
		public string InfoPart2Text
		{
			get { return this.GetPropertyValue<string>("infoPart2Text"); }
		}

		///<summary>
		/// InfoPart2Title
		///</summary>
		[ImplementPropertyType("infoPart2Title")]
		public string InfoPart2Title
		{
			get { return this.GetPropertyValue<string>("infoPart2Title"); }
		}

		///<summary>
		/// InfoPart3Text
		///</summary>
		[ImplementPropertyType("infoPart3Text")]
		public string InfoPart3Text
		{
			get { return this.GetPropertyValue<string>("infoPart3Text"); }
		}

		///<summary>
		/// InfoPart3Title
		///</summary>
		[ImplementPropertyType("infoPart3Title")]
		public string InfoPart3Title
		{
			get { return this.GetPropertyValue<string>("infoPart3Title"); }
		}
	}
}
