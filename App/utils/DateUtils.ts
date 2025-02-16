
export class DateUtils {
  private constructor() {
  }

  public static formatDate(date: Date): string {
    let s = date.toString();

    let dateArray = s.split('T');

    return `${dateArray[0]} ${dateArray[1].split('.')[0]}`;
  }

  public static secondsToDate(seconds: number) {
    const secondsInDay = 3600 * 24;

    const days = seconds / secondsInDay;

    switch (days) {
      case 1:
        return 'День';
      case 7:
        return 'Неделя';
      case 30:
        return 'Месяц';
      case 90:
        return '3 месяца';
      default:
        return `${days} дней`;
    }
  }
}
