
export class FilePathUtils {
  private constructor() {
  }

  public static getFileName(source: string): string | undefined {
    return source.split('\\').pop().split('/').pop();
  }
}
