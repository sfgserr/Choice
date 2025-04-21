
export interface IConnection {
  start(): Promise<void>;

  close(): void;

  on(method: string, handler: (obj: any) => void): void;
}
