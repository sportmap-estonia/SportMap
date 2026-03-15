export function map<TTarget extends object>(
  source: Record<string, unknown>,
  targetFactory: () => TTarget
): TTarget {
  const target = targetFactory();
  const targetKeys = new Set(Object.keys(target));

  for (const [key, value] of Object.entries(source)) {
    if (targetKeys.has(key)) {
      (target as Record<string, unknown>)[key] = value;
    }
  }

  return target;
}
