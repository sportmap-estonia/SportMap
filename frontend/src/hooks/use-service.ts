"use client";

import { useState, useEffect, useCallback } from "react";
import type { BaseData } from "@/types/base";
import type { BaseService } from "@/services/base.service";

interface ServiceState<T> {
  data: T | null;
  loading: boolean;
  error: Error | undefined;
}

export function useGetAll<T extends BaseData>(service: BaseService<T>) {
  const [state, setState] = useState<ServiceState<T[]>>({
    data: null,
    loading: true,
    error: undefined,
  });

  const refetch = useCallback(async () => {
    setState((s) => ({ ...s, loading: true }));
    const result = await service.getAll();
    setState({ data: result.value ?? null, loading: false, error: result.error });
  }, [service]);

  useEffect(() => {
    void refetch();
  }, [refetch]);

  return { ...state, refetch };
}

export function useGetById<T extends BaseData>(
  service: BaseService<T>,
  id: string
) {
  const [state, setState] = useState<ServiceState<T>>({
    data: null,
    loading: true,
    error: undefined,
  });

  const refetch = useCallback(async () => {
    setState((s) => ({ ...s, loading: true }));
    const result = await service.getById(id);
    setState({ data: result.value ?? null, loading: false, error: result.error });
  }, [service, id]);

  useEffect(() => {
    void refetch();
  }, [refetch]);

  return { ...state, refetch };
}
