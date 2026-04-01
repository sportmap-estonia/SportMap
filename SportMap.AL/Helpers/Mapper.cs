namespace SportMap.AL.Helpers
{
    internal static class Mapper
    {
        public static TTo Map<TFrom, TTo>(TFrom from) where TTo : new()
        {
            var to = new TTo();
            if (from is null) return to;
            foreach (var property in from.GetType().GetProperties())
            {
                var name = property.Name;
                var p = to.GetType().GetProperty(name);
                if (p is null) continue;
                var v = property.GetValue(from);
                try
                {
                    p.SetValue(to, v);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return to;
        }
        public static TTo Map<TFrom, TTo>(TFrom from, params string[] exclude) where TTo : new()
        {
            var to = new TTo();
            if (to is null) return default;
            if (from is null) return to;
            foreach (var property in from.GetType().GetProperties())
            {
                var name = property.Name;
                if (exclude?.Contains(name) ?? false) continue;
                var p = to.GetType().GetProperty(name);
                if (p is null) continue;
                var v = property.GetValue(from);
                try
                {
                    p.SetValue(to, v);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return to;
        }
    }
}
