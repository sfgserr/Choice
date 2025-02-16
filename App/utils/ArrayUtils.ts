
export class ArrayUtils {
  private constructor() {
  }

  public static findLastIndex<T>(array: T[], predicate: (e: T) => boolean): number {
    let index = array.length - 1;

    while (index >= 0 && !predicate(array[index])) {
      index--;
    }

    return index;
  }
}
