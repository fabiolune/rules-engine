﻿using System.Collections.Generic;
using RulesEngine.Interfaces;

namespace RulesEngine.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> @this, IRulesManager<T> manager) where T : new() => manager.Filter(@this);
        public static T FirstOrDefault<T>(this IEnumerable<T> @this, IRulesManager<T> manager) where T : new() => manager.FirstOrDefault(@this);
    }
}