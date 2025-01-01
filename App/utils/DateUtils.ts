
export class DateUtils {
  private constructor() {
  }

  public static formatDate(date: Date): string {
    let s = date.toString();

    let dateArray = s.split('T');

    return `${dateArray[0]} ${dateArray[1].split('.')[0]}`;
  }
}
