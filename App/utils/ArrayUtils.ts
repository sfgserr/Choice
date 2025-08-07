
export type Group<T, TItems> = {
  key: T,
  items:  TItems[]
}

export class GroupCollection<T, TItem> {
  private readonly groups: Group<T, TItem>[];

  constructor(groups: Group<T, TItem>[]) {
    this.groups = groups;
  }

  public getGroup(key: T) {
    const groups = this.groups.filter(g => g.key == key);

    if (groups.length == 0) return [];

    return groups[0].items;
  }

  public add(key: T, item: TItem) {
    let index = this.groups.findIndex(g => g.key == key);

    if (index == -1) {
      index = this.groups.length;
      this.groups[index] = {key, items: []};
    };

    this.groups[index].items.push(item);
  }

  public remove(key: T, index: number) {
    let groupIndex = this.groups.findIndex(g => g.key == key);

    if (groupIndex != -1) {
      this.groups[groupIndex].items.splice(index, 1);
    }
  }

  public getAll() {
    return [...this.groups];
  }
}

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

  public static groupByKey<T, TItem>(array: TItem[], keyE: (i: TItem) => T) {
    const groups: Group<T, TItem>[] = [];
    for (let i = 0; i < array.length; i++) {
      const key = keyE(array[i]);

      let index = groups.findIndex(g => g.key == key);

      if (index == -1) {
        index = groups.length;
        groups.push({key, items: []});
      }

      groups[index].items.push(array[i]);
    }

    return new GroupCollection(groups);
  }
}
