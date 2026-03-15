import type { BaseData } from "@/types/base";
import { Result, ResultOf } from "@/lib/result";

export abstract class BaseService<T extends BaseData> {
  protected abstract readonly endpoint: string;
  protected readonly baseUrl: string;

  constructor(baseUrl: string = "/api") {
    this.baseUrl = baseUrl;
  }

  protected get url(): string {
    return `${this.baseUrl}/${this.endpoint}`;
  }

  async getById(id: string): Promise<ResultOf<T>> {
    try {
      const response = await fetch(`${this.url}/${id}`);
      if (response.status === 404) return ResultOf.notFound(this.endpoint);
      if (!response.ok) throw new Error(`HTTP error: ${response.status}`);
      return ResultOf.withValue((await response.json()) as T);
    } catch (error) {
      return ResultOf.withError<T>(
        error instanceof Error ? error : new Error(String(error))
      );
    }
  }

  async getAll(): Promise<ResultOf<T[]>> {
    try {
      const response = await fetch(this.url);
      if (!response.ok) throw new Error(`HTTP error: ${response.status}`);
      return ResultOf.withValue((await response.json()) as T[]);
    } catch (error) {
      return ResultOf.withError<T[]>(
        error instanceof Error ? error : new Error(String(error))
      );
    }
  }

  async add(entity: Omit<T, keyof BaseData>): Promise<ResultOf<T>> {
    try {
      const response = await fetch(this.url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(entity),
      });
      if (!response.ok) throw new Error(`HTTP error: ${response.status}`);
      return ResultOf.withValue((await response.json()) as T);
    } catch (error) {
      return ResultOf.withError<T>(
        error instanceof Error ? error : new Error(String(error))
      );
    }
  }

  async update(
    id: string,
    entity: Partial<Omit<T, keyof BaseData>>
  ): Promise<ResultOf<T>> {
    try {
      const response = await fetch(`${this.url}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(entity),
      });
      if (!response.ok) throw new Error(`HTTP error: ${response.status}`);
      return ResultOf.withValue((await response.json()) as T);
    } catch (error) {
      return ResultOf.withError<T>(
        error instanceof Error ? error : new Error(String(error))
      );
    }
  }

  async remove(id: string): Promise<Result> {
    try {
      const response = await fetch(`${this.url}/${id}`, { method: "DELETE" });
      if (!response.ok) throw new Error(`HTTP error: ${response.status}`);
      return Result.withMessage("Removed successfully");
    } catch (error) {
      return Result.withError(
        error instanceof Error ? error.message : String(error)
      );
    }
  }
}
