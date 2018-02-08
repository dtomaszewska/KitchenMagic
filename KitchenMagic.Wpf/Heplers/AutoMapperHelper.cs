using AutoMapper;

namespace KitchenMagic.Wpf.Heplers
{
	public static class AutoMapperHelper
	{
		public static TDestination Map<TDestination>(this object obj)
		{
			return Mapper.Map<TDestination>(obj);
		}
	}
}
