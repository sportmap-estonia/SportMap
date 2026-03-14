using System.Reflection;

namespace SportMap.AL.Helpers
{
    internal static class Mapper
    {
        public static T Map<T>(object source) where T : class, new()
        {
            var target = new T();
            var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.CanWrite);
                if (targetProp != null)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }

            return target;
        }
    }
}
